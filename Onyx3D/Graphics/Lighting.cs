
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

	static class MaxLights
	{
		public const int Spot = 8;
		public const int Directional = 2;
		public const int Point = 8;
	}
	

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

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxLights.Point)]
		public LightUBufferData[] PointLight;
		
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxLights.Directional)]
		public LightUBufferData[] DirectionalLight;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxLights.Spot)]
		public LightUBufferData[] SpotLight;

		public int PointLightsNum;
		public int DirectionalLightsNum;
		public int SpotLightsNum;
	}


	public class Lighting : IXmlSerializable, IDisposable
	{
		public static readonly int NumSpotlights = 8;

		private LightingUBufferData mUBufferData = new LightingUBufferData();
		private UBO<LightingUBufferData> mLightingUBO;

		public Vector3 Ambient = Vector3.Zero; 

		public UBO<LightingUBufferData> UBO { get { return mLightingUBO; } }

		// --------------------------------------------------------------------

		public Lighting()
		{
			mUBufferData.PointLight = new LightUBufferData[8];
			mUBufferData.SpotLight = new LightUBufferData[8];
			mUBufferData.DirectionalLight = new LightUBufferData[2];

			mLightingUBO = new UBO<LightingUBufferData>(mUBufferData, "LightingData");
		}

		// --------------------------------------------------------------------

		public void UpdateUBO(Scene scene)
		{
			
			mUBufferData.AmbientColor = new Vector4(Ambient, 1f);
			mUBufferData.PointLightsNum = 0;
			mUBufferData.DirectionalLightsNum = 0;
			mUBufferData.SpotLightsNum = 0;

			AddGlobalLigths(scene);
			AddEntitiesLights(scene);
		
			mLightingUBO.Update(mUBufferData);
		}
		
		// --------------------------------------------------------------------

		private void AddGlobalLigths(Scene scene)
		{
			List<Light> lights = scene.Root.GetComponentsInChildren<Light>(true);
			for (int i = 0; i < lights.Count; ++i)
			{
				AddLight(lights[i]);
			}
		}

        // --------------------------------------------------------------------

        private void AddEntitiesLights(Scene scene)
		{
			List<Light> lights = new List<Light>();
			List<EntityProxy> entities = scene.EntityProxies;
			foreach(EntityProxy proxy in entities)
			{
				if (proxy.EntityRef != null)
				{ 
					lights.Clear();
					proxy.GetComponentsInChildren(lights);
					for (int i = 0; i < lights.Count; ++i)
					{
						proxy.EntityRef.Root.Transform.SetModelMatrix(proxy.Transform.ModelMatrix);
						AddLight(lights[i]);
					}
				}
			}
		}

		// --------------------------------------------------------------------

		private void AddLight(Light l)
		{
			switch (l.Type)
			{
				case LightType.Point:
					AddPointLight(l);
					break;
				case LightType.Spot:
					AddSpotLight(l);
					break;
				case LightType.Directional:
					AddDirectionalLight(l);
					break;
			}
		}

		// --------------------------------------------------------------------

		private void AddPointLight(Light light)
		{
			if (mUBufferData.DirectionalLightsNum >= MaxLights.Point)
				return;

			mUBufferData.PointLightsNum++;
			mUBufferData.PointLight[mUBufferData.PointLightsNum - 1] = GetLightUBufferData(light);
		}

		// --------------------------------------------------------------------

		private void AddDirectionalLight(Light light)
		{
			if (mUBufferData.DirectionalLightsNum >= MaxLights.Directional)
				return;

			mUBufferData.DirectionalLightsNum++;
			mUBufferData.DirectionalLight[mUBufferData.DirectionalLightsNum - 1] = GetLightUBufferData(light);
		}

		// --------------------------------------------------------------------

		private void AddSpotLight(Light light)
		{
			if (mUBufferData.SpotLightsNum >= MaxLights.Spot)
				return;

			mUBufferData.SpotLightsNum++;
			mUBufferData.SpotLight[mUBufferData.SpotLightsNum - 1] = GetLightUBufferData(light);
		}

		// --------------------------------------------------------------------

		private LightUBufferData GetLightUBufferData(Light light)
		{
			LightUBufferData data = new LightUBufferData();
			
			data.Position = new Vector4(light.Transform.Position);
			data.Color = light.Color;
			data.Direction = new Vector4(light.Transform.Forward);
			data.Intensity = light.Intensity;

			return data;
		}

		// --------------------------------------------------------------------

		public void Dispose()
		{
			mLightingUBO.Dispose();
			mLightingUBO = null;
		}


		// --------------------------------------------------------------------
		// ------ Serialization ------
		// --------------------------------------------------------------------

		public XmlSchema GetSchema()
		{
			throw new NotImplementedException();
		}

		// --------------------------------------------------------------------

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

		// --------------------------------------------------------------------

		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("Lighting");
			writer.WriteElementString("Ambient", XmlUtils.Vector3ToString(Ambient));
			writer.WriteEndElement();
		}
	}
}
