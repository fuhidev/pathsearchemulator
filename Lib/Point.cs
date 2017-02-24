using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            var other = obj as Point;
            return this.X == other.X && this.Y == other.Y;
        }

        public override string ToString()
        {
            return String.Format("Point: X={0} Y={1}", X, Y);
        }
    }
}
