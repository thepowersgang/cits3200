using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticEngine
{
    interface Evaluator<T>
    {
        float Evaluate(T chromosome);
    }
}
