using System;
using System.IO;
using System.Xml.Serialization;

namespace DumbLogger.Configuration
{
    /// <remarks>Specifies what can be configured in logger.</remarks>
    [Serializable]
    public class LogConfig
    {
        public LogConfig() { }

        //public LogConfig(string logName)
        //{
        //    LogName = logName;
        //    LogDirectory = Directory.GetCurrentDirectory() + @"\Logs";
        //    LogLevel = LogLevelEnum.Debug;
        //    LogFormat = LogFormatEnum.Xml;
        //    LogFileName = logName + "." + Enum.GetName(typeof(LogFormatEnum), LogFormat);
        //}
        static LogConfig()
        {
            _logDirectoryDefault = Directory.GetCurrentDirectory() + @"\Logs" ;
            _logLevelDefault = LogLevelEnum.Debug;
            _logFormatDefault = LogFormatEnum.Xml;
        }

        public LogConfig(string logName, string logDirectory = "", LogLevelEnum? logLevel = null, LogFormatEnum? logFormat = null, string logFileName = "")
        {
            LogName = logName;
            LogDirectory = logDirectory == "" ? _logDirectoryDefault : logDirectory;
            LogLevel = logLevel==null ? _logLevelDefault : logLevel.GetValueOrDefault(LogLevelEnum.Debug);
            LogFormat = logFormat==null ? _logFormatDefault : logFormat.GetValueOrDefault(LogFormatEnum.Xml);
            LogFileName = logFileName == "" ? logName + "." + Enum.GetName(typeof(LogFormatEnum), LogFormat) : logFileName;
        }

        public static void SetDefaultValues(string logDirectory = "", LogLevelEnum? logLevel = null, LogFormatEnum? logFormat = null)
        {
            if (logDirectory != "") _logDirectoryDefault = logDirectory;
            if (logLevel.HasValue) _logLevelDefault = logLevel.GetValueOrDefault(LogLevelEnum.Debug);
            if (logFormat.HasValue) _logFormatDefault = logFormat.GetValueOrDefault(LogFormatEnum.Xml);
        }

        private static string _logDirectoryDefault;
        private static LogLevelEnum _logLevelDefault;
        private static LogFormatEnum _logFormatDefault;


        [XmlAttribute]
        public string LogName { get; set; }

        public string LogDirectory { get; set; }

        public LogLevelEnum LogLevel { get; set; }

        public LogFormatEnum LogFormat { get; set; }

        public string LogFileName { get; set; }

    }
}



