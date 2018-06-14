using System;
using Onyx3D;

namespace Onyx3DEditor.Controls.Inspector
{
	[ComponentInspector(typeof(MeshRenderer))]
	public partial class MeshRendererInspectorControl : InspectorControl
	{
		MeshRenderer mRenderer;
		MaterialPreviewRenderer mPreview;

		public MeshRendererInspectorControl(MeshRenderer renderer)
		{
			mRenderer = renderer;
			InitializeComponent();
			materialAssetField.AssetChanged += new EventHandler(OnMaterialSelected);
		}

		private void MeshRendererInspectorControl_Load(object sender, EventArgs e)
		{
			//meshAssetField.Fill<Mesh>("Mesh", mRenderer.Mesh);
			UpdateMaterial();
		}

		private void UpdateMaterial()
		{
			materialAssetField.Fill<MaterialSelectorWindow>("Material", mRenderer.Material);
			

			if (mPreview == null) { 
				mPreview = new MaterialPreviewRenderer();
				mPreview.Init(materialPreviewPictureBox.Width, materialPreviewPictureBox.Height);
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

		private void materialPreviewPictureBox_Click(object sender, EventArgs e)
		{
			materialAssetField.SearchAsset();
		}
	}
}
