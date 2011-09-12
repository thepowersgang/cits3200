using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoadNetworkSolver
{
    class RoadNetwokConjugationOperator
    {
        Random random = new Random();

        private class BackwardPath
        {
            public Edge lastEdge;
            public BackwardPath previous;

            public BackwardPath(Edge lastEdge, BackwardPath previous)
            {
                this.lastEdge = lastEdge;
                this.previous = previous;
            }
        }

        private void Cut(RoadNetwork network)
        {
            int nVertices = network.VertexCount;            
            
            Edge edge = network.GetEdge(random.Next(network.EdgeCount));
            int start = edge.Start;
            int end = edge.End;

            edge.Start = -1;
            edge.End = -1;

            bool[] connected;
            BackwardPath path;
            while ((path = FindPath(network, start, end, out connected)) != null)
            {
                List<Edge> edges = new List<Edge>();

                while (path != null)
                {
                    edges.Add(path.lastEdge);
                    path = path.previous;
                }

                Edge edgeToBreak = edges[random.Next(edges.Count)];

                edgeToBreak.Start = -1;
                edgeToBreak.End = -1;
            }

        }

        private BackwardPath FindPath(RoadNetwork network, int start, int end, out bool[] connected)
        {
            connected = new bool[network.VertexCount];
            connected[start] = true;

            LinkedList<BackwardPath> pathQueue = new LinkedList<BackwardPath>();

            Vertex startVertex = network.GetVertex(start);

            int nEdges = startVertex.EdgeCount;

            for(int i=0;i<nEdges;i++)
            {
                Edge edge = startVertex.GetEdge(i);
                pathQueue.AddLast(new BackwardPath(edge, null));
            }

            while (pathQueue.Count > 0)
            {
                BackwardPath path = pathQueue.First();
                pathQueue.RemoveFirst();

                int index = path.lastEdge.End;

                if (index == end)
                {
                    return path;
                }

                if (index >= 0 && !connected[index])
                {
                    connected[index] = true;

                    Vertex vertex = network.GetVertex(path.lastEdge.End);

                    nEdges = vertex.EdgeCount;

                    for (int i = 0; i < nEdges; i++)
                    {
                        Edge edge = startVertex.GetEdge(i);
                        pathQueue.AddLast(new BackwardPath(edge, path));
                    }

                }
            }

            return null;
        }

    }
}
