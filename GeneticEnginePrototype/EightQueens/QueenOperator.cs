using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticEnginePlugin;

namespace EightQueens
{
    public class QueenOperator : IGeneticOperator
    {
        Random random = new Random();

        private QueenArrangement conjugate(QueenArrangement chromosome1, QueenArrangement chromosome2)
        {
            QueenArrangement child = new QueenArrangement(chromosome1);

            int cut = random.Next(1, 8);
            
            for (int i = cut; i < 8; i++)
            {
                child.SetQueenColumn(i, chromosome2.GetQueenColumn(i));
            }

            return child;
        }

        private QueenArrangement mutate(QueenArrangement chromosome)
        {
            QueenArrangement child = new QueenArrangement(chromosome);

            child.SetQueenColumn(random.Next(8), random.Next(8));

            return child;
        }

        public void Operate(Generation source, ArrayList destination)
        {
            Individual mother = source.First();
            source.Remove(mother);
            Individual father = source.First();
            source.Remove(father);

            QueenArrangement motherChromosome = (QueenArrangement)mother.GetChromosome();
            QueenArrangement fatherChromosome = (QueenArrangement)father.GetChromosome();

            destination.Add(motherChromosome);
            destination.Add(fatherChromosome);
            destination.Add(mutate(motherChromosome));
            destination.Add(mutate(fatherChromosome));
            destination.Add(conjugate(motherChromosome, fatherChromosome));
            destination.Add(conjugate(fatherChromosome, motherChromosome));
        }
    }
}
