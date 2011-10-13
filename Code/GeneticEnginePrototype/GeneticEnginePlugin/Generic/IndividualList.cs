using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace GeneticAlgorithm.Plugin.Generic
{
    public class IndividualList : List<IndividualList.Entry>
    {
        public IndividualList()
        {
        }

        public IndividualList(XmlReader reader)
        {
            int depth = 1;

            while (depth > 0 && reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        switch (reader.Name)
                        {
                            case "individual":
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

        public static IndividualList FromFile(string filename)
        {
            XmlTextReader reader = new XmlTextReader(filename);
            reader.MoveToContent();

            if (reader.Name != "generation")
            {
                throw new Exception("Generation XML file must have <generation> element as root.");
            }

            IndividualList list = new IndividualList(reader);
            reader.Close();
            return list;
        }

        public class Entry
        {
            private string individualPath;
            private float fitness;

            public float Fitness
            {
                get
                {
                    return fitness;
                }
            }
           
            public Entry(XmlReader reader)
            {
                string fitnessString = reader.GetAttribute("fitness");
                individualPath = reader.GetAttribute("path");

                float.TryParse(fitnessString, out fitness);
            }

            public IndividualWithFitness LoadIndividual(string workingDirectory)
            {
                string combinedPath = Path.Combine(workingDirectory, individualPath);
                string absolutePath =  (new Uri(combinedPath)).LocalPath;

                //Load individual;

                return new IndividualWithFitness(null, 0);
            }
        }
    }
}
