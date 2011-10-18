using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticAlgorithm.Plugin;
using RoadNetworkDefinition;

namespace RoadNetworkSolver
{
    public class StepCyclePopulator : IPopulator
    {
        Random random = new Random();

        int populationSize = 200;
        Map map;
        
        public StepCyclePopulator(object config)
        {
            map = Map.FromFile((string)config);
        }

        /// <summary>
        /// Generate an initial population of individuals
        /// </summary>
        /// <param name="individuals">An empty list to populate with the individuals</param>
        public void Populate(ArrayList individuals)
        {
            while (individuals.Count < populationSize)
            {
                RoadNetwork network = new RoadNetwork(map);

                int startIndex = network.AddVertexUnique(map.Start);
                int endIndex = network.AddVertexUnique(map.End);
                Vertex start = network.GetVertex(startIndex);
                Vertex end = network.GetVertex(endIndex);

                int nPaths = random.Next(10);
                for (int i = 0; i < nPaths; i++)
                {
                    int midpointIndex = network.AddVertexUnique(random.Next(map.Width), random.Next(map.Height));
                    Vertex midpoint = network.GetVertex(midpointIndex);
                    StepMutator.StepPath(network, start, midpoint);
                    StepMutator.StepPath(network, midpoint, end);
                }

                int nCross = random.Next(10);
                for (int i = 0; i < nCross; i++)
                {
                    Vertex startPoint = network.GetVertex(random.Next(network.VertexCount));
                    Vertex endPoint = network.GetVertex(random.Next(network.VertexCount));
                    StepMutator.StepPath(network, startPoint, endPoint);
                }

                network.SetEnd(endIndex);

                individuals.Add(network);
            }
        }
    }
}
