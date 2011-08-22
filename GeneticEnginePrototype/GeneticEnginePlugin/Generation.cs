using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticEnginePlugin
{
    public class Generation : List<Individual>
    {
        private float minFitness = float.MaxValue;
        private float maxFitness = float.MinValue;
        private float sumFitness = 0;
                
        public float GetMinFitness()
        {
            return minFitness;
        }

        public float GetMaxFitness()
        {
            return maxFitness;
        }

        public float GetAverageFitness()
        {
            int count = Count;
            
            if (count > 0)
            {
                return sumFitness / count;
            }
            else
            {
                return 0.0f;
            }            
        }

        public Generation()
        {
        }
                
        public void AddIndividual(object chromosome, float fitness)
        {
            Add(new Individual(chromosome, fitness));

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
