
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DumbLogger.Configuration;

namespace DumbLogger.LogWriters
{
    internal class LogWriterPlain : LogWriter
    {
        public LogWriterPlain(LogConfig logConfig) : base(logConfig) { }

        internal override void LogWrite(LogParameters logInfo)
        {
            throw new System.NotImplementedException();
        }

    }
}



