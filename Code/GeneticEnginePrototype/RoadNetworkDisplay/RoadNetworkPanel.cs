using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RoadNetworkSolver;

namespace RoadNetworkDisplay
{
    public partial class RoadNetworkPanel : UserControl
    {
        RoadNetwork network = null;        

        public RoadNetworkPanel()
        {
            InitializeComponent();
        }

        public RoadNetwork Network
        {
            get
            {
                return network;
            }

            set
            {
                network = value;
                Invalidate();
            }
        }

        private void RoadNetworkPanel_Paint(object sender, PaintEventArgs e)
        {
            if (network != null)
            {
                Coordinates startCoordinates = network.Start.Coordinates;
                e.Graphics.FillEllipse(Brushes.Red, startCoordinates.X - 2, startCoordinates.Y - 2, 5, 5);

                Coordinates endCoordinates = network.End.Coordinates;
                e.Graphics.FillEllipse(Brushes.Green, endCoordinates.X - 2, endCoordinates.Y - 2, 5, 5);

                for (int i = 0; i < network.EdgeCount; i++)
                {
                    Edge edge = network.GetEdge(i);
                    Coordinates coordinates0 = edge.Start.Coordinates;
                    Coordinates coordinates1 = edge.End.Coordinates;

                    Pen pen;

                    if (edge.IsBroken)
                    {
                        pen = Pens.Blue;
                    }
                    else if (edge.End.Visited)
                    {
                        pen = Pens.Green;
                    }
                    else
                    {
                        pen = Pens.Red;
                    }

                    e.Graphics.DrawLine(pen, coordinates0.X, coordinates0.Y, coordinates1.X, coordinates1.Y); 
                }
            }
        }
    }
}
