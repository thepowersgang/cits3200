using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticEngine
{
    class PerfectionTerminator<T> : Terminator<T>
    {
        public bool Terminate(Population<T> population)
        {
            return population.MaxFitness == 1.0;
        }
    }
}
