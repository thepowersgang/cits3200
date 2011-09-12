using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticEngineSupport;

namespace EightQueens
{
    public class QueenGenerator : IPopulator
    {
        private Random random = new Random();

        public void Populate(ArrayList individuals)
        {
            for (int n = 0; n < 200; n++)
            {
                QueenArrangement individual = new QueenArrangement();

                for (int i = 0; i < 8; i++)
                {
                    individual.SetQueenColumn(i, random.Next(8));
                }

                individuals.Add(individual);
            }
        }
    }
}
