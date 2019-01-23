using System;

namespace Logging
{
    public class DateRecorderDecorator : IRecorder
    {
        private IRecorder Component { get; set; }

        public DateRecorderDecorator(IRecorder aRecorder)
        {
            Component = aRecorder;
        }

        #region IRecorder Members

        public void Write(string aMessage)
        {
            var dateinfo = DateTime.Now.ToString("yyyy-mm-dd") + " " + DateTime.Now.ToLongTimeString().ToString();
            Component.Write(String.Format("{0} : {1}", dateinfo, aMessage));
        }

        #endregion IRecorder Members
    }
}