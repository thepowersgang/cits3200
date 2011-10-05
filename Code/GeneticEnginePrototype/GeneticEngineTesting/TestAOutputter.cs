using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticAlgorithm.Plugin;

public class TestAOutputter : IOutputter
{
    public int numberGenerations = 0;
    public int[] fitnesses;
	public TestAOutputter()
	{

	}

    //IOutputter has spelling error, int genrationCount -> int generationCount.
    public void OutputGeneration(IGeneration generation, int generationCount)
    {
        fitnesses = new int[100];
        Console.WriteLine("Generations: " + generationCount);
        for (int i = 0; i < generationCount; i++)
        {
            IndividualWithFitness thisIndividual = generation[i];
            Console.WriteLine("Current Generation: " + i);
            Console.WriteLine("Best Fitness: " + thisIndividual.Fitness);
            Console.WriteLine("Value: " + (((IntegerIndividual)(thisIndividual.Individual)).value).ToString());
            Console.WriteLine();
            fitnesses[i] = ((IntegerIndividual)(thisIndividual.Individual)).value;
        }
    }
}
