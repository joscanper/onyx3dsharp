using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Onyx3D;
using OpenTK;

namespace Onyx3DEditor
{
	public class SingleMeshPreviewRenderer : PreviewRenderer
	{
		
		private MeshRenderer mRenderer;

		private SceneObject mFloor;

		public override void InitializeBasicScene()
		{
			base.InitializeBasicScene();

			SceneObject sphere = new SceneObject("sphere");
			mRenderer = sphere.AddComponent<MeshRenderer>();
			mRenderer.Material = OnyxInstance.Resources.GetMaterial(BuiltInMaterial.Default);
			SetMesh(BuiltInMesh.Sphere);
			sphere.Parent = Scene.Root;

			mFloor = new SceneObject("floor");
			MeshRenderer mr = mFloor.AddComponent<MeshRenderer>();
			mr.Mesh = OnyxInstance.Resources.GetMesh(BuiltInMesh.Quad);
			mr.Material = OnyxInstance.Resources.GetMaterial(BuiltInMaterial.Default);
			mFloor.Transform.LocalPosition = new Vector3(0, -0.5f, 0);
			mFloor.Transform.LocalScale = new Vector3(2, 2, 2);
			mFloor.Parent = Scene.Root;

			Camera.Transform.Translate(-Camera.Transform.Forward * 0.25f);
		}

		public void SetMaterial(int guid)
		{
			mRenderer.Material = OnyxInstance.Resources.GetMaterial(guid);
		}

		public void SetMesh(int guid)
		{
			
			mRenderer.Mesh = OnyxInstance.Resources.GetMesh(guid);
			float scale = 1.25f / mRenderer.Mesh.Bounds.Size.Max();

			mRenderer.Transform.LocalScale = Vector3.One * scale;
		}

		public void SetFloorActive(bool active)
		{
			mFloor.Active = active;
		}
	}
}
