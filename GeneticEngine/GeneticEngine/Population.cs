using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticEngine
{
    class Population<T> : IEnumerable<Individual<T>>
    {
        LinkedList<Individual<T>> individuals = new LinkedList<Individual<T>>();
        float minFitness = float.MaxValue;
        float maxFitness = float.MinValue;
        float sumFitness = 0;

        public int Count
        {
            get
            {
                return individuals.Count;
            }
        }

        public float MinFitness
        {
            get
            {
                return minFitness;
            }
        }

        public float MaxFitness
        {
            get
            {
                return maxFitness;
            }
        }

        public float AverageFitness
        {
            get
            {
                int count = individuals.Count;
                if (count > 0)
                {
                    return sumFitness / count;
                }
                else
                {
                    return 0.0f;
                }
            }
        }

        public Population()
        {
        }

        public IEnumerator<Individual<T>> GetEnumerator()
        {
            return individuals.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void AddIndividual(T chromosome, float fitness)
        {
            individuals.AddLast(new Individual<T>(chromosome, fitness));
            
            if (fitness < minFitness)
            {
                minFitness = fitness;
            }

            if (fitness > maxFitness)
            {
                maxFitness = fitness;
            }

            sumFitness += fitness;
        }
    }
}
