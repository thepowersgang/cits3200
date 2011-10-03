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
        List<Coordinates> towns = null;
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

        public List<Coordinates> Towns
        {
            get
            {
                return towns;
            }
            set
            {
                towns = value;
            }
        }
        private void RoadNetworkPanel_Paint(object sender, PaintEventArgs e)
        {
            drawGrid(e);
            drawTowns(e);
            drawVerticesEdges(e);
        }
        private void drawGrid(PaintEventArgs e)
        {
            Pen pen = Pens.Black;
            int startX = 0;
            int startY = 0;
            int finishX = Width - startX;
            int finishY = Height - startY;
            e.Graphics.DrawLine(pen, startX, startY, startX, finishY);
            e.Graphics.DrawLine(pen, startX, startY, finishX, startY);
            e.Graphics.DrawLine(pen, startX, finishY, finishX, finishY);
            e.Graphics.DrawLine(pen, finishX, startY, finishX, finishY);
            int colWidth = (int)Math.Ceiling((double)(finishX - startX) / 10);
            int rowWidth = (int)Math.Ceiling((double)(finishY - startY) / 10);
            for (int i = startX + colWidth; i < finishX; i = i + colWidth)
            {
                e.Graphics.DrawLine(pen, i, startY, i, finishY);
            }
            for (int j = startY + rowWidth; j < finishY; j = j + rowWidth)
            {
                e.Graphics.DrawLine(pen, startX, j, finishX, j);
            }
        }
        private void drawTowns(PaintEventArgs e)
        {
            if (Towns != null)
            {
                for (int i = 0; i < towns.Count; i++)
                {
                    e.Graphics.FillEllipse(Brushes.Red, towns[i].X - 2, towns[i].Y - 2, 5, 5);
                }
            }
        }
        private void drawVerticesEdges(PaintEventArgs e)
        {
            if (network != null)
            {
                Coordinates startCoordinates = network.Start.Coordinates;
                e.Graphics.FillEllipse(Brushes.Green, startCoordinates.X - 2, startCoordinates.Y - 2, 5, 5);

                Coordinates endCoordinates = network.End.Coordinates;
                e.Graphics.FillEllipse(Brushes.Green, endCoordinates.X - 2, endCoordinates.Y - 2, 5, 5);

                for (int i = 0; i < network.EdgeCount; i++)
                {
                    Edge edge = network.GetEdge(i);
                    Coordinates coordinates0 = edge.Start.Coordinates;
                    Coordinates coordinates1 = edge.End.Coordinates;

                    Pen pen = Pens.Blue;
                    e.Graphics.DrawLine(pen, coordinates0.X, coordinates0.Y, coordinates1.X, coordinates1.Y);
                }
            }
        }
    }
}