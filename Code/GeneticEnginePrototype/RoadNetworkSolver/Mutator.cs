using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticAlgorithm.Plugin;

namespace RoadNetworkSolver
{
    class Mutator : IGeneticOperator
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

        public RoadNetwork Mutate(RoadNetwork original)
        {
            return original;
        }

        private void RandomDepthFirstTraversal(RoadNetwork source, RoadNetwork destination)
        {
            source.ClearCopies();
            source.SetVisited(false);
            source.SetBroken(true);
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
                if (nextVertices.Count > 0 || vertex==source.End || random.Next(4)>0)
                {
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

            }

        }        
    }
}
