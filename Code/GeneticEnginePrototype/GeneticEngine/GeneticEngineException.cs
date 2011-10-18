using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticAlgorithm
{
    /// <summary>
    /// Represents an error that occurs while running a genetic algorithm.
    /// </summary>
    public class GeneticEngineException : Exception
    {
        /// <summary>
        /// Identifies the source of the error.
        /// </summary>
        public enum Plugin
        {
            /// <summary>
            /// The error did not occur in a plug-in.
            /// </summary>
            None,

            /// <summary>
            /// The error occurred in the IPopulator.
            /// </summary>
            Populator,

            /// <summary>
            /// The error occurred in the IEvaluator.
            /// </summary>
            Evaluator,
            
            /// <summary>
            /// The Error occurred in the IGeneticOperator.
            /// </summary>
            GeneticOperator,

            /// <summary>
            /// The Error occurred in the ITerminator.
            /// </summary>
            Terminator,

            /// <summary>
            /// The Error occurred in the IOutputter.
            /// </summary>
            Outputter,

            /// <summary>
            /// The Error occurred in the IGenerationFactory.
            /// </summary>
            GenerationFactory,

            /// <summary>
            /// The Error occurred in the IGeneration.
            /// </summary>
            Generation
        }

        /// <summary>
        /// The plug-in in which the error occurred.
        /// </summary>
        private Plugin sourcePlugin;

        /// <summary>
        /// Get the plug-in in which the error occured.
        /// </summary>
        public Plugin SourcePlugin
        {
            get
            {
                return sourcePlugin;
            }
        }

        /// <summary>
        /// Initialise a new GeneticEngineException
        /// </summary>
        /// <param name="sourcePlugin">The plug-in in which the error occured</param>
        /// <param name="message">A string describing the error</param>
        /// <param name="innerException">The exception which caused this exception to be thrown</param>
        public GeneticEngineException(Plugin sourcePlugin, string message, Exception innerException)
            : base(message, innerException)
        {
            this.sourcePlugin = sourcePlugin;
        }

        /// <summary>
        /// Initialise a new GeneticEngineException
        /// </summary>
        /// <param name="sourcePlugin">The plug-in in which the error occured</param>
        /// <param name="message">A string describing the error</param>
        public GeneticEngineException(Plugin sourcePlugin, string message)
            : base(message)
        {
            this.sourcePlugin = sourcePlugin;
        }

        /// <summary>
        /// Initialise a new GeneticEngineException which did not occur in a plug-in
        /// </summary>
        /// <param name="message">A string describing the error</param>
        /// <param name="innerException">The exception which caused this exception to be thrown</param>
        public GeneticEngineException(string message, Exception innerException)
            : base(message, innerException)
        {
            this.sourcePlugin = Plugin.None;
        }

        /// <summary>
        /// Initialise a new GeneticEngineException which did not occur in a plug-in
        /// </summary>
        /// <param name="message">A string describing the error</param>
        public GeneticEngineException(string message)
            : base(message)
        {
            this.sourcePlugin = Plugin.None;
        }
    }
}
