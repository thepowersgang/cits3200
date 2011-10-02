using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticEngineSupport
{
    /// <summary>
    /// Defines a GeneticEngine populator plug-in.
    /// </summary>
    public interface IPopulator
    {
        /// <summary>
        /// Generate an initial population of individuals
        /// </summary>
        /// <param name="individuals">An empty list to populate with the individuals</param>
        void Populate(ArrayList individuals);
    }
}
