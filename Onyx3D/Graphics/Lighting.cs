using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Schema;

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
		public float Intensity;
		public float Linear;
		public float Quadratic;
	};

	[StructLayout(LayoutKind.Sequential)]
	public struct LightingUBufferData
	{
		public Vector4 AmbientColor;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		public LightUBufferData[] PointLight;
		
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
		public LightUBufferData[] DirectionalLight;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		public LightUBufferData[] SpotLight;

		public int PointLightsNum;
		public int DirectionalLightsNum;
		public int SpotLightsNum;
	}


	public class Lighting : IXmlSerializable
	{
		private LightingUBufferData mUBufferData = new LightingUBufferData();
		private UBO<LightingUBufferData> mLightingUBO;

		public Vector3 Ambient = Vector3.One * 0.15f; 

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
			List<Light> ligths = scene.Root.GetComponentsInChildren<Light>();

			mUBufferData.AmbientColor = new Vector4(Ambient, 1.0f);
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
			data.Color = light.Color;
			data.Direction = new Vector4(light.Transform.Forward);
			data.Intensity = light.Intensity;
			// TODO - Add more things

			return data;
		}


		// ------ Serialization ------

		public XmlSchema GetSchema()
		{
			throw new NotImplementedException();
		}

		public void ReadXml(XmlReader reader)
		{
			while (reader.Read())
			{
				switch (reader.NodeType)
				{
					case XmlNodeType.Element:
						if (reader.Name.Equals("Ambient"))
							Ambient = XmlUtils.StringToVector3(reader.ReadElementContentAsString());
						
						//ComponentLoader.Load(obj, reader);
						break;
					case XmlNodeType.EndElement:
						if (reader.Name.Equals("Lighting"))
							return;
						break;
				}

			}
		}

		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("Lighting");
			writer.WriteElementString("Ambient", XmlUtils.Vector3ToString(Ambient));
			writer.WriteEndElement();
		}
	}
}
