using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using RoadNetworkSolver;
using System.Xml;

namespace RoadNetworkGUI
{
    public partial class Road_Network_Visualiser : Form
    {
        OpenFileDialog openDialog;
        RoadNetwork network;
        public Road_Network_Visualiser()
        {
            InitializeComponent();
            openDialog = new OpenFileDialog();
            for (int i = 0; i <= 200; i++)
            {
                generationScroller.Items.Add(i.ToString());
            }
        }

        /**
         * Prompt user to select a file.
         * If a file is selected, check if the file is an xml file.
         * If not, display error message to alert the user that they cannot open the file.
         * If it is, then read from file and create new network and use it to draw up the network. 
         */ 
        private void Road_Network_Visualiser_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Select a file once you hit OK\n");
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                string extension = Path.GetExtension(openDialog.FileName);
                if (extension != ".xml")
                {
                    MessageBox.Show("Cannot open file for reading XML\n");
                }
                else
                {
                    XmlTextReader reader = new XmlTextReader(openDialog.FileName);
                    network = new RoadNetwork(null, reader);
                    visualiser2.Network = network;
                }
            }
        }

        /**
         * The index selected is the index of the generation
         * If the index is 0 or the generation is not selected, display an error message to select a proper generation index.
         * The index ranges from 1 to 200. 
         * For a specific generation, dynamically fill up the 'individual' scroll bar from 1 up to the number of individuals. 
         */ 
        private void generationScroller_SelectedItemChanged(object sender, EventArgs e)
        {
            if (generationScroller.SelectedIndex == 0 || generationScroller.SelectedItem == null)
            {
                MessageBox.Show("Select a proper generation index from 1 to 200\n");
            }
            else
            {
                for (int i = 1; i <= 200; i++)
                {
                    individualScroller.Items.Add(i.ToString());
                }
            }
        }

        private void individualScroller_SelectedItemChanged(object sender, EventArgs e)
        {
            if (individualScroller.SelectedIndex == 0 || individualScroller.SelectedItem == null)
            {
                MessageBox.Show("Select an individual index from 1 to 200 for this generation\n");
            }
            else
            {
                int index = individualScroller.SelectedIndex - 1;
            }
        }


    }
}
