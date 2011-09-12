using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoadNetworkSolver
{
    class Edge
    {
        private Edge otherSide;
        private int end;

        public int Start
        {
            get
            {
                return otherSide.end;
            }
            set
            {
                otherSide.end = value;
            }
        }

        public int End
        {
            get
            {
                return end;
            }
            set
            {
                end = value;
            }
        }

        public Edge Reversed
        {
            get
            {
                return otherSide;
            }
        }

        private Edge(int end)
        {
            this.end = end;
        }

        public Edge(int start, int end)
        {
            this.otherSide = new Edge(start);
            this.end = end;
        }
    }
}
