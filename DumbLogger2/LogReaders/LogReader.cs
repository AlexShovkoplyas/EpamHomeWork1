using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using DumbLogger.Configuration;
using System.IO;
using DumbLogger.Serializers;
using System.Xml.Serialization;

namespace DumbLogger.LogReaders
{
    internal static class LogReader
    {
        static LogConfig Config;
        static string logFilePath;

        public static void ReadLogFile(LogConfig logConfig)
        {
            Config = logConfig;
            logFilePath = logConfig.LogDirectory + @"\" + logConfig.LogFileName;

            switch (logConfig.LogFormat)
            {
                case LogFormatEnum.Txt:
                    ReadPlain();
                    break;
                case LogFormatEnum.Xml:
                    ReadXml();
                    break;
                case LogFormatEnum.Json:
                    ReadJson();
                    break;
                case LogFormatEnum.Csv:
                    ReadCsv();
                    break;
                default:
                    throw new NotImplementedLogFormat(logConfig.LogFormat);
            }
        }

        private static void ReadCsv()
        {
            List<LogParameters> logData = new List<LogParameters>();
            var serializer = new CsvSerializer<LogParameters>();

            try
            {
                using (FileStream fileStream = new FileStream(logFilePath, FileMode.Open))
                {
                    logData = (List<LogParameters>)serializer.Deserialize(fileStream);
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"DumbLogger. Error, Unable to read from file : {logFilePath}");
                throw;
            }           

            ConsoleOutput(logData);
        }

        private static void ReadJson()
        {
            List<LogParameters> logData = new List<LogParameters>();
            var serializer = new DataContractJsonSerializer(typeof(List<LogParameters>));

            try
            {
                using (FileStream fileStream = new FileStream(logFilePath, FileMode.Open))
                {
                    logData = (List<LogParameters>)serializer.ReadObject(fileStream);
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"DumbLogger. Error, Unable to read from file : {logFilePath}");
                throw;
            }            

            ConsoleOutput(logData);
        }

        private static void ReadXml()
        {
            List<LogParameters> logData = new List<LogParameters>();
            XmlSerializer serializer = new XmlSerializer(typeof(List<LogParameters>));

            try
            {
                using (FileStream fs = new FileStream(logFilePath, FileMode.Open))
                {
                    logData = (List<LogParameters>)serializer.Deserialize(fs);
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"DumbLogger. Error, Unable to read from file : {logFilePath}");
                throw;
            }            

            ConsoleOutput(logData);
        }

        private static void ReadPlain()
        {
            throw new NotImplementedException();
        }

        private static void ConsoleOutput(List<LogParameters> logData)
        {
            Console.WriteLine("===============================");
            Console.WriteLine($"Logs from logger <{Config.LogName}>, file path : {logFilePath}");
            foreach (var param in logData)
            {
                Console.WriteLine($"{param.LogLevel} | message = {param.Message} | time = {param.TimeStamp}");

                //if (param.Error==null)
                //{
                //    Console.WriteLine($"{param.LogLevel} | message = {param.Message}");
                //}
                //else
                //{
                //    Console.WriteLine($"{param.LogLevel} | message = {param.Message} | Error msg = {param.Error}");
                //}
                
            }
        }
    }
}
