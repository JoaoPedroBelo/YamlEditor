using System;

namespace Logging
{
    public class ConsoleRecorder : IRecorder
    {
        public void Write(string aMessage)
        {
            Console.Write(aMessage);
        }
    }
}