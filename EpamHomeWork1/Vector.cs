using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpamHomeWork1
{
    class Vector3D : VectorGeneral
    {
        public Vector3D(params double[] coordinates) : base(coordinates, 0)
        {
            if (coordinates.Length!=3)
            {
                throw new Exception("Vector3D operates only with vectors of 3 dimensions");
            }
        }

        static public Vector3D VectorProduct(Vector3D a, Vector3D b)
        {
            return new Vector3D(
                a[1] * b[2] - a[2] * b[1],
                a[2] * b[0] - a[0] * b[2],
                a[0] * b[1] - a[1] * b[0]);
        }

        static public double TrippleProduct(Vector3D a, Vector3D b, Vector3D c)
        {
            return ScalarProduct(a, VectorProduct(b, c));
        }

        static public double Angle(Vector3D a, Vector3D b)
        {
            double cosinus = ScalarProduct(a, b) / (a.VectorLength() * b.VectorLength());
            return  180* Math.Acos(cosinus) / Math.PI;
        }
    }
}
