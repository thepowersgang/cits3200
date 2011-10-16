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
        void Operate(IGeneration source, ArrayList destination)
        {
        }

        public RoadNetwork Mutate(RoadNetwork original)
        {
            return original;
        }

        private void RandomDepthFirstTraversal(RoadNetwork network)
        {
            LinkedList<Vertex> vertexStack = new LinkedList<Vertex>();
            network.Start.Visited = true;
            vertexStack.AddFirst(network.Start);

            while (vertexStack.Count > 0)
            {
                Vertex vertex = vertexStack.First();
                vertexStack.RemoveFirst();

                List<Vertex> nextVertices = new List<Vertex>();
                for (int i = 0; i < vertex.EdgeCount; i++)
                {
                    Vertex nextVertex = vertex.GetEdge(i).End;

                    if (!nextVertex.Visited)
                    {
                        nextVertex.Visited = true;
                        nextVertices.Add(nextVertex);
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
        }

        
    }
}
