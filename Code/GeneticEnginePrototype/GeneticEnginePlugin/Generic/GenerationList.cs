using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using GeneticAlgorithm.Plugin;

namespace GeneticAlgorithm.Plugin.Generic
{
    public class GenerationList : List<GenerationList.Entry>
    {
        public void AddGeneration(IGeneration generation, string generationPath)
        {
            Add(new Entry(generation, generationPath));
        }

        public GenerationList()
        {
        }

        public GenerationList(XmlReader reader)
        {
            int depth = 1;

            while (depth > 0 && reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        switch (reader.Name)
                        {
                            case "generation":
                                Add(new Entry(reader));
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

        public static GenerationList FromFile(string filename)
        {
            XmlTextReader reader = new XmlTextReader(filename);
            reader.MoveToContent();

            if (reader.Name != "results")
            {
                throw new Exception("Results XML file must have <results> element as root.");
            }

            GenerationList list = new GenerationList(reader);
            reader.Close();
            return list;
        }

        public class Entry
        {
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

            public Entry(IGeneration generation, string generationPath)
            {
                this.generationPath = generationPath;

                count = generation.Count;
                minFitness = generation.MinFitness;
                maxFitness = generation.MaxFitness;
                averageFitness = generation.AverageFitness;
            }

            public Entry(XmlReader reader)
            {
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

            public IndividualList LoadGeneration(string workingDirectory)
            {
                string combinedPath = Path.Combine(workingDirectory, generationPath);
                string absolutePath =  (new Uri(combinedPath)).LocalPath;
                return IndividualList.FromFile(absolutePath);
            }
        }
    }
}
