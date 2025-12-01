using Homiepet_Corner_Sales_and_Inventory_Management_System.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Homiepet_Corner_Sales_and_Inventory_Management_System
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());

            // Optional: set DataDirectory if using .mdf relative to EXE
            // var dataDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            // AppDomain.CurrentDomain.SetData("DataDirectory", dataDir);
            Application.ApplicationExit += (_, __) => red_framework.AppDb.Dispose();

        }   
    }
}
