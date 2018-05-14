using System;
using System.Windows.Forms;

using Onyx3D;

namespace Onyx3DEditor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            ProjectLoader.Load("../../../TestProject/test2.o3dproj");

			Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }
    }
}
