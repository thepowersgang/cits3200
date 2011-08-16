using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticEngine
{
    class QueenMutator : Mutator<QueenArrangement>
    {
        Random random = new Random();

        public QueenArrangement Mutate(QueenArrangement mutatee)
        {
            QueenArrangement mutated = new QueenArrangement(mutatee);

            mutated.SetQueenColumn(random.Next(8), random.Next(8));

            return mutated;
        }
    }
}
