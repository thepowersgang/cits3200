using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticEngineSupport
{
    public interface IGenerationFactory
    {
        IGeneration CreateGeneration(ArrayList individuals);
    }
}
