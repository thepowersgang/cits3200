using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticAlgorithm
{
    public class GeneticEngineException : Exception
    {
        public enum Component
        {
            Engine,
            Populator,
            Evaluator,
            GeneticOperator,
            Terminator,
            GenerationFactory
        }

        private Component sourceComponent;

        public Component SourceComponent
        {
            get
            {
                return sourceComponent;
            }
        }
        
        public GeneticEngineException(Component sourceComponent, string message, Exception innerException)
            : base(message, innerException)
        {
            this.sourceComponent = sourceComponent;
        }
    }
}
