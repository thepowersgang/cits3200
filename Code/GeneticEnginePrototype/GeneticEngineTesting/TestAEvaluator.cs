using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticEngineSupport;

public class TestAEvaluator : IEvaluator
{
	public TestAEvaluator()
	{

	}
        
    public void Initialise(int generationCount, ArrayList individuals) { }

    public uint Evaluate(object individual)
    {
            int fitness = 0;
            IntegerIndividual theIndividual = (IntegerIndividual)individual;
            if (((int)theIndividual.value)%2 == 1) fitness = 2*((int)theIndividual.value);
            return (uint)(fitness);
    }

}

