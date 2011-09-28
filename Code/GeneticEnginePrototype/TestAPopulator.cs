using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticEngineSupport;

public class TestAPopulator : IPopulator
{
	public TestAPopulator()
	{

	}

    public void Populate(ArrayList individuals)
    {
        for (int i = 1; i < 101; i++)
        {
            IntegerIndividual theIndividual = new IntegerIndividual();
            theIndividual.value = i;
            individuals.Add(thisIndividual);
        }
    }
}
