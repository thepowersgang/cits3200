using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

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
        }

        public string Id
        {
            get
            {
                return id;
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

        public void WriteXml(string id, XmlWriter writer)
        {
            this.id = id;
            writer.WriteStartElement("vertex");
            writer.WriteAttributeString("id", id.ToString());
            writer.WriteAttributeString("x", coordinates.X.ToString());
            writer.WriteAttributeString("y", coordinates.Y.ToString());
            writer.WriteEndElement();
        }
    }    
}
