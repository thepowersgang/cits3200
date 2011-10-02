using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticEngineSupport
{
    /// <summary>
    /// Defines a GeneticEngine teminator plug-in.
    /// </summary>
    public interface ITerminator
    {
        /// <summary>
        /// Determine if a generation satisfies the termination condition.
        /// </summary>
        /// <param name="generation">The generation</param>
        /// <returns>True iff the generation satisfies the termination condition</returns>
        bool Terminate(IGeneration generation);
    }
}
