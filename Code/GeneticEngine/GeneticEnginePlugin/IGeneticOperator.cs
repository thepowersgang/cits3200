﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticAlgorithm.Plugin
{
    /// <summary>
    /// Defines a GeneticEngine genetic operator plug-in.
    /// </summary>
    public interface IGeneticOperator
    {
        /// <summary>
        /// Process a generation to produce the individuals which will make up the next generation.
        /// The GeneticEngine calls this once every generation after the individuals have been evaluated
        /// and the generation has been outputted.
        /// </summary>
        /// <param name="source">The current generation</param>
        /// <param name="destination">An empty collection of individuals to be populated</param>
        void Operate(IGeneration source, ArrayList destination);
    }
}
