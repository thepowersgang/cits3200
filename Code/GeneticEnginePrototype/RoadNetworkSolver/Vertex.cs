using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoadNetworkSolver
{
    class Vertex
    {
        private Coordinates coordinates;
        private List<Edge> edges;
        private Vertex copy;
        private bool visited;

        public int EdgeCount
        {
            get
            {
                return edges.Count;
            }
        }

        public Edge GetEdge(int index)
        {
            return edges[index];
        }

        public void AddEdge(Edge edge)
        {
            edges.Add(edge);
        }

        public Vertex Copy
        {
            get
            {
                return copy;
            }
        }

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

        public Vertex(Coordinates coordinates)
        {
            this.coordinates = coordinates;
            this.edges = new List<Edge>();
        }

        public Vertex CreateCopy() {
            copy = new Vertex(coordinates);
            return copy;
        }
    }    
}
