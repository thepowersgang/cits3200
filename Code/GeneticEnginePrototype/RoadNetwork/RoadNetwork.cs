using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace RoadNetworkDefinition
{
    /// <summary>
    /// Describes a network of roads with a start and end vertex
    /// </summary>
    public class RoadNetwork
    {
        /// <summary>
        /// The map this road network exits within.
        /// </summary>
        Map map;

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

        Dictionary<Coordinates, int> vertexIndices = new Dictionary<Coordinates, int>();

        /// <summary>
        /// Get the map this RoadNetwork exists within.
        /// </summary>
        public Map Map
        {
            get
            {
                return map;
            }
        }

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
		/// Number of edges in the road network
		/// </summary>
        public int EdgeCount
        {
            get
            {
                return edges.Count;
            }
        }

        /// <summary>
        /// Initialise an empty RoadNetwork
        /// </summary>
        /// <param name="map">The map this Roadnetwork exists within.</param>
        public RoadNetwork(Map map)
        {
            this.map = map;
        }

        /// <summary>
        /// Initialise an RoadNetwork with edges and vertices identical to another RoadNetwork
        /// </summary>
        /// <param name="map">The RoadNetwork to copy</param>
        public RoadNetwork(RoadNetwork network, bool keepEdges = true, bool keepVertices = true)
        {
            map = network.map;

            if (keepVertices)
            {
                CopyVertices(network.vertices);
            }

            if (keepEdges)
            {
                CopyEdges(network.edges);
            }
        }

        public void SetStart(int index)
        {
            Vertex temp = vertices[0];
            vertices[0] = vertices[index];
            vertices[index] = temp;
        }

        public void SetEnd(int index)
        {
            int endIndex = vertices.Count - 1;

            Vertex temp = vertices[endIndex];
            vertices[endIndex] = vertices[index];
            vertices[index] = temp;
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
		/// The added <see cref="Vertex"/>
		/// </returns>
        public Vertex AddVertex(Coordinates coordinates)
        {
            Vertex vertex = new Vertex(this, coordinates);
            vertices.Add(vertex);
            return vertex;            
        }

		/// <summary>
		/// Add a vertex to the network
		/// </summary>
		/// <param name="x">The x-coordinate of the vertex</param>
        /// <param name="y">The y-coordinate of the vertex</param>
		/// <returns>
		/// The added <see cref="Vertex"/>
		/// </returns>
        public Vertex AddVertex(int x, int y)
        {
            return AddVertex(new Coordinates(x, y));
        }

        public int AddVertexUnique(Coordinates coordinates)
        {
            if (vertexIndices.ContainsKey(coordinates))
            {
                return vertexIndices[coordinates];
            }
            else
            {
                int index = vertices.Count;
                AddVertex(coordinates);
                vertexIndices.Add(coordinates, index);
                return index;
            }
        }

        public int AddVertexUnique(int x, int y)
        {
            return AddVertexUnique(new Coordinates(x, y));
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
		/// Add an edge joining two <see cref="Vertex"/> objects
		/// </summary>
		/// <param name="start">
		/// Start <see cref="Vertex"/>
		/// </param>
		/// <param name="end">
		/// End <see cref="Vertex"/>
		/// </param>
		/// <returns>
		/// The added <see cref="Edge"/>
		/// </returns>
        public Edge AddEdge(Vertex start, Vertex end)
        {
            if (!start.BelongsToNetwork(this))
            {
                throw new Exception("Start Vertex does not belong to this RoadNetwork.");
            }

            if (!end.BelongsToNetwork(this))
            {
                throw new Exception("End Vertex does not belong to this RoadNetwork.");
            }
            
            Edge edge = new Edge(start, end);
            edges.Add(edge);
            return edge;
        }
        		
        /// <summary>
        /// Set the "visited" flag on all verticies
        /// </summary>
        /// <param name="state">
        /// The state to set the flag to.
        /// </param>
        public void SetVisited(bool state)
        {
            foreach (Vertex vertex in vertices)
            {
                vertex.Visited = state;
            }
        }
        
        /// <summary>
        /// Set the "broken" flag on all edges
        /// </summary>
        /// <param name="state">
        /// The state to set the flag to.
        /// </param>
        public void SetBroken(bool state)
        {
            foreach (Edge edge in edges)
            {
                edge.Broken = state;
            }
        }

        /// <summary>
        /// Clear the "Copy" field on all vertices
        /// </summary>        
        public void ClearCopies()
        {
            foreach (Vertex vertex in vertices)
            {
                vertex.Copy = null;
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
			/// The <see cref="Edge"/> traveled along last.
			/// </param>
			/// <param name="previous">
			/// The previous <see cref="Path"/> segment
			/// </param>
            public Path(Edge lastEdge, Path previous)
            {
                this.lastEdge = lastEdge;
                this.previous = previous;
            }
        }

		/// <summary>
		/// Explore the edges leading from a vertex
		/// </summary>
		/// <param name="vertex">
		/// The <see cref="Vertex"/> to work from
		/// </param>
		/// <param name="path">
        /// The <see cref="Path"/> segment which led to this <see cref="Vertex"/>
		/// </param>
		/// <param name="pathQueue">
		/// Queue to add the new <see cref="Path"/> objects to, to be explored later.
		/// </param>
        private void VisitVertex(Vertex vertex, Path path, LinkedList<Path> pathQueue)
        {
            vertex.Visited = true;

            for (int i = 0; i < vertex.EdgeCount; i++)
            {
                Edge edge = vertex.GetEdge(i);
                if (!edge.Broken)
                {
                    pathQueue.AddLast(new Path(edge, path));
                }
            }
        }
		
		/// <summary>
		/// Find (one of) the shortest path(s) from Start to End, ignoring edges marked as "broken".
        /// If no path exists then all vertices reachable from End are marked "visited".
		/// </summary>
		/// <returns>
		/// A <see cref="List<Edge>"/> containing the edges (in order) linking Start to End or null if no path exists.
		/// </returns>
        public List<Edge> FindPath()
        {
            SetVisited(false);
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

			// No possible path
            return null;
        }

        /// <summary>
        /// Add a copy of a vertex to the network.
        /// </summary>
        /// <param name="vertex">
        /// The vertex to copy
        /// </param>
        public void CopyVertex(Vertex vertex)
        {
            vertex.Copy = AddVertex(vertex.Coordinates);
        }

        public int CopyVertexUnique(Vertex vertex)
        {
            int index = AddVertexUnique(vertex.Coordinates);
            vertex.Copy = vertices[index];
            return index;
        }

		/// <summary>
		/// Add a copy of each vertex in a list to the network.
		/// </summary>
		/// <param name="sourceVertices">
		/// Source list of verticies
		/// </param>
        public void CopyVertices(List<Vertex> sourceVertices)
        {
            foreach (Vertex vertex in sourceVertices)
            {
                vertex.Copy = AddVertex(vertex.Coordinates);
            }
        }

        public int CopyVerticesUnique(List<Vertex> sourceVertices)
        {
            int lastIndex = -1;
            foreach (Vertex vertex in sourceVertices)
            {
                lastIndex = CopyVertexUnique(vertex);
            }
            return lastIndex;
        }

        /// <summary>
        /// Add a copy of an edge to the network.
        /// the "Copy" property of each vertex in the edge should already point to a vertex in this RoadNetwork.
        /// </summary>
        /// <param name="edge">
        /// The edge to copy
        /// </param>
        public void CopyEdge(Edge edge)
        {
            AddEdge(edge.Start.Copy, edge.End.Copy);
        }

		/// <summary>
		/// Add a copy of each edge in a list to the network.
        /// the "Copy" property of each vertex in the source should already point to a vertex in this RoadNetwork.
		/// </summary>
		/// <param name="sourceEdges">
		/// Source list of edges
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
        /// 
        /// If called after FindPath() returns null then:
        ///     endPartition will be populated with all vertices connected to the End.
        ///     startPartition will pe populated with all other vertices.
        ///         (For the purposes of the Conjucation Operator these are all connected to the Start)
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
		/// Sorts edges into three lists, depending on the "visited" state of their Start and End vertices.
        /// 
        /// When used alongside PartitionVertices:
        ///     startPartition will contain all edges which connect 2 vertices in the start partition.
        ///     endPartition will contain all edges which connect 2 vertices in the end partition.
        ///     brokenPartition will contain all edges which connect vertices from different partitions.
		/// </summary>
		/// <param name="startPartition">
		/// Will be populated with Edges that have neither vertices visited
		/// </param>
		/// <param name="endPartition">
        /// Will be populated with Edges that have both vertices visited
		/// </param>
		/// <param name="brokenPartition">
        /// Will be populated with Edges that have one vertex unvisited and one visited
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
		/// Deserealise the vertices and edges from an <see cref="XmlReader"/>
		/// </summary>
		/// <param name="reader">
		/// <see cref="XmlReader"/> containing data
		/// </param>
        public RoadNetwork(XmlReader reader)
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
                            case "map":
                                map = new Map(reader);
                                break;
                            case "vertex": 
                                string id = reader.GetAttribute("id");                                
                                verticesById.Add(id, AddVertex(new Coordinates(reader)));                                
                                break;

                            case "edge":

                                string startId = reader.GetAttribute("start");
                                string endId = reader.GetAttribute("end");

                                Vertex start = verticesById[startId];
                                Vertex end = verticesById[endId];

                                AddEdge(start, end);
                                
                                break;
                        }

                        if (!reader.IsEmptyElement)
                        {
                            depth++;
                        }

                        break;

                    case XmlNodeType.EndElement:
                        depth--;
                        break;
                }
                
            }
        }

		/// <summary>
		/// Serealise the RoadNetwork to an XmlWriter
		/// </summary>
		/// <param name="writer">
		/// Destination <see cref="XmlWriter"/>
		/// </param>
        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("network");

            map.WriteXml(writer);

            for (int i = 0; i < vertices.Count; i++)
            {
                Vertex vertex = vertices[i];
                vertex.Id = i.ToString();

                writer.WriteStartElement("vertex");
                writer.WriteAttributeString("id", vertex.Id);
                vertex.Coordinates.WriteXml(writer);
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
