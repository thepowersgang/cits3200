using System;
using GeneticEngineCore;
using GeneticEnginePrototype;
using GeneticEngineSupport;
using GeneticEngineTesting;
using GenericPlugins;
using RoadNetworkDisplay;
using RoadNetworkOperatorTest;
using RoadNetworkSolver;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeneticEngineTesting
{
    [TestClass]
    public class TestClass
    {
        TestAPopulator APopulator;
        TestAEvaluator AEvaluator;
        TestAGeneticOperator AGeneticOperator;
        MaxFitnessTerminator AMaxFitnessTerminator;
        TestAOutputter AOutputter;
        IGeneration currentGeneration;

        public TestClass()
        {
            Console.WriteLine("Constructor running.. Completed");
        }

        [TestMethod]
        public void TestA1()
        {
            initialiseA();
            Boolean passed = true;
            int[] countNumbers;
            countNumbers = new int[150];
            GeneticEngine testEngine = new GeneticEngine(APopulator, AEvaluator, AGeneticOperator, AMaxFitnessTerminator, AOutputter, null);
            currentGeneration = testEngine.Generation;
            int temp = 0;
            //Check that 100 individuals were generated:
            if (currentGeneration.Count != 100) throw new Exception("Count not set to 100.");

            //Check that individuals exist with values 1-100 exactly once each.
            for (int i = 0; i < 100; i++)
            {
                temp = (((IntegerIndividual)(currentGeneration.Get(i)).Individual)).value;
                countNumbers[temp-1]++;
            }
            for (int i = 0; i < 100; i++)
            {
                if (countNumbers[i] != 1)
                {
                    passed = false;
                    break;
                }
            }
            //if (!passed) throw new Exception("Individuals not populated from 1-100 exactly once each");
            Assert.IsTrue(passed);           
            //If no exceptions halt test:
            Console.WriteLine("Test A1 Successful");
        }

        [TestMethod]
        public void TestA2()
        {
            initialiseA();
            Boolean passed = true;
            int[] countNumber;
            countNumber = new int[198];
            GeneticEngine testEngine = new GeneticEngine(APopulator, AEvaluator, AGeneticOperator, AMaxFitnessTerminator, AOutputter, null);          
            //testEngine.Step();
            //testEngine.Step();
            currentGeneration = testEngine.Generation;
            int temp = 0;
            //Check that individuals exist from 2 to 200.
            for (int i = 0; i < currentGeneration.Count; i++)
            {
                temp = (((IntegerIndividual)(currentGeneration.Get(i)).Individual)).value;
                countNumber[temp-1]++;
            }
            for (int i = 0; i < currentGeneration.Count; i++)
            {
                if (countNumber[i] == 0)
                {
                    passed = false;
                    break;
                }
            }
            //if (!passed) throw new Exception("Individuals not generated from 2-200 correctly.");
            Assert.IsTrue(passed);
            //If no exceptions halt test, then successful:
            Console.WriteLine("Test A2 Successful");
        }

        [TestMethod]
        public void TestA3()
        {
            Boolean passed = true;
            initialiseA();
            GeneticEngine testEngine = new GeneticEngine(APopulator, AEvaluator, AGeneticOperator, AMaxFitnessTerminator, AOutputter, null);
            //testEngine.Reset();
            if (testEngine.IsComplete) passed = false;//throw new Exception("Is Complete returns true when expected value is false.");
            testEngine.Step();
            if (testEngine.IsComplete) passed = false;// throw new Exception("Is Complete returns true when expected value is false.");
            testEngine.Repeat(99);
            if (testEngine.IsComplete) passed = false;// throw new Exception("Is Complete returns true when expected value is false.");
            testEngine.Step();
            if (!testEngine.IsComplete) passed = false;// throw new Exception("Is Complete returns false when expected value is true.");

            Assert.IsTrue(passed);
            //If no exceptions halt test, then successful:
            Console.WriteLine("Test A3 Successful");
        }

        [TestMethod] //How do we check this one?
        public void TestA4()
        {
            initialiseA();
            GeneticEngine testEngine = new GeneticEngine(APopulator, AEvaluator, AGeneticOperator, AMaxFitnessTerminator, AOutputter, null);
            //testEngine.Reset();
            testEngine.Repeat(5);
            currentGeneration = testEngine.Generation;          
            //Assert.Equals
            Assert.AreEqual(AOutputter.numberGenerations, 5);
            Assert.AreEqual(AOutputter.fitnesses[0], 99);
            Assert.AreEqual(AOutputter.fitnesses[0], 101);
            Assert.AreEqual(AOutputter.fitnesses[0], 103);
            Assert.AreEqual(AOutputter.fitnesses[0], 105);
            Assert.AreEqual(AOutputter.fitnesses[0], 107);
            //Console.WriteLine("Test A4 Completed, visually inspect output to verify.");
        }

        [TestMethod]
        public void TestA5()
        {
            initialiseA();
            GeneticEngine testEngine = new GeneticEngine(APopulator, AEvaluator, AGeneticOperator, AMaxFitnessTerminator, AOutputter, null);
            //testEngine.Reset();
            testEngine.Run();
            int[] countNumber;
            Boolean passed = true;
            countNumber = new int[101];
            currentGeneration = testEngine.Generation;
            int temp = 0;
            //Check that individuals exist from 100 to 200.
            for (int i = 0; i < currentGeneration.Count; i++)
            {
                temp = (((IntegerIndividual)(currentGeneration.Get(i)).Individual)).value;
                countNumber[temp-1]++;
            }
            for (int i = 0; i < currentGeneration.Count; i++)
            {
                if (countNumber[i] == 0)
                {
                    passed = false;
                    break;
                }
            }

            //if (!passed) throw new Exception("Individuals not generated from 101-200 correctly.");
            Assert.IsTrue(passed);

            //If no exceptions halt test, then successful:
            Console.WriteLine("Test A5 Successful");
        }
        /*
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
            RoadNetworkMutationOperator RNMO = new RoadNetworkMutationOperator();
            IGeneration generation = new IGeneration();
            ArrayList individuals = new ArrayList();
            //Populate with identical entries.
            RoadNetwork RN = new RoadNetwork(someRoadNetworkSolverObject);
            for (int i = 0; i < 10; i++)
            {
                generation.Insert(RN, 1);
            }
            RNMO.Operate(generation, individuals);
            //Ensure population is made up of valid roadnetworks.
            //Ensure all are not the same.
        }

        public void TestD2()
        {
            RoadNetworkConjugationOperator RNCO = new RoadNetworkConjugationOperator();
            IGeneration generation = new IGeneration();
            ArrayList individuals = new ArrayList();
            //Populate with identical entries.
            RoadNetwork RN = new RoadNetwork(someRoadNetworkSolverObject);
            for (int i = 0; i < 10; i++)
            {
                //Add vertex and add edge
                generation.Insert(RN, 1);
            }
            RNCO.Operate(generation, individuals);
            //Ensure population is made up of valid roadnetworks.
            //Ensure all are not the same.
        }

        public void TestE1()
        {
            RoadNetworkEvaluator RoadEvaluator = new RoadNetworkEvaluator();
            IGeneration individuals = new IGeneration();
            //Where do we get the RoadNetworks we need to test?
            individuals.Insert(theRoadNetworksWeNeedToTest, RoadEvaluator.Evaluate(theRoadNetworksWeNeedToTest));
            //Maybe we don't even have to add it to an IGeneration, just do the console writeln thing with each road network we have just like that.
            for (int i = 0; i < numberofroadnetworks; i++)
            {
                Console.WriteLine("Checking individual: " + i + ", Fitness: " + RoadEvaluator.Evaluate(individuals.Get(i).Individual));
            }
        }
        */

        public void initialiseA()
        {
            APopulator = new TestAPopulator();
            AEvaluator = new TestAEvaluator();
            AGeneticOperator = new TestAGeneticOperator();
            AMaxFitnessTerminator = new MaxFitnessTerminator((uint)200);
            AOutputter = new TestAOutputter();
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Starting tests..");
        }
    }
}

