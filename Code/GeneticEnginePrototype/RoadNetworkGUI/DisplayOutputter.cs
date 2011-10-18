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
        /// <summary>
        /// Get the current status fo the Display outputter
        /// </summary>
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
        /// <summary>
        /// If a proper outputter is specified and it is closed, open the outputter.
        /// </summary>
        public void OpenOutput()
        {
            if (wrappedOutputter!=null && wrappedOutputter.Status == OutputterStatus.Closed)
            {
                wrappedOutputter.OpenOutput();
            }
        }

        /// <summary>
        /// Output the generation to the output file if the outputter is currently open
        /// Otherwise open the outputter first. 
        /// </summary>
        /// <param name="generation">The current generation to send to the outputter</param>
        /// <param name="generationCount">The number of generations before the current one</param>
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

        /// <summary>
        /// If the outputter is specified and it is currently open, close it. 
        /// </summary>
        public void CloseOutput()
        {
            if (wrappedOutputter != null && wrappedOutputter.Status == OutputterStatus.Open)
            {
                wrappedOutputter.CloseOutput();
            }            
        }
    }
}
