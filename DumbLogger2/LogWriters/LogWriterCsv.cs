using System;
using DumbLogger.Configuration;
using DumbLogger.Serializers;


namespace DumbLogger.LogWriters
{
    internal class LogWriterCsv : LogWriter
    {
        public LogWriterCsv(LogConfig logConfig) : base(logConfig) { }

        internal override void LogWrite(LogParameters logInfo)
        {
            var serializer = new CsvSerializer<LogParameters>();

            try
            {
                serializer.Serialize(logFilePath, logInfo);
            }
            catch (System.Exception)
            {
                Console.WriteLine($"DumbLogger. Error, log was not written into log file : {logFilePath}");
                throw;
            }            
        }
    }
}



