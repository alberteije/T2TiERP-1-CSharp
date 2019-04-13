using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;
using UniNFeLibrary;

namespace uninfe
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Esta deve ser a primeira linha do Main, não coloque nada antes dela. Wandrey 31/07/2009
            InfoApp.oAssemblyEXE = Assembly.GetExecutingAssembly(); 

            System.Threading.Mutex oneMutex = null;

            if (InfoApp.AppExecutando(ref oneMutex))
            {
                return;
            }

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            finally 
            {
                if (oneMutex != null)
                {
                    oneMutex.Close();
                }
            }
        }
    }
}
