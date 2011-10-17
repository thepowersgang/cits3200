using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RoadNetworkDefinition;
using GeneticAlgorithm.Plugin;

namespace RoadNetworkSolver
{
    public class StepMutator : IGeneticOperator
    {
        private static Random random = new Random();

        public StepMutator(object config)
        {
        }

        //public RoadNetwork MakeSteps(RoadNetwork network)
        //{
        //    RoadNetwork result = new RoadNetwork(network, false, true);
        //    int endVertexIndex = result.VertexCount-1;

        //    for (int i = 0; i < network.EdgeCount; i++)
        //    {
        //        Edge edge = network.GetEdge(i);
        //        Vertex start = edge.Start.Copy;
        //        Vertex end = edge.End.Copy;
        //        Coordinates startCoordinates = start.Coordinates;
        //        Coordinates endCoordinates = end.Coordinates;

        //        int dX = endCoordinates.X - startCoordinates.X;
        //        int dY = endCoordinates.Y - startCoordinates.Y;

        //        int xStep = 1;
        //        if (dX < 0)
        //        {
        //            dX = -dX;
        //            xStep = -1;
        //        }

        //        int yStep = 1;
        //        if (dY < 0)
        //        {
        //            dY = -dY;
        //            yStep = -1;
        //        }

        //        int x=startCoordinates.X;
        //        int y=startCoordinates.Y;

        //        Vertex startPoint = start;

        //        while (dX > 1 || dY > 1)
        //        {
        //            if (dX > dY)
        //            {
        //                x += xStep;
        //                dX--;
        //            }
        //            else if (dY > dX)
        //            {
        //                y += yStep;
        //                dY--;
        //            }
        //            else
        //            {
        //                x += xStep;
        //                dX--;
        //                y += yStep;
        //                dY--;
        //            }
                                        
        //            Vertex endPoint = result.AddVertex(x, y);
        //            result.AddEdge(startPoint, endPoint);
        //            startPoint = endPoint;
        //        }

        //        result.AddEdge(startPoint, end);
        //    }

        //    result.SetEnd(endVertexIndex);

        //    return result;
        //}

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

        public static RoadNetwork Mutate(RoadNetwork source)
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

                    if (vertex != source.End || random.Next(4) > 0)
                    {
                        destination.CopyVertex(vertex);
                    }
                    else
                    {
                        vertex.Copy = destination.AddVertex(RandomMove(vertex.Coordinates));
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
                    StepPath(destination, edge.Start.Copy, edge.End.Copy);                    
                }
            }

            int newVertices = random.Next(10);
            for (int i = 0; i < newVertices; i++)
            {
                Vertex startVertex = destination.GetVertex(random.Next(destination.VertexCount));
                Vertex endVertex = destination.AddVertex(RandomMove(startVertex.Coordinates));
                destination.AddEdge(startVertex, endVertex);
            }

            int newCycles = random.Next(3);
            for (int i = 0; i < newCycles; i++)
            {
                Vertex startVertex = destination.GetVertex(random.Next(destination.VertexCount));
                Vertex endVertex = destination.GetVertex(random.Next(destination.VertexCount));
                StepPath(destination, startVertex, endVertex);
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

        private static Coordinates RandomMove(Coordinates coordinates)
        {
            int x = coordinates.X;
            int y = coordinates.Y;

            switch (random.Next(8))
            {
                case 0:
                    x++;
                    break;
                case 1:
                    x++;
                    y++;
                    break;
                case 2:
                    y++;
                    break;
                case 3:
                    x--;
                    y++;
                    break;
                case 4:
                    x--;
                    break;
                case 5:
                    x--;
                    y--;
                    break;
                case 6:
                    y--;
                    break;
                case 7:
                    x++;
                    y--;
                    break;
            }

            return new Coordinates(x, y);
        }

        public static void StepPath(RoadNetwork network, Vertex start, Vertex end)
        {
            int dX = end.Coordinates.X - start.Coordinates.X;
            int dY = end.Coordinates.Y - start.Coordinates.Y;

            int xStep = 1;
            if (dX < 0)
            {
                dX = -dX;
                xStep = -1;
            }

            int yStep = 1;
            if (dY < 0)
            {
                dY = -dY;
                yStep = -1;
            }
            
            int x = start.Coordinates.X;
            int y = start.Coordinates.Y;

            Vertex startVertex = start;
            
            while (dX > 1 || dY > 1)
            {
                if (dX > dY)
                {
                    x += xStep;
                    dX--;
                }
                else if (dY > dX)
                {
                    y += yStep;
                    dY--;
                }
                else
                {
                    x += xStep;
                    dX--;
                    y += yStep;
                    dY--;
                }

                Vertex endVertex = network.AddVertex(x, y);
                network.AddEdge(startVertex, endVertex);
                startVertex = endVertex;
            }

            network.AddEdge(startVertex, end);
        }
    }
}
