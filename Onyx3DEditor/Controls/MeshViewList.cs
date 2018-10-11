using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using Onyx3D;

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

		protected override Bitmap GeneratePreview(int guid)
		{
			mPreview.SetMesh(guid);
			mPreview.Render();
			return mPreview.AsBitmap();
		}

		// --------------------------------------------------------------------
		protected override List<OnyxProjectAsset> GetAssets()
		{
			return ProjectManager.Instance.Content.Meshes;
		}
	}
}
