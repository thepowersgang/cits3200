using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticEngineSupport
{
    public interface IEvaluator
    {
        void Initialise(int generationCount, ArrayList individuals);
        uint Evaluate(object individual);
    }
}
