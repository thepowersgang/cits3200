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
        bool networkBeenGiven = false;
        int xWidth = 0;
        int yHeight = 0;
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
                if (value != null)
                {
                    network = value;
                    double x = Width / network.Map.Width;
                    double y = Height / network.Map.Height;
                    xWidth = (int) Math.Ceiling(x);
                    yHeight = (int)Math.Ceiling(y);
                    networkBeenGiven = true;
                }
                Invalidate();
            }
        }

        private void RoadNetworkPanel_Paint(object sender, PaintEventArgs e)
        {
            if (networkBeenGiven)
            {
                drawTowns(e);
                drawStartEndCoords(e);
                drawVerticesEdges(e);
            }
        }
        private void drawStartEndCoords(PaintEventArgs e)
        {
            if (network.Map != null)
            {
                Coordinates start = network.Map.Start;
                Coordinates end = network.Map.End;
                e.Graphics.FillEllipse(Brushes.Green, (start.X) * xWidth,(start.Y) * yHeight, 10, 10);
                e.Graphics.FillEllipse(Brushes.Green, (end.X) * xWidth, (end.Y) * yHeight, 10, 10);
            }
        }
        private void drawTowns(PaintEventArgs e)
        {
            if (network.Map.TownCount > 0 && network.Map != null)
            {
                for (int i = 0; i < network.Map.TownCount; i++)
                {
                    Coordinates town = network.Map.GetTown(i);
                    int X = (town.X) * xWidth;
                    int Y = (town.Y) * yHeight;
                    e.Graphics.FillEllipse(Brushes.Red, X, Y, 10, 10);
                }
            }
        }
        private void drawVerticesEdges(PaintEventArgs e)
        {
            if (network != null && network.EdgeCount > 0)
            {
                Coordinates startCoordinates = network.Map.Start;
                int startX = xWidth * (startCoordinates.X);
                int startY = yHeight * (startCoordinates.Y);
                e.Graphics.FillEllipse(Brushes.Green, startX, startY, 10, 10);

                Coordinates endCoordinates = network.Map.End;
                int endX = xWidth * (endCoordinates.X);
                int endY = yHeight * (endCoordinates.Y);
                e.Graphics.FillEllipse(Brushes.Green, endX, endY, 10, 10);

                for (int i = 0; i < network.EdgeCount; i++)
                {
                    Edge edge = network.GetEdge(i);
                    Coordinates coordinates0 = edge.Start.Coordinates;
                    Coordinates coordinates1 = edge.End.Coordinates;

                    Pen pen = Pens.Blue;
                    e.Graphics.DrawLine(pen, (coordinates0.X) * xWidth, coordinates0.Y * yHeight, coordinates1.X * xWidth, coordinates1.Y * yHeight);
                }
            }
        }
    }
}