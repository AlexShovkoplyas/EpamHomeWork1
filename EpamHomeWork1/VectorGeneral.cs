using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpamHomeWork1
{
    class VectorGeneral
    {
        public int MinIndex
        {
            get;
        }
        public int MaxIndex
        {
            get { return MinIndex + Length - 1; }
        }
        public int Length
        {
            get;
        }

        protected double[] coordinates;

        public VectorGeneral(double[] coordinates, int startIndex = 0)
        {
            MinIndex = startIndex;
            Length = coordinates.Length;

            this.coordinates = coordinates.Clone() as double[];
        }

        public static VectorGeneral GetRandom(int length, int startIndex)
        {
            double[] coordinates = new double[length];

            Random rand = new Random();
            for (int i = 0; i < length; i++)
            {
                coordinates[i] = rand.Next(50);
            }
            return new VectorGeneral(coordinates, startIndex);
        }

        public double this[int i]
        {
            get
            {
                if (i < MinIndex || i > MaxIndex)
                {
                    throw new IndexOutOfRangeException($"Index {i} should be between {MinIndex} and {MaxIndex}");
                }
                return coordinates[i - MinIndex];
            }
        }

        static public VectorGeneral Add(VectorGeneral v1, VectorGeneral v2)
        {
            if (v1.MinIndex != v2.MinIndex || v1.MaxIndex != v2.MaxIndex)
            {
                throw new Exception("Vectors of different size or starting index");
            }

            double[] newCoordinates = new double[v1.Length];
            for (int i = v1.MinIndex; i <= v1.MaxIndex; i++)
            {
                newCoordinates[i - v1.MinIndex] = v1[i] + v2[i];
            }

            return new VectorGeneral(newCoordinates, v1.MinIndex);
        }

        static public VectorGeneral Substract(VectorGeneral v1, VectorGeneral v2)
        {
            if (v1.MinIndex != v2.MinIndex || v1.MaxIndex != v2.MaxIndex)
            {
                throw new Exception("Vectors of different size or starting index");
            }

            double[] newCoordinates = new double[v1.Length];
            for (int i = v1.MinIndex; i <= v1.MaxIndex; i++)
            {
                newCoordinates[i - v1.MinIndex] = v1[i] - v2[i];
            }

            return new VectorGeneral(newCoordinates, v1.MinIndex);
        }

        static public double ScalarProduct(VectorGeneral v1, VectorGeneral v2)
        {
            if (v1.MinIndex != v2.MinIndex || v1.MaxIndex != v2.MaxIndex)
            {
                throw new Exception("Vectors of different size or starting index");
            }

            double sum = 0;
            for (int i = v1.MinIndex; i <= v1.MaxIndex; i++)
            {
                sum += v1[i] * v2[i];
            }
            return sum;
        }

        public double VectorLength()
        {
            return Math.Sqrt(coordinates.Select(c => Math.Pow(c, 2)).Sum());
        }

        public VectorGeneral MultScalar(double s)
        {
            double[] newCoordinates = new double[Length];
            for (int i = MinIndex; i <= MaxIndex; i++)
            {
                newCoordinates[i - MinIndex] = this[i] * s;
            }

            return new VectorGeneral(newCoordinates, MinIndex);
        }

        public static double Compare(VectorGeneral a, VectorGeneral b)
        {
            return a.VectorLength() - b.VectorLength();
        }

        public static VectorGeneral operator +(VectorGeneral a, VectorGeneral b)
        {
            return Add(a, b);
        }

        public static VectorGeneral operator -(VectorGeneral a, VectorGeneral b)
        {
            return Substract(a, b);
        }

        public void Output()
        {
            for (int i = MinIndex; i <= MaxIndex; i++)
            {
                Console.Write($"{i,4}");
            }
            Console.WriteLine();
            for (int i = 0; i < Length; i++)
            {
                Console.Write($"{coordinates[i],4}");
            }
            Console.WriteLine();
        }
    }
}
