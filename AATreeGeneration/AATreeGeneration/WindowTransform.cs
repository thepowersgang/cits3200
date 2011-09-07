using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Generations
{
    public class WindowTransform
    {
        private double a;
        private double b;

        public WindowTransform(ulong sumFitness, uint minFitness, int nIndividuals, double minProbability)
        {
            a = (1.0 - minProbability * nIndividuals) / (sumFitness - minFitness * (uint)nIndividuals);
            b = minProbability - a * minFitness;
        }

        public double Transform(uint fitness)
        {
            return a * fitness + b;
        }

        public double TransformSum(ulong sumFitness, int n)
        {
            return a * sumFitness + b * n;
        }
    }
}
