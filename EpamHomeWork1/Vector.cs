using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpamHomeWork1
{
    class Vector
    {
        public static int dimension;

        static Vector()
        {
            dimension = 3;
        }

        public Vector(params double[] coordinates)
        {
            for (int i = 0; i < coordinates.Length; i++)
            {
                Coordinates[i] = coordinates[i];
            }
        }

        public double[] Coordinates { get; set; } = new double[dimension];

        public double this[int i]
        {
            get { return Coordinates[i]; }

            //контроль виходу за допустимі межі
            set { }
        }

        static public Vector Add(Vector v1, Vector v2)
        {
            double[] newCoordinates = new double[dimension];
            for (int i = 0; i < dimension; i++)
            {
                newCoordinates[i] = v1[i] + v2[i];
            }

            return new Vector(newCoordinates);
        }

        static public Vector Substract(Vector v1, Vector v2)
        {
            double[] newCoordinates = new double[dimension];
            for (int i = 0; i < dimension; i++)
            {
                newCoordinates[i] = v1[i] - v2[i];
            }

            return new Vector(newCoordinates);
        }

        static public double ScalarProduct(Vector v1, Vector v2)
        {
            double sum = 0;
            for (int i = 0; i < dimension; i++)
            {
                sum += v1[i] * v2[i];
            }
            return sum;
        }

        static public Vector VectorProduct(Vector a, Vector b)
        {
            if (dimension == 3)
            {
                return new Vector(
                    a[2] * b[3] - a[3] * b[2],
                    a[3] * b[1] - a[1] * b[3],
                    a[1] * b[2] - a[2] * b[1]); //  a2b3 - a3b2, a3b1 - a1b3, a1b2 - a2b1)
            }
            else
            {
                Console.WriteLine($"I don't know how to calculate vector product for {dimension} dimentions");
                return null;
            }
        }

        static public double TrippleProduct(Vector a, Vector b, Vector c)
        {
            return ScalarProduct(a, VectorProduct(b, c));
        }

        public double Length()
        {
            double sum = 0;
            for (int i = 0; i < dimension; i++)
            {
                sum += Math.Pow(Coordinates[i], 2);
            }
            return Math.Sqrt(sum);
        }

        public Vector ScalarProd(double s)
        {

            return 
        }

        static double Angle(Vector a, Vector b)
        {
            double cosinus = ScalarProduct(a, b) / (a.Length() * b.Length());
            return Math.Acos(cosinus);
        }

        static double Compare(Vector a, Vector b)
        {
            return a.Length() - b.Length();
        }

        public static Vector operator +(Vector a, Vector b)
        {
            return Add(a, b);
        }

        public static Vector operator -(Vector a, Vector b)
        {
            return Substract(a, b);
        }
    }
}
