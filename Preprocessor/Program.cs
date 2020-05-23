using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Preprocessor
{
    static class Program
    {
        static public Preprocessor p;
        /// <summary>
        /// The main entry node for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            p = new Preprocessor(args);
            Application.Run(p);            
        }
    }
}
