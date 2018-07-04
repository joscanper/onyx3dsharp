using System;
using Onyx3D;

namespace Onyx3DEditor.Controls.Inspector
{
	[ComponentInspector(typeof(MeshRenderer))]
	public partial class MeshRendererInspectorControl : InspectorControl
	{
		MeshRenderer mRenderer;
		SingleMeshPreviewRenderer mPreview;

		public MeshRendererInspectorControl(MeshRenderer renderer)
		{
			mRenderer = renderer;
			InitializeComponent();

			meshAssetField.AssetChanged += new EventHandler(OnMeshSelected);
			materialAssetField.AssetChanged += new EventHandler(OnMaterialSelected);
		}

		private void MeshRendererInspectorControl_Load(object sender, EventArgs e)
		{
			//meshAssetField.Fill<Mesh>("Mesh", mRenderer.Mesh);
			UpdateMaterial();
		}

		private void UpdateMaterial()
		{
			meshAssetField.Fill<MeshSelectorWindow>("Mesh", mRenderer.Mesh);
			materialAssetField.Fill<MaterialSelectorWindow>("Material", mRenderer.Material);
			

			if (mPreview == null) { 
				mPreview = new SingleMeshPreviewRenderer();
				mPreview.Init(materialPreviewPictureBox.Width, materialPreviewPictureBox.Height, this.Handle);
			}
			mPreview.SetMaterial(mRenderer.Material.LinkedProjectAsset.Guid);
			mPreview.Render();

			materialPreviewPictureBox.Image = mPreview.AsBitmap();
		}

		private void OnMaterialSelected(object sender, EventArgs e)
		{
			mRenderer.Material = Onyx3DEngine.Instance.Resources.GetMaterial(materialAssetField.SelectedAssetGuid);
			UpdateMaterial();
			InspectorChanged?.Invoke(this, e);
		}

		private void OnMeshSelected(object sender, EventArgs e)
		{
			mRenderer.Mesh = Onyx3DEngine.Instance.Resources.GetMesh(meshAssetField.SelectedAssetGuid);
			InspectorChanged?.Invoke(this, e);
		}

		private void materialPreviewPictureBox_Click(object sender, EventArgs e)
		{
			materialAssetField.SearchAsset();
		}

		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);

			mPreview.Dispose();
			mPreview = null;
		}
	}
}
