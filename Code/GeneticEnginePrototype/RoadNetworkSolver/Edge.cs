using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        /// Get or set a flag indicating that the edge is broken.
        /// </summary>
        public bool Broken
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

        /// <summary>
        /// Initialise a new Edge when the 'other side' already exists.
        /// </summary>
        /// <param name="end">The end Vertex of this edge.</param>
        /// <param name="otherSide">The 'other side' of this edge.</param>
        private Edge(Vertex end, Edge otherSide)
        {
            this.end = end;
            this.otherSide = otherSide;
            end.AddEdge(otherSide);
        }

        /// <summary>
        /// Initialise a new Edge
        /// </summary>
        /// <param name="start">The start Vertex</param>
        /// <param name="end">The end Vertex</param>
        public Edge(Vertex start, Vertex end)
        {
            this.end = end;
            this.otherSide = new Edge(start, this);
            end.AddEdge(otherSide);
        }
    }
}
