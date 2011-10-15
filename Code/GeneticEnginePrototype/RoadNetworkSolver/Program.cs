using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using GeneticAlgorithm.Plugin;
using GeneticAlgorithm.Plugin.Generic;
using GeneticAlgorithm.Plugin.Xml;
using GeneticAlgorithm.Plugin.Util;

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
        
        static void Main(string[] args)
        {
            XmlTextReader reader = new XmlTextReader("network.xml");

            reader.MoveToContent();

            RoadNetwork network = new RoadNetwork(reader);

            Console.WriteLine("Dimensions: " + network.Map.Width+ " x " + network.Map.Height);
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
            Console.WriteLine("Floating Point Fitness: " + v + " " + 1/v);

            Console.ReadLine();
        }
    }
}
