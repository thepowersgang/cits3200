using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticAlgorithm.Plugin
{
    /// <summary>
    /// Holds an individual and its fitness value
    /// </summary>
    public class IndividualWithFitness
    {
        /// <summary>
        /// The individual
        /// </summary>
        private object individual;

        /// <summary>
        /// The individual's fitness value
        /// </summary>
        private uint fitness;

        /// <summary>
        /// Get the individual
        /// </summary>
        public object Individual
        {
            get
            {
                return individual;
            }
        }

        /// <summary>
        /// Get the individual's fitness value
        /// </summary>
        public uint Fitness
        {
            get
            {
                return fitness;
            }
        }

        /// <summary>
        /// Initialise a new IndividualWithFitness
        /// </summary>
        /// <param name="individual">The individual</param>
        /// <param name="fitness">The individual's fitness</param>
        public IndividualWithFitness(object individual, uint fitness)
        {
            this.individual = individual;
            this.fitness = fitness;
        }
    }
}
