using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            GeneticEngine<QueenArrangement> engine = new GeneticEngine<QueenArrangement>();

            engine.SetEvaluator(new QueenEvaluator());
            engine.SetSelector(new CutoffSelector<QueenArrangement>(100,50,50));
            engine.SetMutator(new QueenMutator());
            engine.SetBreeder(new QueenBreeder());
            engine.SetTerminator(new PerfectionTerminator<QueenArrangement>());

            engine.initialise(new QueenArrangement(), 200);

            Population<QueenArrangement> population = engine.start();

            List<Individual<QueenArrangement>> sorted = new List<Individual<QueenArrangement>>(population);
            sorted.Sort();
            sorted.Last().Chromosome.print();
            Console.WriteLine(sorted.Last().Fitness);

            Console.ReadLine();
        }
        
    }
}
