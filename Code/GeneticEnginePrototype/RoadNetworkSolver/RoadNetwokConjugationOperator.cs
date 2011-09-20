using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoadNetworkSolver
{
    public class RoadNetwokConjugationOperator
    {
        Random random = new Random();
                
        private void Conjugate(RoadNetwork parent1, RoadNetwork parent2, out RoadNetwork child1, out RoadNetwork child2)
        {
            parent1 = parent1.Duplicate();
            parent2 = parent2.Duplicate();

            Cut(parent1);
            Cut(parent2);

            Recombine(parent1, parent2, out child1, out child2);
        }

        public void Recombine(RoadNetwork parent1, RoadNetwork parent2, out RoadNetwork child1, out RoadNetwork child2)
        {
            List<Vertex> visitedVertices1 = new List<Vertex>();
            List<Vertex> unvisitedVertices1 = new List<Vertex>();
            List<Vertex> visitedVertices2 = new List<Vertex>();
            List<Vertex> unvisitedVertices2 = new List<Vertex>();

            List<Edge> visitedEdges1 = new List<Edge>();
            List<Edge> unvisitedEdges1 = new List<Edge>();
            List<Edge> visitedEdges2 = new List<Edge>();
            List<Edge> unvisitedEdges2 = new List<Edge>();
            List<Edge> brokenEdges1 = new List<Edge>();
            List<Edge> brokenEdges2 = new List<Edge>();

            parent1.PartitionVertices(visitedVertices1, unvisitedVertices1);
            parent2.PartitionVertices(visitedVertices2, unvisitedVertices2);
            
            parent1.PartitionEdges(visitedEdges1, unvisitedEdges1, brokenEdges1);
            parent2.PartitionEdges(visitedEdges2, unvisitedEdges2, brokenEdges2);

            child1 = new RoadNetwork(parent1.Start.CreateCopy(), parent2.End.CreateCopy());
            child2 = new RoadNetwork(parent2.Start.CreateCopy(), parent1.End.CreateCopy());

            child1.CopyVertices(unvisitedVertices1);
            child1.CopyEdges(unvisitedEdges1);
            
            child1.CopyVertices(visitedVertices2);
            child1.CopyEdges(visitedEdges2);
                        
            child2.CopyVertices(unvisitedVertices2);
            child2.CopyEdges(unvisitedEdges2);
            
            child2.CopyVertices(visitedVertices1);
            child2.CopyEdges(visitedEdges1);
                        
            unvisitedVertices1.Add(parent1.Start);
            visitedVertices1.Add(parent1.End);
            unvisitedVertices2.Add(parent2.Start);
            visitedVertices2.Add(parent2.End);

            ShuffleEdges(brokenEdges1);
            ShuffleEdges(brokenEdges2);

            int nEdges = Math.Max(brokenEdges1.Count, brokenEdges2.Count);

            for (int i = 0; i < nEdges; i++)
            {
                Vertex start1;
                Vertex end1;
                Vertex start2;
                Vertex end2;

                GetStartAndEnd(brokenEdges1, i, visitedVertices1, unvisitedVertices1, out start1, out end1);
                GetStartAndEnd(brokenEdges2, i, visitedVertices2, unvisitedVertices2, out start2, out end2);

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
            List<Edge> edges;
            while ((edges = network.FindPath()) != null)
            {
                edges[random.Next(edges.Count)].IsBroken = true;
            }
        }
        
    }
}
