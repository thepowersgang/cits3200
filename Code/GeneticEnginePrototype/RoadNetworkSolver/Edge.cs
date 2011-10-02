using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace RoadNetworkSolver
{
    /// <summary>
    /// Represents an edge in a RoadNetwork
    /// </summary>
    public class Edge
    {
        /// <summary>
        /// The 'other side' of this edge. An edge linking the same two vertices but with the start and end swapped. 
        /// </summary>
        private Edge otherSide;

        /// <summary>
        /// The end vertex of this edge.
        /// </summary>
        private Vertex end;

        /// <summary>
        /// Flag to indicate that this edge has been broken.
        /// </summary>
        private bool broken;

        /// <summary>
        /// Get the start vertex of this edge.
        /// </summary>
        public Vertex Start
        {
            get
            {
                return otherSide.end;
            }
        }

        /// <summary>
        /// Get the end vertex of this edge.
        /// </summary>
        public Vertex End
        {
            get
            {
                return end;
            }
        }

        /// <summary>
        /// Get the 'other side' of this edge.
        /// The other side has Start and End swapped.
        /// </summary>
        public Edge Reversed
        {
            get
            {
                return otherSide;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsBroken
        {
            get
            {
                return broken;
            }

            set
            {
                broken = value;
                otherSide.broken = value;
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
    }
}
