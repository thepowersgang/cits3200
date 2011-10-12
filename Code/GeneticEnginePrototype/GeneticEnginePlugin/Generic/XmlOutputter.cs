using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticAlgorithm.Plugin;
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
                
        public XmlOutputter(string path)
        {
            this.path = path;            
        }

        abstract protected void WriteIndividual(object individual, XmlWriter writer);

        public void StartOutput()
        {
            index = new GenerationIndex();
            index.StartTime = DateTime.Now;

            directory = Path.GetDirectoryName(path);
            prefix = Path.GetFileNameWithoutExtension(path);            
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
            generationWriter.WriteAttributeString("count", generation.Count.ToString());
            generationWriter.WriteAttributeString("min", generation.MinFitness.ToString());
            generationWriter.WriteAttributeString("max", generation.MaxFitness.ToString());
            generationWriter.WriteAttributeString("average", generation.AverageFitness.ToString());

            for (int i = 0; i < generation.Count; i++)
            {
                IndividualWithFitness individualWithFitness = generation[i];

                string individualPath = "/individual" + i.ToString("D8") + ".xml";
                XmlTextWriter individualWriter  = new XmlTextWriter(generationDirectoryAbsolute+individualPath, Encoding.ASCII);
                individualWriter.Formatting = Formatting.Indented;

                individualWriter.WriteStartElement("individual");
                individualWriter.WriteAttributeString("fitness", individualWithFitness.Fitness.ToString());
                WriteIndividual(individualWithFitness.Individual, individualWriter);
                individualWriter.WriteEndElement();
                individualWriter.Flush();
                individualWriter.Close();

                generationWriter.WriteStartElement("individual");
                generationWriter.WriteAttributeString("index", i.ToString());
                generationWriter.WriteAttributeString("path", "." + individualPath);
                generationWriter.WriteEndElement();
            }

            generationWriter.WriteEndElement();
            generationWriter.Flush();
            generationWriter.Close();

            index.AddGeneration(generation, generationNumber, "." + generationPath);
        }

        public void FinishOutput()
        {
            index.FinishTime = DateTime.Now;

            XmlTextWriter indexWriter = new XmlTextWriter(path, Encoding.ASCII);
            indexWriter.Formatting = Formatting.Indented;

            index.WriteXml(indexWriter);
            
            indexWriter.Flush();
            indexWriter.Close();
        }
    }
}
