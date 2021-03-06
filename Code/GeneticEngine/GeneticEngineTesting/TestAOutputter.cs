﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticAlgorithm.Plugin;

public class TestAOutputter : IOutputter
{
    public int numberGenerations = 0;
    public int[] fitnesses;
    public int best;

    public OutputterStatus Status
    {
        get
        {
            return OutputterStatus.Open;
        }
    }

    public TestAOutputter()
    {
        fitnesses = new int[1000];
    }

    public void OpenOutput()
    {
    }

    public void OutputGeneration(IGeneration generation, int generationNumber)
    {
        best = 0;

        for (int i = 0; i < generation.Count; i++)
        {
            IndividualWithFitness thisIndividual = generation[i];
            //Console.WriteLine("Current Generation: " + i);
            //Console.WriteLine("Best Fitness: " + thisIndividual.Fitness);
            //Console.WriteLine("Value: " + (((IntegerIndividual)(thisIndividual.Individual)).value).ToString());
            //Console.WriteLine();
            if (((IntegerIndividual)(thisIndividual.Individual)).value > best)
            {
                best = ((IntegerIndividual)(thisIndividual.Individual)).value;
            }
            //numberGenerations = i + 1;
        }
        fitnesses[generationNumber] = best;
        numberGenerations = generationNumber;
    }

    public void CloseOutput()
    {
    }
}

