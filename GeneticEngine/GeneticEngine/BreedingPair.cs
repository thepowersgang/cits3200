using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticEngine
{
    class BreedingPair<T>
    {
        private T mother;
        private T father;

        public T Mother
        {
            get
            {
                return mother;
            }
        }

        public T Father
        {
            get
            {
                return father;
            }
        }

        public BreedingPair(T mother, T father) 
        {
            this.mother = mother;
            this.father = father;
        }
    }
}
