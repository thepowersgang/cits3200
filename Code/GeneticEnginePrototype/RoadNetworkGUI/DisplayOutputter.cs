using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticAlgorithm.Plugin;
using RoadNetworkDefinition;
using RoadNetworkDisplay;

namespace RoadNetworkGUI
{
    class DisplayOutputter : IOutputter //Implement IOutputter
    {
        IOutputter wrappedOutputter;
        RoadNetworkFinder mainWindow;

        public OutputterStatus Status
        {
            get
            {
                return OutputterStatus.Open;
            }
        }

        public DisplayOutputter(RoadNetworkFinder mainWindow, IOutputter wrappedOutputter)
        {
            this.mainWindow = mainWindow;
            this.wrappedOutputter = wrappedOutputter; //Set wrappedOutputter
        }

        public void OpenOutput()
        {
            if (wrappedOutputter!=null && wrappedOutputter.Status == OutputterStatus.Closed)
            {
                wrappedOutputter.OpenOutput();
            }
        }

        public void OutputGeneration(IGeneration generation, int generationCount)//Renamed from output
        {
            mainWindow.DisplayGeneration(generation);
            
            if (wrappedOutputter != null)
            {
                if (wrappedOutputter.Status == OutputterStatus.Closed)
                {
                    wrappedOutputter.OpenOutput();
                }

                if (wrappedOutputter.Status == OutputterStatus.Open)
                {
                    wrappedOutputter.OutputGeneration(generation, generationCount);
                }
            }
        }

        public void CloseOutput()
        {
            if (wrappedOutputter != null && wrappedOutputter.Status == OutputterStatus.Open)
            {
                wrappedOutputter.CloseOutput();
            }            
        }
    }
}
