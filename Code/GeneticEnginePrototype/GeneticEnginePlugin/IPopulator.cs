using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticAlgorithm.Plugin
{
    /// <summary>
    /// Defines a GeneticEngine populator plug-in.
    /// </summary>
    public interface IPopulator
    {
        /// <summary>
        /// Generate an initial population of individuals.
        /// The GeneticEngine calls this when initialising.
        /// </summary>
        /// <param name="individuals">An empty list to populate with the individuals</param>
        void Populate(ArrayList individuals);
    }
}
