using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticEnginePlugin
{
    public interface IGeneticOperator
    {
        void Operate(Generation source, ArrayList destination);
    }
}
