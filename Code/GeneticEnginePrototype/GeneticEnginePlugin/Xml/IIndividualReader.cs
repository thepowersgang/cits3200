using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace GeneticAlgorithm.Plugin.Xml
{
    /// <summary>
    /// Generates an object from XML
    /// </summary>
    public interface IIndividualReader
    {
        /// <summary>
        /// Determine if a certain XML element marks the start of an object.
        /// </summary>
        /// <param name="name">The element name</param>
        /// <returns>True if the element marks the start of an object.</returns>
        bool HandleTag(string name);

        /// <summary>
        /// Generate an object.
        /// </summary>
        /// <param name="reader">The XmlReader to read the object data from.</param>
        /// <returns>The object.</returns>
        object ReadIndividual(XmlReader reader);
    }
}
