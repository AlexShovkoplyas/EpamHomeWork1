using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.ComponentModel;

namespace DumbLogger.Serializers
{
    internal interface ITextSerializer<T>
    {
        void Serialize(string fullFilePath, T list);
        IList<T> Deserialize(FileStream fs);
    }

    internal class CsvSerializer<T> : ITextSerializer<T> where T : class, new()
    {
        PropertyInfo[] properties;

        public char Separator { get; } = ',';

        public CsvSerializer()
        {
            Type type = typeof(T);

            properties = type.GetProperties(
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.GetProperty |
                BindingFlags.SetProperty);
        }

        public IList<T> Deserialize(FileStream fs)
        {
            List<T> data = new List<T>();
            string[] columns;
            string[] rows;
            
            try
            {
                using (StreamReader StreamReader = new StreamReader(fs))
                {
                    columns = StreamReader.ReadLine().Split(Separator);
                    rows = StreamReader.ReadToEnd().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"DumbLogger. Error, Problems with reading from file : {fs.Name}");
                throw;
            }

            try
            {   
                for (int row = 0; row < rows.Length; row++)
                {
                    string line = rows[row];

                    if (line.Length == 0) continue;

                    string[] parts = line.Split(Separator);

                    T datum = new T();

                    for (int i = 0; i < parts.Length; i++)
                    {
                        if (parts[i].Length == 0) continue;

                        var value = parts[i];
                        var column = columns[i];

                        PropertyInfo p = properties.FirstOrDefault(a => a.Name.Equals(column, StringComparison.InvariantCultureIgnoreCase));

                        if (value.IndexOf("\"") == 0)
                            value = value.Substring(1);

                        if (value[value.Length - 1].ToString() == "\"")
                            value = value.Substring(0, value.Length - 1);

                        TypeConverter converter = TypeDescriptor.GetConverter(p.PropertyType);
                        var convertedvalue = converter.ConvertFrom(value);

                        p.SetValue(datum, convertedvalue);
                    }

                    data.Add(datum);
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"DumbLogger. Error, Problems with deserializing from file : {fs.Name}");
                throw;
            }            

            return data;
        }

        public void Serialize(string fullFilePath, T data)
        {
            var sb = new StringBuilder();
            var outputValues = new List<string>();

            foreach (var property in properties)
            {
                var value = property.GetValue(data);
                var outputValue = string.Format("\"{0}\"", value == null ? "" : value);

                outputValues.Add(outputValue);
            }

            sb.Append(string.Join(Separator.ToString(), outputValues));

            try
            {
                using (var sw = new StreamWriter(fullFilePath, true, Encoding.Default))
                {
                    if (sw.BaseStream.Length < 5)
                    {
                        sw.WriteLine(Header());
                    }
                    sw.WriteLine(sb.ToString());
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"DumbLogger. Error, Problems with writing into file : {fullFilePath}");
                throw;
            }            
        }

        private string Header()
        {
            var header = properties.Select(a => a.Name);

            return string.Join(Separator.ToString(), header);
        }
    }
}
