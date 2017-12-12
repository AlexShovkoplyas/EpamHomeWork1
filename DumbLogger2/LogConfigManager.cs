using System;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using DumbLogger.Configuration;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace DumbLogger
{
    internal static class LogConfigManager
    {
        const string configFileName = "logConfig.xml";

        private static List<LogConfig> configs = new List<LogConfig>();

        static LogConfigManager()
        {
            CreateConfigFile(); 
            GetLogConfigsFromFile();
        }

        private static void CreateConfigFile()
        {
            FileInfo configFile;
            FileStream fs = null;

            try
            {
                configFile = new FileInfo(configFileName);

                if (!configFile.Exists)
                {
                    fs = configFile.Create();
                    Console.WriteLine($"DumbLogger. Config file : {configFileName} was created");
                }                
            }
            catch (ArgumentException)
            {
                Console.WriteLine("DumbLogger. Error, Wrong path for file config.");
                throw;
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("DumbLogger. Error, It was not authorised to create config file.");
                throw;
            }
            catch (Exception)
            {
                Console.WriteLine("DumbLogger. Error, Problems with creating config file.");
                throw;
            }
            finally
            {
                fs?.Dispose();
            }
        }

        private static void GetLogConfigsFromFile()
        {
            try
            {
                using (FileStream fs = new FileStream(configFileName, FileMode.Open))
                {
                    if (fs.Length>0)
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(List<LogConfig>));
                        configs = (List<LogConfig>)serializer.Deserialize(fs);
                        Console.WriteLine("DumbLogger. Config file was read");
                    }                    
                };
            }
            catch (Exception)
            {
                Console.WriteLine("DumbLogger. Error, It was not possible to read data from config file");
                throw;
            }
        }

        public static bool Contains(string name)
        {
            return configs.Select(x => x.LogName).Contains(name);
        }        

        public static LogConfig GetLogConfig(string name)
        {
            return configs.Where(c => c.LogName == name).First();
        }

        public static LogConfig AddLogConfig(LogConfig logConfig)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<LogConfig>));

            configs.Add(logConfig);

            try
            {
                using (Stream fileStream = new FileStream(configFileName, FileMode.Open))
                {
                    serializer.Serialize(fileStream, configs);

                    Console.WriteLine($"DumbLogger. New log configuration : {logConfig.LogName} was added to config file {configFileName}");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("DumbLogger. Error, It was not possible to update log configuration file");
                throw;
            }

            return logConfig;                      
        }

        public static LogConfig AddLogConfig(string name)
        {
            LogConfig logConfigDefault = new LogConfig(name);
            return AddLogConfig(logConfigDefault);
        }
    }
}
