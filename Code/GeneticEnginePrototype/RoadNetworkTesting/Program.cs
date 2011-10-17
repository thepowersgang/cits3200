using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using GeneticAlgorithm;
using GeneticAlgorithm.Util;
using GeneticAlgorithm.Plugin;
using GeneticAlgorithm.Plugin.Generic;
using GeneticAlgorithm.Plugin.Xml;
using GeneticAlgorithm.Plugin.Util;
using RoadNetworkDefinition;

namespace RoadNetworkSolver
{
    class Program
    {
        static void Save()
        {
            Random random = new Random();

            Map map = Map.FromFile("map.xml");

            IOutputter outputter = new RoadNetworkXmlOutputter("c:\\cits3200test\\roadnetworks.xml");
            outputter.OpenOutput();

            for (int g = 0; g < 10; g++)
            {
                AATreeGeneration generation = new AATreeGeneration();

                for (int i = 0; i < 20; i++)
                {
                    RoadNetwork network = new RoadNetwork(map);

                    for (int v = 0; v < 100; v++)
                    {
                        network.AddVertex(random.Next(100), random.Next(100));
                    }

                    for (int e = 0; e < 1000; e++)
                    {
                        network.AddEdge(network.GetVertex(random.Next(100)), network.GetVertex(random.Next(100)));
                    }

                    generation.Insert(network, (uint)i);
                }

                outputter.OutputGeneration(generation, g);
            }

            outputter.CloseOutput();
        }

        static void evaluate()
        {
            XmlTextReader reader = new XmlTextReader("network.xml");

            reader.MoveToContent();

            RoadNetwork network = new RoadNetwork(reader);

            Console.WriteLine("Dimensions: " + network.Map.Width + " x " + network.Map.Height);
            Console.WriteLine("Start:" + network.Map.Start.X + ", " + network.Map.Start.Y);
            Console.WriteLine("End:" + network.Map.End.X + ", " + network.Map.End.Y);

            for (int i = 0; i < network.Map.TownCount; i++)
            {
                Coordinates town = network.Map.GetTown(i);
                Console.WriteLine("Town:" + town.X + "," + town.Y);
            }

            for (int i = 0; i < network.EdgeCount; i++)
            {
                Edge edge = network.GetEdge(i);
                Console.WriteLine("Edge:" + edge.Start.Coordinates.X + "," + edge.Start.Coordinates.Y + " - " + edge.End.Coordinates.X + "," + edge.End.Coordinates.Y);
            }

            IEvaluator evaluator = new Evaluator(null);

            uint fitness = evaluator.Evaluate(network);
            float v = FitnessConverter.ToFloat(fitness);

            Console.WriteLine("Fitness: " + fitness);
            Console.WriteLine("Floating Point Fitness: " + v + " " + 1 / v);
        }

        static void mutate()
        {
            XmlTextReader reader = new XmlTextReader("network.xml");

            reader.MoveToContent();

            RoadNetwork network = new RoadNetwork(reader);

            MutationOperator mutator = new MutationOperator(null);

            RoadNetwork mutated = mutator.Mutate(network);

            XmlTextWriter writer = new XmlTextWriter("mutant.xml", Encoding.ASCII);

            writer.Formatting = Formatting.Indented;

            mutated.WriteXml(writer);

            writer.Flush();
            writer.Close();
        }

        static void mutate2()
        {
            XmlTextReader reader = new XmlTextReader("network.xml");

            reader.MoveToContent();

            RoadNetwork network = new RoadNetwork(reader);

            Mutator mutator = new Mutator(null);

            RoadNetwork mutated = mutator.Mutate(network);

            XmlTextWriter writer = new XmlTextWriter("mutant.xml", Encoding.ASCII);

            writer.Formatting = Formatting.Indented;

            mutated.WriteXml(writer);

            writer.Flush();
            writer.Close();
        }
                
        static void run()
        {
            IPopulator populator = new Populator("map.xml");
            IEvaluator evaluator = new Evaluator(null);
            IGeneticOperator mutator = new Mutator(null);
            ITerminator terminator = new FitnessThresholdTerminator(FitnessConverter.FromFloat(1.0f / 1024.0f));
            IOutputter outputter = new RoadNetworkXmlOutputter(@"c:\roadnetworktest\index.xml");

            GeneticEngine engine = new GeneticEngine(populator, evaluator, mutator, terminator, outputter);
            engine.Repeat(100);
            engine.FinishOutput();
        }

        static void Main(string[] args)
        {

            Mutator mutator = new Mutator(null);
            
            Map map = Map.FromFile("map.xml");
            RoadNetwork network = new RoadNetwork(map);

            network.AddVertex(map.Start);
            network.AddVertex(map.End);
            network.AddEdge(network.Start, network.End);

            StepMutator sm = new StepMutator();

            network = sm.MakeSteps(network);

            network = mutator.Mutate(network);

            network = sm.MakeSteps(network);

            network = mutator.Mutate(network);

            network = sm.MakeSteps(network);

            network = mutator.Mutate(network);

            network = sm.MakeSteps(network);

            network = mutator.Mutate(network);

            //XmlTextWriter writer = new XmlTextWriter("network.xml", Encoding.ASCII);
            //writer.Formatting = Formatting.Indented;
            //network.WriteXml(writer);
            //writer.Flush();
            //writer.Close();

            Console.ReadLine();
        }
    }
}
