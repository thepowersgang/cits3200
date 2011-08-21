using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticEnginePlugin
{
    public class Individual : IComparable<Individual>
    {
        private object chromosome;
        private float fitness;

        public object GetChromosome()
        {
            return chromosome;
        }
        
        public float GetFitness()
        {
            return fitness;
        }

        public Individual(object chromosome, float fitness)
        {
            this.chromosome = chromosome;
            this.fitness = fitness;
        }

        public int CompareTo(Individual other)
        {
            return Math.Sign(other.fitness - fitness);
        }
    }
}
