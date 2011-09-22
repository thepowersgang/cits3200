using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoadNetworkSolver
{
    public class Coordinates
    {
        private int x;
        private int y;

        public int X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }

        public int Y
        {
            get
            {
                return y;
            }
            set
            {
               y = value;
            }
        }

        public Coordinates(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int GetDistanceSquared(Coordinates destination)
        {
            int dx = x - destination.x;
            int dy = y - destination.y;

            return dx * dx + dy * dy;
        }

        public double GetDistance(Coordinates destination)
        {
            return Math.Sqrt((double)GetDistanceSquared(destination));
        }

        public bool Equals(Coordinates c)
        {
            return x == c.x && y == c.y;
        }
    }
}
