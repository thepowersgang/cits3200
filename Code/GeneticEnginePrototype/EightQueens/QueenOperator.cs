using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticEngineSupport;

namespace EightQueens
{
    public class QueenOperator : IGeneticOperator
    {
        Random random = new Random();

        private QueenArrangement Conjugate(QueenArrangement chromosome1, QueenArrangement chromosome2)
        {
            QueenArrangement child = new QueenArrangement(chromosome1);

            int cut = random.Next(1, 8);
            
            for (int i = cut; i < 8; i++)
            {
                child.SetQueenColumn(i, chromosome2.GetQueenColumn(i));
            }

            return child;
        }

        private QueenArrangement Mutate(QueenArrangement chromosome)
        {
            QueenArrangement child = new QueenArrangement(chromosome);

            child.SetQueenColumn(random.Next(8), random.Next(8));

            return child;
        }

        public void Operate(IGeneration source, ArrayList destination)
        {
            for (int i = 0; i < 20; i++)
            {
                QueenArrangement motherChromosome = (QueenArrangement)source.Get(i).Individual;

                destination.Add(motherChromosome);
                destination.Add(Mutate(motherChromosome));

                for (int j = i+1; j < 30; j++)
                {
                    QueenArrangement fatherChromosome = (QueenArrangement)source.Get(j).Individual;

                    destination.Add(Conjugate(motherChromosome, fatherChromosome));
                    destination.Add(Conjugate(fatherChromosome, motherChromosome));   
                }
            }
        }
    }
}
