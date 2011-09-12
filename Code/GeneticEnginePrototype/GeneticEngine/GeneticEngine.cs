using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticEngineSupport;

namespace GeneticEngineCore
{
    /// <summary>
    /// Runs a genetic algorithm using the supplied plug-ins.
    /// </summary>
    public class GeneticEngine
    {
        //Plug-ins
        IPopulator populator;        
        IEvaluator evaluator;
        IGeneticOperator geneticOperator;
        ITerminator terminator;
        IOutputter outputter;
        IGenerationFactory generationFactory;

        //The number of generations which have been processed
        int generationCount;

        //Current generation 
        IGeneration generation = null;

        /// <summary>
        /// Gets the number of generations processed.
        /// </summary>
        public int GenerationCount
        {
            get
            {
                return generationCount;
            }
        }

        /// <summary>
        /// Gets the current generation.
        /// </summary>
        public IGeneration Generation
        {
            get
            {
                return generation;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the current generation satisfies the termination condidtion.
        /// </summary>
        /// <value>true if the termination condition is satisfied; otherwise false.</value>
        public bool IsComplete
        {
            get
            {
                return generation != null && terminator.Terminate(generation);
            }
        }

        /// <summary>
        /// Initialises a new instance of the GeneticEngine class with the supplied plug-ins.
        /// </summary>
        /// <param name="populator">The populator plug-in. Generates the initial population.</param>
        /// <param name="evaluator">The evaluator plug-in. Provides the fitness function.</param>
        /// <param name="geneticOperator">The genetic operator plug-in. Processes one generation to produce the individuals for the next.</param>
        /// <param name="terminator">The terminator plug-in. Provides the termination condition.</param>
        /// <param name="outputter">The outputter plug-in or null for no output. Outputs each generation.</param>
        /// <param name="generationFactory">The generation factory plug-in or null to use the default. Creates the generation container.</param>
        public GeneticEngine(IPopulator populator, IEvaluator evaluator, IGeneticOperator geneticOperator, ITerminator terminator, IOutputter outputter = null, IGenerationFactory generationFactory = null)
        {
            if (populator == null)
            {
                throw new ArgumentException("Populator must not be null.", "populator");
            }

            if (evaluator == null)
            {
                throw new ArgumentException("Evaluator must not be null.", "evaluator");
            }

            if (geneticOperator == null)
            {
                throw new ArgumentException("Genetic operator must not be null.", "geneticOperator");
            }

            if (terminator == null)
            {
                throw new ArgumentException("Terminator must not be null.", "terminator");
            }

            this.populator = populator;            
            this.evaluator = evaluator;
            this.geneticOperator = geneticOperator;            
            this.terminator = terminator;
            this.outputter = outputter;
            this.generationFactory = generationFactory == null ? new DefaultGenerationFactory() : generationFactory;
            generationCount = 0;
        }
         
        /// <summary>
        /// Process the current generation to produce the next.
        /// </summary>
        public void Step()
        {
            //The individuals which will make up the next generation
            ArrayList individuals = new ArrayList();

            //If there is no current generation then get the individuals from the populator.
            //Otherwise pass the current generation to the genetic operator.
            if (generation == null)
            {
                populator.Populate(individuals);
            }
            else
            {
                geneticOperator.Operate(generation, individuals);
            }

            //Get a new instance of the generation container.
            generation = generationFactory.CreateGeneration(individuals);

            //Inform the evaluator that an new generation has been started.
            evaluator.Initialise(generationCount, individuals);

            //Evaluate each individual and add it to the generation.
            foreach (object individual in individuals)
            {
                generation.Insert(individual, evaluator.Evaluate(individual));
            }

            //Perform any preparation the generation needs before being given to the outputter or genetic operator plug-ins.
            generation.Prepare();

            //Increment the generation counter.
            generationCount++;

            //If an outputter plug-in has been supplied then output the new generation.
            if (outputter != null)
            {
                outputter.OutputGeneration(generation, generationCount);
            }
        }

        /// <summary>
        /// Execute the genetic algorithm for a number of generations.
        /// </summary>
        /// <param name="n">The number of generations to process.</param>
        public void Repeat(int n)
        {
            for (int i = 0; i < n; i++)
            {
                Step();
            }
        }
        
        /// <summary>
        /// Execute the genetic algorithm until the termination condidtion is satisfied.
        /// </summary>
        public void Run()
        {
            while (!IsComplete)
            {
                Step();
            }
        }
    }
}
