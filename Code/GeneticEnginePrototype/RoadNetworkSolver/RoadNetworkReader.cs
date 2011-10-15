using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using GeneticAlgorithm.Plugin.Xml;

namespace RoadNetworkSolver
{
    public class RoadNetworkReader : IIndividualReader
    {
        public bool HandleTag(string name)
        {
            return name == "network";
        }

        public object ReadIndividual(XmlReader reader)
        {
            return new RoadNetwork(reader);
        }
    }
}
