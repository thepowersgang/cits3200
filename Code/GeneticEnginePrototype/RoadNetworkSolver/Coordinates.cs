using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace RoadNetworkSolver
{
    /// <summary>
    /// Represents a location in 2D (x,y) space
    /// </summary>
    public class Coordinates
    {        
        private int[] values;
        
        /// <summary>
        /// Get the x-coordinate
        /// </summary>
        public int X
        {
            get
            {
                return values[0];
            }
        }

        /// <summary>
        /// Get the y-coordinate
        /// </summary>
        public int Y
        {
            get
            {
                return values[1];
            }
        }

        public int this[int index]
        {
            get
            {
                return values[index];
            }
        }

        /// <summary>
        /// Initialise a new instance of Coordinates and set its x and y-coordinates
        /// </summary>
        /// <param name="x">The x-coordinate</param>
        /// <param name="y">The y-coordinate</param>
        public Coordinates(int x, int y)
        {
            values = new int[] { x, y };
        }

        /// <summary>
        /// Initialise a new instance of Coordinates from an XmlReader.
        /// The reader's current node must be an element which includes the attributes 'x' and 'y', 
        /// both holding integers.
        /// </summary>
        /// <param name="reader">The XmlReader to read the x and y coordinates from.</param>
        public Coordinates(XmlReader reader)
        {
            string xString = reader.GetAttribute("x");
            string yString = reader.GetAttribute("y");

            int x;
            int y;

            int.TryParse(xString, out x);
            int.TryParse(yString, out y);

            values = new int[] { x, y };
        }

        public void WriteXml(XmlWriter writer) {
            writer.WriteAttributeString("x", values[0].ToString());
            writer.WriteAttributeString("y", values[1].ToString());
        }

        /// <summary>
        /// Calcuate the square of the distance between this locations and another.
        /// 
        /// This is faster than calculating the distance as no square root operation is required.
        /// </summary>
        /// <param name="c">The coordinates of the other location</param>
        /// <returns>The square of the distance between c0 and c1</returns>
        public int GetDistanceSquared(Coordinates c)
        {
            int dx = values[0] - c.values[0];
            int dy = values[1] - c.values[1];

            return dx * dx + dy * dy;
        }

        /// <summary>
        /// Calculate the distance between this locations and another.
        /// </summary>
        /// <param name="c">The coordinates of the other location</param>
        /// <returns>The distance between c0 and c1</returns>
        public double GetDistance(Coordinates c)
        {
            return Math.Sqrt((double)GetDistanceSquared(c));
        }

        /// <summary>
        /// Determine if another set of coordinates is equivalent to this one.
        /// </summary>
        /// <param name="c">The other set of coordinates</param>
        /// <returns>True iff the other set of coordinates are equivalent</returns>
        public bool Equals(Coordinates c)
        {
            return values[0] == c.values[0] && values[1] == c.values[1];
        }
    }
}
