﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticAlgorithm.Plugin.Generic;

namespace RoadNetworkSolver
{
    /// <summary>
    /// An ITerminator which terminates when a given fitness has been reached.
    /// </summary>
    public class Terminator : FitnessThresholdTerminator
    {
        public Terminator(object config)
            : base((uint)config)
        {
        }
    }
}
