using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoadNetworkSolver
{
    public class Vertex
    {
        private Coordinates coordinates;
        private List<Edge> edges;
        private Vertex copy;
        private string id;
        private bool visited;

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

            set
            {
                copy = value;
            }
        }

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
