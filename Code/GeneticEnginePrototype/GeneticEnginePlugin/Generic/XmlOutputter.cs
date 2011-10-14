using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticAlgorithm.Plugin;
using GeneticAlgorithm.Plugin.Xml;
using System.Xml;
using System.IO;

namespace GeneticAlgorithm.Plugin.Generic
{
    public abstract class XmlOutputter : IOutputter
    {        
        private string path;
        private string directory;
        private string prefix;
        private GenerationIndex index;

        public OutputterStatus Status
        {
            get
            {
                return OutputterStatus.Open;
            }
        }
 
        public XmlOutputter(string path)
        {
            this.path = path;
            index = new GenerationIndex(path);
            directory = Path.GetDirectoryName(path);
            prefix = Path.GetFileNameWithoutExtension(path);  
        }

        abstract protected void WriteIndividual(object individual, XmlWriter writer);

        public void OpenOutput()
        {             
        }
        
        public void OutputGeneration(IGeneration generation, int generationNumber)
        {                        
            string generationDirectory = "/" + prefix + generationNumber.ToString("D8");
            string generationDirectoryAbsolute = directory + generationDirectory;

            if (!Directory.Exists(generationDirectoryAbsolute))
            {
                Directory.CreateDirectory(generationDirectoryAbsolute);
            }

            string generationPath = generationDirectory + "/generation.xml";

            XmlTextWriter generationWriter = new XmlTextWriter(directory + generationPath, Encoding.ASCII);
            generationWriter.Formatting = Formatting.Indented;

            generationWriter.WriteStartElement("generation");

            for (int i = 0; i < generation.Count; i++)
            {
                IndividualWithFitness individualWithFitness = generation[i];

                string individualPath = "/individual" + i.ToString("D8") + ".xml";
                XmlTextWriter individualWriter  = new XmlTextWriter(generationDirectoryAbsolute+individualPath, Encoding.ASCII);
                individualWriter.Formatting = Formatting.Indented;

                WriteIndividual(individualWithFitness.Individual, individualWriter);                
                individualWriter.Flush();
                individualWriter.Close();

                generationWriter.WriteStartElement("individual");
                generationWriter.WriteAttributeString("fitness", individualWithFitness.Fitness.ToString());
                generationWriter.WriteAttributeString("path", "." + individualPath);
                generationWriter.WriteEndElement();
            }

            generationWriter.WriteEndElement();
            generationWriter.Flush();
            generationWriter.Close();

            index.AddGeneration(generation, "." + generationPath);
        }

        public void CloseOutput()
        {
            index.Save();
        }
    }
}
