using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticAlgorithm.Plugin.Util
{
    public class FitnessConverter
    {
        private FitnessConverter()
        {
        }

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

        public static float ToFloat(uint fitness)
        {
            byte[] bytes = BitConverter.GetBytes((int)fitness);
            return BitConverter.ToSingle(bytes, 0);
        } 
    }
}
