using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace RoadNetworkSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlTextReader mapreader = new XmlTextReader("map.xml");

            Map map = new Map(mapreader);

            mapreader.Close();

            XmlTextWriter writer = new XmlTextWriter("test.xml", Encoding.ASCII);

            writer.Formatting = Formatting.Indented;

            RoadNetwork network = new RoadNetwork(map);

            network.AddVertex(1, 2);
            network.AddVertex(2, 3);
            network.AddVertex(3, 4);

            network.AddEdge(network.GetVertex(0), network.GetVertex(1));
            network.AddEdge(network.GetVertex(1), network.GetVertex(2));

            network.WriteXml(writer);

            writer.Flush();
            writer.Close();

            XmlTextReader reader = new XmlTextReader("test.xml");

            RoadNetwork network2 = new RoadNetwork(map);

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name.Equals("network"))
                {
                    network2 = new RoadNetwork(map, reader);
                }
            }

            reader.Close();

            Console.WriteLine(network2.VertexCount);
            Console.WriteLine(network2.EdgeCount);
            Console.ReadLine();
        }
    }
}
