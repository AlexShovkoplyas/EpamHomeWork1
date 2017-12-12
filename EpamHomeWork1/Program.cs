using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DumbLogger;
using DumbLogger.Configuration;

namespace EpamHomeWork1
{
    class Program
    {
        static LogWriter logger;
        static string message;

        static void Main(string[] args)
        {
            string choice;

            Console.WriteLine("****** Hello dear teacher!!! ******".ToUpper());

            Thread.Sleep(2000);

            logger = LogManager.GetLogger("Home work Logger");

            Console.WriteLine("What task would you like to check?");
            PrintOptions();

            choice = Console.ReadLine();

            while (choice.ToUpper()!="Q")
            {
                try
                {
                    switch (choice.ToUpper())
                    {
                        case "1":
                            Console.WriteLine("\n*** TASK 1 ***");
                            Task1Default();
                            break;
                        case "2":
                            Console.WriteLine("\n*** TASK 2 ***");
                            Task2Default();
                            break;
                        case "3":
                            Console.WriteLine("\n*** TASK 3 ***");
                            Task3Default();
                            break;
                        case "4":
                            Console.WriteLine("\n*** TASK 4 ***");
                            Task4Default();
                            break;
                        case "O":
                            PrintOptions();
                            break;
                        case "L":
                            PrintLogs();
                            break;

                        default:
                            Console.WriteLine("Wrong input. Use one of options stated below.");
                            PrintOptions();
                            break;
                    }
                }
                catch (Exception e)
                {
                    message = $"Problems with input parameters in task {choice}. Try to test task from beggining.";
                    Console.WriteLine(message);
                    logger.Error(message, e);
                }
                
                Console.WriteLine("\nSome other task?");
                choice = Console.ReadLine();
            }            
            //Console.ReadKey();
        }

        private static void PrintLogs()
        {
            logger.ReadLogFile();
        }

        static public void PrintOptions()
        {
            Console.WriteLine("Options :");
            Console.WriteLine(1);
            Console.WriteLine(2);
            Console.WriteLine(3);
            Console.WriteLine(4);
            Console.WriteLine("O  -  options");
            Console.WriteLine("L  -  pring logs");
            Console.WriteLine("Q  -  quit");
        }

        static public void Task1Default()
        {
            Console.WriteLine("=== Input vector 1 :");
            var vector1 = new Vector3D(Verificaton.DoubleArray());

            Console.WriteLine("=== Input vector 2 :");
            var vector2 = new Vector3D(Verificaton.DoubleArray());

            Console.WriteLine("=== Input vector 3 :");
            var vector3 = new Vector3D(Verificaton.DoubleArray());

            Console.WriteLine("\nVector 1 :");
            vector1.Output();

            Console.WriteLine("\nVector 2 :");
            vector2.Output();

            Console.WriteLine("\nVector 3 :");
            vector3.Output();

            try
            {
                Console.WriteLine("\nVector 1 + Vector 2 :");
                (vector1 + vector2).Output();

                Console.WriteLine("\nVector 1 - Vector 2 :");
                (vector1 - vector2).Output();
            }
            catch (Exception e)
            {
                message = "Unable to Sum or Substract 2 vector 1 and vector 2";
                Console.WriteLine(message);
                logger.Error(message, e);
            }

            try
            {
                Console.WriteLine("\nScalar Product (Vector 1 , Vector 2) :");
                Console.WriteLine(Vector3D.ScalarProduct(vector1, vector2).ToString());

                Console.WriteLine("\nAngle between (Vector 1 , Vector 2) :");
                Console.WriteLine(Vector3D.Angle(vector1, vector2).ToString("F2") + " grad.");
            }
            catch (Exception e)
            {
                message = "Unable to calculate scalar product of vector 1 and vector 2";
                Console.WriteLine(message);
                logger.Error(message, e);
            }

            try
            {
                Console.WriteLine("\nVector Product (Vector 1 , Vector 2) :");
                Vector3D.VectorProduct(vector1, vector3).Output();
            }
            catch (Exception e)
            {
                message = "Unable to calculate vector product of vector 1 and vector 2";
                Console.WriteLine(message);
                logger.Error(message, e);
            }

            try
            {
                Console.WriteLine("\nTripple Product (Vector 1 , Vector 2, Vector 3) :");
                Console.WriteLine(Vector3D.TrippleProduct(vector1, vector2, vector3).ToString());
            }
            catch (Exception e)
            {
                message = "Unable to calculate tripple product";
                Console.WriteLine(message);
                logger.Error(message, e);
            }

            try
            {
                Console.WriteLine("\nVector Length (Vector 1) :");
                Console.WriteLine(vector1.VectorLength().ToString("F2"));
                Console.WriteLine($"\nVector 1 is greater than Vector 2 ? {Vector3D.Compare(vector1, vector2) > 0}");
            }
            catch (Exception e)
            {
                message = "Unable to calculate vector length";
                Console.WriteLine(message);
                logger.Error(message, e);
            }
        }

