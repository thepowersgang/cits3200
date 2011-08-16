using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticEngine
{
    class CutoffSelector<T> : Selector<T>
    {
        private int nSurvivors;
        private int nMutatees;
        private int nBreedingPairs;

        public CutoffSelector(int nSurvivors, int nMutatees, int nBreedingPairs)
        {
            this.nSurvivors = nSurvivors;
            this.nMutatees = nMutatees;
            this.nBreedingPairs = nBreedingPairs;
        }

        public PopulationGroups<T> doSelection(Population<T> population)
        {
            PopulationGroups<T> populationGroups = new PopulationGroups<T>();

            List<Individual<T>> sorted = new List<Individual<T>>(population);
            sorted.Sort();
            sorted.Reverse();

            for (int i = 0; i < nSurvivors; i++)
            {
                populationGroups.AddSurvivor(sorted[i].Chromosome);
            }

            for (int i = 0; i < nMutatees; i++)
            {
                populationGroups.AddMutatee(sorted[i].Chromosome);
            }

            for (int i = 0; i < nBreedingPairs; i++)
            {
                populationGroups.AddBreedingPair(sorted[i].Chromosome, sorted[i+1].Chromosome);
            }

            return populationGroups;
        }
    }
}
