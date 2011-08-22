using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticEnginePlugin
{
    public interface IEvaluator
    {
        float Evaluate(object chromosome);
    }
}
