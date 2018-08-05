using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using Onyx3D;

namespace Onyx3DEditor
{
	public partial class MeshViewList : UserControl
	{
		public event EventHandler SelectedChanged;

		private static readonly int mPreviewSize = 80;

		private SingleMeshPreviewRenderer mPreview;
		private int mSelectedIndex;
		private List<OnyxProjectAsset> mMeshes = new List<OnyxProjectAsset>();

        // --------------------------------------------------------------------

        public int SelectedIndex
		{
			get
			{
				return mSelectedIndex;
			}
			set
			{
				if (value > listViewMeshes.Items.Count - 1)
					return;

				listViewMeshes.SelectedIndices.Clear();
				listViewMeshes.SelectedIndices.Add(value);
				mSelectedIndex = value;
			}
		}

        // --------------------------------------------------------------------

        public OnyxProjectAsset SelectedMesh
		{
			get { return mMeshes[SelectedIndex]; }
		}

        // --------------------------------------------------------------------

        public MeshViewList()
		{
			InitializeComponent();
		}

        // --------------------------------------------------------------------

        public void UpdateMaterialList(int selectedGuid)
		{
			mMeshes.Clear();

			listViewMeshes.Items.Clear();
			listViewMeshes.SelectedIndices.Clear();
			listViewMeshes.LargeImageList = new ImageList();
			listViewMeshes.LargeImageList.ColorDepth = ColorDepth.Depth32Bit;
			listViewMeshes.LargeImageList.ImageSize = new Size(mPreviewSize, mPreviewSize);

			AddElement(ProjectManager.Instance.Content.GetAsset(BuiltInMesh.Cube), 0, selectedGuid);
			AddElement(ProjectManager.Instance.Content.GetAsset(BuiltInMesh.Sphere), 1, selectedGuid);
			AddElement(ProjectManager.Instance.Content.GetAsset(BuiltInMesh.Quad), 2, selectedGuid);
			AddElement(ProjectManager.Instance.Content.GetAsset(BuiltInMesh.Cylinder), 3, selectedGuid);
			AddElement(ProjectManager.Instance.Content.GetAsset(BuiltInMesh.Torus), 4, selectedGuid);
			AddElement(ProjectManager.Instance.Content.GetAsset(BuiltInMesh.Teapot), 5, selectedGuid);
			
			int i = 6;
			foreach (OnyxProjectAsset t in ProjectManager.Instance.Content.Meshes)
			{

				AddElement(t, i, selectedGuid);
				i++;
			}
		}

        // --------------------------------------------------------------------

        private void AddElement(OnyxProjectAsset t, int index, int selectedGuid)
		{
			Bitmap bmp = GenerateMeshPreview(t.Guid);
			Image small_img = bmp.GetThumbnailImage(mPreviewSize, mPreviewSize, null, IntPtr.Zero);

			listViewMeshes.LargeImageList.Images.Add(small_img);
			listViewMeshes.Items.Add(new ListViewItem(t.Name, index));

			if (t.Guid == selectedGuid)
				listViewMeshes.SelectedIndices.Add(index);

			mMeshes.Add(t);
		}

        // --------------------------------------------------------------------

        private Bitmap GenerateMeshPreview(int guid)
		{
			mPreview.SetMesh(guid);
			mPreview.Render();
			return mPreview.AsBitmap();
		}

        // --------------------------------------------------------------------

        private void MeshViewList_Load(object sender, EventArgs e)
		{
		
			if (!DesignMode)
			{
				mPreview = new SingleMeshPreviewRenderer();
				mPreview.Init(mPreviewSize, mPreviewSize, this.Handle);
				mPreview.SetFloorActive(false);
				mPreview.Render();
				UpdateMaterialList(0);
			}
		
		}

        // --------------------------------------------------------------------

        private void ListViewMeshes_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listViewMeshes.SelectedIndices.Count > 0)
			{ 
				mSelectedIndex = listViewMeshes.SelectedIndices[0];
				SelectedChanged?.Invoke(this, e);
			}
			else
			{
				mSelectedIndex = -1;
			}
		}

        // --------------------------------------------------------------------

        public void UpdateMesh(int guid)
		{
			int i = 0;
			listViewMeshes.SelectedIndices.Clear();
			foreach (OnyxProjectAsset t in ProjectManager.Instance.Content.Meshes)
			{
				if (t.Guid == guid)
				{
					Bitmap bitmap = GenerateMeshPreview(t.Guid);
					listViewMeshes.LargeImageList.Images[i] = bitmap.GetThumbnailImage(mPreviewSize, mPreviewSize, null, IntPtr.Zero);
					listViewMeshes.Items[i].Text = t.Name;
					listViewMeshes.SelectedIndices.Add(i);
					listViewMeshes.Refresh();
					return;
				}

				i++;
			}
		}

        // --------------------------------------------------------------------

        protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);

			mPreview.Dispose();
			mPreview = null;
		}
	}
}
