using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Generations
{
    class ListGeneration : IGeneration
    {
        private List<Entry> entries = new List<Entry>();
        private uint minFitness;
        private uint maxFitness;
        private ulong totalFitness;

        public int Count
        {
            get
            {
                return entries.Count;
            }
        }

        public uint MinFitness
        {
            get
            {
                return minFitness;
            }
        }

        public uint MaxFitness
        {
            get
            {
                return maxFitness;
            }
        }

        public ulong TotalFitness
        {
            get
            {
                return totalFitness;
            }
        }

        public float AverageFitness
        {
            get
            {
                return (float)totalFitness / entries.Count;
            }
        }

        public IndividualWithFitness this[int index]
        {
            get
            {
                return Get(index);
            }
        }

        public ListGeneration()
        {
            minFitness = uint.MaxValue;
            maxFitness = 0;
            totalFitness = 0;
        }

        public void Insert(Object individual, uint fitness)
        {
            entries.Add(new Entry(individual, fitness));
        }

        public IndividualWithFitness Get(int index)
        {
            return entries[index].IndividualWithFitness;
        }

        public IndividualWithFitness GetByPartialSum(ulong sum)
        {
            int index = entries.Count / 2;

            while (sum!=entries[index].partialSum)
            {
                if (sum < entries[index].partialSum)
                {
                    index = index / 2;
                }
                else
                {
                    index += index / 2;
                }
            }

            return entries[index].IndividualWithFitness;
        }

        public int GetIndexByPartialSum(ulong sum)
        {
            int index = entries.Count / 2;

            while (sum != entries[index].partialSum)
            {
                if (sum < entries[index].partialSum)
                {
                    index = index / 2;
                }
                else
                {
                    index += index / 2;
                }
            }

            return index;
        }

        public IndividualWithFitness Remove(int index)
        {
            throw new NotSupportedException("Removal not suppoted in this implementation.");
        }

        public IndividualWithFitness RemoveByPartialSum(ulong sum)
        {
            throw new NotSupportedException("Removal not suppoted in this implementation.");
        }

        public void Prepare()
        {
            entries.Sort();

            ulong sum = 0;

            for (int i = 0; i < entries.Count; i++)
            {
                sum += entries[i].fitness;
                entries[i].partialSum = sum;
            }
        }

        private class Entry : IComparable
        {
            public object individual;
            public uint fitness;
            public ulong partialSum;

            public Entry(object individual, uint fitness)
            {
                this.individual = individual;
                this.fitness = fitness;
            }

            public IndividualWithFitness IndividualWithFitness
            {
                get
                {
                    return new IndividualWithFitness(individual, fitness);
                }
            }

            public int CompareTo(object b)
            {
                Entry entryB = (Entry)b;
                if (fitness > entryB.fitness)
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            }
        }
    }
}
