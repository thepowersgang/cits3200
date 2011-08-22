using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticEnginePlugin;

namespace CITS3200GeneticEngine
{
    public class GeneticEngine
    {
        Generation generation = null;
        IChromosomeGenerator chromosomeGenerator;
        IEvaluator evaluator;
        IGeneticOperator geneticOperator;
        ITerminator terminator;
        
        public GeneticEngine()
        {
        }

        public void SetChromosomeGenerator(IChromosomeGenerator chromosomeGenerator)
        {
            this.chromosomeGenerator = chromosomeGenerator;
        }

        public void SetEvaluator(IEvaluator evaluator)
        {
            this.evaluator = evaluator;
        }

        public void SetGeneticOperator(IGeneticOperator geneticOperator)
        {
            this.geneticOperator = geneticOperator;
        }

        public void SetTerminator(ITerminator terminator)
        {
            this.terminator = terminator;
        }

        public Generation GetGeneration()
        {
            return generation;
        }

        public void NextGeneration(int size)
        {
            ArrayList nextGeneration = new ArrayList();

            if (generation == null)
            {
                while (nextGeneration.Count < size)
                {
                    chromosomeGenerator.GenerateChromosomes(nextGeneration);
                }

                generation = new Generation();
            }
            else
            {
                while (nextGeneration.Count < size)
                {
                    geneticOperator.Operate(generation, nextGeneration);
                }

                generation.Clear();
            }                       
            
            foreach (object chromosome in nextGeneration)
            {
                generation.AddIndividual(chromosome, evaluator.Evaluate(chromosome));
            }

            generation.Sort();
        }

        public bool IsComplete()
        {
            return generation!=null && terminator.Terminate(generation);
        }

        public void RunAlgorithm(int size)
        {
            while (!IsComplete())
            {
                NextGeneration(size);
            }
        }
    }
}
