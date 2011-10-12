using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using GeneticAlgorithm.Plugin;

namespace GeneticAlgorithm.Plugin.Generic
{
    public class GenerationIndex : List<GenerationIndexEntry>
    {
        private DateTime startTime;
        private DateTime finishTime;

        public DateTime StartTime
        {
            get
            {
                return startTime;
            }

            set
            {
                startTime = value;
            }
        }

        public DateTime FinishTime
        {
            get
            {
                return finishTime;
            }

            set
            {
                finishTime = value;
            }
        }
        
        public void AddGeneration(IGeneration generation, int generationNumber, string generationPath)
        {
            Add(new GenerationIndexEntry(generation, generationNumber, generationPath));
        }

        public GenerationIndex()
        {
        }

        public GenerationIndex(XmlReader reader)
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
                                Add(new GenerationIndexEntry(reader));
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
            writer.WriteAttributeString("start", startTime.ToString());
            writer.WriteAttributeString("finish", finishTime.ToString());

            foreach (GenerationIndexEntry entry in this)
            {
                entry.WriteXml(writer);
            }

            writer.WriteEndElement();
        }
    }
}
