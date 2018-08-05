using Onyx3D;
using System;
using System.IO;
using System.Windows.Forms;

namespace Onyx3DEditor
{
    public static class EditorSceneUtils
    {

        // --------------------------------------------------------------------

        public static void Save()
        {

            if (SceneManagement.ActiveSceneAsset == null)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Onyx3d scene files (*.o3dscene)|*.o3dscene";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        AssetLoader<Scene>.Save(SceneManagement.ActiveScene, saveFileDialog1.FileName, false);
                        ProjectManager.Instance.Content.AddScene(saveFileDialog1.FileName, false, SceneManagement.ActiveScene);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error saving the scene : " + ex.Message);
                    }


                }
            }
            else
            {
                AssetLoader<Scene>.Save(SceneManagement.ActiveScene, SceneManagement.ActiveSceneAsset.Path);
            }
        }

        // --------------------------------------------------------------------

        public static void Open()
        {
            Stream myStream;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "Onyx3d project files (*.o3dscene)|*.o3dscene";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        // TODO - Load scenes that are assets in the project, not files from hard drive

                        //mScene = AssetLoader<Scene>.Load(openFileDialog1.FileName);
                        //renderCanvas.Refresh();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }
    }
}
