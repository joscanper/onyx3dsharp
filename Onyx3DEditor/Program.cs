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
            if (Properties.Settings.Default.LastProjectPath.Length > 0)
                ProjectLoader.Load(Properties.Settings.Default.LastProjectPath);
            else
                ProjectManager.Instance.New();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }
    }
}
