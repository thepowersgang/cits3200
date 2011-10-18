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
    /// <summary>
    /// Outputter plugin that writes generations to a set of XML files
    /// </summary>
    public abstract class XmlOutputter : IOutputter
    {     
        /// <summary>
        /// The path to write the index file to
        /// </summary>
        private string path;

        /// <summary>
        /// The directory the index file will be written to
        /// </summary>
        private string directory;

        /// <summary>
        /// The name of the file without it's directory and extension.
        /// This will be used to name the subdirectories
        /// </summary>
        private string prefix;

        /// <summary>
        /// The list of generations which will be written to the index.
        /// </summary>
        private GenerationIndex index;

        /// <summary>
        /// Gets the status of the IOutputter
        /// Always returns Open.
        /// </summary>
        public OutputterStatus Status
        {
            get
            {
                return OutputterStatus.Open;
            }
        }
 
        /// <summary>
        /// Initialises a new XmlOutputter
        /// </summary>
        /// <param name="path">The path to save the index file to</param>
        public XmlOutputter(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path must not be null");
            }

            this.path = path;
            index = new GenerationIndex(path);
            directory = Path.GetDirectoryName(path);
            prefix = Path.GetFileNameWithoutExtension(path);  
        }

        /// <summary>
        /// Writes an individual as XML
        /// </summary>
        /// <param name="individual">The individual to write</param>
        /// <param name="writer">The XmlWriter to write to</param>
        abstract protected void WriteIndividual(object individual, XmlWriter writer);

        /// <summary>
        /// Prepare the IOutputter to recieve output.
        /// Do nothing.
        /// </summary>
        public void OpenOutput()
        {             
        }
        
        /// <summary>
        /// Output a generation.
        /// Writes the generation and each individual to an XML file.
        /// </summary>
        /// <param name="generation">The generation to write.</param>
        /// <param name="generationNumber">The number of generations before this one.</param>
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

        /// <summary>
        /// Finish outputting.
        /// Write the index file.
        /// </summary>
        public void CloseOutput()
        {
            index.Save();
        }
    }
}
