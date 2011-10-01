using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticEngineSupport
{
    /// <summary>
    /// Defines A GeneticEngine evaluator plug-in
    /// </summary>
    public interface IEvaluator
    {
        /// <summary>
        /// Prepare the evaluator to process a new genration of individuals
        /// </summary>
        /// <param name="generationCount">The number of previous generations</param>
        /// <param name="individuals">The individuals which will be evaluated</param>
        void Initialise(int generationCount, ArrayList individuals);

        /// <summary>
        /// Evaluate an individual
        /// </summary>
        /// <param name="individual">The individual to evaluate</param>
        /// <returns>The fitness value of the individual</returns>
        uint Evaluate(object individual);
    }
}
