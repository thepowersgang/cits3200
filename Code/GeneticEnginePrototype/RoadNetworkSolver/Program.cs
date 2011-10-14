using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using GeneticAlgorithm.Plugin;
using GeneticAlgorithm.Plugin.Generic;
using GeneticAlgorithm.Plugin.Xml;

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
            GenerationIndex results = GenerationIndex.Load("c:\\cits3200test\\roadnetworks.xml");

            Console.WriteLine(results[0].Count);

            IGeneration generation = results[0].LoadGeneration(new RoadNetworkReader());



            Console.WriteLine(generation.Count);
            
            Console.WriteLine(((RoadNetwork)generation[0].Individual).Map.Width);

            Console.ReadLine();
        }
    }
}
