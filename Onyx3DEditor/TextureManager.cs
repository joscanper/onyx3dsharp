using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Onyx3DEditor
{
	public partial class TextureManager : Form
	{
		private TextureDBEntry mCurrentTexture;
		//private TextureDBEntry mChangedTexture;


		public TextureManager()
		{
			InitializeComponent();
			FillTexturesList();
		}

		private void FillTexturesList()
		{
			listViewTextures.Items.Clear();
			int i = 0;

			
			//listViewTextures.LargeImageList = new ImageList();
			//listViewTextures.LargeImageList.ImageSize = new Size(128, 128);
			listViewTextures.SmallImageList = new ImageList();
			listViewTextures.SmallImageList.ImageSize = new Size(64,64);
			foreach (TextureDBEntry t in ProjectManager.Instance.Content.Textures)
			{
				Bitmap bmp = new Bitmap(t.path);
				Image small_img = bmp.GetThumbnailImage(64, 64, null, IntPtr.Zero);
				//Image big_img = bmp.GetThumbnailImage(256, 256, null, IntPtr.Zero);
				listViewTextures.SmallImageList.Images.Add(small_img);
				//listViewTextures.LargeImageList.Images.Add(big_img);
				listViewTextures.Items.Add(new ListViewItem(t.id, i));
				i++;
			}
		}

		private void toolStripButtonOpen_Click(object sender, EventArgs e)
		{
			string filePath = OpenFileSelector(ProjectManager.Instance.CurrentProjectPath);
			if (filePath.Length > 0)
				CreateNewTexture(filePath);
		}

		private string OpenFileSelector(string initPath)
		{
			OpenFileDialog openFileDialog1 = new OpenFileDialog();
			openFileDialog1.InitialDirectory = initPath;
			openFileDialog1.Filter = "PNG files (*.png)|*.png|JPG files (*.jpg)|*.jpg";//All files (*.*)|*.*
			openFileDialog1.FilterIndex = 2;
			openFileDialog1.RestoreDirectory = true;
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				return openFileDialog1.FileName;
			}
			return "";
		}

		private void CreateNewTexture(string path)
		{
			mCurrentTexture = new TextureDBEntry();
			mCurrentTexture.path = path;
			mCurrentTexture.id = "NONE";
			ProjectManager.Instance.Content.Textures.Add(mCurrentTexture);

			UpdateTextureInfo();
			FillTexturesList();
		}

		private void UpdateTextureInfo()
		{
			textBoxFilePath.Show();
			textBoxId.Show();
			textBoxFilePath.Text = mCurrentTexture.path;
			textBoxId.Text = mCurrentTexture.id;
			LoadTexturePreview();
		}

		private void LoadTexturePreview()
		{
			Bitmap bmp = new Bitmap(textBoxFilePath.Text);
			pictureBoxPreview.Image = bmp.GetThumbnailImage(256, 256, null, IntPtr.Zero);
		}

		private void ClearTextureInfo()
		{
			textBoxFilePath.Hide();
			textBoxId.Hide();
			pictureBoxPreview.Image = null;
		}

		
		private void listViewTextures_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listViewTextures.SelectedIndices.Count > 0) { 
				mCurrentTexture = ProjectManager.Instance.Content.Textures[listViewTextures.SelectedIndices[0]];
				UpdateTextureInfo();
			}
			else
			{
				ClearTextureInfo();
			}
		}

		private void buttonApply_Click(object sender, EventArgs e)
		{
			if (mCurrentTexture != null)
			{
				mCurrentTexture.id = textBoxId.Text;
				mCurrentTexture.path = textBoxFilePath.Text;
				FillTexturesList();
			}
		}

		private void buttonOpen_Click(object sender, EventArgs e)
		{
			string filePath = OpenFileSelector(textBoxFilePath.Text);
			if (filePath.Length > 0) {
				textBoxFilePath.Text = filePath;
				LoadTexturePreview();
			}
		}
		
	}

}
