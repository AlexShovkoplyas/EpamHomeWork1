using System;
using System.Collections.Generic;
using DumbLogger.Configuration;
using DumbLogger.LogWriters;
using System.IO;
using System.Text;
using System.Linq;

namespace DumbLogger
{
    /// <remarks>Class which creates logger instances, configure them, and manage all created loggers.</remarks>
    public static class LogManager
    {
        private static Dictionary<string,LogWriter> activeLoggers = new Dictionary<string, LogWriter>();

        static LogManager() { }

        public static LogWriter GetLogger(string name)
        {
            LogWriter logger = null;

            try
            {
                if (activeLoggers.ContainsKey(name))
                {
                    Console.WriteLine($"DumbLogger. Logger with requested name : {name} was already created. You can log in the same log file.");
                    return activeLoggers[name];
                }

                LogConfig logConfig = null;
                if (LogConfigManager.Contains(name))
                {
                    Console.WriteLine($"DumbLogger. Logger with requested name : {name} was already set up in config file.");
                    logConfig = LogConfigManager.GetLogConfig(name);
                }
                else
                {
                    logConfig = LogConfigManager.AddLogConfig(name);
                }                              

                logger = CreateLogger(logConfig);
            }
            catch (Exception e)
            {
                Console.WriteLine($"DumbLogger. Error, Logger with the name : {name} was not created");
                Console.WriteLine($"DumbLogger. " + e.Message);
            }
                        
            return logger;
        }

        public static LogWriter GetLogger(LogConfig logConfig)
        {
            LogWriter logger = null;

            try
            {
                if (activeLoggers.ContainsKey(logConfig.LogName))
                {
                    Console.WriteLine($"DumbLogger. Logger with requested name : {logConfig.LogName} was already created. You can log in the same log file.");
                    return activeLoggers[logConfig.LogName];
                }

                LogConfig logConfigChecked;
                if (LogConfigManager.Contains(logConfig.LogName))
                {
                    Console.WriteLine($"DumbLogger. Logger with requested name : {logConfig.LogName} was already set up in config file.");
                    logConfigChecked = LogConfigManager.GetLogConfig(logConfig.LogName);
                }
                else
                {
                    logConfigChecked = logConfig;
                    LogConfigManager.AddLogConfig(logConfigChecked);
                }

                logger = CreateLogger(logConfigChecked);
            }
            catch (Exception e)
            {
                Console.WriteLine($"DumbLogger. Error, Logger with the name : {logConfig.LogName} was not created");
                Console.WriteLine($"DumbLogger. " + e.Message);
            }
            
            return logger;
        }

        private static LogWriter CreateLogger(LogConfig logConfig)
        {
            LogWriter logger = null;

            switch (logConfig.LogFormat)
            {
                case LogFormatEnum.Txt:
                    logger = new LogWriterPlain(logConfig);
                    break;
                case LogFormatEnum.Xml:
                    logger = new LogWriterXml(logConfig);
                    break;
                case LogFormatEnum.Json:
                    logger = new LogWriterJson(logConfig);
                    break;
                case LogFormatEnum.Csv:
                    logger = new LogWriterCsv(logConfig);
                    break;
                default:
                    throw new NotImplementedLogFormat(logConfig.LogFormat);
            }

            CreateLogFile(logConfig);

            activeLoggers.Add(logConfig.LogName, logger);
            Console.WriteLine($"DumbLogger. Logger with the name : {logConfig.LogName} was created and processed");

            return logger;
        }


        private static void CreateLogFile(LogConfig logConfig)
        {
            string logFileFullName = logConfig.LogDirectory + @"\" + logConfig.LogFileName;

            try
            {
                DirectoryInfo configDirectory = new DirectoryInfo(logConfig.LogDirectory);

                if (!configDirectory.Exists)
                {
                    configDirectory.Create();
                }                

                FileInfo configFile = new FileInfo(logFileFullName);

                if (!configFile.Exists)
                {
                    Console.WriteLine($"DumbLogger. Config file was created");
                    using (FileStream fileStream = configFile.Create())
                    {
                        SetUpLogFile(fileStream, logConfig);
                    }
                }
                else
                {                    
                    using (FileStream fileStream = configFile.Open(FileMode.Open))
                    {
                        fileStream.SetLength(0);
                        Console.WriteLine($"DumbLogger. Config file was cleaned up");
                        SetUpLogFile(fileStream, logConfig);
                    }
                }
            }
            catch (ArgumentException)
            {
                Console.WriteLine("DumbLogger. Error, Wrong path for log file.");
                throw;
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("DumbLogger. Error, It was not authorised to create log file.");
                throw;
            }
            catch (Exception)
            {
                Console.WriteLine("DumbLogger. Error, Problems with creating log file.");
                throw;
            }
        }

        private static void SetUpLogFile(FileStream fileStream, LogConfig logConfig)
        {
            string initialString = "";

            switch (logConfig.LogFormat)
            {
                case LogFormatEnum.Txt:
                    break;
                case LogFormatEnum.Xml:
                    initialString = @"<?xml version=""1.0""?>" + 
                        Environment.NewLine +
                        @"<ArrayOfLogParameters xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">" + 
                        Environment.NewLine +
                        @"</ArrayOfLogParameters>";
                    break;
                case LogFormatEnum.Json:
                    initialString = "[" + Environment.NewLine + "]";
                    break;
                case LogFormatEnum.Csv:
                    break;
                default:
                    break;
            }
            byte[] initialBytes = Encoding.Default.GetBytes(initialString);
            fileStream.Write(initialBytes, 0, initialBytes.Length);
        }
    }

}

