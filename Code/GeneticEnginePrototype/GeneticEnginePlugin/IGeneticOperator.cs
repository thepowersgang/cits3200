using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticEngineSupport
{
    /// <summary>
    /// Defines a GeneticEngine genetic operator plug-in.
    /// </summary>
    public interface IGeneticOperator
    {
        /// <summary>
        /// Process a generation to produce the individuals which will make up the next generation
        /// </summary>
        /// <param name="source">The current generation</param>
        /// <param name="destination">An empty collection of individuals to be populated</param>
        void Operate(IGeneration source, ArrayList destination);
    }
}
