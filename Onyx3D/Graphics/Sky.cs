
using OpenTK;
using System.Drawing;

namespace Onyx3D
{
	
	public class Sky
	{
		public enum ShadingType
		{
			SolidColor,
			Procedural
		};

		public ShadingType Type = ShadingType.Procedural;
		public Color Color = Color.SlateGray;
		public float Time = 0.5f;

		public MeshRenderer SkyMesh;

		public void Prepare(Onyx3DInstance context)
		{
			if (Type == ShadingType.Procedural)
			{
				if (SkyMesh == null)
				{ 
					SceneObject sky = new SceneObject("{Sky}");
					sky.Transform.LocalScale = new Vector3(-1, 1, 1);
					SkyMesh = sky.AddComponent<MeshRenderer>();
					SkyMesh.Mesh = context.Resources.GetMesh(BuiltInMesh.Sphere);
					SkyMesh.Material = context.Resources.GetMaterial(BuiltInMaterial.Sky);
				}

				// TODO - Set material properties;
			}
			else if (SkyMesh != null)
			{
				SkyMesh.SceneObject.Destroy();
				SkyMesh = null;
			}
		}
	}
}
