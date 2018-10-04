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
	public partial class TextureSelectorWindow : Form
	{
		public event EventHandler TextureSelected;
		public OnyxProjectAsset SelectedTexture;

		private List<OnyxProjectAsset> mTextureIds;

		public TextureSelectorWindow()
		{
			InitializeComponent();
			FillTexturesList();
		}


		private void FillTexturesList()
		{
			listViewTextures.Items.Clear();
			
            mTextureIds = new List<OnyxProjectAsset>();
			listViewTextures.SmallImageList = new ImageList();
            listViewTextures.SmallImageList.ColorDepth = ColorDepth.Depth32Bit;
            listViewTextures.SmallImageList.ImageSize = new Size(64, 64);

            AddTextureToList(ProjectManager.Instance.Content.GetAsset(BuiltInTexture.Checker));
            AddTextureToList(ProjectManager.Instance.Content.GetAsset(BuiltInTexture.Black));
            AddTextureToList(ProjectManager.Instance.Content.GetAsset(BuiltInTexture.White));
			AddTextureToList(ProjectManager.Instance.Content.GetAsset(BuiltInTexture.Normal));

			foreach (OnyxProjectAsset t in ProjectManager.Instance.Content.Textures)
                AddTextureToList(t);
			
		}

        private void AddTextureToList(OnyxProjectAsset t)
        {
            listViewTextures.SmallImageList.Images.Add(GetThumbnail(t.AbsolutePath));
            listViewTextures.Items.Add(new ListViewItem(t.Guid.ToString(), mTextureIds.Count));
            mTextureIds.Add(t);
        }

		Image GetThumbnail(string path)
		{
			Bitmap bmp = new Bitmap(path);
			return bmp.GetThumbnailImage(64, 64, null, IntPtr.Zero);
		}

		private void OnTextureSelected(object sender, EventArgs e)
		{
            SelectedTexture = mTextureIds[listViewTextures.SelectedItems[0].Index];
            TextureSelected.Invoke(this, null);
			Close();
		}
	}
}
