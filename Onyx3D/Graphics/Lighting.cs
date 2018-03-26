using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using System.Runtime.InteropServices;

namespace Onyx3D
{
	[StructLayout(LayoutKind.Sequential)]
	public struct LightUBufferData
	{
		public Vector4 Position;
		public Vector4 Direction;
		public Vector4 Color;
		public Vector4 Specular;
		public float Angle;
		public float Range;
		public float Linear;
		public float Quadratic;
	};

	[StructLayout(LayoutKind.Sequential)]
	public struct LightingUBufferData
	{
		public Vector3 AmbientColor;

		public int PointLightsNum;
		
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		public LightUBufferData[] PointLight;
		
		public int DirectionalLightsNum;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
		public LightUBufferData[] DirectionalLight;

		public int SpotLightsNum;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		public LightUBufferData[] SpotLight;
	}


	public class Lighting
	{
		private LightingUBufferData mUBufferData = new LightingUBufferData();
		private UBO<LightingUBufferData> mLightingUBO;

		public UBO<LightingUBufferData> UBO { get { return mLightingUBO; } }

		public Lighting()
		{
			mUBufferData.PointLight = new LightUBufferData[8];
			mUBufferData.SpotLight = new LightUBufferData[8];
			mUBufferData.DirectionalLight = new LightUBufferData[2];

			mLightingUBO = new UBO<LightingUBufferData>(mUBufferData, "LightingData");
		}
	
		public void UpdateUBO(Scene scene)
		{
			List<Light> ligths = scene.Root.GetComponentInChildren<Light>();

			mUBufferData.PointLightsNum = 0;
			mUBufferData.DirectionalLightsNum = 0;
			mUBufferData.SpotLightsNum = 0;

			for (int i=0; i < ligths.Count; ++i)
			{
				switch (ligths[i].Type)
				{
					case LightType.Point:
						AddPointLight(ligths[i]);
						break;
					case LightType.Spot:
						AddSpotLight(ligths[i]);
						break;
					case LightType.Directional:
						AddDirectionalLight(ligths[i]);
						break;
				}
			}
		
			mLightingUBO.Update(mUBufferData);
		}

		private void AddPointLight(Light light)
		{
			mUBufferData.PointLightsNum++;
			mUBufferData.PointLight[mUBufferData.PointLightsNum - 1] = GetLightUBufferData(light);
		}

		private void AddDirectionalLight(Light light)
		{
			mUBufferData.DirectionalLightsNum++;
			mUBufferData.DirectionalLight[mUBufferData.DirectionalLightsNum - 1] = GetLightUBufferData(light);
		}

		private void AddSpotLight(Light light)
		{
			mUBufferData.SpotLightsNum++;
			mUBufferData.SpotLight[mUBufferData.SpotLightsNum - 1] = GetLightUBufferData(light);
		}
		
		private LightUBufferData GetLightUBufferData(Light light)
		{
			LightUBufferData data = new LightUBufferData();
			data.Position = new Vector4(light.Transform.Position);
			// TODO - Add more things

			return data;
		}

	}
}
