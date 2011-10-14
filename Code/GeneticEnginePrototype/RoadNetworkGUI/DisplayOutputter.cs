using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticAlgorithm.Plugin;
using RoadNetworkSolver;
using RoadNetworkDisplay;

namespace RoadNetworkGUI
{
    class DisplayOutputter : IOutputter //Implement IOutputter
    {
        IOutputter wrappedOutputter;
        RoadNetworkPanel visualiser;

        public OutputterStatus Status
        {
            get
            {
                return OutputterStatus.Open;
            }
        }

        public DisplayOutputter(RoadNetworkPanel visualiser, IOutputter wrappedOutputter)
        {
            this.visualiser = visualiser;
            this.wrappedOutputter = wrappedOutputter; //Set wrappedOutputter
        }

        public void StartOutput()
        {
            if (wrappedOutputter.Status == OutputterStatus.Closed)
            {
                wrappedOutputter.StartOutput();
            }
        }

        public void OutputGeneration(IGeneration generation, int generationCount)//Renamed from output
        {
            visualiser.Network = (RoadNetwork)generation[0].Individual;

            if (wrappedOutputter.Status == OutputterStatus.Closed)
            {
                wrappedOutputter.StartOutput();
            }

            if (wrappedOutputter.Status == OutputterStatus.Open)
            {
                wrappedOutputter.OutputGeneration(generation, generationCount);
            }
        }

        public void FinishOutput()
        {
            if (wrappedOutputter.Status == OutputterStatus.Open)
            {
                wrappedOutputter.FinishOutput();
            }            
        }
    }
}
