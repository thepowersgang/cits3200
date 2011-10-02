using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace RoadNetworkSolver
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

        public int Width
        {
            get
            {
                return width;
            }
        }

        public int Height
        {
            get
            {
                return height;
            }
        }

        public Coordinates Start
        {
            get
            {
                return start;
            }
        }

        public Coordinates End
        {
            get
            {
                return end;
            }
        }

        public int TownCount
        {
            get
            {
                return towns.Count;
            }
        }

        public Coordinates GetTown(int index)
        {
            return towns[index];
        }
                
        public void ReadXml(XmlReader reader)
        {
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
            //TODO
        }
    }
}
