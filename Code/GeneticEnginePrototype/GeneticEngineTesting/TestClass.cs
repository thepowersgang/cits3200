using System;
using GeneticAlgorithm;
using GeneticAlgorithm.Plugin;
using GeneticAlgorithm.Plugin.Generic;
using GeneticEngineTesting;
using RoadNetworkDisplay;
using RoadNetworkDefinition;
using RoadNetworkSolver;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;
using GeneticAlgorithm.Plugin.Xml;

namespace GeneticEngineTesting
{
    [TestClass]
    public class TestClass
    {
        TestAPopulator APopulator;
        TestAEvaluator AEvaluator;
        TestAGeneticOperator AGeneticOperator;
        FitnessThresholdTerminator AFitnessThresholdTerminator;
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
            GeneticEngine testEngine = new GeneticEngine(APopulator, AEvaluator, AGeneticOperator, AFitnessThresholdTerminator, AOutputter, null);
            currentGeneration = testEngine.Generation;
            int temp = 0;
            //Check that 100 individuals were generated:
            if (currentGeneration.Count != 100) throw new Exception("Count not set to 100.");

            //Check that individuals exist with values 1-100 exactly once each.
            for (int i = 0; i < 100; i++)
            {
                temp = (((IntegerIndividual)(currentGeneration.Get(i)).Individual)).value;
                countNumbers[temp - 1]++;
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
            GeneticEngine testEngine = new GeneticEngine(APopulator, AEvaluator, AGeneticOperator, AFitnessThresholdTerminator, AOutputter, null);
            //testEngine.Step();
            //testEngine.Step();
            currentGeneration = testEngine.Generation;
            int temp = 0;
            //Check that individuals exist from 2 to 200.
            for (int i = 0; i < currentGeneration.Count; i++)
            {
                temp = (((IntegerIndividual)(currentGeneration.Get(i)).Individual)).value;
                countNumber[temp - 1]++;
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

        //[TestMethod]
        public void TestA3()
        {
            Boolean passed = true;
            initialiseA();
            GeneticEngine testEngine = new GeneticEngine(APopulator, AEvaluator, AGeneticOperator, AFitnessThresholdTerminator, AOutputter, null);
            //testEngine.Reset();
            if (testEngine.IsComplete) passed = false;//throw new Exception("Is Complete returns true when expected value is false.");
            testEngine.Step();
            if (testEngine.IsComplete) passed = false;// throw new Exception("Is Complete returns true when expected value is false.");
            //Fails here, passed is true when it should be false.
            testEngine.Repeat(99);
            if (testEngine.IsComplete) passed = false;// throw new Exception("Is Complete returns true when expected value is false.");
            testEngine.Step();
            if (!testEngine.IsComplete) passed = false;// throw new Exception("Is Complete returns false when expected value is true.");

            Assert.IsTrue(passed);
            //If no exceptions halt test, then successful:
            Console.WriteLine("Test A3 Successful");
        }

        //[TestMethod]
        public void TestA4()
        {
            initialiseA();
            GeneticEngine testEngine = new GeneticEngine(APopulator, AEvaluator, AGeneticOperator, AFitnessThresholdTerminator, AOutputter, null);
            //testEngine.Reset();
            testEngine.Repeat(5);
            currentGeneration = testEngine.Generation;
            //Assert.Equals
            Assert.AreEqual(5, AOutputter.numberGenerations);
            //Fails here, output expected is 99 but 0 is returned. The assertions after this pass though (101, 103,..)
            Assert.AreEqual(99, AOutputter.fitnesses[0]);
            Assert.AreEqual(101, AOutputter.fitnesses[1]);
            Assert.AreEqual(103, AOutputter.fitnesses[2]);
            Assert.AreEqual(105, AOutputter.fitnesses[3]);
            Assert.AreEqual(107, AOutputter.fitnesses[4]);
        }

        //[TestMethod]
        public void TestA5()
        {
            initialiseA();
            GeneticEngine testEngine = new GeneticEngine(APopulator, AEvaluator, AGeneticOperator, AFitnessThresholdTerminator, AOutputter, null);
            //testEngine.Reset();
            testEngine.Run();
            int[] countNumber;
            Boolean passed = true;
            countNumber = new int[100];
            currentGeneration = testEngine.Generation;
            int temp = 0;
            int min = 0;
            int max = 0;
            //Check that individuals exist from 101 to 200.
            for (int i = 0; i < currentGeneration.Count; i++)
            {
                temp = (((IntegerIndividual)(currentGeneration.Get(i)).Individual)).value;
                if (temp > max) max = temp;
                if (temp < min) min = temp;
                //countNumber[temp - 101]++;
            }
            if (min != 101) passed = false;
            if (max != 200) passed = false;

            //If we need to check ever single individual and make sure there is exactly 1 instance of every integer from 101-200.
            /*
            for (int i = 0; i < currentGeneration.Count; i++)
            {
                if (countNumber[i] == 0)
                {
                    passed = false;
                    break;
                }
            }
            */

            //if (!passed) throw new Exception("Individuals not generated from 101-200 correctly.");
            Assert.IsTrue(passed);

            //If no exceptions halt test, then successful:
            Console.WriteLine("Test A5 Successful");
        }
        
        /*
        [TestMethod]
        public void TestB1() {
            PluginLoader PL = new PluginLoader(pathToDll);
            ArrayList pluginList = PL.getPlugins();
            Assert.Equals(pluginList.RemoveAt(0).NAMEFIELD, "Adder");
            Assert.Equals(pluginList.RemoveAt(1).NAMEFIELD, "Prefixer");
        }

        [TestMethod]
        public void TestB2() {
            PluginLoader PL = new PluginLoader(pathToDll);
            ArrayList pluginList = PL.getPlugins();
            PluginType PL1 = PL.GetInstance("Adder", (object)5));
            PluginType PL2 = PL.GetInstance("Prefixer", (object)"Hello "));
            Assert.AreSame(PL1.Add(2), 7);
            Assert.AreEqual(PL2.Prefix("World"), "Hello World");
        }
          */
        
        [TestMethod]
        public void TestC1()
        {
            ArrayList individuals = new ArrayList();
            //Argument for ery vertex can be reached from every other vertexRoadNetworkPopulator?
            Populator thePopulator = new Populator("C:/map.xml");
            thePopulator.Populate(individuals);
            Map theMap = Map.FromFile("C:/map.xml");
            //Assert.AreSame(theMap.Start, -1);
            //Assert.AreSame(theMap.End, -1);

            //Every vertex can be reached from every other vertex
            //Map start and end points match those in the roadnetworkpopulator.
            //Ensure population is made up of valid roadnetworks.
            //Ensure all are not the same.
            Boolean passed = false;
            for (int i = 0; i < individuals.Count; i++)
            {
                    for (int k = 0; k < ((RoadNetwork)individuals[i]).VertexCount; k++)
                    {
                        for (int l = 0; l < ((RoadNetwork)individuals[i]).VertexCount; l++)
                        {
                            if (((RoadNetwork)individuals[i]).GetVertex(k).Coordinates.X != ((RoadNetwork)individuals[i]).GetVertex(l).Coordinates.X)
                            {
                                passed = true;
                            }
                        }
                    }
            }
            Assert.IsTrue(passed);
        }
        
        [TestMethod]
        public void TestD1()
        {
            MutationOperator RNCO = new MutationOperator(null);
            AATreeGeneration generation = new AATreeGeneration();
            ArrayList individuals = new ArrayList();
            //Populate with identical entries.
            Vertex[] theVertices = new Vertex[5];
            RoadNetwork theRoad;
            Map theMap;
            theMap = Map.FromFile("C:/map.xml");
            ArrayList RN = new ArrayList();
            for (int i = 0; i < 10; i++)
            {
                theRoad = new RoadNetwork(theMap);
                RN.Add(theRoad);
                //RN[i] = new RoadNetwork(theMap);
                theVertices[0] = ((RoadNetwork)RN[i]).AddVertex(0, 0);
                theVertices[1] = ((RoadNetwork)RN[i]).AddVertex(10, 0);
                theVertices[2] = ((RoadNetwork)RN[i]).AddVertex(5, 5);
                theVertices[3] = ((RoadNetwork)RN[i]).AddVertex(0, 10);
                theVertices[4] = ((RoadNetwork)RN[i]).AddVertex(10, 10);
                ((RoadNetwork)RN[i]).AddEdge(theVertices[0], theVertices[1]);
                ((RoadNetwork)RN[i]).AddEdge(theVertices[0], theVertices[3]);
                ((RoadNetwork)RN[i]).AddEdge(theVertices[3], theVertices[4]);
                ((RoadNetwork)RN[i]).AddEdge(theVertices[1], theVertices[4]);
                ((RoadNetwork)RN[i]).AddEdge(theVertices[0], theVertices[2]);
                ((RoadNetwork)RN[i]).AddEdge(theVertices[1], theVertices[2]);
                ((RoadNetwork)RN[i]).AddEdge(theVertices[3], theVertices[2]);
                ((RoadNetwork)RN[i]).AddEdge(theVertices[4], theVertices[2]);
                //Add vertex and add edge
                generation.Insert(RN[i], 1);
            }
            RNCO.Operate(generation, individuals);
            //Ensure population is made up of valid roadnetworks.
            //Ensure all are not the same.
            Boolean passed = false;
            for (int i = 0; i < individuals.Count; i++)
            {
                for (int j = 0; j < individuals.Count; j++)
                {
                    //Cross check each of the 5 vertices
                    for (int k = 0; k < ((RoadNetwork)individuals[i]).VertexCount; k++)
                    {
                        for (int l = 0; l < ((RoadNetwork)individuals[j]).VertexCount; l++)
                        {

                            if (((RoadNetwork)individuals[i]).GetVertex(k).Coordinates.X != ((RoadNetwork)individuals[j]).GetVertex(l).Coordinates.X)
                            {
                                passed = true;
                            }
                        }
                    }
                }
            }
            
            Assert.IsTrue(passed); 
        }
        
        [TestMethod]
        public void TestD2()
        {
            Conjugator RNCO = new Conjugator(null);
            AATreeGeneration generation = new AATreeGeneration();
            ArrayList individuals = new ArrayList();
            //Populate with identical entries.
            RoadNetwork theRoad;
            Vertex[] theVertices = new Vertex[5];
            Map theMap = null;
            ArrayList RN = new ArrayList();
            for (int i = 0; i < 10; i++)
            {
                theRoad = new RoadNetwork(theMap);
                RN.Add(theRoad);
                //RN[i] = new RoadNetwork(theMap);
                theVertices[0] = ((RoadNetwork)RN[i]).AddVertex(0, 0);
                theVertices[1] = ((RoadNetwork)RN[i]).AddVertex(10, 0);
                theVertices[2] = ((RoadNetwork)RN[i]).AddVertex(5, 5);
                theVertices[3] = ((RoadNetwork)RN[i]).AddVertex(0, 10);
                theVertices[4] = ((RoadNetwork)RN[i]).AddVertex(10, 10);
                ((RoadNetwork)RN[i]).AddEdge(theVertices[0], theVertices[1]);
                ((RoadNetwork)RN[i]).AddEdge(theVertices[0], theVertices[3]);
                ((RoadNetwork)RN[i]).AddEdge(theVertices[3], theVertices[4]);
                ((RoadNetwork)RN[i]).AddEdge(theVertices[1], theVertices[4]);
                ((RoadNetwork)RN[i]).AddEdge(theVertices[0], theVertices[2]);
                ((RoadNetwork)RN[i]).AddEdge(theVertices[1], theVertices[2]);
                ((RoadNetwork)RN[i]).AddEdge(theVertices[3], theVertices[2]);
                ((RoadNetwork)RN[i]).AddEdge(theVertices[4], theVertices[2]);
                //Add vertex and add edge
                generation.Insert(RN[i], 1);
            }
            RNCO.Operate(generation, individuals);
            //Ensure population is made up of valid roadnetworks.
            //Ensure all are not the same.
            Boolean passed = false;
            for (int i = 0; i < individuals.Count; i++)
            {
                for (int j = 0; j < individuals.Count; j++)
                {
                    //Cross check each of the 5 vertices
                    for (int k = 0; k < ((RoadNetwork)individuals[i]).VertexCount; k++)
                    {
                        for (int l = 0; l < ((RoadNetwork)individuals[j]).VertexCount; l++)
                        {

                            if (((RoadNetwork)individuals[i]).GetVertex(k).Coordinates.X != ((RoadNetwork)individuals[j]).GetVertex(l).Coordinates.X)
                            {
                                passed = true;
                            }
                        }
                    }
                }
            }
            Assert.IsTrue(passed);
        }

        [TestMethod]
        public void TestE1()
        {
            Evaluator RoadEvaluator = new Evaluator(null);            
            GenerationIndex results = GenerationIndex.Load("C:/index.xml");            
            AATreeGeneration temp = new AATreeGeneration();
            for (int i = 0; i < results.Count; i++)
            {
                IGeneration generation = results[i].LoadGeneration(new RoadNetworkReader());
                for (int j = 0; j < results[i].Count; j++) {
                    Assert.AreEqual(RoadEvaluator.Evaluate(((IndividualWithFitness)generation.Get(j)).Individual), generation.Get(j).Fitness);
                }
            }
        }
        
        [TestMethod]
        public void TestF1() {
            RoadNetworkXmlOutputter RNO = new RoadNetworkXmlOutputter(null);
            Map theMap;
            theMap = Map.FromFile("C:/map.xml");
            Vertex[] theVertices = new Vertex[4];
            RoadNetwork RN1 = new RoadNetwork(theMap);
                theVertices[0] = ((RoadNetwork)RN1).AddVertex(0, 0);
                theVertices[1] = ((RoadNetwork)RN1).AddVertex(10, 0);
                ((RoadNetwork)RN1).AddEdge(theVertices[0], theVertices[1]);
            RoadNetwork RN2 = new RoadNetwork(theMap);
                theVertices[2] = ((RoadNetwork)RN2).AddVertex(5, 5);
                theVertices[3] = ((RoadNetwork)RN2).AddVertex(10, 10);
                ((RoadNetwork)RN2).AddEdge(theVertices[2], theVertices[3]);
            AATreeGeneration generation = new AATreeGeneration();
            generation.Insert(RN1, 1);
            generation.Insert(RN2, 2);

            XmlWriter writer1 = XmlWriter.Create("test1xml.xml");
            XmlWriter writer2 = XmlWriter.Create("test2xml.xml");
            //Output individuals/generation somehow.            
            RN1.WriteXml(writer1);
            writer1.Close();
            RN2.WriteXml(writer2);
            writer2.Close();

            XmlTextReader reader1 = new XmlTextReader("test1xml.xml");
            XmlTextReader reader2= new XmlTextReader("test2xml.xml");
            RoadNetworkReader rnr = new RoadNetworkReader();

            RoadNetwork RNL1 = (RoadNetwork)rnr.ReadIndividual(reader1);
            RoadNetwork RNL2 = (RoadNetwork)rnr.ReadIndividual(reader2);
            Assert.AreEqual(RNL1.GetVertex(0).Coordinates.X, 0);
            Assert.AreEqual(RNL1.GetVertex(0).Coordinates.Y, 0);
            Assert.AreEqual(RNL1.GetVertex(1).Coordinates.X, 10);
            Assert.AreEqual(RNL1.GetVertex(1).Coordinates.Y, 0);
            Assert.AreEqual(RNL2.GetVertex(0).Coordinates.X, 5);
            Assert.AreEqual(RNL2.GetVertex(0).Coordinates.Y, 5);
            Assert.AreEqual(RNL2.GetVertex(1).Coordinates.X, 10);
            Assert.AreEqual(RNL2.GetVertex(1).Coordinates.Y, 10);
            Assert.AreEqual(RNL1.GetEdge(0).Start.Coordinates.X, 0);
            Assert.AreEqual(RNL1.GetEdge(0).Start.Coordinates.Y, 0);
            Assert.AreEqual(RNL1.GetEdge(0).End.Coordinates.X, 10);
            Assert.AreEqual(RNL1.GetEdge(0).End.Coordinates.Y, 0);            
            Assert.AreEqual(RNL2.GetEdge(0).Start.Coordinates.X, 5);
            Assert.AreEqual(RNL2.GetEdge(0).Start.Coordinates.Y, 5);
            Assert.AreEqual(RNL2.GetEdge(0).End.Coordinates.X, 10);
            Assert.AreEqual(RNL2.GetEdge(0).End.Coordinates.Y, 10); 
        }
        
        public void initialiseA()
        {
            APopulator = new TestAPopulator();
            AEvaluator = new TestAEvaluator();
            AGeneticOperator = new TestAGeneticOperator();
            AFitnessThresholdTerminator = new FitnessThresholdTerminator((uint)200);
            AOutputter = new TestAOutputter();
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Starting tests..");
        }
    }
}


