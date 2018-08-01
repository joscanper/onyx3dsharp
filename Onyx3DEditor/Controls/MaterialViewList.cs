﻿using System;
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
		private SingleMeshPreviewRenderer mPreview;
		private int mSelectedIndex;

		private List<OnyxProjectMaterialAsset> mMaterials = new List<OnyxProjectMaterialAsset>();


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

		public OnyxProjectMaterialAsset SelectedMaterial
		{
			get { return mMaterials[SelectedIndex]; }
		}
		
		public MaterialViewList()
		{
			InitializeComponent();
		}
		
		public void UpdateMaterialList(int selectedGuid)
		{
			mPreview.Init(mPreviewSize, mPreviewSize, this.Handle);
			mMaterials.Clear();

			listViewMaterials.Items.Clear();
			listViewMaterials.SelectedIndices.Clear();
			listViewMaterials.LargeImageList = new ImageList();
			listViewMaterials.LargeImageList.ColorDepth = ColorDepth.Depth32Bit;
			listViewMaterials.LargeImageList.ImageSize = new Size(mPreviewSize, mPreviewSize);

			int i = 0;
			foreach (OnyxProjectMaterialAsset t in ProjectManager.Instance.Content.Materials)
			{
				
				Bitmap bmp = GenerateMaterialPreview(t.Guid);
				Image small_img = bmp.GetThumbnailImage(mPreviewSize, mPreviewSize, null, IntPtr.Zero);

				listViewMaterials.LargeImageList.Images.Add(small_img);
				listViewMaterials.Items.Add(new ListViewItem(t.Name, i));

				if (t.Guid == selectedGuid)
					listViewMaterials.SelectedIndices.Add(i);

				mMaterials.Add(t);

				i++;
			}
		}
		
		private Bitmap GenerateMaterialPreview(int guid)
		{
			
			mPreview.SetMaterial(guid);
			mPreview.Render();
			Bitmap preview = mPreview.AsBitmap();
			
			
			return preview;
		}

		private void MaterialViewList_Load(object sender, EventArgs e)
		{
		
			if (!DesignMode)
			{
				mPreview = new SingleMeshPreviewRenderer();
				mPreview.Init(mPreviewSize, mPreviewSize, this.Handle);
				mPreview.Render();
				UpdateMaterialList(0);
			}
		
		}

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

		public void UpdateMaterial(int guid)
		{
			int i = 0;
			listViewMaterials.SelectedIndices.Clear();
			foreach (OnyxProjectMaterialAsset t in ProjectManager.Instance.Content.Materials)
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


		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);

			mPreview.Dispose();
			mPreview = null;
		}
	}
}
