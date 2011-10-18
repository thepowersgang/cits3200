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

            RoadNetwork mutated = Mutator.Mutate(network);

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

        static RoadNetwork RandomNetwork(Map map)
        {
            Random random = new Random();

            RoadNetwork network = new RoadNetwork(map);

            network.AddVertex(map.Start);
            network.AddVertex(map.End);

            Vertex start = network.Start;
            Vertex end = network.End;
            
            for (int i = 0; i < 2; i++)
            {
                Vertex startPoint = start;

                for (int j = 0; j < 10; j++)
                {
                    Vertex endPoint = network.AddVertex(random.Next(map.Width), random.Next(map.Height));
                    network.AddEdge(startPoint, endPoint);
                    startPoint = endPoint;
                }

                network.AddEdge(startPoint, end);
            }

            for (int k = 0; k < 80; k++)
            {
                Vertex startPoint = network.GetVertex(random.Next(network.VertexCount));
                Vertex endPoint = network.AddVertex(random.Next(map.Width), random.Next(map.Height));
                network.AddEdge(startPoint, endPoint);
            }

            for (int l = 0; l < 20; l++)
            {
                Vertex startPoint = network.GetVertex(random.Next(network.VertexCount));
                Vertex endPoint = network.GetVertex(random.Next(network.VertexCount));
                network.AddEdge(startPoint, endPoint);
            }

            network.SetEnd(1);
            return network;
        }

        static void Main(string[] args)
        {
            Coordinates c = new Coordinates(123, 456);
            Coordinates d = new Coordinates(321, 654);

            Dictionary<Coordinates, int> indices = new Dictionary<Coordinates, int>();

            indices.Add(new Coordinates(123, 456), 1);
            indices.Add(new Coordinates(321, 654), 2);

            Console.WriteLine(indices[c]);
            Console.WriteLine(indices[d]);

            Console.ReadLine();
        }
    }
}
