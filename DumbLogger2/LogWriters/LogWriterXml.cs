using System;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using DumbLogger.Configuration;


namespace DumbLogger.LogWriters
{
    public class LogWriterXml : LogWriter
    {
        public LogWriterXml(LogConfig logConfig) : base(logConfig) { }

        internal override void LogWrite(LogParameters logInfo)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(LogParameters));

            var settings = new XmlWriterSettings() { OmitXmlDeclaration = true };
            var emptyNs = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            try
            {
                using (FileStream fileStream = new FileStream(logFilePath, FileMode.Open))
                {
                    fileStream.Seek(-"</ArrayOfLogParameters>".Length - 1, SeekOrigin.End);

                    LogFormat.WriteString(Environment.NewLine, fileStream);

                    XmlWriter xmlWriter = XmlWriter.Create(fileStream, settings);
                    serializer.Serialize(xmlWriter, logInfo, emptyNs);
                    xmlWriter.Dispose();

                    LogFormat.WriteString(Environment.NewLine + "</ArrayOfLogParameters>", fileStream);
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



