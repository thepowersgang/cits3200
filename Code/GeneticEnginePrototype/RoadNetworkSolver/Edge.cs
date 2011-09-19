using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoadNetworkSolver
{
    class Edge
    {
        private Edge otherSide;
        private Vertex end;

        public Vertex Start
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

        public Vertex End
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

        private Edge(Vertex end, Edge otherSide)
        {
            this.end = end;
            this.otherSide = otherSide;
            end.AddEdge(otherSide);
        }

        public Edge(Vertex start, Vertex end)
        {
            this.end = end;
            this.otherSide = new Edge(start, this);
            end.AddEdge(otherSide);
        }

        public Edge CreateCopy()
        {
            return new Edge(otherSide.end.Copy, end.Copy);
        }
    }
}
