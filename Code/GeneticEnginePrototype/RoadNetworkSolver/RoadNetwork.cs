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
                return 2*edges.Count;
            }
        }

        public Vertex GetVertex(int index)
        {
            return vertices[index];
        }

        public void AddVertex(Coordinates coordinates)
        {
            vertices.Add(new Vertex(coordinates));
        }

        public Edge GetEdge(int index)
        {
            return edges[index];            
        }

        public void AddEdge(int start, int end)
        {
            Edge edge = new Edge(start, end);
            edges.Add(edge);
            vertices[start].AddEdge(edge);
            vertices[end].AddEdge(edge.Reversed);
        }
    }
}
