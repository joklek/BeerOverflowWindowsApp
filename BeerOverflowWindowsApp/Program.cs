using BeerOverflowWindowsApp.DataModels;
using System;
using System.Windows.Forms;
using System.Linq;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace BeerOverflowWindowsApp
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
            Application.Run(new MainWindow());
        }
    }
}
