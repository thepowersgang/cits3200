﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using RoadNetworkSolver;
using GeneticAlgorithm.Plugin;
using GeneticAlgorithm.Plugin.Generic;
using GeneticAlgorithm.Plugin.Xml;
using System.Xml;

namespace RoadNetworkGUI
{
    public partial class Road_Network_Visualiser : Form
    {
        OpenFileDialog openDialog;
        int IndividualIndex = -1, GenerationIndex = -1;
        GenerationIndex results; 
        public Road_Network_Visualiser(bool isFileLoaded, string filename )
        {
            InitializeComponent();
            openDialog = new OpenFileDialog();
            getFile(isFileLoaded, filename);
        }

        /**
         * Prompt user to select a file.
         * If a file is selected, check if the file is an xml file.
         * If not, display error message to alert the user that they cannot open the file and terminate the program.
         * If it is, then read from file and create new network and use it to draw up the network. 
         * If the file is selected, display an error message and terminate the program.
         * isFileLoaded is a boolean that specifies whether a file has been loaded
         * Filename is the file path that the loaded file exists in
         */ 
        private void getFile(bool isFileLoaded, string Filename)
        {
            if (!isFileLoaded && String.IsNullOrEmpty(Filename))
            {
                MessageBox.Show("Select a file once you hit OK\n");
                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    loadFile(openDialog.FileName);
                }
                else
                {
                        MessageBox.Show("No file is chosen. Terminating program now\n");
                        this.Dispose(true);
                }
            }
            else
            {
                loadFile(Filename);
            }
        }
        private void loadFile(string filename)
        {
            string extension = Path.GetExtension(filename);
            if (extension != ".xml")
            {
                    MessageBox.Show("Cannot open file for reading XML.\n Terminating program now");
                    this.Dispose(true);
            }
            else
            {
                XmlTextReader reader = new XmlTextReader(filename);
                reader.MoveToContent();
                if (reader.Name != "results")
                {
                    throw new Exception("Results XML file must have <results> element as root.");
                }
                results = new GenerationIndex(filename,reader);
                
            }
        }
        private void Road_Network_Visualiser_Load(object sender, EventArgs e)
        {
        }

        /**
         * The index selected is the index of the generation
         * If the index is 0 or the generation is not selected, display an error message to select a proper generation index.
         * For a specific generation, if the individual count is at least 1, dynamically fill up the 'individual' scroll bar from 1 up to the number of individuals. 
         * Otherwise, request user to select another generation through an displayed error message.
         */ 
        private void generationScroller_SelectedItemChanged(object sender, EventArgs e)
        {
            if ((int) generation.Value == results.Count)
            {
                MessageBox.Show("Select a proper generation index from 0 to "+ (results.Count-1)+" \n");
            }
            else
            {
                GenerationIndex = (int)generation.Value;
                if (results[GenerationIndex].Count <= 0)
                {
                    string errorMsg = "There are no individuals for this Generation\n";
                    errorMsg += "Therefore please select another generation with at least one individual\n";
                    MessageBox.Show(errorMsg);
                }
            }
        }

        /**
         * If an individual index is selected by the user, then check if the individual is valid,
         * if so, redraw the individual on the visualiser. 
         * If not, display an error message to select a proper individual index. 
         */ 
        private void individualScroller_SelectedItemChanged(object sender, EventArgs e)
        {
            if ((int)individual.Value == results[GenerationIndex].Count)
            {
                MessageBox.Show("Select an individual index from 0 to "+(results[GenerationIndex].Count-1) +" for this generation\n");
            }
            else
            {
                IndividualIndex = (int)(individual.Value);
                IGeneration generation = results[GenerationIndex].LoadGeneration(new RoadNetworkReader(),null);
                IndividualWithFitness iwf = generation[IndividualIndex];
                uint fitness = iwf.Fitness;
                fitnessLabel.Text = fitness.ToString();
                visualiser2.Network = (RoadNetwork)iwf.Individual;
            }
        }


    }
}
