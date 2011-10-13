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
    class DisplayOutputter : IOutputter //Implement IOutputter
    {
        IOutputter wrappedOutputter;
        RoadNetworkPanel visualiser;

        public DisplayOutputter(RoadNetworkPanel visualiser, IOutputter wrappedOutputter)
        {
            this.visualiser = visualiser;
            this.wrappedOutputter = wrappedOutputter; //Set wrappedOutputter
        }

        public void StartOutput()
        {
            wrappedOutputter.StartOutput();
        }

        public void OutputGeneration(IGeneration generation, int generationCount)//Renamed from output
        {
            visualiser.Network = (RoadNetwork)generation[0].Individual;
            wrappedOutputter.OutputGeneration(generation, generationCount);
        }

        public void FinishOutput()
        {
            wrappedOutputter.FinishOutput();
        }
    }
}
