using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticEngineSupport;

namespace EightQueens
{
    public class BestQueenPrinter : IOutputter
    {
        private uint bestSoFar = 0;

        public void OutputGeneration(IGeneration generation, int generationCount)
        {
            IndividualWithFitness best = generation[0];

            if (best.Fitness > bestSoFar)
            {
                bestSoFar = best.Fitness;

                Console.WriteLine("Generations: " + generationCount);
                Console.WriteLine("Best Fitness: " + best.Fitness);
                Console.WriteLine(((QueenArrangement)best.Individual).ToString());
                Console.WriteLine();
            }
        }
    }
}
