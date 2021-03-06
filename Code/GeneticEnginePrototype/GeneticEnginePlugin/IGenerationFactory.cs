﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticAlgorithm.Plugin
{
    /// <summary>
    /// Defines a GeneticEngine generation factory plug-in
    /// </summary>
    public interface IGenerationFactory
    {
        /// <summary>
        /// Get an empty instance of an implementation of the IGeneration interface
        /// The GeneticEngine calls this once every generation.
        /// </summary>
        /// <param name="individuals">The individuals which will eventually populate the generation</param>
        /// <returns>An empty generation</returns>
        IGeneration CreateGeneration(ArrayList individuals);
    }
}
