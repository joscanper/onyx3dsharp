using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Onyx3D;

namespace Onyx3DEditor
{
	public partial class TextureSelector : Form
	{
		public event EventHandler TextureSelected;
		public Texture SelectedTexture;

		private OnyxProjectAsset[] mTextureIds;

		public TextureSelector()
		{
			InitializeComponent();
			FillTexturesList();
		}


		private void FillTexturesList()
		{
			listViewTextures.Items.Clear();
			int i = 0;

			mTextureIds = new OnyxProjectAsset[ProjectManager.Instance.Content.Textures.Count];
			listViewTextures.SmallImageList = new ImageList();
			listViewTextures.SmallImageList.ImageSize = new Size(64, 64);
			foreach (OnyxProjectAsset t in ProjectManager.Instance.Content.Textures)
			{
				Bitmap bmp = new Bitmap(ProjectContent.GetAbsolutePath(t.Path));
				Image small_img = bmp.GetThumbnailImage(64, 64, null, IntPtr.Zero);
				listViewTextures.SmallImageList.Images.Add(small_img);
				listViewTextures.Items.Add(new ListViewItem(t.Guid.ToString(), i));
				mTextureIds[i] = t;
				i++;
			}
		}

		private void OnTextureSelected(object sender, EventArgs e)
		{
			OnyxProjectAsset textureEntry = mTextureIds[listViewTextures.SelectedItems[0].Index];
			SelectedTexture = new Texture(ProjectContent.GetAbsolutePath(textureEntry.Path));
			TextureSelected.Invoke(this, null);
			Close();
		}
	}
}
