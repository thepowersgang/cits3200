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
    }
}
