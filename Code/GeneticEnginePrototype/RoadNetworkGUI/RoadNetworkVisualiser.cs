using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RoadNetworkGUI
{
    public partial class Road_Network_Visualiser : Form
    {
        OpenFileDialog openDialog;
        public Road_Network_Visualiser()
        {
            InitializeComponent();
            openDialog = new OpenFileDialog();
        }

        private void Road_Network_Visualiser_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Select a file once you hit OK\n");
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
            }
        }
    }
}
