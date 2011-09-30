using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace RoadNetworkSolver
{
    /// <summary>
    /// Describes a network of roads with a start and end vertex
    /// </summary>
    public class RoadNetwork
    {
        /// <summary>
        /// The list of vertices
        /// The first vertex is considered the start vertex
        /// The last vertex is considered the end vertex
        /// </summary>
        private List<Vertex> vertices = new List<Vertex>();

        /// <summary>
        /// The list of edges
        /// </summary>
        private List<Edge> edges = new List<Edge>();

		/// <summary>
		/// Initial (start) vertex for the RoadNetwork
		/// </summary>
        public Vertex Start
        {
            get
            {
                return vertices[0];
            }
        }

		/// <summary>
		/// Final (destination) vertex for the RoadNetwork
		/// </summary>
        public Vertex End
        {
            get
            {
                return vertices[vertices.Count-1];
            }
        }

		/// <summary>
		/// Number of vertices in the network
		/// </summary>
        public int VertexCount
        {
            get
            {
                return vertices.Count;
            }
        }

		/// <summary>
		/// Number of edges (links) in the road network
		/// </summary>
        public int EdgeCount
        {
            get
            {
                return edges.Count;
            }
        }

		/// <summary>
		/// Create a blank RoadNetwork
		/// </summary>
        public RoadNetwork()
        {
        }

		/// <summary>
		/// Create a copy of another RoadNetwork
		/// </summary>
		/// <param name="roadNetwork">
		/// A <see cref="RoadNetwork"/> to duplicate
		/// </param>
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

		/// <summary>
		/// Fetch a vertex by its index
		/// </summary>
		/// <param name="index">
		/// A integer denoting the index of the vertex
		/// </param>
		/// <returns>
		/// The corresponding <see cref="Vertex"/>
		/// </returns>
        public Vertex GetVertex(int index)
        {
            return vertices[index];
        }

		/// <summary>
		/// Add a vertex to the network
		/// </summary>
		/// <param name="coordinates">
		/// A <see cref="Coordinates"/> object denoting the position
		/// </param>
		/// <returns>
		/// Added <see cref="Vertex"/>
		/// </returns>
        public Vertex AddVertex(Coordinates coordinates)
        {
            Vertex vertex = new Vertex(coordinates);
            vertices.Add(vertex);
            return vertex;
        }

		/// <summary>
		/// Add a vertex by an (x,y) pair
		/// </summary>
		/// <param name="x">X Coordinate</param>
		/// <param name="y">Y Coordinate</param>
		/// <returns>
		/// Added <see cref="Vertex"/>
		/// </returns>
        public Vertex AddVertex(int x, int y)
        {
            return AddVertex(new Coordinates(x, y));
        }

		/// <summary>
		/// Get an edge by index
		/// </summary>
		/// <param name="index">
		/// <see cref="System.Int32"/> index to the edge
		/// </param>
		/// <returns>
		/// Corresponding <see cref="Edge"/>
		/// </returns>
        public Edge GetEdge(int index)
        {
            return edges[index];            
        }

		/// <summary>
		/// Create an edge between two <see cref="Vertex"/> objects
		/// </summary>
		/// <param name="start">
		/// Start <see cref="Vertex"/>
		/// </param>
		/// <param name="end">
		/// End <see cref="Vertex"/>
		/// </param>
		/// <returns>
		/// Created <see cref="Edge"/>
		/// </returns>
        public Edge AddEdge(Vertex start, Vertex end)
        {
            Edge edge = new Edge(start, end);
            edges.Add(edge);
            return edge;
        }

		/// <summary>
		/// Clear the visited flag on all verticies
		/// </summary>
        public void ClearVisited()
        {
            foreach (Vertex vertex in vertices)
            {
                vertex.Visited = false;
            }
        }

		/// <summary>
		/// Clear the "broken" flag on all edges
		/// </summary>
        public void ClearBroken()
        {
            foreach (Edge edge in edges)
            {
                edge.IsBroken = false;
            }
        }
        
		/// <summary>
		/// Represents a segement of a path
		/// </summary>
        private class Path
        {
			/// <summary>
			/// Edge most recently traveled
			/// </summary>
            public Edge lastEdge;
			/// <summary>
			/// Previous path segment
			/// </summary>
            public Path previous;

			/// <summary>
			/// Create a new path segment from the end of <see cref="previous" /> along
			/// <see cref="lastEdge" />
			/// </summary>
			/// <param name="lastEdge">
			/// <see cref="Edge"/> traveled
			/// </param>
			/// <param name="previous">
			/// Previous <see cref="Path"/> segment
			/// </param>
            public Path(Edge lastEdge, Path previous)
            {
                this.lastEdge = lastEdge;
                this.previous = previous;
            }
        }

		/// <summary>
		/// Populate pathQueue with all possible paths from vertex
		/// </summary>
		/// <param name="vertex">
		/// <see cref="Vertex"/> to work from
		/// </param>
		/// <param name="path">
		/// Previous <see cref="Path"/> segment
		/// </param>
		/// <param name="pathQueue">
		/// Destination list of <see cref="Path"/> objects
		/// </param>
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
		
		/// <summary>
		/// 
		/// </summary>
		/// <returns>
		/// A <see cref="List<Edge>"/>
		/// </returns>
        public List<Edge> FindPath()
        {
            ClearVisited();
            LinkedList<Path> pathQueue = new LinkedList<Path>();
			
			// Initialise from the end of the path
            VisitVertex(End, null, pathQueue);

			// While there are remaining paths to follow
            while (pathQueue.Count > 0)
            {
				// Try a path
                Path path = pathQueue.First();
                pathQueue.RemoveFirst();

                Vertex vertex = path.lastEdge.End;

				// If we've reached the start, we're done
                if (vertex == Start)
                {
                    List<Edge> edges = new List<Edge>();

                    while (path != null)
                    {
                        edges.Add(path.lastEdge);
                        path = path.previous;
                    }

					// Return the path taken
                    return edges;
                }

				// If this vertex hasn't been processed yet, add it to the path list
                if (!vertex.Visited)
                {
                    VisitVertex(vertex, path, pathQueue);
                }
            }

			// No possible path?!
            return null;
        }

		/// <summary>
		/// Get a copy of all verticies in the network
		/// </summary>
		/// <param name="sourceVertices">
		/// Destination list for the verticies
		/// </param>
        public void CopyVertices(List<Vertex> sourceVertices)
        {
            foreach (Vertex vertex in sourceVertices)
            {
                vertices.Add(vertex.CreateCopy());
            }
        }

		/// <summary>
		/// Get a copy of all of the edges in the network
		/// </summary>
		/// <param name="sourceEdges">
		/// Destination list for the edges
		/// </param>
        public void CopyEdges(List<Edge> sourceEdges)
        {
            foreach (Edge edge in sourceEdges)
            {
                AddEdge(edge.Start.Copy, edge.End.Copy);
            }
        }
        
		/// <summary>
		/// Sorts all verticies into two lists, depending on if they have been visited or not
		/// </summary>
		/// <param name="startPartition">
		/// Verticies that have not been visited
		/// </param>
		/// <param name="endPartition">
		/// Verticies that have been visited
		/// </param>
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

		/// <summary>
		/// Sorts edges into three lists, depending on if they have been taken
		/// </summary>
		/// <param name="startPartition">
		/// Edges that have not been fully visited (start and end haven't)
		/// </param>
		/// <param name="endPartition">
		/// Edges that have been visited (both start and end have been visited)
		/// </param>
		/// <param name="brokenPartition">
		/// Edges that either the start or end have been visited, but not both
		/// </param>
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

		/// <summary>
		/// Populate the class from an <see cref="XmlReader"/> object
		/// </summary>
		/// <param name="reader">
		/// <see cref="XmlReader"/> containing data
		/// </param>
        public void ReadXml(XmlReader reader)
        {
            Dictionary<string, Vertex> verticesById = new Dictionary<string, Vertex>();

            int depth = 1;

            while (depth > 0 && reader.Read())
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

		/// <summary>
		/// Save the RoadNetwork to an XmlWriter object
		/// </summary>
		/// <param name="writer">
		/// Destination <see cref="XmlWriter"/>
		/// </param>
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
