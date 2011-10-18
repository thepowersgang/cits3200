using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using GeneticAlgorithm.Plugin.Xml;

namespace RoadNetworkDefinition
{
    /// <summary>
    /// Describes the rules for loading a RoadNetwork from XML
    /// </summary>
    public class RoadNetworkReader : IIndividualReader
    {
        /// <summary>
        /// Determined if an XML element marks the start of a RoadNetwork
        /// </summary>
        /// <param name="name">The name of the XML element</param>
        /// <returns>True when name is "network" otherwise false.</returns>
        public bool HandleTag(string name)
        {
            return name == "network";
        }

        /// <summary>
        /// Load an RoadNetwork from XML
        /// </summary>
        /// <param name="reader">The XmlReader to load from.</param>
        /// <returns>The RoadNetwork</returns>
        public object ReadIndividual(XmlReader reader)
        {
            return new RoadNetwork(reader);
        }
    }
}
