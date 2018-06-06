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
	public class MaterialPreviewRenderer : PreviewRenderer
	{

		private MeshRenderer mSphereRenderer;

		public override void InitializeBasicScene()
		{
			base.InitializeBasicScene();

			SceneObject sphere = new SceneObject("sphere");
			mSphereRenderer = sphere.AddComponent<MeshRenderer>();
			mSphereRenderer.Mesh = OnyxInstance.Resources.GetMesh(BuiltInMesh.Sphere);
			mSphereRenderer.Material = OnyxInstance.Resources.GetMaterial(BuiltInMaterial.Default);
			sphere.Parent = Scene.Root;

			SceneObject quad = new SceneObject("floor");
			MeshRenderer mr = quad.AddComponent<MeshRenderer>();
			mr.Mesh = OnyxInstance.Resources.GetMesh(BuiltInMesh.Quad);
			mr.Material = OnyxInstance.Resources.GetMaterial(BuiltInMaterial.Default);
			quad.Transform.LocalPosition = new Vector3(0, -0.5f, 0);
			quad.Transform.LocalScale = new Vector3(2, 2, 2);
			quad.Parent = Scene.Root;

			Camera.Transform.Translate(-Camera.Transform.Forward * 0.25f);
		}

		public void SetMaterial(int guid)
		{
			mSphereRenderer.Material = OnyxInstance.Resources.GetMaterial(guid);
		}
	}
}
