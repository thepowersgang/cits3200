using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticEnginePlugin
{
    public interface IChromosomeGenerator
    {
        void GenerateChromosomes(ArrayList chromosomes);
    }
}
