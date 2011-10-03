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
        /// <summary>
        /// The x-coordinate
        /// </summary>
        private int x;

        /// <summary>
        /// The y-coordinate
        /// </summary>
        private int y;
        
        /// <summary>
        /// Get the x-coordinate
        /// </summary>
        public int X
        {
            get
            {
                return x;
            }
        }

        /// <summary>
        /// Get the y-coordinate
        /// </summary>
        public int Y
        {
            get
            {
                return y;
            }
        }

        /// <summary>
        /// Initialise a new instance of Coordinates and set its x and y-coordinates
        /// </summary>
        /// <param name="x">The x-coordinate</param>
        /// <param name="y">The y-coordinate</param>
        public Coordinates(int x, int y)
        {
            this.x = x;
            this.y = y;
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
            
            int.TryParse(xString, out x);
            int.TryParse(yString, out y);
        }

        public void WriteXml(XmlWriter writer) {
            writer.WriteAttributeString("x", x.ToString());
            writer.WriteAttributeString("y", y.ToString());
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
            int dx = x - c.x;
            int dy = y - c.y;

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
            return x == c.x && y == c.y;
        }
    }
}
