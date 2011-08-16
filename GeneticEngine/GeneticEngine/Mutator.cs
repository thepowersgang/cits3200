using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticEngine
{
    interface Mutator<T>
    {
        T Mutate(T mutatee);
    }
}
