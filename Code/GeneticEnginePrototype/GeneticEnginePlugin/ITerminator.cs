using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticEngineSupport
{
    public interface ITerminator
    {
        bool Terminate(IGeneration generation);
    }
}
