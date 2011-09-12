using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticEngineSupport
{
    public class IndividualWithFitness
    {
        private object individual;
        private uint fitness;

        public object Individual
        {
            get
            {
                return individual;
            }
        }

        public uint Fitness
        {
            get
            {
                return fitness;
            }
        }

        public IndividualWithFitness(object individual, uint fitness)
        {
            this.individual = individual;
            this.fitness = fitness;
        }
    }
}
