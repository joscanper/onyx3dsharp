
using OpenTK;
using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Onyx3D
{

	[StructLayout(LayoutKind.Sequential)]
	public struct SkyUBufferData
	{
		public float Time;
	}

	public class Sky : IDisposable
	{
		private SkyUBufferData mUBufferData = new SkyUBufferData();
		private UBO<SkyUBufferData> mSkyUBO;

		public UBO<SkyUBufferData> UBO { get { return mSkyUBO; } }

		public enum ShadingType
		{
			SolidColor,
			Procedural
		};

		public ShadingType Type = ShadingType.Procedural;
		public Color Color = Color.SlateGray;
		public float Time = 0.25f;

		public MeshRenderer SkyMesh;


		public Sky()
		{
			mSkyUBO = new UBO<SkyUBufferData>(mUBufferData, "SkyData");
		}

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

				UpdateUBO();
				SkyMesh.Material.Shader.BindUBO(mSkyUBO);
			}
			else if (SkyMesh != null)
			{
				SkyMesh.SceneObject.Destroy();
				SkyMesh = null;
			}

			
		}

		public void UpdateUBO()
		{
			mUBufferData.Time = Time;
			mSkyUBO.Update(mUBufferData);
		}

		public void Dispose()
		{
			mSkyUBO.Dispose();
			mSkyUBO = null;
		}
	}
}
