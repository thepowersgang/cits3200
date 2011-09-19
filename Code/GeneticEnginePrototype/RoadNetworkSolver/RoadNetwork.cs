using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoadNetworkSolver
{
    class RoadNetwork
    {
        private List<Vertex> vertices;
        private List<Edge> edges;
        private List<Edge> brokenEdges;

        public int VertexCount
        {
            get
            {
                return vertices.Count;
            }
        }

        public int EdgeCount
        {
            get
            {
                return edges.Count;
            }
        }

        public Vertex GetVertex(int index)
        {
            return vertices[index];
        }

        public void AddVertex(Vertex vertex)
        {
            vertices.Add(vertex);
        }

        public Edge GetEdge(int index)
        {
            return edges[index];            
        }

        public void AddEdge(Vertex start, Vertex end)
        {
            Edge edge = new Edge(start, end);
            edges.Add(edge);
        }

        public void ClearVisisted()
        {
            foreach (Vertex vertex in vertices)
            {
                vertex.Visited = false;
            }
        }

        public RoadNetwork Duplicate()
        {
            RoadNetwork duplicate = new RoadNetwork();

            foreach (Vertex vertex in vertices)
            {
                duplicate.vertices.Add(vertex.CreateCopy());
            }

            foreach (Edge edge in edges)
            {
                duplicate.edges.Add(edge.CreateCopy());
            }

            return duplicate;
        }
    }
}
