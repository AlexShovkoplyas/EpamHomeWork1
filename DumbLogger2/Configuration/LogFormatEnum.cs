using System;

namespace DumbLogger.Configuration
{
    public enum LogFormatEnum
    {
        Xml,
        Json,
        Csv,
        Txt
    }

    public class NotImplementedLogFormat : ApplicationException
    {
        //private LogFormatEnum logFormat;

        public NotImplementedLogFormat() : base() { }

        public NotImplementedLogFormat(string message) : base(message) { }

        public NotImplementedLogFormat(string message, Exception innerException) : base(message, innerException) { }

        public NotImplementedLogFormat(LogFormatEnum logFormat) : base(GetMessage(logFormat)) { }

        public static string GetMessage(LogFormatEnum logFormat)
        {
            return $"Log Writer : {Enum.GetName(typeof(LogFormatEnum), logFormat)} is not implemented";
        }
    }
}
