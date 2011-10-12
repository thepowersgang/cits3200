using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticAlgorithm.Plugin;

namespace RoadNetworkSolver
{
    public class ConjugationOperator : IGeneticOperator
    {
        Random random = new Random();

        public ConjugationOperator(object config)
        {
        }

        public void Operate(IGeneration source, ArrayList destination)
        {
            int n = source.Count;

            
        }

        private void Conjugate(RoadNetwork parent1, RoadNetwork parent2, out RoadNetwork child1, out RoadNetwork child2)
        {   
            Cut(parent1);
            Cut(parent2);
                     
            List<Vertex> startVertexPartition1 = new List<Vertex>();
            List<Vertex> endVertexPartition1 = new List<Vertex>();
            List<Vertex> startVertexPartition2 = new List<Vertex>();
            List<Vertex> endVertexPartition2 = new List<Vertex>();

            List<Edge> startEdgePartition1 = new List<Edge>();
            List<Edge> endEdgePartition1 = new List<Edge>();
            List<Edge> startEdgePartition2 = new List<Edge>();
            List<Edge> endEdgePartition2 = new List<Edge>();
            List<Edge> brokenEdgePartition1 = new List<Edge>();
            List<Edge> brokenEdgePartition2 = new List<Edge>();

            parent1.PartitionVertices(startVertexPartition1, endVertexPartition1);
            parent2.PartitionVertices(startVertexPartition2, endVertexPartition2);
            
            parent1.PartitionEdges(startEdgePartition1, endEdgePartition1, brokenEdgePartition1);
            parent2.PartitionEdges(startEdgePartition2, endEdgePartition2, brokenEdgePartition2);

            child1 = new RoadNetwork(parent1.Map);
            child2 = new RoadNetwork(parent2.Map);

            child1.CopyVertices(startVertexPartition1);
            child1.CopyEdges(startEdgePartition1);
            
            child1.CopyVertices(endVertexPartition2);
            child1.CopyEdges(endEdgePartition2);
                        
            child2.CopyVertices(startVertexPartition2);
            child2.CopyEdges(startEdgePartition2);
            
            child2.CopyVertices(endVertexPartition1);
            child2.CopyEdges(endEdgePartition1);
            
            ShuffleEdges(brokenEdgePartition1);
            ShuffleEdges(brokenEdgePartition2);

            int nEdges = Math.Max(brokenEdgePartition1.Count, brokenEdgePartition2.Count);

            for (int i = 0; i < nEdges; i++)
            {
                Vertex start1;
                Vertex end1;
                Vertex start2;
                Vertex end2;

                GetStartAndEnd(brokenEdgePartition1, i, startVertexPartition1, endVertexPartition1, out start1, out end1);
                GetStartAndEnd(brokenEdgePartition2, i, startVertexPartition2, endVertexPartition2, out start2, out end2);

                if (start1 != null && start2 != null)
                {
                    child2.AddEdge(start1.Copy, end2.Copy);
                    child1.AddEdge(start2.Copy, end1.Copy);
                }
            }
        }

        private void GetStartAndEnd(List<Edge> brokenEdges, int index, List<Vertex> visitedVertices, List<Vertex> unvisitedVertices, out Vertex start, out Vertex end)
        {
            if (index < brokenEdges.Count)
            {
                Edge edge = brokenEdges[index];

                if (edge.End.Visited)
                {
                    edge = edge.Reversed;
                }

                start = edge.Start;
                end = edge.End;
            }
            else
            {
                if (random.Next(2) == 0)
                {
                    start = visitedVertices[random.Next(visitedVertices.Count)];
                    end = unvisitedVertices[random.Next(unvisitedVertices.Count)];
                }
                else
                {
                    start = null;
                    end = null;
                }
            }
        }

        private void ShuffleEdges(List<Edge> edges)
        {
            for (int i = edges.Count-1; i > 0 ; i--)
            {
                int j = random.Next(i + 1);
                Edge temp = edges[i];
                edges[i] = edges[j];
                edges[j] = temp;
            }
        }
                
        public void Cut(RoadNetwork network)
        {
            network.ClearBroken();

            List<Edge> edges;
            while ((edges = network.FindPath()) != null)
            {
                edges[random.Next(edges.Count)].IsBroken = true;
            }
        }
        
    }
}
