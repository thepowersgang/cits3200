using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using GeneticAlgorithm.Plugin.Generic;
using RoadNetworkDefinition;

namespace RoadNetworkSolver
{
    /// <summary>
    /// IOutputter which saves generations and individuals as XML files with an index file listing each generation.
    /// </summary>
    public class RoadNetworkXmlOutputter : XmlOutputter
    {
        /// <summary>
        /// Initialises a new RoadNetworkXmlOutputter
        /// </summary>
        /// <param name="config">A string containing the path to save the index file to</param>
        public RoadNetworkXmlOutputter(object config)
            : base((string)config)
        {            
        }

        /// <summary>
        /// Write a RoadNetwork to XML
        /// </summary>
        /// <param name="individual">The RoadNetwork to write</param>
        /// <param name="writer">The XmlWriter to write to</param>
        protected override void WriteIndividual(object individual, XmlWriter writer)
        {
            ((RoadNetwork)individual).WriteXml(writer);
        }
    }
}
