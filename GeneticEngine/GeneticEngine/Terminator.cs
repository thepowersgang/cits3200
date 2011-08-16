using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticEngine
{
    interface Terminator<T>
    {
        bool Terminate(Population<T> populaion);
    }
}
