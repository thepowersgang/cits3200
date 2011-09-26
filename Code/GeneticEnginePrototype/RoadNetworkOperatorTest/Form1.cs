using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RoadNetworkSolver;

namespace RoadNetworkOperatorTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Populate1()
        {
            RoadNetwork network = new RoadNetwork();

            Vertex start = network.AddVertex(100, 10);            
            
            Vertex a = network.AddVertex(50, 10);
            Vertex b = network.AddVertex(150, 10);
            Vertex c = network.AddVertex(50, 75);
            Vertex d = network.AddVertex(100, 75);
            Vertex e = network.AddVertex(150, 75);
            
            Vertex f = network.AddVertex(50, 150);
            Vertex g = network.AddVertex(100, 150);
            Vertex h = network.AddVertex(150, 150);

            Vertex i = network.AddVertex(50, 210);
            Vertex j = network.AddVertex(150, 210);

            Vertex end = network.AddVertex(100, 210);
            
            network.AddEdge(start, a);
            network.AddEdge(start, b);

            network.AddEdge(a, c);
            network.AddEdge(b, e);

            network.AddEdge(c, d);
            network.AddEdge(e, d);

            network.AddEdge(c, f);
            network.AddEdge(d, g);
            network.AddEdge(e, h);

            network.AddEdge(f, g);
            network.AddEdge(h, g);

            network.AddEdge(f, i);
            network.AddEdge(h, j);

            network.AddEdge(i, end);
            network.AddEdge(j, end);

            parent1.Network = network;

            RoadNetwokConjugationOperator op = new RoadNetwokConjugationOperator();

            RoadNetwork cut = new RoadNetwork(network);

            op.Cut(cut);

            cut1.Network = cut;
        }

        private void Populate2()
        {
            RoadNetwork network = new RoadNetwork();

            Vertex start = network.AddVertex(100, 10);            
            
            Vertex a = network.AddVertex(50, 50);
            Vertex b = network.AddVertex(150, 50);

            Vertex c = network.AddVertex(25, 75);
            Vertex d = network.AddVertex(100, 75);
            Vertex e = network.AddVertex(175, 75);
            
            Vertex f = network.AddVertex(100, 150);

            Vertex g = network.AddVertex(50, 190);
            Vertex h = network.AddVertex(150, 190);

            Vertex end = network.AddVertex(100, 210);
            
            network.AddEdge(start, a);
            network.AddEdge(start, b);
            network.AddEdge(a, c);
            network.AddEdge(a, d);
            network.AddEdge(b, d);
            network.AddEdge(b, e);

            network.AddEdge(c, f);
            network.AddEdge(e, f);

            network.AddEdge(f, g);
            network.AddEdge(f, h);

            network.AddEdge(g, end);
            network.AddEdge(h, end);

            parent2.Network = network;
            
            RoadNetwokConjugationOperator op = new RoadNetwokConjugationOperator();

            RoadNetwork cut = new RoadNetwork(network);

            op.Cut(cut);

            cut2.Network = cut;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Populate1();
            Populate2();

            RoadNetwokConjugationOperator op = new RoadNetwokConjugationOperator();

            RoadNetwork ch1 = null;
            RoadNetwork ch2 = null;

            try
            {
                op.Recombine(cut1.Network, cut2.Network, out ch1, out ch2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            child1.Network = ch1;
            child2.Network = ch2;
        }
    }
}
