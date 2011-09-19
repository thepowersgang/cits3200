using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoadNetworkSolver
{
    class RoadNetwokConjugationOperator
    {
        Random random = new Random();

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

        private void Conjugate(RoadNetwork parent1, RoadNetwork parent2, out RoadNetwork child1, out RoadNetwork child2)
        {
            parent1 = parent1.Duplicate();
            parent2 = parent2.Duplicate();

            Cut(parent1);
            Cut(parent2);

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

            PartitionVertices(parent1, visitedVertices1, unvisitedVertices1);
            PartitionVertices(parent2, visitedVertices2, unvisitedVertices2);
            PartitionEdges(parent1, visitedEdges1, unvisitedEdges1, brokenEdges1);
            PartitionEdges(parent2, visitedEdges2, unvisitedEdges2, brokenEdges2);
            
            child1 = new RoadNetwork(parent1.Start.CreateCopy(), parent2.End.CreateCopy());            
            child2 = new RoadNetwork(parent2.Start.CreateCopy(), parent1.End.CreateCopy());

            CopyVertices(visitedVertices1, child1);
            CopyVertices(unvisitedVertices2, child1);
            CopyVertices(visitedVertices2, child2);
            CopyVertices(unvisitedVertices1, child1);

            ShuffleEdges(brokenEdges1);
            ShuffleEdges(brokenEdges2);

            int nPairs = Math.Max(brokenEdges1.Count, brokenEdges2.Count);

            for (int i = 0; i < nPairs; i++)
            {
                Edge edge1 = brokenEdges1[i];
                Edge edge2 = brokenEdges2[i];

                //Flip edges so that start is always in the visited set.
                if (edge1.End.Visited)
                {
                    edge1 = edge1.Reversed;
                }

                if (edge2.End.Visited)
                {
                    edge2 = edge2.Reversed;
                }

                child1.AddEdge(edge1.Start.Copy, edge2.End.Copy);
                child2.AddEdge(edge2.Start.Copy, edge1.End.Copy);
            }
        }

        private void CopyVertices(List<Vertex> source, RoadNetwork destination)
        {
            foreach (Vertex vertex in source)
            {
                destination.AddVertex(vertex.CreateCopy());
            }
        }

        private void CopyEdges(List<Edge> source, RoadNetwork destination)
        {
            foreach (Edge edge in source)
            {
                destination.AddEdge(edge.Start.Copy, edge.End.Copy);
            }
        }

        private void RepairEdges(List<Edge> brokenEdgesA, List<Edge> brokenEdgesB, RoadNetwork childA, RoadNetwork childB)
        {
            for (int i = 0; i < brokenEdgesB.Count; i++)
            {
                Edge edgeA = brokenEdgesA[i];
                Edge edgeB = brokenEdgesB[i];

                //Flip edges so that start is always in the visited set.
                if (edgeA.End.Visited)
                {
                    edgeA = edgeA.Reversed;
                }

                if (edgeB.End.Visited)
                {
                    edgeB = edgeB.Reversed;
                }

                childA.AddEdge(edgeA.Start.Copy, edgeB.End.Copy);
                childB.AddEdge(edgeB.Start.Copy, edgeA.End.Copy);
            }

            for (int i = brokenEdgesB.Count; i < brokenEdgesA.Count; i++)
            {
                Edge edgeA = brokenEdgesA[i];

                //Flip edges so that start is always in the visited set.
                if (edgeA.End.Visited)
                {
                    edgeA = edgeA.Reversed;
                }

                if (random.Next(2) == 1)
                {

                }
            }
        }
        
        private void PartitionVertices(RoadNetwork parent, List<Vertex> visited, List<Vertex> unvisited)
        {
            for (int i = 0; i < parent.VertexCount; i++)
            {
                Vertex vertex = parent.GetVertex(i);

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

        private void PartitionEdges(RoadNetwork parent, List<Edge> visited, List<Edge> unvisited, List<Edge> broken)
        {
            for (int i = 0; i < parent.EdgeCount; i++)
            {
                Edge edge = parent.GetEdge(i);

                if (edge.IsBroken)
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

        private void ShuffleEdges(List<Edge> edges)
        {
            for (int i = edges.Count-1; i > 0 ; i++)
            {
                int j = random.Next(i + 1);
                Edge temp = edges[i];
                edges[i] = edges[j];
                edges[j] = temp;
            }
        }
                
        private void Cut(RoadNetwork network)
        {
            Vertex start = network.Start;
            Vertex end = network.End;
                        
            Path path;
            while ((path = FindPath(network)) != null)
            {
                List<Edge> edges = new List<Edge>();

                while (path != null)
                {
                    edges.Add(path.lastEdge);
                    path = path.previous;
                }

                edges[random.Next(edges.Count)].IsBroken = true;
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

        private Path FindPath(RoadNetwork network)
        {
            network.ClearVisisted();
            LinkedList<Path> pathQueue = new LinkedList<Path>();

            VisitVertex(network.Start, null, pathQueue);

            while (pathQueue.Count > 0)
            {
                Path path = pathQueue.First();
                pathQueue.RemoveFirst();

                Vertex vertex = path.lastEdge.End;

                if (vertex == network.End)
                {
                    vertex.Visited = true;
                    return path;
                }

                if (!vertex.Visited)
                {
                    VisitVertex(vertex, path, pathQueue);
                }
            }

            return null;
        }

    }
}
