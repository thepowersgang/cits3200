using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticEngine
{
    class QueenBreeder : Breeder<QueenArrangement>
    {
        Random random = new Random();

        public QueenArrangement Breed(QueenArrangement mother, QueenArrangement father)
        {
            QueenArrangement child = new QueenArrangement(mother);

            int cut = random.Next(1,8);

            for (int i = cut; i < 8; i++)
            {
                child.SetQueenColumn(i, father.GetQueenColumn(i));
            }

            return child;
        }
    }
}
