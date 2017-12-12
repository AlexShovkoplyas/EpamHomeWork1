
using System;
using System.Runtime.Serialization;
using DumbLogger.Configuration;
using System.Xml.Serialization;

namespace DumbLogger
{
    /// <remarks>Specifies all data that are requested to be logged.</remarks>
    [Serializable]
    [DataContract]
    public class LogParameters
    {
        public LogParameters() { }

        [DataMember]
        [XmlAttribute]
        public string Message { get; set; }

        [DataMember]
        [XmlAttribute]
        public LogLevelEnum LogLevel { get; set; }

        [DataMember]
        [XmlAttribute]
        public string Error { get; set; }

        [DataMember]
        [XmlAttribute]
        public string Application { get; set; }

        [DataMember]
        [XmlAttribute]
        public string ClassName { get; set; }

        [DataMember]
        [XmlAttribute]
        public string MethodPath { get; set; }

        [DataMember]
        [XmlAttribute]
        public DateTime TimeStamp { get; set; }

    }
}
