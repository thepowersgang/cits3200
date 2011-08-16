using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticEngine
{
    class PopulationGroups<T>
    {
        LinkedList<T> survivors = new LinkedList<T>();
        LinkedList<T> mutatees = new LinkedList<T>();
        LinkedList<BreedingPair<T>> breedingPairs = new LinkedList<BreedingPair<T>>();

        public LinkedList<T> Survivors
        {
            get
            {
                return survivors;
            }
        }

        public LinkedList<T> Mutatees
        {
            get
            {
                return mutatees;
            }
        }

        public LinkedList<BreedingPair<T>> BreedingPairs
        {
            get
            {
                return breedingPairs;
            }
        }

        public PopulationGroups()
        {
        }

        public void AddSurvivor(T survivor)
        {
            survivors.AddLast(survivor);
        }

        public void AddMutatee(T mutatee)
        {
            mutatees.AddLast(mutatee);
        }

        public void AddBreedingPair(T mother, T father)
        {
            breedingPairs.AddLast(new BreedingPair<T>(mother, father));
        }
    }
}
