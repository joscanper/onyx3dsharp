using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Imaging;

using Onyx3D;

namespace Onyx3DEditor
{

	public partial class MaterialViewList : UserControl
	{
		public event EventHandler SelectedChanged;

		private static readonly int mPreviewSize = 80;

        private List<OnyxProjectAsset> mMaterials = new List<OnyxProjectAsset>();
        private SingleMeshPreviewRenderer mPreview;
		private int mSelectedIndex;
        
        // --------------------------------------------------------------------

        public int SelectedIndex
		{
			get
			{
				return mSelectedIndex;
			}
			set
			{
				if (value > listViewMaterials.Items.Count - 1)
					return;

				listViewMaterials.SelectedIndices.Clear();
				listViewMaterials.SelectedIndices.Add(value);
				mSelectedIndex = value;
			}
		}

        // --------------------------------------------------------------------

        public OnyxProjectAsset SelectedMaterial
		{
			get { return mMaterials[SelectedIndex]; }
		}

        // --------------------------------------------------------------------

        public MaterialViewList()
		{
			InitializeComponent();
		}

        // --------------------------------------------------------------------

        public void UpdateMaterialList(int selectedGuid, bool addBuiltIn)
		{
            if (mPreview == null) 
            {
                mPreview = new SingleMeshPreviewRenderer();
                mPreview.Init(mPreviewSize, mPreviewSize, this.Handle);
            }

			mMaterials.Clear();

			listViewMaterials.Items.Clear();
			listViewMaterials.SelectedIndices.Clear();
			listViewMaterials.LargeImageList = new ImageList();
			listViewMaterials.LargeImageList.ColorDepth = ColorDepth.Depth32Bit;
			listViewMaterials.LargeImageList.ImageSize = new Size(mPreviewSize, mPreviewSize);

            if (addBuiltIn)
            {
                AddElement(ProjectManager.Instance.Content.GetAsset(BuiltInMaterial.Default), 0, selectedGuid);
                AddElement(ProjectManager.Instance.Content.GetAsset(BuiltInMaterial.Unlit), 1, selectedGuid);
                AddElement(ProjectManager.Instance.Content.GetAsset(BuiltInMaterial.UnlitVertexColor), 2, selectedGuid);
            }

            int i = mMaterials.Count;
			foreach (OnyxProjectAsset matAsset in ProjectManager.Instance.Content.Materials)
			{
                AddElement(matAsset, i, selectedGuid);
				i++;
			}
		}

        // --------------------------------------------------------------------

        private void AddElement(OnyxProjectAsset asset, int index, int selectedGuid)
        {
            Bitmap bmp = GenerateMaterialPreview(asset.Guid);
            Image small_img = bmp.GetThumbnailImage(mPreviewSize, mPreviewSize, null, IntPtr.Zero);

            listViewMaterials.LargeImageList.Images.Add(small_img);
			
			listViewMaterials.Items.Add(new ListViewItem(asset.Name, index));

			mMaterials.Add(asset);

			if (asset.Guid == selectedGuid)
                listViewMaterials.SelectedIndices.Add(index);
            
        }

        // --------------------------------------------------------------------

        private Bitmap GenerateMaterialPreview(int guid)
		{
			mPreview.SetMaterial(guid);
			mPreview.Render();
			Bitmap preview = mPreview.AsBitmap();
			
			return preview;
		}

        // --------------------------------------------------------------------

        private void ListViewMaterials_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listViewMaterials.SelectedIndices.Count > 0)
			{ 
				mSelectedIndex = listViewMaterials.SelectedIndices[0];
				SelectedChanged?.Invoke(this, e);
			}
			else
			{
				mSelectedIndex = -1;
			}
		}

        // --------------------------------------------------------------------

        public void UpdateMaterial(int guid)
		{
			int i = 0;
			listViewMaterials.SelectedIndices.Clear();
			foreach (OnyxProjectAsset t in ProjectManager.Instance.Content.Materials)
			{
				if (t.Guid == guid)
				{
					Bitmap bitmap = GenerateMaterialPreview(t.Guid);
					listViewMaterials.LargeImageList.Images[i] = bitmap.GetThumbnailImage(mPreviewSize, mPreviewSize, null, IntPtr.Zero);
					listViewMaterials.Items[i].Text = t.Name;
					listViewMaterials.SelectedIndices.Add(i);
					listViewMaterials.Refresh();
					return;
				}

				i++;
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
