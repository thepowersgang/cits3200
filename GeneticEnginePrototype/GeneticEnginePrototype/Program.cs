using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CITS3200GeneticEngine;
using GeneticEnginePlugin;

namespace GeneticEnginePrototype
{
    class Program
    {
        static void Main(string[] args)
        {
            PluginLoader eightQueens = new PluginLoader("EightQueens.dll");
            PluginLoader generic = new PluginLoader("GenericPlugins.dll");
            
            IChromosomeGenerator generator = eightQueens.GetChromosomeGenerator("EightQueens.QueenGenerator");
            IEvaluator evaluator = eightQueens.GetEvaluator("EightQueens.QueenEvaluator");
            IGeneticOperator geneticOperator = eightQueens.GetGeneticOperator("EightQueens.QueenOperator");
            ITerminator terminator = generic.GetTerminator("GenericPlugins.PerfectTerminator");

            GeneticEngine engine = new GeneticEngine();

            engine.SetChromosomeGenerator(generator);
            engine.SetEvaluator(evaluator);
            engine.SetGeneticOperator(geneticOperator);
            engine.SetTerminator(terminator);

            engine.RunAlgorithm(200);

            Generation generation = engine.GetGeneration();

            object best = generation[0].GetChromosome();

            Console.Write(best);

            Console.ReadLine();
        }
    }
}
