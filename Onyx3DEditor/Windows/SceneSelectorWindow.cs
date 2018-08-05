using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Onyx3D;
using System.IO;

namespace Onyx3DEditor
{
	public partial class SceneSelectorWindow : Form
	{
		public Action<Scene> OnSceneSelected;

		public SceneSelectorWindow()
		{
			InitializeComponent();

			UpdateScenes();
		}

		private void UpdateScenes()
		{
			int i = 0;
			tableLayoutPanelScenes.ColumnCount = 4;
			tableLayoutPanelScenes.RowCount = ProjectManager.Instance.Content.Scenes.Count;
			foreach (OnyxProjectAsset sceneAsset in ProjectManager.Instance.Content.Scenes)
			{
                
				Label tbId = new Label();
				tbId.Text = sceneAsset.Guid.ToString();
				Label tbName = new Label();
				tbName.Text = sceneAsset.Name;
				Label tbPath = new Label();
				tbPath.Text = sceneAsset.Path;
				
				tableLayoutPanelScenes.Controls.Add(tbId, 1, i);
				tableLayoutPanelScenes.Controls.Add(tbName, 2, i);
				tableLayoutPanelScenes.Controls.Add(tbPath, 3, i);
				++i;
			}
		}

		private void ImportScene()
		{
			Stream myStream;
			OpenFileDialog openFileDialog1 = new OpenFileDialog();

			openFileDialog1.InitialDirectory = "c:\\";
			openFileDialog1.Filter = "Onyx3d scene files (*.o3dscene)|*.o3dscene";
			openFileDialog1.FilterIndex = 2;
			openFileDialog1.RestoreDirectory = true;

			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				try
				{
					if ((myStream = openFileDialog1.OpenFile()) != null)
					{
						string path = openFileDialog1.FileName;
						Scene scene = AssetLoader<Scene>.Load(path, false);

                        OnyxProjectAsset asset = new OnyxProjectAsset(path, Path.GetFileName(path), 0);
						ProjectManager.Instance.Content.Scenes.Add(asset);
						UpdateScenes();

						OnSceneSelected?.Invoke(scene);
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
				}
			}
		}

		private void NewScene()
		{
			Stream myStream;
			SaveFileDialog saveFileDialog1 = new SaveFileDialog();

			saveFileDialog1.InitialDirectory = "c:\\";
			saveFileDialog1.Filter = "Onyx3d scene files (*.o3dscene)|*.o3dscene";
			saveFileDialog1.FilterIndex = 2;
			saveFileDialog1.RestoreDirectory = true;

			if (saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
				try
				{
					if ((myStream = saveFileDialog1.OpenFile()) != null)
					{
						string path = saveFileDialog1.FileName;
						Scene scene = new Scene();

                        OnyxProjectAsset asset = new OnyxProjectAsset(saveFileDialog1.FileName, Path.GetFileName(path), 0);
						ProjectManager.Instance.Content.Scenes.Add(asset);
						UpdateScenes();

						OnSceneSelected?.Invoke(scene);
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
				}
			}
		}



		private void toolStripButtonImportScene_Click(object sender, EventArgs e)
		{
			ImportScene();
		}

		private void toolStripButtonNewScene_Click(object sender, EventArgs e)
		{
			NewScene();
		}
	}
}
