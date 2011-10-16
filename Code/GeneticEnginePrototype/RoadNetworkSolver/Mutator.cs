using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticAlgorithm.Plugin;

namespace RoadNetworkSolver
{
    public class Mutator : IGeneticOperator
    {
        Random random = new Random();

        /// <summary>
        /// Process a generation to produce the individuals which will make up the next generation
        /// </summary>
        /// <param name="source">The current generation</param>
        /// <param name="destination">An empty collection of individuals to be populated</param>
        public void Operate(IGeneration source, ArrayList destination)
        {
                        
        }

        public RoadNetwork Mutate(RoadNetwork source)
        {
            Map map = source.Map;
            int mapWidth = map.Width;
            int mapHeight = map.Height;
                        
            source.ClearCopies();
            source.SetVisited(false);
            source.SetBroken(true);

            RoadNetwork destination = new RoadNetwork(map);
            int endVertexIndex = -1;

            LinkedList<Vertex> vertexStack = new LinkedList<Vertex>();
            source.Start.Visited = true;
            vertexStack.AddFirst(source.Start);

            while (vertexStack.Count > 0)
            {
                Vertex vertex = vertexStack.First();
                vertexStack.RemoveFirst();

                List<Vertex> nextVertices = new List<Vertex>();
                for (int i = 0; i < vertex.EdgeCount; i++)
                {
                    Edge edge = vertex.GetEdge(i);
                    Vertex nextVertex = edge.End;

                    if (!nextVertex.Visited)
                    {
                        edge.Broken = false;
                        nextVertex.Visited = true;
                        nextVertices.Add(nextVertex);
                    }                    
                }

                //If this vertex is a leaf of the tree and not the end vertex it has a 25% chance of
                //begin removed.
                if (nextVertices.Count > 0 || vertex == source.End || random.Next(4) > 0)
                {
                    if (vertex == source.End)
                    {
                        endVertexIndex = destination.VertexCount;
                    }

                    destination.CopyVertex(vertex);
                }

                //Shuffle Vertices
                for (int n = nextVertices.Count - 1; n > 0; n--)
                {
                    int m = random.Next(n + 1);
                    Vertex temp = nextVertices[m];
                    nextVertices[m] = nextVertices[n];
                    nextVertices[n] = temp;
                }

                //Push vertices onto stack in shiffled order.
                foreach (Vertex nextVertex in nextVertices)
                {
                    vertexStack.AddFirst(nextVertex);
                } 
            }

            for (int i = 0; i < source.EdgeCount; i++)
            {
                Edge edge = source.GetEdge(i);

                if ((!edge.Broken || random.Next(4) > 0) && edge.Start.Copy != null && edge.End.Copy != null)
                {
                    destination.CopyEdge(edge);
                }
            }

            int verticesToAdd = random.Next(10);

            for (int i = 0; i < verticesToAdd; i++)
            {
                Vertex startVertex = destination.GetVertex(random.Next(source.VertexCount));
                Vertex endVertex = destination.AddVertex(random.Next(mapWidth), random.Next(mapHeight));
                destination.AddEdge(startVertex, endVertex);
            }

            int edgesToAdd = random.Next(destination.VertexCount);

            for (int i = 0; i < edgesToAdd; i++)
            {
                Vertex startVertex = destination.GetVertex(random.Next(source.VertexCount));
                Vertex endVertex = destination.GetVertex(random.Next(source.VertexCount));
                destination.AddEdge(startVertex, endVertex);
            }

            if (endVertexIndex == -1)
            {
                throw new Exception("End vertex not found.");
            }
            else
            {
                destination.SetEnd(endVertexIndex);
            }

            return destination;
        }        
    }
}
