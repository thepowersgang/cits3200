using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RoadNetworkDefinition;
using GeneticAlgorithm.Plugin;

namespace RoadNetworkSolver
{
    class StepPopulator : IPopulator
    {
        Random random = new Random();

        int populationSize = 200;
        Map map;
        
        public StepPopulator(object config)
        {
            map = Map.FromFile((string)config);
        }

        /// <summary>
        /// Generate an initial population of individuals
        /// </summary>
        /// <param name="individuals">An empty list to populate with the individuals</param>
        public void Populate(ArrayList individuals)
        {            
            RoadNetwork original = new RoadNetwork(map);

            int startIndex = original.AddVertexUnique(map.Start);
            int endIndex = original.AddVertexUnique(map.End);
            Vertex start = original.GetVertex(startIndex);
            Vertex end = original.GetVertex(endIndex);

            StepMutator.StepPath(original, start, end);

            original.SetEnd(endIndex);

            individuals.Add(original);

            while (individuals.Count < populationSize)
            {
                individuals.Add(StepMutator.Mutate((RoadNetwork)individuals[random.Next(individuals.Count)]));
            }
        }
    }
}
