using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticEnginePlugin;

namespace EightQueens
{
    public class QueenGenerator : IChromosomeGenerator
    {
        private Random random = new Random();

        public void GenerateChromosomes(ArrayList chromosomes)
        {
            QueenArrangement chromosome = new QueenArrangement();

            for (int i = 0; i < 8; i++)
            {
                chromosome.SetQueenColumn(i, random.Next(8));
            }

            chromosomes.Add(chromosome);
        }
    }
}
