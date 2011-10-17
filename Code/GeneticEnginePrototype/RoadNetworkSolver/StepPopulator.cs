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

        int populationSize = 100;
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

            Vertex start = original.AddVertex(map.Start);
            Vertex end = original.AddVertex(map.End);

            StepMutator.StepPath(original, start, end);

            original.SetEnd(1);

            individuals.Add(original);

            while (individuals.Count < populationSize)
            {
                individuals.Add(StepMutator.Mutate((RoadNetwork)individuals[random.Next(individuals.Count)]));
            }
        }
    }
}
