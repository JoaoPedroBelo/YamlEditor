using System;

namespace Logging
{
    internal interface ILogger
    {
        IRecorder Recorder { get; set; }

        void Write(string aFormat, params object[] aArgs);

        void WriteLine(string aFormat, params object[] aArgs);

        void Write(Exception aException);

        void WriteLine(Exception aException);
    }
}