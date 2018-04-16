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
		public OnyxProjectAsset SelectedTexture;

		private List<OnyxProjectAsset> mTextureIds;

        private ResourcesManager mResources;

		public TextureSelector()
		{
			InitializeComponent();
			FillTexturesList();
		}


		private void FillTexturesList()
		{
			listViewTextures.Items.Clear();
			
            mTextureIds = new List<OnyxProjectAsset>();
			listViewTextures.SmallImageList = new ImageList();
			listViewTextures.SmallImageList.ImageSize = new Size(64, 64);

            AddTexture(ProjectManager.Instance.Content.GetAsset(BuiltInTexture.Checker));
            AddTexture(ProjectManager.Instance.Content.GetAsset(BuiltInTexture.Black));
            AddTexture(ProjectManager.Instance.Content.GetAsset(BuiltInTexture.White));

            foreach (OnyxProjectAsset t in ProjectManager.Instance.Content.Textures)
                AddTexture(t);
			
		}

        private void AddTexture(OnyxProjectAsset t)
        {
            Bitmap bmp = new Bitmap(t.AbsolutePath);
            Image small_img = bmp.GetThumbnailImage(64, 64, null, IntPtr.Zero);
            listViewTextures.SmallImageList.Images.Add(small_img);
            listViewTextures.Items.Add(new ListViewItem(t.Guid.ToString(), mTextureIds.Count));
            mTextureIds.Add(t);
        }

		private void OnTextureSelected(object sender, EventArgs e)
		{
            SelectedTexture = mTextureIds[listViewTextures.SelectedItems[0].Index];
            TextureSelected.Invoke(this, null);
			Close();
		}
	}
}
