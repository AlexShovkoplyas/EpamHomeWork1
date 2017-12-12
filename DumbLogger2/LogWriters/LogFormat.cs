using System.Text;
using System.IO;

namespace DumbLogger.LogWriters
{
    internal static class LogFormat
    {
        public static void WriteString(string text, FileStream fileStream)
        {
            byte[] textToByteArray = Encoding.Default.GetBytes(text);
            fileStream.Write(textToByteArray, 0, textToByteArray.Length);
        }
    }
}
