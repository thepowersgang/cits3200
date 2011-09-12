using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticEngineSupport;

namespace GenericPlugins
{
    class MaxFitnessTerminator : ITerminator
    {
        uint maxFitness;

        public MaxFitnessTerminator(uint maxFitness)
        {
            this.maxFitness = maxFitness;
        }

        public bool Terminate(IGeneration generation)
        {
            return generation.MaxFitness >= maxFitness;
        }
    }
}
