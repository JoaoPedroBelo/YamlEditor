using System.Windows.Forms;
using Logging;

namespace Logging
{
    public class TextBoxRecorder : IRecorder
    {
        public TextBox TextBox { get; private set; }
        private static object padlock = new object();

        public TextBoxRecorder( TextBox aTextBox )
        {
            TextBox = aTextBox;
        }

        delegate void AppendTextCallback( string aMessage );

        public void Write( string aMessage )
        {
            if ( TextBox == null ) return;

            if ( TextBox.InvokeRequired )
            {
                AppendTextCallback callback = Write;
                TextBox.Invoke( callback, aMessage );
            }
            else
            {
                lock ( padlock )
                {
                    TextBox.AppendText( aMessage );
                }
            }
        }
    }
}
