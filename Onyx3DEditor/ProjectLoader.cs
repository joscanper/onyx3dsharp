using System;
using Onyx3D;
using System.Windows.Forms;
using System.IO;

namespace Onyx3DEditor
{
    class ProjectLoader
    {

        public static void Save()
        {
            if (ProjectManager.Instance.CurrentProjectPath.Length == 0)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Title = "Save Project";
                saveFileDialog1.Filter = "Onyx3d project files (*.o3dproj)|*.o3dproj";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        ProjectManager.Instance.Save(saveFileDialog1.FileName);

                        Properties.Settings.Default.LastProjectPath = saveFileDialog1.FileName;
                        Properties.Settings.Default.Save();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: Could not save the project: " + ex.StackTrace);
                    }
                }
            }
            else
            {
                ProjectManager.Instance.Save();
            }
        }
        
        // --------------------------------------------------------------------

        public static void Load(string path)
        {
            ProjectManager.Instance.Load(path);
        }

    }
}
