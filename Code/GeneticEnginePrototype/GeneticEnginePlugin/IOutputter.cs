using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticEngineSupport
{
    public interface IOutputter
    {
        void OutputGeneration(IGeneration generation, int genrationCount);
    }
}
