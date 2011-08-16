using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticEngine
{
    interface Breeder<T>
    {
        T Breed(T mother, T father);
    }
}
