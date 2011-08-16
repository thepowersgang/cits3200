using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticEngine
{
    class GeneticEngine<T>
    {
        private LinkedList<T> chromosomes = new LinkedList<T>();
        private Evaluator<T> evaluator;
        private Selector<T> selector;
        private Mutator<T> mutator;
        private Breeder<T> breeder;
        private Terminator<T> terminator;

        public GeneticEngine()
        {
        }

        public void SetEvaluator(Evaluator<T> evaluator)
        {
            this.evaluator = evaluator;
        }

        public void SetSelector(Selector<T> selector)
        {
            this.selector = selector;
        }

        public void SetMutator(Mutator<T> mutator)
        {
            this.mutator = mutator;
        }

        public void SetBreeder(Breeder<T> breeder)
        {
            this.breeder = breeder;
        }

        public void SetTerminator(Terminator<T> terminator)
        {
            this.terminator = terminator;
        }

        public void initialise(T chromosome, int n)
        {
            for (int i = 0; i < n; i++)
            {
                chromosomes.AddLast(mutator.Mutate(chromosome));
            }
        }

        public Population<T> start()
        {
            while (true)
            {
                Population<T> population = new Population<T>();

                foreach (T chromosome in chromosomes)
                {
                    population.AddIndividual(chromosome, evaluator.Evaluate(chromosome));
                }

                if (terminator.Terminate(population))
                {
                    return population;
                }

                PopulationGroups<T> groups = selector.doSelection(population);

                chromosomes.Clear();

                foreach (T survivor in groups.Survivors)
                {
                    chromosomes.AddLast(survivor);
                }

                foreach (T mutatee in groups.Mutatees)
                {
                    chromosomes.AddLast(mutator.Mutate(mutatee));
                }

                foreach (BreedingPair<T> breedingPair in groups.BreedingPairs)
                {
                    chromosomes.AddLast(breeder.Breed(breedingPair.Mother, breedingPair.Father));
                }
            }
        }
    }
}
