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

            child1 = new RoadNetwork();
            child2 = new RoadNetwork();

            for (int i = 0; i < parent1.VertexCount; i++)
            {
                Vertex vertex = parent1.GetVertex(i).CreateCopy();

                if (vertex.Visited)
                {
                    child1.AddVertex(vertex);
                }
                else
                {
                    child2.AddVertex(vertex);
                }
            }
        }

        private void Cut(RoadNetwork network)
        {
            Edge edge = network.GetEdge(random.Next(network.EdgeCount));
            Vertex start = edge.Start;
            Vertex end = edge.End;
                        
            Path path;
            while ((path = FindPath(network, start, end)) != null)
            {
                List<Edge> edges = new List<Edge>();

                while (path != null)
                {
                    edges.Add(path.lastEdge);
                    path = path.previous;
                }

                Edge edgeToBreak = edges[random.Next(edges.Count)];

                edgeToBreak.Start = null;
                edgeToBreak.End = null;
            }

        }

        private Path FindPath(RoadNetwork network, Vertex start, Vertex end)
        {
            network.ClearVisisted();
            LinkedList<Path> pathQueue = new LinkedList<Path>();
                        
            for (int i = 0; i < start.EdgeCount; i++)
            {
                Edge edge = start.GetEdge(i);
                pathQueue.AddLast(new Path(edge, null));
            }

            while (pathQueue.Count > 0)
            {
                Path path = pathQueue.First();
                pathQueue.RemoveFirst();

                Vertex next = path.lastEdge.End;

                if (next == end)
                {
                    return path;
                }

                if (next != null && !next.Visited)
                {
                    next.Visited = true;

                    for (int i = 0; i < next.EdgeCount; i++)
                    {
                        Edge edge = next.GetEdge(i);
                        pathQueue.AddLast(new Path(edge, path));
                    }
                }
            }

            return null;
        }

    }
}
