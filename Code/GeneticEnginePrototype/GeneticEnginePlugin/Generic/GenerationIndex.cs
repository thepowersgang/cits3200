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
        private string path;
     
        public void AddGeneration(IGeneration generation, string generationPath)
        {
            Add(new GenerationIndexEntry(generation, generationPath));
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

            foreach (GenerationIndexEntry entry in this)
            {
                entry.WriteXml(writer);
            }

            writer.WriteEndElement();
        }

        public static GenerationIndex FromFile(string filename)
        {
            XmlTextReader reader = new XmlTextReader(filename);
            reader.MoveToContent();

            if (reader.Name != "results")
            {
                throw new Exception("Results index XML file must have <results> element as root.");
            }

            GenerationIndex index = new GenerationIndex(reader);
            reader.Close();
            return index;
        }
    }
}
