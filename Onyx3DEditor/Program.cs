using System;
using System.IO;
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
            string lastProject = Properties.Settings.Default.LastProjectPath;
            if (lastProject.Length > 0 && File.Exists(lastProject))
                ProjectLoader.Load(lastProject);
            else
                ProjectManager.Instance.New();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(MainWindow.Instance);
        }
    }
}
