using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace GeneticAlgorithm.Plugin.Xml
{
    public interface IIndividualReader
    {
        bool HandleTag(string name);
        object ReadIndividual(XmlReader reader);
    }
}
