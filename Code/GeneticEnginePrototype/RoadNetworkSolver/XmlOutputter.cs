using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticAlgorithm.Plugin;
using System.Xml;
using System.IO;

namespace RoadNetworkSolver
{
    class XmlOutputter : IOutputter
    {        
        private string path;
        

        public XmlOutputter(object config)
        {
            path = (string)config;
        }

        public void StartOutput()
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            } 
            
            Directory.CreateDirectory(path);
        }

        public void OutputGeneration(IGeneration generation, int generationNumber)
        {
            string generationPath = path + "/Generation" + generationIndex.ToString("D8");

            if (Directory.Exists(generationPath))
            {
                Directory.Delete(path, true);
            }

            Directory.CreateDirectory(generationPath);

            indexWriter.writeStartElement("generation
        }

        public void FinishOutput()
        {
        }
    }
}
