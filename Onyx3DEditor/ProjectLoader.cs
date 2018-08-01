using System;
using Onyx3D;
using System.Windows.Forms;

namespace Onyx3DEditor
{
    class ProjectLoader
    {

        public static void Save()
        {
            if (ProjectManager.Instance.Content == null)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Onyx3d project files (*.o3dproj)|*.o3dproj";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Properties.Settings.Default.LastProjectPath = saveFileDialog1.FileName;
                        ProjectManager.Instance.Save(saveFileDialog1.FileName);
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

        public static void Load(string path)
        {
            try
            {
                ProjectManager.Instance.Load(path);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error loading the project : " + e.Message);
                ProjectManager.Instance.New();
            }

        }

    }
}
