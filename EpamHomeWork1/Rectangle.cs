using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpamHomeWork1
{
    //class Coordinate
    //{
    //    public int X { get; set; }
    //    public int Y { get; set; }

    //    public Coordinate(int horizontal, int vertical)
    //    {
    //        X = horizontal;
    //        Y = vertical;
    //    }
    //}

    class Rectangle
    {
        public double Left { get; set; }
        public double Top { get; set; }
        public double Right { get; set; }
        public double Bottom { get; set; }

        public Rectangle()
        {

        }

        public Rectangle(double left, double top,double right,double bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public void Move(double horizontalOffset, double verticalOffset)
        {
            Left += horizontalOffset;
            Right += horizontalOffset;

            Top += verticalOffset;
            Bottom += verticalOffset;
        }

        public static Rectangle Intercept(Rectangle a,Rectangle b)
        {
            Rectangle c = new Rectangle();

            if (a.Right< b.Left || a.Left>b.Right|| a.Top<b.Bottom || a.Bottom >b.Top)
            {
                return null;
            }
            else
            {
                c.Left = Math.Max(a.Right, b.Left);
                c.Right = Math.Min(a.Left, b.Right);
                c.Top = Math.Min(a.Top, b.Bottom);
                c.Bottom = Math.Max(a.Bottom, b.Top);
            }

            return c;
        }

        public static Rectangle Union(Rectangle a, Rectangle b)
        {
            Rectangle c = new Rectangle();

            if (false)
            {
                return null;
            }
            else
            {
                c.Left = Math.Max(a.Right, b.Left);
                c.Right = Math.Min(a.Left, b.Right);
                c.Top = Math.Min(a.Top, b.Bottom);
                c.Bottom = Math.Max(a.Bottom, b.Top);
            }

            return c;
        }
    }
}
