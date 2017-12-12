using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpamHomeWork1
{
    class Matrix
    {
        public string Info { get; set; }

        private double[,] _matrix;

        public int Rows { get; }

        public int Columns { get; }

        public Matrix(double[,] values)
        {
            Rows = values.GetLength(0);
            Columns = values.GetLength(1);
            _matrix = values.Clone() as double[,];
        }

        public static Matrix GetRandom(int rows, int columns)
        {
            Random rand = new Random();

            double[,] result = new double[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    result[i, j] = rand.Next(50);
                }
            }
            return new Matrix(result);
        }

        public int[] Dimensions
        {
            get { return new int[] { Rows, Columns }; }
        }

        public double this[int i, int j]
        {
            get
            {
                if (i >= Rows || j >= Columns)
                {
                    throw new Exception($"Indexes [{i},{j}] are out of range");
                }
                return _matrix[i, j];
            }
        }

        public Vector3D GetRow(int row)
        {
            double[] values = new double[Columns];

            for (int i = 0; i < Columns; i++)
            {
                values[i] = _matrix[row, i];
            }
            return new Vector3D(values);
        }

        public Vector3D GetColumn(int column)
        {
            double[] values = new double[Rows];

            for (int i = 0; i < Rows; i++)
            {
                values[i] = _matrix[i, column];
            }
            return new Vector3D(values);
        }

        static public Matrix Add(Matrix x, Matrix y)
        {
            if (!x.Dimensions.SequenceEqual(y.Dimensions))
                throw new Exception("It is not possible 2 add matrixes as they have different sizes");

            double[,] result = new double[x.Rows, x.Columns];

            for (int i = 0; i < x.Rows; i++)
            {
                for (int j = 0; j < x.Columns; j++)
                {
                    result[i, j] = x[i, j] + y[i, j];
                }
            }

            return new Matrix(result);
        }

        static public Matrix Substract(Matrix x, Matrix y)
        {
            if (!x.Dimensions.SequenceEqual(y.Dimensions))
                throw new Exception("It is not possible to add matrixes as they have different sizes");

            double[,] result = new double[x.Dimensions[0], x.Dimensions[1]];

            for (int i = 0; i < x.Rows; i++)
            {
                for (int j = 0; j < x.Columns; j++)
                {
                    result[i, j] = x[i, j] - y[i, j];
                }
            }

            return new Matrix(result);
        }

        static public Matrix Apply(Matrix x, Matrix y, Func<double, double, double> operation)
        {
            if (!x.Dimensions.SequenceEqual(y.Dimensions))
                throw new Exception("It is not possible to add matrixes as they have different sizes");

            double[,] result = new double[x.Dimensions[0], x.Dimensions[1]];

            for (int i = 0; i < x.Rows; i++)
            {
                for (int j = 0; j < x.Columns; j++)
                {
                    result[i, j] = operation(x[i, j], y[i, j]);
                }
            }

            return new Matrix(result);
        }

        static public Matrix Multiply(Matrix x, Matrix y)
        {
            if (x.Rows != y.Columns || x.Columns != y.Rows)
                throw new Exception("It is not possible to multiply matrixes as their sizes are not appropriate for this operation");

            double[,] result = new double[x.Rows, y.Columns];

            for (int i = 0; i < x.Rows; i++)
            {
                for (int j = 0; j < y.Columns; j++)
                {
                    result[i, j] = Vector3D.ScalarProduct(x.GetRow(i), y.GetColumn(j));
                }
            }

            return new Matrix(result);
        }

        public static Matrix operator +(Matrix x, Matrix y)
        {
            return Add(x, y);
        }

        public static Matrix operator -(Matrix x, Matrix y)
        {
            return Substract(x, y);
        }

        public static Matrix operator *(Matrix x, Matrix y)
        {
            return Multiply(x, y);
        }

        public static Matrix operator *(double s, Matrix y)
        {
            return y.ScalarMult(s);
        }

        public static Matrix operator *(Matrix y, double s)
        {
            return y.ScalarMult(s);
        }

        public Matrix ScalarMult(double s)
        {
            double[,] result = new double[Rows, Columns];

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    result[i, j] = _matrix[i, j] * s;
                }
            }

            return new Matrix(result);
        }

        public Matrix Transpose()
        {
            double[,] result = new double[Rows, Columns];

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    result[i, j] = _matrix[j, i];
                }
            }

            return new Matrix(result);
        }

        public Matrix SubMatrix(int[] initialPoint, int[] subSize)
        {
            try
            {
                double[,] result = new double[subSize[0], subSize[1]];

                for (int i = 0; i < subSize[0]; i++)
                {
                    for (int j = 0; j < subSize[1]; j++)
                    {
                        result[i, j] = _matrix[initialPoint[0] + i, initialPoint[1] + j];
                    }
                }

                return new Matrix(result);
            }
            catch (Exception)
            {
                Console.WriteLine("Problems with getting submatrix");
                throw;
            }
        }

        public double Sum()
        {
            double sum = 0;

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    sum += _matrix[i, j];
                }
            }

            return sum;
        }

        public static double Compare(Matrix x, Matrix y, Func<Matrix, double> func)
        {
            return func(x) - func(y);
        }

        public void Output()
        {
            Console.WriteLine($"===Matrix <{Info}>. Size : {Rows}*{Columns} ===");
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    Console.Write($"{_matrix[i, j],3}");
                }
                Console.WriteLine();
            }
        }
    }

    static class Operations
    {
        public static double Add(double x, double y) => x + y;

        public static double Substract(double x, double y) => x - y;
    }
}
