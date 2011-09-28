using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticEngineSupport;


public class TestAGeneticOperator : IGeneticOperator
{
    public TestAGeneticOperator()
	{

	}

    public void Operate(IGeneration source, ArrayList destination)
    {
        //Are the individuals in source already ordered by fitness? If so:
        for (int i = 0; i < 50; i++)
        {
            IntegerIndividual theIndividual = (IntegerIndividual)soure.Get(i).Individual;
            IntegerIndividual operatedIndividual1 = new IntegerIndividual();
            IntegerIndividual operatedIndividual2 = new IntegerIndividual();
            opertedIndividual1.value = theIndividual.value + 1;
            opertedIndividual2.value = theIndividual.value + 2;
            destination.Add(operatedIndividual1);
            destination.Add(operatedInvididual2);
        }
    }
}
