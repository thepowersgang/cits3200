using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticAlgorithm.Plugin;
using System.Xml;

namespace RoadNetworkSolver
{
    class XmlOutputter : IOutputter
    {
        private string filename;

        public XmlOutputter(object config)
        {
            filename = (string)config;
        }

        public void Initialise()
        {
        }

        public void OutputGeneration(IGeneration generation, int generationIndex)
        {
        }

        public void Finalise()
        {
        }
    }
}
