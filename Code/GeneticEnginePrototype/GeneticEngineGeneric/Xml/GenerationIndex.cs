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
    public class GenerationIndex : List<GenerationIndex.Entry>
    {
        private string filename;

        public GenerationIndex(string filename)
        {
            this.filename = filename;
        }

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

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("results");

            foreach (Entry entry in this)
            {
                entry.WriteXml(writer);
            }

            writer.WriteEndElement();
        }

        public void Save()
        {
            XmlTextWriter writer = new XmlTextWriter(filename, Encoding.ASCII);
            writer.Formatting = Formatting.Indented;

            WriteXml(writer);

            writer.Flush();
            writer.Close();
        }

        public void AddGeneration(IGeneration generation, string generationPath)
        {
            Add(new Entry(this, generation, generationPath));
        }

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
                
        public class Entry
        {
            private GenerationIndex index;
            private string generationPath;
            private int count;
            private uint minFitness;
            private uint maxFitness;
            private float averageFitness;

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

            public Entry(GenerationIndex index, IGeneration generation, string generationPath)
            {
                this.index = index;
                this.generationPath = generationPath;

                count = generation.Count;
                minFitness = generation.MinFitness;
                maxFitness = generation.MaxFitness;
                averageFitness = generation.AverageFitness;
            }

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

            private string GetAbsolutePath(string workingPath, string relativePath)
            {
                string combined = Path.Combine(Path.GetDirectoryName(workingPath), relativePath);
                Uri combinedUri = new Uri(combined);
                return combinedUri.LocalPath;
            }

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

            public IGeneration LoadGeneration(IIndividualReader individualReader, IGenerationFactory generationFactory=null)
            {
                string generationAbsolutePath = GetAbsolutePath(index.filename, generationPath);

                if (generationFactory == null)
                {
                    generationFactory = new AATreeGenerationFactory(null);
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
