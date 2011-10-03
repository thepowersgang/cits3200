using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoadNetworkSolver
{
    /// <summary>
    /// Represents a vertex in a RoadNetwork
    /// </summary>
    public class Vertex
    {
        /// <summary>
        /// The location of the vertex.
        /// </summary>
        private Coordinates coordinates;

        /// <summary>
        /// The edges leading out from this vertex.
        /// </summary>
        private List<Edge> edges;

        /// <summary>
        /// The most recent copy made of this vertex.
        /// </summary>
        private Vertex copy;

        /// <summary>
        /// The ID that this vertex was most recently saved with.
        /// </summary>
        private string id;

        /// <summary>
        /// Flag to indicate whether this vertex has been reached in a serach of the RoadNetwork.
        /// </summary>
        private bool visited;

        /// <summary>
        /// The RoadNetwork this vertex belongs to.
        /// </summary>
        private RoadNetwork network;

        /// <summary>
        /// Get or set the location of this vertex.
        /// </summary>
        public Coordinates Coordinates
        {
            get
            {
                return coordinates;
            }

            set
            {
                coordinates = value;
            }
        }

        /// <summary>
        /// Get the number of edges leading out of this vertex.
        /// </summary>
        public int EdgeCount
        {
            get
            {
                return edges.Count;
            }
        }
        
        /// <summary>
        /// Get or set the most recent copy made of this Vertex.
        /// </summary>
        public Vertex Copy
        {
            get
            {
                return copy;
            }

            set
            {
                copy = value;
            }
        }

        /// <summary>
        /// Get or set the ID this vertex was most recently saved with.
        /// </summary>
        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        /// <summary>
        /// Get or set a flag to indicate whether this Vertex has been visited in a search of the RoadNetwork.
        /// </summary>
        public bool Visited
        {
            get
            {
                return visited;
            }

            set
            {
                visited = value;
            }
        }

        /// <summary>
        /// Initialise a new Vertex
        /// </summary>
        /// <param name="network">The RoadNetwork this Vertex belongs to.</param>
        /// <param name="coordinates">The location of the Vertex.</param>
        public Vertex(RoadNetwork network, Coordinates coordinates)
        {
            this.network = network;
            this.coordinates = coordinates;
            this.edges = new List<Edge>();
        }

        /// <summary>
        /// Get an edge by index
        /// </summary>
        /// <param name="index">
        /// A integer denoting the index of the edge
        /// </param>
        /// <returns>
        /// The corresponding <see cref="Edge"/>
        /// </returns>
        public Edge GetEdge(int index)
        {
            return edges[index];
        }

        /// <summary>
        /// Add an edge.
        /// </summary>
        /// <param name="edge">The edge to add.</param>
        public void AddEdge(Edge edge)
        {
            edges.Add(edge);
        }

        /// <summary>
        /// Determine if this Vertex belongs to a certain RoadNetwork
        /// </summary>
        /// <param name="network">The RoadNetwork to test</param>
        /// <returns>True iff this vertex belongs to network</returns>
        public bool BelongsToNetwork(RoadNetwork network)
        {
            return this.network == network;
        }
    }    
}
