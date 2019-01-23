using System;

namespace Logging
{
    public class CounterDecorator : IRecorder
    {
        private int Counter = 0;
        private IRecorder Component { get; set; }

        public CounterDecorator(IRecorder aRecorder)
        {
            Component = aRecorder;
        }

        #region IRecorder Members

        public void Write(string aMessage)
        {
            Component.Write(String.Format("{0} # {1}", ++Counter, aMessage));
        }

        #endregion IRecorder Members
    }
}