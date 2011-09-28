using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticEngineSupport;

public class TestAOutputter : IOutputter
{
	public TestAOutputter()
	{

	}

    //IOutputter has spelling error, int genrationCount -> int generationCount.
    void OutputGeneration(IGeneration generation, int genrationCount)
    {
        Console.WriteLine("Generations: " + generationCount);
        for (int i = 0; i < genrationCount; i++)
        {
            IndividualWithFitness thisIndividual = generation[i];
            Console.WriteLine("Current Generation: " + i);
            Console.WriteLine("Best Fitness: " + thisIndividual.Fitness);
            Console.WriteLine("Value: " + (thisIndividual.Individual.value).ToString());
            Console.WriteLine();
        }
    }
}
