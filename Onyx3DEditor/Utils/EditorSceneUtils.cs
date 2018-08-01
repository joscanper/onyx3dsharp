using Onyx3D;
using System;
using System.IO;
using System.Windows.Forms;

namespace Onyx3DEditor
{
    public static class EditorSceneUtils
    {
        public static void New()
        {
            mSceneAsset = null;
            mScene = new Scene();
            sceneHierarchy.SetScene(mScene);
            renderCanvas.Refresh();
        }

        // --------------------------------------------------------------------

        public static void Save()
        {

            if (mSceneAsset == null)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Onyx3d scene files (*.o3dscene)|*.o3dscene";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    AssetLoader<Scene>.Save(mScene, saveFileDialog1.FileName);
                    try
                    {
                        mSceneAsset = new OnyxProjectSceneAsset(saveFileDialog1.FileName);
                        ProjectManager.Instance.Content.Scenes.Add(mSceneAsset);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error saving the scene : " + ex.Message);
                    }


                }
            }
            else
            {
                AssetLoader<Scene>.Save(mScene, mSceneAsset.Path);
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
                        mScene = AssetLoader<Scene>.Load(openFileDialog1.FileName);
                        renderCanvas.Refresh();
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
