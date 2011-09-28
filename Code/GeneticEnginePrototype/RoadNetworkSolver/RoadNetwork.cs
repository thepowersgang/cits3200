using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace RoadNetworkSolver
{
    public class RoadNetwork
    {
        private List<Vertex> vertices = new List<Vertex>();
        private List<Edge> edges = new List<Edge>();

        public Vertex Start
        {
            get
            {
                return vertices[0];
            }
        }

        public Vertex End
        {
            get
            {
                return vertices[vertices.Count-1];
            }
        }

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

        public RoadNetwork()
        {
        }

        public RoadNetwork(RoadNetwork roadNetwork)
        {
            foreach (Vertex vertex in roadNetwork.vertices)
            {
                vertices.Add(vertex.CreateCopy());
            }

            foreach (Edge edge in roadNetwork.edges)
            {
                AddEdge(edge.Start.Copy, edge.End.Copy);
            }
        }

        public Vertex GetVertex(int index)
        {
            return vertices[index];
        }

        public Vertex AddVertex(Coordinates coordinates)
        {
            Vertex vertex = new Vertex(coordinates);
            vertices.Add(vertex);
            return vertex;
        }

        public Vertex AddVertex(int x, int y)
        {
            return AddVertex(new Coordinates(x, y));
        }

        public Edge GetEdge(int index)
        {
            return edges[index];            
        }

        public Edge AddEdge(Vertex start, Vertex end)
        {
            Edge edge = new Edge(start, end);
            edges.Add(edge);
            return edge;
        }
        
        public void ClearVisisted()
        {
            foreach (Vertex vertex in vertices)
            {
                vertex.Visited = false;
            }
        }

        public void ClearBroken()
        {
            foreach (Edge edge in edges)
            {
                edge.IsBroken = false;
            }
        }
        
        private class Path
        {
            public Edge lastEdge;
            public Path previous;

            public Path(Edge lastEdge, Path previous)
            {
                this.lastEdge = lastEdge;
                this.previous = previous;
            }
        }

        private void VisitVertex(Vertex vertex, Path path, LinkedList<Path> pathQueue)
        {
            vertex.Visited = true;

            for (int i = 0; i < vertex.EdgeCount; i++)
            {
                Edge edge = vertex.GetEdge(i);
                if (!edge.IsBroken)
                {
                    pathQueue.AddLast(new Path(edge, path));
                }
            }
        }
                
        public List<Edge> FindPath()
        {
            ClearVisisted();
            LinkedList<Path> pathQueue = new LinkedList<Path>();

            VisitVertex(End, null, pathQueue);

            while (pathQueue.Count > 0)
            {
                Path path = pathQueue.First();
                pathQueue.RemoveFirst();

                Vertex vertex = path.lastEdge.End;

                if (vertex == Start)
                {
                    List<Edge> edges = new List<Edge>();

                    while (path != null)
                    {
                        edges.Add(path.lastEdge);
                        path = path.previous;
                    }

                    return edges;
                }

                if (!vertex.Visited)
                {
                    VisitVertex(vertex, path, pathQueue);
                }
            }

            return null;
        }

        public void CopyVertices(List<Vertex> sourceVertices)
        {
            foreach (Vertex vertex in sourceVertices)
            {
                vertices.Add(vertex.CreateCopy());
            }
        }

        public void CopyEdges(List<Edge> sourceEdges)
        {
            foreach (Edge edge in sourceEdges)
            {
                AddEdge(edge.Start.Copy, edge.End.Copy);
            }
        }
        
        public void PartitionVertices(List<Vertex> startPartition, List<Vertex> endPartition)
        {
            foreach (Vertex vertex in vertices)
            {
                if (vertex.Visited)
                {
                    endPartition.Add(vertex);
                }
                else
                {
                    startPartition.Add(vertex);
                }
            }
        }

        public void PartitionEdges(List<Edge> startPartition, List<Edge> endPartition, List<Edge> brokenPartition)
        {
            foreach (Edge edge in edges)
            {
                if (edge.Start.Visited != edge.End.Visited)
                {
                    brokenPartition.Add(edge);
                }
                else
                {
                    if (edge.End.Visited)
                    {
                        endPartition.Add(edge);
                    }
                    else
                    {
                        startPartition.Add(edge);
                    }
                }
            }
        }

        public void ReadXml(XmlReader reader)
        {
            Dictionary<string, Vertex> verticesById = new Dictionary<string, Vertex>();

            int depth = 1;

            while (depth>0 && reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        switch (reader.Name)
                        {
                            case ("vertex"): 

                                string id = reader.GetAttribute("id");
                                string xString = reader.GetAttribute("x");
                                string yString = reader.GetAttribute("y");

                                int x;
                                int y;

                                int.TryParse(xString, out x);
                                int.TryParse(yString, out y);

                                verticesById.Add(id, AddVertex(x, y));

                                if (!reader.IsEmptyElement)
                                {
                                    depth++;
                                }

                                break;

                            case ("edge"):

                                string startId = reader.GetAttribute("start");
                                string endId = reader.GetAttribute("end");

                                Vertex start = verticesById[startId];
                                Vertex end = verticesById[endId];

                                AddEdge(start, end);

                                if (!reader.IsEmptyElement)
                                {
                                    depth++;
                                }

                                break;
                        }
                        break;

                    case XmlNodeType.EndElement:
                        depth--;
                        break;
                }
                
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("network");
            
            for (int i = 0; i < vertices.Count; i++)
            {
                Vertex vertex = vertices[i];
                vertex.Id = i.ToString();

                writer.WriteStartElement("vertex");
                writer.WriteAttributeString("id", vertex.Id);
                writer.WriteAttributeString("x", vertex.Coordinates.X.ToString());
                writer.WriteAttributeString("y", vertex.Coordinates.Y.ToString());
                writer.WriteEndElement();
            }

            foreach (Edge edge in edges)
            {
                writer.WriteStartElement("edge");
                writer.WriteAttributeString("start", edge.Start.Id.ToString());
                writer.WriteAttributeString("end", edge.End.Id.ToString());
                writer.WriteEndElement();
            }

            writer.WriteEndElement();
        }

    }
}
