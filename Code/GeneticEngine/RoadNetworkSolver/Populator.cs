using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticAlgorithm.Plugin;
using RoadNetworkDefinition;

namespace RoadNetworkSolver
{
    /// <summary>
    /// IPopulator which produces a set of RoadNetworks.
    /// It first creates a RoadNetwork with a single edge from start to finish.
    /// Then it repeatedly randomly selects from the list of RoadNetworks already created,
    /// mutates (using Mutator) the individual selected and adds the reuslt to the list.
    /// </summary>
    public class Populator : IPopulator
    {
        /// <summary>
        /// The random number generator used to choose which RoadNetwork to mutate.
        /// </summary>
        private Random random = new Random();

        /// <summary>
        /// The number of individuals to generate.
        /// </summary>
        int populationSize = 200;

        /// <summary>
        /// The map in which the networks will exist
        /// </summary>
        Map map;

        /// <summary>
        /// Initialise a new Populator
        /// </summary>
        /// <param name="config">A string with the path to the map file</param>
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
            RoadNetwork original = new RoadNetwork(map);
            original.AddEdge(original.AddVertex(map.Start), original.AddVertex(map.End));
            individuals.Add(original);

            while (individuals.Count < populationSize)
            {
                individuals.Add(Mutator.Mutate((RoadNetwork)individuals[random.Next(individuals.Count)]));
            }
        }
    }
}
