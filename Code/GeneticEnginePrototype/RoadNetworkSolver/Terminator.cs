using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticAlgorithm.Plugin.Generic;

namespace RoadNetworkSolver
{
    public class Terminator : FitnessThresholdTerminator
    {
        public Terminator(object config)
            : base((uint)config)
        {
        }
    }
}
