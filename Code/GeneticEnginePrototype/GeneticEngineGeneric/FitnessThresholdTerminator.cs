using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticAlgorithm.Plugin;

namespace GeneticAlgorithm.Plugin.Generic
{
    /// <summary>
    /// Simple implementation of ITerminator.
    /// Terminates the algorithm when a fitness threshold is met.
    /// </summary>
    public class FitnessThresholdTerminator : ITerminator
    {
        /// <summary>
        /// The fitness threshold
        /// </summary>
        private uint threshold;

        /// <summary>
        /// Initializes a new instance of the <see cref="FitnessThresholdTerminator"/> class and sets the fitness threshold.
        /// </summary>
        /// <param name="config">An object which will will be cast as a uint to give the threshold.</param>
        public FitnessThresholdTerminator(object config)
        {
            threshold = (uint)config;
        }

        /// <summary>
        /// Determine if the algorithm should terminate.
        /// </summary>
        /// <param name="generation">The current generation.</param>
        /// <returns>true iff the MaxFitness property of the generation is greater than or equal to the threshold</returns>
        public bool Terminate(IGeneration generation)
        {
            return generation.MaxFitness >= threshold;
        }
    }
}
