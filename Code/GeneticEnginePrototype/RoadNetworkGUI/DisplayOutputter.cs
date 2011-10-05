using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticAlgorithm.Plugin;
using GeneticAlgorithm.Generation;
using RoadNetworkSolver;
using RoadNetworkDisplay;

namespace RoadNetworkGUI
{
    class DisplayOutputter
    {
        IOutputter wrappedOutputter;
        RoadNetworkPanel visualiser;

        public DisplayOutputter(RoadNetworkPanel visualiser)
        {
            this.visualiser = visualiser;
        }

        public void output(IGeneration generation, int generationCount)
        {
            visualiser.Network = (RoadNetwork)generation[0].Individual;
            wrappedOutputter.OutputGeneration(generation, generationCount);
        }
    }
}
