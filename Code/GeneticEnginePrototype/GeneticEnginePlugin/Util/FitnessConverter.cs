using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticAlgorithm.Plugin.Util
{
    /// <summary>
    /// Utility class for converting uint fitness values to/from other formats.
    /// </summary>
    public class FitnessConverter
    {
        /// <summary>
        /// Private constructor.
        /// There is no need to create instances of this class.
        /// </summary>
        private FitnessConverter()
        {
        }

        /// <summary>
        /// Convert a floating point number to a uint fitness value
        /// </summary>
        /// <param name="value">The floating point value.</param>
        /// <returns>The fitness value as a uint</returns>
        public static uint FromFloat(float value)
        {
            if (value > 0)
            {
                byte[] bytes = BitConverter.GetBytes(value);
                return (uint)BitConverter.ToInt32(bytes, 0);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Convert a uint fitness value to a floating point number.
        /// This can retrieve the floating point value from which a uint fitness value
        /// was produced. It should only be called on values which are returned by
        /// FromFloat().
        /// </summary>
        /// <param name="fitness">The uint fitness value.</param>
        /// <returns>The floating point fitness value.</returns>
        public static float ToFloat(uint fitness)
        {
            byte[] bytes = BitConverter.GetBytes((int)fitness);
            return BitConverter.ToSingle(bytes, 0);
        } 
    }
}
