using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticEnginePlugin;

namespace GenericPlugins
{
    class PerfectTerminator : ITerminator
    {
        public bool Terminate(Generation generation)
        {
            return generation.GetMaxFitness() == 1.0;
        }
    }
}
