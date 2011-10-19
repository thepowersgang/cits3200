using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using GeneticAlgorithm.Plugin;
using GeneticAlgorithm.Plugin.Generic;

namespace GeneticAlgorithm.Plugin.Xml
{
    /// <summary>
    /// Manages the data in an XML index file which lists the generations 
    /// </summary>
    public class GenerationIndex : List<GenerationIndex.Entry>
    {
        /// <summary>
        /// The filename of the index
        /// </summary>
        private string filename;

        /// <summary>
        /// Initialise a new GenerationIndex
        /// </summary>
        /// <param name="filename">The filename the index will be saved as.</param>
        public GenerationIndex(string filename)
        {
            this.filename = filename;
        }

        /// <summary>
        /// Initialise a new GenerationIndex from an XML file.
        /// </summary>
        /// <param name="filename">The name of the file being loaded.</param>
        /// <param name="reader">The XmlReader to load the data from.</param>
        public GenerationIndex(string filename, XmlReader reader)
        {
            this.filename = filename;

            int depth = 1;

            while (depth > 0 && reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        switch (reader.Name)
                        {
                            case "generation":
                                Add(new Entry(this, reader));
                                break;
                        }

                        if (!reader.IsEmptyElement)
                        {
                            depth++;
                        }

                        break;

                    case XmlNodeType.EndElement:
                        depth--;
                        break;
                }

            }
        }

        /// <summary>
        /// Write the index to XML
        /// </summary>
        /// <param name="writer">The XmlWriter to write to.</param>
        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("results");

            foreach (Entry entry in this)
            {
                entry.WriteXml(writer);
            }

