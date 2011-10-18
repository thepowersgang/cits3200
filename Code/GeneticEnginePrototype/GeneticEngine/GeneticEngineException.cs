using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticAlgorithm
{
    public class GeneticEngineException : Exception
    {
        public enum Plugin
        {
            None,
            Populator,
            Evaluator,            
            GeneticOperator,
            Terminator,
            Outputter,
            GenerationFactory,
            Generation
        }

        private Plugin sourcePlugin;

        public Plugin SourcePlugin
        {
            get
            {
                return sourcePlugin;
            }
        }

        public GeneticEngineException(Plugin sourcePlugin, string message, Exception innerException)
            : base(message, innerException)
        {
            this.sourcePlugin = sourcePlugin;
        }

        public GeneticEngineException(Plugin sourcePlugin, string message)
            : base(message)
        {
            this.sourcePlugin = sourcePlugin;
        }

        public GeneticEngineException(string message, Exception innerException)
            : base(message, innerException)
        {
            this.sourcePlugin = Plugin.None;
        }

        public GeneticEngineException(string message)
            : base(message)
        {
            this.sourcePlugin = Plugin.None;
        }
    }
}
