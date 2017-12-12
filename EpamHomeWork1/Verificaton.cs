using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace EpamHomeWork1
{
    class Verificaton
    {
        static ConsoleColor defaultColor = Console.BackgroundColor;
        static ConsoleColor emphasizeColor = ConsoleColor.Red;

        public static int IntValue(string parameterName)
        {
            int result;
            string resultString;

            Console.WriteLine($"=> Enter {parameterName} : ");
            resultString = Console.ReadLine();
            while (!int.TryParse(resultString, out result))
            {
                Write($"=> Enter ");
                Console.BackgroundColor = emphasizeColor;
                Write("correct");
                Console.BackgroundColor = defaultColor;
                Write($" {parameterName} : \n");
                resultString = Console.ReadLine();
            }
            return result;
        }

        public static double DoubleValue(string parameterName)
        {
            double result;
            string resultString;

            Console.WriteLine($"=> Enter {parameterName} : ");
            resultString = Console.ReadLine();
            while (!double.TryParse(resultString, out result))
            {
                Write($"=> Enter ");
                Console.BackgroundColor = emphasizeColor;
                Write("correct");
                Console.BackgroundColor = defaultColor;
                Write($" {parameterName} : \n");
                resultString = Console.ReadLine();
            }
            return result;
        }

        public static double[] DoubleArray()
        {
            string resultString;
            string[] stringNumbers;
            double[] numbers;

            Console.WriteLine($"=> Enter array of integers separated by space symbols : ");
            resultString = ReadLine();
            stringNumbers = resultString.Split(' ');
            numbers = new double[stringNumbers.Length];
            while (!IsParsed(stringNumbers, out numbers))
            {
                Write($"=> Enter ");
                Console.BackgroundColor = emphasizeColor;
                Write("correct");
                Console.BackgroundColor = defaultColor;
                Write($" array of integers separated by space symbols : \n");
                resultString = Console.ReadLine();
                stringNumbers = resultString.Split(' ');
                numbers = new double[stringNumbers.Length];
            }

            return numbers;
        }

        public static bool IsParsed(string[] stringNumbers, out double[] numbers)
        {
            double[] numbersTemp = new double[stringNumbers.Length];
            for (int i = 0; i < stringNumbers.Length; i++)
            {
                if (!double.TryParse(stringNumbers[i], out numbersTemp[i]))
                {
                    numbers = null;
                    return false;
                }
            }
            numbers = numbersTemp;
            return true;
        }
    }
}
