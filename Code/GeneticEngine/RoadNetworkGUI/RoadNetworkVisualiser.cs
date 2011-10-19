using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using RoadNetworkDefinition;
using GeneticAlgorithm.Plugin;
using GeneticAlgorithm.Plugin.Generic;
using GeneticAlgorithm.Plugin.Xml;
using System.Xml;

namespace RoadNetworkGUI
{
    public partial class Road_Network_Visualiser : Form
    {
        OpenFileDialog openDialog;
        int IndividualIndex = 0, GenerationIndex = 0;
        GenerationIndex results; 

        /// <summary>
        /// Initialise a new Road_Network_Visualiser
        /// </summary>
        /// <param name="isFileLoaded">Has a file been loaded</param>
        /// <param name="filename">The name of the file to load</param>
        public Road_Network_Visualiser(bool isFileLoaded, string filename )
        {
            InitializeComponent();
            openDialog = new OpenFileDialog();
            getFile(isFileLoaded, filename);
        }

        ///<summary>
        /// Prompt user to select a file.
        /// If a file is selected, check if the file is an xml file.
        /// If not, display error message to alert the user that they cannot open the file and terminate the program.
        /// If it is, then read from file and create new network and use it to draw up the network. 
        /// If the file is selected, display an error message and terminate the program.
        /// <param name="isFileLoaded">isFileLoaded is a boolean that specifies whether a file has been loaded</param>
        /// <param name="Filename">Filename is the file path that the loaded file exists in</param>
        /// </summary>
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

        /// <summary>
        /// Load output file and check if the file is an XML one.  If not, terminate program
        /// Otherwise show the maximum generation value. 
        /// </summary>
        /// <param name="filename"></param>
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
                drawIndividual(0, 0);
                generation.Maximum = results.Count - 1;
                maxGeneration.Text = "Max : " + (results.Count-1);
                
            }
        }
        private void Road_Network_Visualiser_Load(object sender, EventArgs e)
        {
        }

        ////
        /// The index selected is the index of the generation
        /// If the generation value is the number of generations, reset the generation value to 0.
        /// If the individual count is 0, an error message is displayed to alert the user to select a new generation.
        /// Otherwise, display the maximum individual value on the user interface.
        ///
        private void generation_ValueChanged(object sender, EventArgs e)
        {
            if ((int)generation.Value >= results.Count)
            {
                generation.Value = 0;
            }
            else
            {
                GenerationIndex = (int)generation.Value;
                if (results[GenerationIndex].Count == 0)
                {
                    string errorMsg = "There are no individuals for this Generation\n";
                    errorMsg += "Therefore please select another generation with at least one individual\n";
                    MessageBox.Show(errorMsg);
                }
                else
                {
                    drawIndividual(0, GenerationIndex);
                    individual.Maximum = results[GenerationIndex].Count - 1;
                    individual.Value = 0;
                }
            }
        }

        ///
        /// If an individual index is selected by the user, then check if the individual is valid,
        /// if so, redraw the individual on the visualiser. 
        /// If the individual value is passed the maximum value, reset the index to 0. 
        /// 
        private void individual_ValueChanged(object sender, EventArgs e)
        {
            if ((int)individual.Value >= results[GenerationIndex].Count)
            {
                individual.Value = 0;
            }
            else
            {
                drawIndividual((int)individual.Value, GenerationIndex);
            }
        }


        private void drawIndividual(int IndividualIndex, int GenerationIndex)
        {
            this.IndividualIndex = IndividualIndex;
            IGeneration generation = results[GenerationIndex].LoadGeneration(new RoadNetworkReader(), null);
            IndividualWithFitness iwf = generation[IndividualIndex];
            uint fitness = iwf.Fitness;
            fitnessLabel.Text = fitness.ToString();
            visualiser2.Network = (RoadNetwork)iwf.Individual;
            maxIndividuals.Text = "Max : " + (results[GenerationIndex].Count - 1);
        }
    }
}
