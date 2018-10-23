using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Onyx3D;
using OpenTK;

namespace Onyx3DEditor
{

	public class SingleEntityPreviewRenderer : PreviewRenderer
	{
        private EntityProxy mProxy;
		private SceneObject mFloor;

        // --------------------------------------------------------------------

        public override void InitializeBasicScene()
		{
			base.InitializeBasicScene();

			mProxy = new EntityProxy("proxy");
			mProxy.Parent = Scene.Root;

			mProxy.Transform.Rotate(new Vector3(0, MathHelper.DegreesToRadians(45f), 0));

			mFloor = new SceneObject("floor");
			MeshRenderer mr = mFloor.AddComponent<MeshRenderer>();
			mr.Mesh = OnyxInstance.Resources.GetMesh(BuiltInMesh.Quad);
			mr.Material = OnyxInstance.Resources.GetMaterial(BuiltInMaterial.Default);
			mFloor.Transform.LocalPosition = new Vector3(0, -0.5f, 0);
			mFloor.Transform.LocalScale = new Vector3(2, 2, 2);
			mFloor.Parent = Scene.Root;

			Camera.Transform.Translate(-Camera.Transform.Forward * 0.25f);
		}

       
        // --------------------------------------------------------------------

        public void SetEntity(int guid)
		{
			Entity e = OnyxInstance.Resources.GetEntity(guid);
			mProxy.EntityRef = e;

			EntityRenderer renderer = mProxy.GetComponent<EntityRenderer>();
			float scale = 1.25f / renderer.Bounds.Size.Max();

			mProxy.Transform.LocalScale = Vector3.One * scale;
		}

        // --------------------------------------------------------------------

        public void SetFloorActive(bool active)
		{
			mFloor.Active = active;
		}
	}

}
