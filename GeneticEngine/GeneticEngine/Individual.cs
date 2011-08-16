using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticEngine
{
    class Individual<T> : IComparable<Individual<T>>
    {
        private T chromosome;
        private float fitness;

        public T Chromosome
        {
            get
            {
                return chromosome; 
            }
        }

        public float Fitness
        {
            get
            {
                return fitness;
            }
        }

        public Individual(T chromosome, float fitness)
        {
            this.chromosome = chromosome;
            this.fitness = fitness;
        }

        public int CompareTo(Individual<T> other)
        {
            return Math.Sign(fitness - other.fitness);
        }
    }
}