        static public void Task2Default()
        {
            Console.WriteLine("/n=== Input parameters for rectangle 1 in format : Left Top Right Bottom");
            Rectangle rectangle1 = new Rectangle(Verificaton.DoubleArray());    

            Console.WriteLine("/n=== Input parameters for rectangle 2 in format : Left Top Right Bottom");
            Rectangle rectangle2 = new Rectangle(Verificaton.DoubleArray());

            rectangle1.Output();
            rectangle2.Output();

            Console.WriteLine("/n=== Move rectangle 1 : Left Top Right Bottom");
            rectangle1.Move(Verificaton.DoubleValue("Horizontal offset"), Verificaton.DoubleValue("Vertical offset"));

            rectangle1.Output();

            try
            {
                Console.WriteLine("/n=== Intercept rectangle 1, rectangle 2 : Left Top Right Bottom");
                Rectangle.Intercept(rectangle1, rectangle2).Output();
            }
            catch (Exception e)
            {
                message = "Unable to Intercept rectangles";
                Console.WriteLine(message);
                logger.Error(message, e);
            }

            try
            {
                Console.WriteLine("/n=== Union rectangle 1, rectangle 2 : Left Top Right Bottom");
                Rectangle.Union(rectangle1, rectangle2).Output();
            }
            catch (Exception e)
            {
                message = "Unable to Union rectangles";
                Console.WriteLine(message);
                logger.Error(message, e);
            }
        }

        static public void Task3Default()
        {
            int arrayLength;
            int startIndex;

            Console.WriteLine("=== Input vector 1 :");
            arrayLength = Verificaton.IntValue("Length of array");
            startIndex = Verificaton.IntValue("Start index");
            var vector1 = VectorGeneral.GetRandom(arrayLength, startIndex);

            Console.WriteLine("=== Input vector 2 (with random values :");
            var vector2 = new VectorGeneral(Verificaton.DoubleArray(), Verificaton.IntValue("Start index"));

            Console.WriteLine("\nVector 1 :");
            vector1.Output();

            Console.WriteLine("\nVector 2 :");
            vector2.Output();

            try
            {
                Console.WriteLine("\nGetting element of vector 1 by index :");
                Console.WriteLine(vector1[Verificaton.IntValue("Vector index")]);
            }
            catch (Exception e)
            {
                message = "Unable to get vector element";
                Console.WriteLine(message);
                logger.Error(message, e);
            }

            try
            {
                Console.WriteLine("\nVector 1 + Vector 2 :");
                (vector1 + vector2).Output();

                Console.WriteLine("\nVector 1 - Vector 2 :");
                (vector1 - vector2).Output();
            }
            catch (Exception e)
            {
                message = "Unable to add or substract vectors";
                Console.WriteLine(message);
                logger.Error(message, e);
            }
            
            Console.WriteLine("\nVector 1  * Scalar :");
            vector1.MultScalar(Verificaton.DoubleValue("Scalar value")).Output();

            Console.WriteLine($"\nVector 1 is greater than Vector 2 ? {VectorGeneral.Compare(vector1, vector2) > 0}");
        }        

        static public void Task4Default()
        {
            var matrix1 = Matrix.GetRandom(2, 2);
            matrix1.Info = "Matrix A";
            matrix1.Output();
            var matrix2 = Matrix.GetRandom(2, 2);
            matrix2.Info = "Matrix B";
            matrix2.Output();

            var matrixSum = matrix1 + matrix2;
            matrixSum.Info = "A + B";
            matrixSum.Output();

            var matrixSubstract = matrix1 - matrix2;
            matrixSubstract.Info = "A - B";
            matrixSubstract.Output();

            Console.WriteLine("\n==================================");
            var matrix3 = Matrix.GetRandom(2, 4);
            matrix3.Info = "Matrix A";
            matrix3.Output();
            var matrix4 = Matrix.GetRandom(4, 2);
            matrix4.Info = "Matrix B";
            matrix4.Output();

            var matrixMultipy = matrix3 * matrix4;
            matrixMultipy.Info = "A * B";
            matrix4.Output();

            Console.WriteLine("\n==================================");
            var matrix5 = Matrix.GetRandom(4, 4);
            matrix5.Output();

            var matrixScalarMult = matrix5 * 2;
            matrixScalarMult.Info = "[scalar] 2 * B";
            matrixScalarMult.Output();

            var matrixTranspose = matrix5 * 2;
            matrixTranspose.Info = "Transpose (A)";
            matrixTranspose.Output();

            var subMatrix = matrix5.SubMatrix(new int[] { 1, 1 }, new int[] { 2, 2 });
            subMatrix.Info = "SubMatrix (A)";
            subMatrix.Output();

            Console.ReadKey();
        }
    }
}
