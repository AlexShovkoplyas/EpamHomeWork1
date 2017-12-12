using System;
using System.Runtime.Serialization.Json;
using System.IO;
using DumbLogger.Configuration;


namespace DumbLogger.LogWriters
{
    internal class LogWriterJson : LogWriter
    {
        public LogWriterJson(LogConfig logConfig) : base(logConfig) { }

        internal override void LogWrite(LogParameters logInfo)
        {
            var serializer = new DataContractJsonSerializer(typeof(LogParameters));

            try
            {
                using (FileStream fileStream = new FileStream(logFilePath, FileMode.Open))
                {
                    fileStream.Seek(-2, SeekOrigin.End);

                    if (fileStream.Length > 5)
                    {
                        LogFormat.WriteString("," + Environment.NewLine, fileStream);
                    }
                    else
                    {
                        LogFormat.WriteString(Environment.NewLine, fileStream);
                    }

                    serializer.WriteObject(fileStream, logInfo);

                    LogFormat.WriteString(Environment.NewLine + "]", fileStream);
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"DumbLogger. Error, log was not written into log file : {logFilePath}");
                throw;
            }            
        }
    }
}



