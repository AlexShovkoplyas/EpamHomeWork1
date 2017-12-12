using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpamHomeWork1
{
    class Rectangle
    {
        public double Left { get;private set; }
        public double Top { get; private set; }
        public double Right { get; private set; }
        public double Bottom { get; private set; }

        private Rectangle() { }

        public Rectangle(double left, double top, double right, double bottom)
        {
            if (left >= right || top <= bottom)
            {
                throw new Exception("Wrong parameters in Rectangle constructor");
            }

            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public Rectangle(double[] parameters):this(parameters[0],parameters[1],parameters[2],parameters[3])
        {
            if (parameters.Length!=4)
            {
                throw new Exception("Wrong parameters in Rectangle constructor");
            }
        }

        public void Move(double horizontalOffset = 0, double verticalOffset = 0)
        {
            Left += horizontalOffset;
            Right += horizontalOffset;

            Top += verticalOffset;
            Bottom += verticalOffset;
        }

        public static Rectangle Intercept(Rectangle a, Rectangle b)
        {
            Rectangle c = new Rectangle();

            if (a.Right < b.Left || a.Left > b.Right || a.Top < b.Bottom || a.Bottom > b.Top)
            {
                throw new Exception("Interception of 2 rectangles is not possible");
            }
            else
            {
                c.Left = Math.Max(a.Left, b.Left);
                c.Right = Math.Min(a.Right, b.Right);
                c.Top = Math.Min(a.Top, b.Top);
                c.Bottom = Math.Max(a.Bottom, b.Bottom);
            }

            return c;
        }

        public static Rectangle Union(Rectangle a, Rectangle b)
        {
            Rectangle c = new Rectangle();

            c.Left = Math.Min(a.Left, b.Left);
            c.Right = Math.Max(a.Right, b.Right);
            c.Top = Math.Max(a.Top, b.Top);
            c.Bottom = Math.Min(a.Bottom, b.Bottom);

            return c;
        }

        public void Output()
        {
            Console.WriteLine($"     Top :{Top:F0}");
            Console.WriteLine($"Left :{Left,-5:F0} Right :{Right,-5:F0}");
            Console.WriteLine($"     Bottom :{Bottom:F0}");
        }
    }
}
