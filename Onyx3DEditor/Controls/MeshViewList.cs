using Onyx3D;
using System.Collections.Generic;
using System.Drawing;

namespace Onyx3DEditor
{
	public partial class MeshViewList : AssetViewList
	{
		protected override void AddBuiltIn(int selectedGuid)
		{
			AddElement(ProjectManager.Instance.Content.GetAsset(BuiltInMesh.Cube), 0, selectedGuid);
			AddElement(ProjectManager.Instance.Content.GetAsset(BuiltInMesh.Sphere), 1, selectedGuid);
			AddElement(ProjectManager.Instance.Content.GetAsset(BuiltInMesh.Quad), 2, selectedGuid);
			AddElement(ProjectManager.Instance.Content.GetAsset(BuiltInMesh.Cylinder), 3, selectedGuid);
			AddElement(ProjectManager.Instance.Content.GetAsset(BuiltInMesh.Torus), 4, selectedGuid);
			AddElement(ProjectManager.Instance.Content.GetAsset(BuiltInMesh.Teapot), 5, selectedGuid);
		}

		// --------------------------------------------------------------------

		protected override PreviewRenderer InstantiatePreviewRenderer()
		{
			return new SingleMeshPreviewRenderer();
		}

		// --------------------------------------------------------------------

		protected override Bitmap GeneratePreview(int guid)
		{
			SingleMeshPreviewRenderer meshPreview = (SingleMeshPreviewRenderer)mPreview;
			meshPreview.SetMesh(guid);
			meshPreview.SetFloorActive(false);
			meshPreview.Render();
			return meshPreview.AsBitmap();
		}

		// --------------------------------------------------------------------
		protected override List<OnyxProjectAsset> GetAssets()
		{
			return ProjectManager.Instance.Content.Meshes;
		}
	}
}