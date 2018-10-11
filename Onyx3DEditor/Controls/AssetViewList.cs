using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using Onyx3D;

namespace Onyx3DEditor
{
	public abstract partial class AssetViewList : UserControl
	{
		public event EventHandler SelectedChanged;

		private static readonly int mPreviewSize = 80;

		protected SingleMeshPreviewRenderer mPreview;
		private int mSelectedIndex;
		private List<OnyxProjectAsset> mAssets = new List<OnyxProjectAsset>();

		// --------------------------------------------------------------------

		public int SelectedIndex
		{
			get
			{
				return mSelectedIndex;
			}
			set
			{
				if (value > listView.Items.Count - 1)
					return;

				listView.SelectedIndices.Clear();
				listView.SelectedIndices.Add(value);
				mSelectedIndex = value;
			}
		}

		// --------------------------------------------------------------------

		public OnyxProjectAsset SelectedAsset
		{
			get { return mAssets != null ? mAssets[SelectedIndex] : null; }
		}

		// --------------------------------------------------------------------

		public AssetViewList()
		{
			InitializeComponent();
		}

		// --------------------------------------------------------------------

		public void UpdateList(int selectedGuid, bool addBuiltIn = true)
		{
			mAssets.Clear();

			listView.Items.Clear();
			listView.SelectedIndices.Clear();
			listView.LargeImageList = new ImageList();
			listView.LargeImageList.ColorDepth = ColorDepth.Depth32Bit;
			listView.LargeImageList.ImageSize = new Size(mPreviewSize, mPreviewSize);

			AddBuiltIn(selectedGuid);

			int i = listView.Items.Count;
			List<OnyxProjectAsset> assets = GetAssets();
			foreach (OnyxProjectAsset t in assets)
			{
				AddElement(t, i, selectedGuid);
				i++;
			}
		}
		
		// --------------------------------------------------------------------
		public void UpdateAsset(int guid)
		{
			int i = 0;
			listView.SelectedIndices.Clear();
			foreach (OnyxProjectAsset t in ProjectManager.Instance.Content.Materials)
			{
				if (t.Guid == guid)
				{
					Bitmap bitmap = GeneratePreview(t.Guid);
					listView.LargeImageList.Images[i] = bitmap.GetThumbnailImage(mPreviewSize, mPreviewSize, null, IntPtr.Zero);
					listView.Items[i].Text = t.Name;
					listView.SelectedIndices.Add(i);
					listView.Refresh();
					return;
				}

				i++;
			}
		}

		// --------------------------------------------------------------------

		protected abstract List<OnyxProjectAsset> GetAssets();

		// --------------------------------------------------------------------

		protected abstract void AddBuiltIn(int selectedGuid);

		// --------------------------------------------------------------------

		protected void AddElement(OnyxProjectAsset t, int index, int selectedGuid)
		{
			Bitmap bmp = GeneratePreview(t.Guid);
			Image small_img = bmp.GetThumbnailImage(mPreviewSize, mPreviewSize, null, IntPtr.Zero);

			listView.LargeImageList.Images.Add(small_img);
			listView.Items.Add(new ListViewItem(t.Name, index));

			if (t.Guid == selectedGuid)
				listView.SelectedIndices.Add(index);

			mAssets.Add(t);
		}

		// --------------------------------------------------------------------

		protected abstract Bitmap GeneratePreview(int guid);

		// --------------------------------------------------------------------

		private void AssetViewList_Load(object sender, EventArgs e)
		{

			if (!DesignMode)
			{
				mPreview = new SingleMeshPreviewRenderer();
				mPreview.Init(mPreviewSize, mPreviewSize, this.Handle);
				mPreview.SetFloorActive(false);
				mPreview.Render();
				UpdateList(0);
			}

		}

		// --------------------------------------------------------------------

		private void ListViewMeshes_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listView.SelectedIndices.Count > 0)
			{
				mSelectedIndex = listView.SelectedIndices[0];
				SelectedChanged?.Invoke(this, e);
			}
			else
			{
				mSelectedIndex = -1;
			}
			
		}

		// --------------------------------------------------------------------

		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);

			if (mPreview != null)
				mPreview.Dispose();

			mPreview = null;
		}
	}
}
