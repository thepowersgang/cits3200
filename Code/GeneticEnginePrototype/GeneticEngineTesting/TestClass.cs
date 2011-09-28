using System;
using GeneticEngineCore;
using GeneticEnginePrototype;
using GeneticEngineSupport;
using GeneticEngineTesting;
using GenericPlugins;
using RoadNetworkDisplay;
using RoadNetworkOperatorTest;
using RoadNetworkSolver;

public class TestClass
{
    TestAPopulator APopulator;
    TestAEvaluator AEvaluator;
    TestAGeneticOperator AGeneticOperator;
    MaxFitnessTerminator AMaxFitnessTerminator;
    TestAOutputter AOutputter;
    IGeneration currentGeneration;

    public TestClass()
    { }


    public void TestA1()
    {
        initialiseA();
        Boolean passed = true;
        int[] countNumbers;
        countNumbers = new int[100];
        GeneticEngine testEngine = new GeneticEngine(APopulator, AEvaluator, AGeneticOperator, AMaxFitnessTerminator, AOutputter, null);
        testEngine.Initialise();
        testEngine.Step();
        currentGeneration = testEngine.Generation();
        //Check that 100 individuals were generated:
        if (currentGeneration.Count != 100) throw new Exception("Count not set to 100.");

        //Check that individuals exist with values 1-100 exactly once each.
        for (int i = 0; i < 100; i++)
        {
            countNumbers[currentGeneration.Get(i).individual.value]++;
        }
        for (int i = 0; i < 100; i++)
        {
            if (countNumbers[i] != 1)
            {
                passed = false;
                break;
            }
        }
        if (!passed) throw new Exception("Individuals not populated from 1-100 exactly once each");

        //If no exceptions halt test:
        Console.WriteLine("Test A1 Successful");
    }

    public void TestA2()
    {
        initialiseA();
        Boolean passed = true;
        int[] countNumbers;
        countNumber = new int[198];
        GeneticEngine testEngine = new GeneticEngine(APopulator, AEvaluator, AGeneticOperator, AMaxFitnessTerminator, AOutputter, null);
        testEngine.Initialise();
        testEngine.Step();
        testEngine.Step();
        currentGeneration = testEngine.Generation();

        //Check that individuals exist from 2 to 200.
        for (int i = 0; i < currentGeneration.Count; i++)
        {
            countNumber[currentGeneration.Get(i).individual.value]++;
        }
        for (int i = 0; i < currentGeneration.Count; i++)
        {
            if (countNumber[i] == 0)
            {
                passed = false;
                break;
            }
        }
        if (!passed) throw new Exception("Individuals not generated from 2-200 correctly.");

        //If no exceptions halt test, then successful:
        Console.WriteLine("Test A2 Successful");
    }

    public void TestA3()
    {
        initialiseA();
        GeneticEngine testEngine = new GeneticEngine(APopulator, AEvaluator, AGeneticOperator, AMaxFitnessTerminator, AOutputter, null);
        testEngine.Initialise();
        if (testEngine.isComplete()) throw new Exception("Is Complete returns true when expected value is false.");
        testEngine.Step();
        if (testEngine.isComplete()) throw new Exception("Is Complete returns true when expected value is false.");
        testEngine.Repeat(99);
        if (testEngine.isComplete()) throw new Exception("Is Complete returns true when expected value is false.");
        testEngine.Step();
        if (!testEngine.isComplete()) throw new Exception("Is Complete returns false when expected value is true.");

        //If no exceptions halt test, then successful:
        Console.WriteLine("Test A3 Successful");
    }

    public void TestA4()
    {
        initialiseA();
        GeneticEngine testEngine = new GeneticEngine(APopulator, AEvaluator, AGeneticOperator, AMaxFitnessTerminator, AOutputter, null);
        testEngine.Initialise();
        testEngine.Repeat(5);
        currentGeneration = testEngine.Generation();

        Console.WriteLine("Test A4 Completed, visually inspect output to verify.");
    }

    public void TestA5()
    {
        initialiseA();
        GeneticEngine testEngine = new GeneticEngine(APopulator, AEvaluator, AGeneticOperator, AMaxFitnessTerminator, AOutputter, null);
        testEngine.Initialise();
        testEngine.Run();
        int[] countNumbers;
        Boolean passed = true;
        countNumber = new int[100];
        currentGeneration = testEngine.Generation();

        //Check that individuals exist from 100 to 200.
        for (int i = 0; i < currentGeneration.Count; i++)
        {
            countNumber[currentGeneration.Get(i).individual.value]++;
        }
        for (int i = 0; i < currentGeneration.Count; i++)
        {
            if (countNumber[i] == 0)
            {
                passed = false;
                break;
            }
        }
        if (!passed) throw new Exception("Individuals not generated from 101-200 correctly.");

        //If no exceptions halt test, then successful:
        Console.WriteLine("Test A2 Successful");
    }

    public void TestC1()
    {
        ArrayList individuals = new ArrayList();
        //Argument for ery vertex can be reached from every other vertexRoadNetworkPopulator?
        RoadNetworkPopulator thePopulator = new RoadNetworkPopulator("pathtomapfile?");
        thePopulator.Populate(individuals);
        //Every vertex can be reached from every other vertex
        //Map start and end points match those in the roadnetworkpopulator.
        //Ensure population is made up of valid roadnetworks.
        //Ensure all are not the same.
    }

    public void TestD1()
    {
        //Do we create an instance of GeneticEngine? Where are all the plugins?
        RoadNetworkMutationOperator RNMO = new RoadNetworkMutationOperator();
        IGeneration generation = new IGeneration();
        ArrayList individuals = new ArrayList();
        //Populate with identical entries.
        RoadNetwork RN = new RoadNetwork("?");
        for (int i = 0; i < 10; i++)
        {
            generation.Insert(RN, 1);
        }
        //How do we choose mutation operator?
        //RNMO.
        RNMO.Operate(generation, individuals);
        //Ensure population is made up of valid roadnetworks.
        //Ensure all are not the same.
    }

    public void TestD2()
    {
        //Do we create an instance of GeneticEngine? Where are all the plugins?
        RoadNetworkConjugationOperator RNCO = new RoadNetworkConjugationOperator();
        IGeneration generation = new IGeneration();
        ArrayList individuals = new ArrayList();
        //Populate with identical entries.
        RoadNetwork RN = new RoadNetwork("?");
        for (int i = 0; i < 10; i++)
        {
            //Add vertex and add edge
            generation.Insert(RN, 1);
        }
        //How do we choose conjugation operator?
        //RNCO.
        RNCO.Operate(generation, individuals);
        //Ensure population is made up of valid roadnetworks.
        //Ensure all are not the same.
    }

    public void TestE1()
    {
        RoadNetworkEvaluator RoadEvaluator = new RoadNetworkEvaluator();
        IGeneration individuals = new IGeneration();
        //Where do we get the RoadNetworks we need to test?
        invididuals.add("?");
        for (int i = 0; i < numberofroadnetworks; i++)
        {
            Console.WriteLine("Checking individual: " + i + ", Fitness: " + RoadEvaluator.evaluate(individuals.get(i).individual));
        }
    }

    public void initialiseA()
    {
        APopulator = new TestAPopulator();
        AEvaluator = new TestAEvaluator();
        AGeneticOperator = new TestAGeneticOperator();
        AMaxFitnessGenerator = new MaxFitnessGenerator(200);
        AOutputter = new TestAOutputter();
    }
}


