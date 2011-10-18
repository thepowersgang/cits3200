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
        /// Gets the current status of the IOutputter
        /// </summary>
        OutputterStatus Status
        {
            get;
        }

        /// <summary>
        /// Prepare to start outputting generations.
        /// The GeneticEngine calls this before outputting a generation whenever Status is Closed
        /// </summary>
        void OpenOutput();
        
        /// <summary>
        /// Output a generation.
        /// The GeneticEngine calls this once every generation after all individuals have been evaluated.
        /// </summary>
        /// <param name="generation">The generation to output</param>
        /// <param name="generationNumber">The number of generations before this one</param>
        void OutputGeneration(IGeneration generation, int generationNumber);

        /// <summary>
        /// Finish outputting generations.
        /// Called when FinishOutput() or Reset() is called on the GeneticEngine.
        /// </summary>
        void CloseOutput();
    }
}
