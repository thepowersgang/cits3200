﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticAlgorithm.Plugin;
using GeneticAlgorithm.Plugin.Generic;

namespace GeneticAlgorithm
{
    /// <summary>
    /// Runs a genetic algorithm using the supplied plug-ins.
    /// </summary>
    public class GeneticEngine
    {
        /// <summary>
        /// The populator plugin
        /// </summary>
        IPopulator populator;
        
        /// <summary>
        /// The evaluator plugin
        /// </summary>
        IEvaluator evaluator;

        /// <summary>
        /// The gentic operator plugin
        /// </summary>
        IGeneticOperator geneticOperator;

        /// <summary>
        /// The termination condition plugin
        /// </summary>
        ITerminator terminator;

        /// <summary>
        /// The output plugin
        /// </summary>
        IOutputter outputter;

        /// <summary>
        /// The generation factory plugin
        /// </summary>
        IGenerationFactory generationFactory;

        /// <summary>
        /// The number of generations which have been processed
        /// </summary>
        int generationNumber;

        /// <summary>
        /// The most recent generation
        /// </summary>
        IGeneration generation = null;

        /// <summary>
        /// Get the number of generations before the current generation.
        /// </summary>
        public int GenerationCount
        {
            get
            {
                return generationNumber;
            }
        }

        /// <summary>
        /// Get the current generation.
        /// </summary>
        public IGeneration Generation
        {
            get
            {
                return generation;
            }
        }

        /// <summary>
        /// Get a value indicating whether the current generation satisfies the termination condidtion.
        /// </summary>
        public bool IsComplete
        {
            get
            {
                return generation != null && terminator.Terminate(generation);
            }
        }

        /// <summary>
        /// Initialise a new instance of the GeneticEngine class with the supplied plug-ins and populate the initial generation.
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
            this.generationFactory = generationFactory == null ? new AATreeGenerationFactory() : generationFactory;

            Setup();
        }
                
        private void Setup()
        {
            generationNumber = -1;
            ArrayList individuals = new ArrayList();
            populator.Populate(individuals);
            processIndividuals(individuals);                  
        }

        /// <summary>
        /// Reset the genetic engine by setting the generation count to 0 and using the populator plug-in
        /// to generate a new initial population 
        /// </summary>
        public void Reset()
        {
            FinishOutput();
            Setup();            
        }

        public void FinishOutput()
        {
            if (outputter != null && outputter.Status == OutputterStatus.Open)
            {
                outputter.CloseOutput();
            }
        }

        /// <summary>
        /// Process the current generation to produce the next.
        /// </summary>
        public void Step()
        {
            ArrayList individuals = new ArrayList();
            geneticOperator.Operate(generation, individuals);
            processIndividuals(individuals);
        }

        /// <summary>
        /// Process a list of individuals.
        /// 1) Replace the current generation with a new empty IGeneration
        /// 2) Evaluate each individual and add it to the generation
        /// 3) Send the generation to the outputter
        /// </summary>
        /// <param name="individuals">The list of individuals</param>
        private void processIndividuals(ArrayList individuals)
        {
            //Get a new instance of the generation container.
            generation = generationFactory.CreateGeneration(individuals);

            //Inform the evaluator that an new generation has been started.
            evaluator.Initialise(generationNumber, individuals);

            //Evaluate each individual and add it to the generation.
            foreach (object individual in individuals)
            {
                generation.Insert(individual, evaluator.Evaluate(individual));
            }

            //Perform any preparation the generation needs before being given to the outputter or genetic operator plug-ins.
            generation.Prepare();

            //Increment the generation counter.
            generationNumber++;

            //If an outputter plug-in has been supplied then output the new generation.
            if (outputter != null)
            {
                if (outputter.Status == OutputterStatus.Closed)
                {
                    outputter.OpenOutput();
                }

                if (outputter.Status == OutputterStatus.Open)
                {
                    outputter.OutputGeneration(generation, generationNumber);
                }
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
        /// Execute the genetic algorithm until the termination condition is satisfied.
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
