using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RoadNetworkDefinition;
using GeneticAlgorithm.Plugin;

namespace RoadNetworkSolver
{
    /// <summary>
    /// IPopulator which produces a set of RoadNetworks.
    /// It first creates a RoadNetwork with a simple path from start to finish
    /// made of edges connecting adjacent points.
    /// Then it repeatedly randomly selects from the list of RoadNetworks already created,
    /// mutates (using StepMutator) the individual selected and adds the reuslt to the list.
    /// </summary>
    public class StepPopulator : IPopulator
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
        /// Initialise a new StepPopulator
        /// </summary>
        /// <param name="config">A string with the path to the map file</param>
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
