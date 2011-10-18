using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using RoadNetworkDefinition;
using RoadNetworkGUI;

namespace RoadNetworkVisualiser
{
    static class Visualiser
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Road_Network_Visualiser(false,null));
        }
    }
}
