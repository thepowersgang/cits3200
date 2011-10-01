using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticEngineSupport
{
    /// <summary>
    /// Defines a GeneticEngine outputter plug-in
    /// </summary>
    public interface IOutputter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="generation"></param>
        /// <param name="genrationCount"></param>
        void OutputGeneration(IGeneration generation, int genrationCount);
    }
}
