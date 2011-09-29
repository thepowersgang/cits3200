using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using RoadNetworkGUI;

namespace RoadNetworkOperatorTest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new RoadNetworkFinder());
            Application.Run(new Road_Network_Visualiser());
        }
    }
}