            writer.WriteEndElement();
        }

        /// <summary>
        /// Save the index file.
        /// </summary>
        public void Save()
        {
            XmlTextWriter writer = new XmlTextWriter(filename, Encoding.ASCII);
            writer.Formatting = Formatting.Indented;

            WriteXml(writer);

            writer.Flush();
            writer.Close();
        }

        /// <summary>
        /// Add a generation to the index file.
        /// </summary>
        /// <param name="generation">The generation begin added.</param>
        /// <param name="generationPath">The path to where the generation has been saved.</param>
        public void AddGeneration(IGeneration generation, string generationPath)
        {
            Add(new Entry(this, generation, generationPath));
        }

        /// <summary>
        /// Load a GenerationIndex from an XML file.
        /// </summary>
        /// <param name="filename">The name of the file to load.</param>
        /// <returns>The GenerationIndex loaded.</returns>
        public static GenerationIndex Load(string filename)
        {            
            XmlTextReader reader = new XmlTextReader(filename);
            reader.MoveToContent();

            if (reader.Name != "results")
            {
                throw new Exception("Results XML file must have <results> element as root.");
            }

            GenerationIndex index = new GenerationIndex(filename, reader);
            reader.Close();
            return index;
        }
        
        /// <summary>
        /// Holds an entry in a GenerationIndex
        /// </summary>
        public class Entry
        {
            /// <summary>
            /// The GenerationIndex which contains this Entry
            /// </summary>
            private GenerationIndex index;

            /// <summary>
            /// The path to where the generation was saved.
            /// </summary>
            private string generationPath;

            /// <summary>
            /// The number of indivdals in the generation.
            /// </summary>
            private int count;

            /// <summary>
            /// The minimum fitness of all individuals in the generation.
            /// </summary>
            private uint minFitness;

            /// <summary>
            /// The maximum fitness of all individuals in the generation.
            /// </summary>
            private uint maxFitness;

            /// <summary>
            /// The average fitness of all individuals in the generation.
            /// </summary>
            private float averageFitness;

            /// <summary>
            /// Get the number of individuals in the generation.
            /// </summary>
            public int Count
            {
                get
                {
                    return count;
                }
            }

            /// <summary>
            /// Get the minimum fitness value of all individuals in the generation.
            /// </summary>
            public uint MinFitness
            {
                get
                {
                    return minFitness;
                }
            }

            /// <summary>
            /// Get the maximum fitness value of all individuals in the generation.
            /// </summary>
            public uint MaxFitness
            {
                get
                {
                    return maxFitness;
                }
            }

            /// <summary>
            /// Get the average fitness value of the individuals in the generation.
            /// </summary>
            public float AverageFitness
            {
                get
                {
                    return averageFitness;
                }
            }

            /// <summary>
            /// Initialise a new Entry
            /// </summary>
            /// <param name="index">The GenerationIndex this Entry belongs to.</param>
            /// <param name="generation">The generation this entry represents.</param>
            /// <param name="generationPath">The path this generation was saved to.</param>
            public Entry(GenerationIndex index, IGeneration generation, string generationPath)
            {
                this.index = index;
                this.generationPath = generationPath;

                count = generation.Count;
                minFitness = generation.MinFitness;
                maxFitness = generation.MaxFitness;
                averageFitness = generation.AverageFitness;
            }

            /// <summary>
            /// Initialise an Entry from XML
            /// </summary>
            /// <param name="index">The GenerationIndex this Entry belongs to.</param>
            /// <param name="reader">The XmlReader to load the data from.</param>
            public Entry(GenerationIndex index, XmlReader reader)
            {
                this.index = index;
                string generationNumberString = reader.GetAttribute("index");
                string countString = reader.GetAttribute("count");
                string minFitnessString = reader.GetAttribute("min");
                string maxFitnessString = reader.GetAttribute("max");
                string averageFitnessString = reader.GetAttribute("average");

                generationPath = reader.GetAttribute("path");

                int.TryParse(countString, out count);
                uint.TryParse(minFitnessString, out minFitness);
                uint.TryParse(maxFitnessString, out maxFitness);
                float.TryParse(averageFitnessString, out averageFitness);
            }

            /// <summary>
            /// Write this entry to XML
            /// </summary>
            /// <param name="writer">The XmlWriter to write to.</param>
            public void WriteXml(XmlWriter writer)
            {
                writer.WriteStartElement("generation");
                writer.WriteAttributeString("count", count.ToString());
                writer.WriteAttributeString("min", minFitness.ToString());
                writer.WriteAttributeString("max", maxFitness.ToString());
                writer.WriteAttributeString("average", averageFitness.ToString());
                writer.WriteAttributeString("path", generationPath);
                writer.WriteEndElement();
            }

            /// <summary>
            /// Convert a relative path to an absolute path.
            /// </summary>
            /// <param name="workingPath">The absolute path to the current directory.</param>
            /// <param name="relativePath">The relative path to be resolved.</param>
            /// <returns>An absolute path given by following the relativePath from workingPath</returns>
            private string GetAbsolutePath(string workingPath, string relativePath)
            {
                string combined = Path.Combine(Path.GetDirectoryName(workingPath), relativePath);
                Uri combinedUri = new Uri(combined);
                return combinedUri.LocalPath;
            }

            /// <summary>
            /// Load an individual from an XML file.
            /// </summary>
            /// <param name="filename">The path to the XML file containing the individual.</param>
            /// <param name="individualReader">The rules for decoding the individual</param>
            /// <returns>The individual loaded</returns>
            private object LoadIndividual(string filename, IIndividualReader individualReader)
            {
                XmlTextReader reader = new XmlTextReader(filename);

                reader.MoveToContent();

                if (!individualReader.HandleTag(reader.Name))
                {
                    throw new Exception("<"+reader.Name+"> element invalid as root in individual XML file.");
                }

                object individual = individualReader.ReadIndividual(reader);
                
                reader.Close();

                return individual;
            }

            /// <summary>
            /// Load a generation from XML
            /// </summary>
            /// <param name="individualReader">The rules for decoding individuals.</param>
            /// <param name="generationFactory">
            /// The IGenerationFactory used to obtain an empty IGeneration
            /// or null to use the default IGenerationFactory
            /// </param>
            /// <returns>The generation loaded.</returns>
            public IGeneration LoadGeneration(IIndividualReader individualReader, IGenerationFactory generationFactory=null)
            {
                string generationAbsolutePath = GetAbsolutePath(index.filename, generationPath);

                if (generationFactory == null)
                {
                    generationFactory = new AATreeGenerationFactory();
                }

                ArrayList individuals = new ArrayList();
                List<uint> fitnesses = new List<uint>();

                XmlTextReader reader = new XmlTextReader(generationAbsolutePath);

                reader.MoveToContent();

                if (reader.Name != "generation")
                {
                    throw new Exception("Generation XML file must have <generation> element as root.");
                }

                int depth = 1;

                while (depth>0 && reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            switch (reader.Name)
                            {
                                case "individual":
                                    uint fitness;
                                    string fitnessString = reader.GetAttribute("fitness");
                                    uint.TryParse(fitnessString, out fitness);
                                    fitnesses.Add(fitness);

                                    string individualPath = reader.GetAttribute("path");
                                    string individualAbsolutePath = GetAbsolutePath(generationAbsolutePath, individualPath);

                                    object individual = LoadIndividual(individualAbsolutePath, individualReader);

                                    individuals.Add(individual);

                                    break;
                            }

                            if (!reader.IsEmptyElement)
                            {
                                depth++;
                            }

                            break;

                        case XmlNodeType.EndElement:
                            depth--;
                            break;
                    }
                }

                reader.Close();

                IGeneration generation = generationFactory.CreateGeneration(individuals);

                for (int i = 0; i < individuals.Count; i++)
                {
                    generation.Insert(individuals[i], fitnesses[i]);
                }

                generation.Prepare();
                return generation;
            }
        }
    }
}
