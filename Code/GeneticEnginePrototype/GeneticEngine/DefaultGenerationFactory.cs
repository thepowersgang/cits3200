using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticEngineSupport;

namespace GeneticEngineCore
{
    /// <summary>
    /// Default implementation of IGenerationFactory.
    /// Produces an AATreeGeneration which provides O(lg(n)) time performance for all operations.
    /// </summary>
    class DefaultGenerationFactory : IGenerationFactory
    {
        /// <summary>
        /// Create an empty instance of the AATreeGeneration class.
        /// </summary>
        /// <param name="individuals">The individuals which will eventually populate the generation.</param>
        /// <returns>The AATreeGeneration created.</returns>
        public IGeneration CreateGeneration(ArrayList individuals)
        {
            return new AATreeGeneration();
        }
    }
}
