using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticAlgorithm.Plugin;
using RoadNetworkDefinition;

namespace RoadNetworkSolver
{
    public class Mutator : IGeneticOperator
    {
        private Random random = new Random();

        public Mutator(object config)
        {
        }

        /// <summary>
        /// Process a generation to produce the individuals which will make up the next generation
        /// </summary>
        /// <param name="source">The current generation</param>
        /// <param name="destination">An empty collection of individuals to be populated</param>
        public void Operate(IGeneration source, ArrayList destination)
        {            
            int i = 0;
            while (destination.Count < source.Count)
            {
                int j = 0;
                while (destination.Count < source.Count && j <= i)
                {
                    destination.Add(Mutate((RoadNetwork)source[j].Individual));
                    j++;
                }

                i++;
            }
        }

        public RoadNetwork Mutate(RoadNetwork source)
        {
            Map map = source.Map;
            int mapWidth = map.Width;
            int mapHeight = map.Height;

            int maxXChange = 1 + mapWidth / 10;
            int maxYChange = 1 + mapHeight / 10;

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

                //Console.WriteLine(vertex.Coordinates.X + ", " + vertex.Coordinates.Y + ": " + (vertex==source.End));

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

                    if (vertex == source.Start || vertex == source.End || random.Next(10) > 0)
                    {
                        destination.CopyVertex(vertex);
                    }
                    else
                    {
                        int x = vertex.Coordinates.X + random.Next(2 * maxXChange+1) - maxXChange;
                        int y = vertex.Coordinates.Y + random.Next(2 * maxYChange + 1) - maxYChange;

                        x = Math.Max(0, Math.Min(x, map.Width - 1));
                        y = Math.Max(0, Math.Min(y, map.Height - 1));

                        vertex.Copy = destination.AddVertex(x, y);
                    }
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
                Vertex startVertex = destination.GetVertex(random.Next(destination.VertexCount));
                Vertex endVertex = destination.AddVertex(random.Next(mapWidth), random.Next(mapHeight));
                destination.AddEdge(startVertex, endVertex);
            }

            int edgesToAdd = random.Next(destination.VertexCount/2);

            for (int i = 0; i < edgesToAdd; i++)
            {
                Vertex startVertex = destination.GetVertex(random.Next(destination.VertexCount));
                Vertex endVertex = destination.GetVertex(random.Next(destination.VertexCount));
                if (startVertex != endVertex)
                {
                    destination.AddEdge(startVertex, endVertex);
                }
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

            //StepMutator sm = new StepMutator();

            //return sm.MakeSteps(destination);
        }        
    }
}
