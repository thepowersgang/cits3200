using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using GeneticAlgorithm.Plugin;

namespace GeneticAlgorithm.Plugin.Generic
{
    public class GenerationIndexEntry
    {
        private int generationNumber;
        private string generationPath;
        private int count;
        private uint minFitness;
        private uint maxFitness;
        private float averageFitness;

        public int GenerationNumber
        {
            get
            {
                return generationNumber;
            }
        }

        public string GenerationPath
        {
            get
            {
                return generationPath;
            }
        }

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

        public GenerationIndexEntry(IGeneration generation, int generationNumber, string generationPath)
        {
            this.generationNumber = generationNumber;
            this.generationPath = generationPath;

            count = generation.Count;
            minFitness = generation.MinFitness;
            maxFitness = generation.MaxFitness;
            averageFitness = generation.AverageFitness;
        }

        public GenerationIndexEntry(XmlReader reader)
        {
            string generationNumberString = reader.GetAttribute("index");
            string countString = reader.GetAttribute("count");
            string minFitnessString = reader.GetAttribute("min");
            string maxFitnessString = reader.GetAttribute("max");
            string averageFitnessString = reader.GetAttribute("average");

            generationPath = reader.GetAttribute("path");

            int.TryParse(generationNumberString, out generationNumber);
            int.TryParse(countString, out count);
            uint.TryParse(minFitnessString, out minFitness);
            uint.TryParse(maxFitnessString, out maxFitness);
            float.TryParse(averageFitnessString, out averageFitness);
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("generation");
            writer.WriteAttributeString("index", generationNumber.ToString());
            writer.WriteAttributeString("count", count.ToString());
            writer.WriteAttributeString("min", minFitness.ToString());
            writer.WriteAttributeString("max", maxFitness.ToString());
            writer.WriteAttributeString("average", averageFitness.ToString());
            writer.WriteAttributeString("path", generationPath);
            writer.WriteEndElement();
        }
    }
}
