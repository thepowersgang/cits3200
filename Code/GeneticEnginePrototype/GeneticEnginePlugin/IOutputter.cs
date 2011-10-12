using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticAlgorithm.Plugin
{
    /// <summary>
    /// Defines a GeneticEngine outputter plug-in
    /// </summary>
    public interface IOutputter
    {
        /// <summary>
        /// Prepare to start outputting generations.
        /// </summary>
        void StartOutput();
        
        /// <summary>
        /// Output a generation
        /// </summary>
        /// <param name="generation">The generation to output</param>
        /// <param name="genrationCount">The number of generations before this one</param>
        void OutputGeneration(IGeneration generation, int generationNumber);

        /// <summary>
        /// Clean up after outputting generations.
        /// </summary>
        void FinishOutput();
    }
}
