using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticAlgorithm.Plugin;

namespace RoadNetworkSolver
{
    class Populator : IPopulator
    {
        int populationSize = 1000;
        Map map;
        
        public Populator(object config)
        {
            map = Map.FromFile((string)config);
        }

        /// <summary>
        /// Generate an initial population of individuals
        /// </summary>
        /// <param name="individuals">An empty list to populate with the individuals</param>
        public void Populate(ArrayList individuals)
        {
            Random random = new Random();

            MutationOperator mutationOperator = new MutationOperator(null);

            RoadNetwork original = new RoadNetwork(map);
            original.AddEdge(original.AddVertex(map.Start), original.AddVertex(map.End));
            individuals.Add(original);

            while (individuals.Count < populationSize)
            {
                individuals.Add(mutationOperator.Mutate((RoadNetwork)individuals[random.Next(individuals.Count)]));
            }
        }
    }
}
