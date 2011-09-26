using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;

namespace RoadNetworkSolver
{
    public class RoadNetwork
    {
        private Vertex start;
        private Vertex end;
        private List<Vertex> vertices;
        private List<Edge> edges;

        public Vertex Start
        {
            get
            {
                return start;
            }
        }

        public Vertex End
        {
            get
            {
                return end;
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

        public RoadNetwork(Vertex start, Vertex end)
        {
            this.start = start;
            this.end = end;
            vertices = new List<Vertex>();
            edges = new List<Edge>();
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

        public RoadNetwork Duplicate()
        {
            RoadNetwork duplicate = new RoadNetwork(start.CreateCopy(), end.CreateCopy());

            foreach (Vertex vertex in vertices)
            {
                duplicate.vertices.Add(vertex.CreateCopy());
            }

            foreach (Edge edge in edges)
            {
                duplicate.AddEdge(edge.Start.Copy, edge.End.Copy);
            }

            return duplicate;
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

            VisitVertex(end, null, pathQueue);

            while (pathQueue.Count > 0)
            {
                Path path = pathQueue.First();
                pathQueue.RemoveFirst();

                Vertex vertex = path.lastEdge.End;

                if (vertex == start)
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

        public void CopyVertices(List<Vertex> source)
        {
            foreach (Vertex vertex in source)
            {
                vertices.Add(vertex.CreateCopy());
            }
        }

        public void CopyEdges(List<Edge> source)
        {
            foreach (Edge edge in source)
            {
                AddEdge(edge.Start.Copy, edge.End.Copy);
            }
        }
        
        public void PartitionVertices(List<Vertex> visited, List<Vertex> unvisited)
        {
            for (int i = 0; i < vertices.Count; i++)
            {
                Vertex vertex = vertices[i];

                if (vertex.Visited)
                {
                    visited.Add(vertex);
                }
                else
                {
                    unvisited.Add(vertex);
                }
            }
        }

        public void PartitionEdges(List<Edge> visited, List<Edge> unvisited, List<Edge> broken)
        {
            for (int i = 0; i < edges.Count; i++)
            {
                Edge edge = edges[i];

                if (edge.Start.Visited != edge.End.Visited)
                {
                    broken.Add(edge);
                }
                else
                {
                    if (edge.End.Visited)
                    {
                        visited.Add(edge);
                    }
                    else
                    {
                        unvisited.Add(edge);
                    }
                }
            }
        }

        public void ReadXml(XmlReader reader)
        {

        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("network");
            start.WriteXml("start", writer);
            end.WriteXml("end", writer);

            for (int i = 0; i < vertices.Count; i++)
            {
                vertices[i].WriteXml(i.ToString(), writer);
            }

            for (int i = 0; i < edges.Count; i++)
            {
                edges[i].WriteXml(writer);
            }

            writer.WriteEndElement();
        }

    }
}
