using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RoadNetworkDefinition;

namespace RoadNetworkDisplay
{
    public partial class RoadNetworkPanel : UserControl
    {
        RoadNetwork network = null;
        bool networkBeenGiven = false;
        double xWidth = 0;
        double yHeight = 0;

        /// <summary>
        /// Initialise a new RoadNetworkPanel
        /// </summary>
        public RoadNetworkPanel()
        {
            InitializeComponent();

        }

        /// <summary>
        /// Get or Set the currently displayed RoadNetwork
        /// </summary>
        public RoadNetwork Network
        {
            get
            {
                return network;
            }

            set
            {
                if (value != null)
                {
                    network = value;
                    xWidth = Width / network.Map.Width;
                    yHeight = Height / network.Map.Height;
                    
                    networkBeenGiven = true;
                }
                Invalidate();
            }
        }
        private void RoadNetworkPanel_Paint(object sender, PaintEventArgs e)
        {
            if (networkBeenGiven)
            {
                drawVerticesEdges(e);
                drawTowns(e);
                drawStartEndCoords(e);
            }
        }
        private void drawStartEndCoords(PaintEventArgs e)
        {
            if (network.Map != null)
            {
                Coordinates start = network.Map.Start;
                Coordinates end = network.Map.End;
                e.Graphics.FillEllipse(Brushes.Green, (int) (start.X * xWidth)-5,(int) (start.Y * yHeight)-5, 10, 10);
                e.Graphics.FillEllipse(Brushes.Green, (int) (end.X * xWidth)-5, (int)(end.Y * yHeight)-5, 10, 10);
            }
        }
        private void drawTowns(PaintEventArgs e)
        {
            if (network.Map.TownCount > 0 && network.Map != null)
            {
                for (int i = 0; i < network.Map.TownCount; i++)
                {
                    Coordinates town = network.Map.GetTown(i);
                    int X = (int)(town.X * xWidth);
                    int Y = (int) (town.Y * yHeight);
                    e.Graphics.FillEllipse(Brushes.Red, X-5, Y-5, 10, 10);
                }
            }
        }
        private void drawVerticesEdges(PaintEventArgs e)
        {
            if (network != null && network.EdgeCount > 0)
            {
                Coordinates startCoordinates = network.Map.Start;
                int startX = (int)(xWidth * startCoordinates.X);
                int startY = (int)(yHeight * startCoordinates.Y);
                e.Graphics.FillEllipse(Brushes.Green, startX-5, startY-5, 10, 10);

                Coordinates endCoordinates = network.Map.End;
                int endX = (int)(xWidth * endCoordinates.X);
                int endY = (int)(yHeight * endCoordinates.Y);
                e.Graphics.FillEllipse(Brushes.Green, endX-5, endY-5, 10, 10);

                for (int i = 0; i < network.EdgeCount; i++)
                {
                    Edge edge = network.GetEdge(i);
                    Coordinates coordinates0 = edge.Start.Coordinates;
                    Coordinates coordinates1 = edge.End.Coordinates;

                    Pen pen = Pens.Blue;
                    e.Graphics.DrawLine(pen, (int) (coordinates0.X * xWidth), (int)(coordinates0.Y * yHeight), (int)(coordinates1.X * xWidth), (int)(coordinates1.Y * yHeight));
                }
            }
        }

        private void RoadNetworkPanel_Load(object sender, EventArgs e)
        {

        }

        private void RoadNetworkPanel_Resize(object sender, EventArgs e)
        {

        }

    }
}