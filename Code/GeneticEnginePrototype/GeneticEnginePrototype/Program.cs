using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticEngineCore;
using GeneticEngineSupport;

namespace GeneticEnginePrototype
{
    class Program
    {
        static int DivideByTwo(int num)
        {
            // If num is an odd number, throw an ArgumentException.
            if ((num & 1) == 1)
                throw new ArgumentException("Number must be even", "num");

            // num is even, return half of its value.
            return num / 2;
        }

        static void Main(string[] args)
        {
            try
            {
                DivideByTwo(1);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex);
            }

            PluginLoader eightQueens = new PluginLoader("EightQueens.dll");
            PluginLoader generic = new PluginLoader("GenericPlugins.dll");
            
            IPopulator populator = eightQueens.GetChromosomeGenerator("EightQueens.QueenGenerator");
            IEvaluator evaluator = eightQueens.GetEvaluator("EightQueens.QueenEvaluator");
            IGeneticOperator geneticOperator = eightQueens.GetGeneticOperator("EightQueens.QueenOperator");
            IOutputter outputter = eightQueens.GetOutputter("EightQueens.BestQueenPrinter");
            ITerminator terminator = generic.GetTerminator("GenericPlugins.MaxFitnessTerminator", (uint)30);

            GeneticEngine engine = new GeneticEngine(populator, evaluator, geneticOperator, terminator, outputter);
            
            engine.Run();
                        
            Console.ReadLine();
        }
    }
}
