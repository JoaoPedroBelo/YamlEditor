using System;
using System.Collections.Generic;
using System.Text;

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
