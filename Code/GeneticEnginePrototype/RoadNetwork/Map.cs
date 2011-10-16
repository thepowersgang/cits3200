using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace RoadNetworkDefinition
{
    /// <summary>
    /// Describes a Map in which a RoadNetwork exists.
    /// </summary>
    public class Map
    {
        /// <summary>
        /// The width of the map
        /// </summary>
        private int width;

        /// <summary>
        /// The height of the map
        /// </summary>
        private int height;

        /// <summary>
        /// The start location within the map
        /// </summary>
        private Coordinates start;

        /// <summary>
        /// The end location within the map
        /// </summary>
        private Coordinates end;

        /// <summary>
        /// The towns on the map
        /// </summary>
        private List<Coordinates> towns = new List<Coordinates>();

        /// <summary>
        /// Get the width of the map
        /// </summary>
        public int Width
        {
            get
            {
                return width;
            }
        }

        /// <summary>
        /// Get the height of the map
        /// </summary>
        public int Height
        {
            get
            {
                return height;
            }
        }

        /// <summary>
        /// Get the start location for the road network
        /// </summary>
        public Coordinates Start
        {
            get
            {
                return start;
            }
        }

        /// <summary>
        /// Get the end location for the road network
        /// </summary>
        public Coordinates End
        {
            get
            {
                return end;
            }
        }

        /// <summary>
        /// Get the number of towns
        /// </summary>
        public int TownCount
        {
            get
            {
                return towns.Count;
            }
        }

        public Map(XmlReader reader)
        {
            string widthString = reader.GetAttribute("width");
            string heightString = reader.GetAttribute("height");

            int.TryParse(widthString, out width);
            int.TryParse(heightString, out height);

            int depth = 1;

            while (depth > 0 && reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        switch (reader.Name)
                        {
                            case ("start"):
                                start = new Coordinates(reader);
                                break;
                            case ("end"):
                                end = new Coordinates(reader);
                                break;
                            case ("town"):
                                towns.Add(new Coordinates(reader));
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
            writer.WriteStartElement("map");

            writer.WriteAttributeString("width", width.ToString());
            writer.WriteAttributeString("height", height.ToString());

            writer.WriteStartElement("start");
            start.WriteXml(writer);
            writer.WriteEndElement();

            writer.WriteStartElement("end");
            end.WriteXml(writer);
            writer.WriteEndElement();

            foreach (Coordinates town in towns)
            {
                writer.WriteStartElement("town");
                end.WriteXml(writer);
                writer.WriteEndElement();
            }

            writer.WriteEndElement();
        }

        /// <summary>
        /// Get a town by its index
        /// </summary>
        /// <param name="index">A integer denoting the index of the town</param>
        /// <returns>The Coordinates of the town</returns>
        public Coordinates GetTown(int index)
        {
            return towns[index];
        }

        public static Map FromFile(string filename)
        {
            XmlTextReader reader = new XmlTextReader(filename);
            reader.MoveToContent();

            if (reader.Name != "map")
            {
                throw new Exception("Map XML file must have <map> element as root.");
            }

            Map map = new Map(reader);
            reader.Close();
            return map;
        }
    }
}
