namespace Logging
{
    public class TraceRecorder : IRecorder
    {
        public TraceRecorder()
        {
        }

        #region IRecorder Members

        public void Write(string aMessage)
        {
            System.Diagnostics.Trace.WriteLine(aMessage);
        }

        #endregion IRecorder Members
    }
}