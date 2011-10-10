using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace RoadNetworkGUI
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
            Road_Network_Visualiser program = new Road_Network_Visualiser(false, null);
            try
            {
                Application.Run(program);
                program.Dispose();
            }
            catch (Exception)
            {
            }
        }
    }
}
