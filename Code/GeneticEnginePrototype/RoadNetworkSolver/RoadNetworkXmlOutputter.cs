using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using GeneticAlgorithm.Plugin.Generic;

namespace RoadNetworkSolver
{
    class RoadNetworkXmlOutputter : XmlOutputter
    {
        public RoadNetworkXmlOutputter(object config)
            : base((string)config)
        {            
        }

        protected override void WriteIndividual(object individual, XmlWriter writer)
        {
            ((RoadNetwork)individual).WriteXml(writer);
        }
    }
}
