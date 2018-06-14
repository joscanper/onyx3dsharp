using System;
using System.Xml;

using OpenTK;

namespace Onyx3D
{
	
	public enum LightType
	{
		Point,
		Spot,
		Directional,
	}

	public class Light : Component
	{

		public LightType Type = LightType.Point;
		public Vector4 Color = Vector4.One;
		public float Intensity = 1.0f;

        public override void OnDrawGizmos(GizmosManager gizmos)
        {
			
			gizmos.DrawWireSphere(Transform.Position, 0.1f, Color.Xyz, 20);

			if (Type == LightType.Directional)
			{
				gizmos.DrawCircle(Transform.Position, 0.1f, Color.Xyz, Transform.Forward);
				gizmos.DrawLine(Transform.Position + Transform.Right * 0.1f, Transform.Position + Transform.Right * 0.1f + Transform.Forward, Color.Xyz);
				gizmos.DrawLine(Transform.Position - Transform.Right * 0.1f, Transform.Position - Transform.Right * 0.1f + Transform.Forward, Color.Xyz);
				gizmos.DrawLine(Transform.Position + Transform.Up * 0.1f, Transform.Position + Transform.Up * 0.1f + Transform.Forward, Color.Xyz);
				gizmos.DrawLine(Transform.Position - Transform.Up * 0.1f, Transform.Position - Transform.Up * 0.1f + Transform.Forward, Color.Xyz);
			}


			if (Type == LightType.Spot)
			{
				float endRadius = 0.5f;
				
				gizmos.DrawCircle(Transform.Position + Transform.Forward, endRadius, Color.Xyz, Transform.Forward);
				
				gizmos.DrawLine(Transform.Position, Transform.Position + Transform.Right * endRadius + Transform.Forward, Color.Xyz);
				gizmos.DrawLine(Transform.Position, Transform.Position - Transform.Right * endRadius + Transform.Forward, Color.Xyz);
				gizmos.DrawLine(Transform.Position, Transform.Position + Transform.Up * endRadius + Transform.Forward, Color.Xyz);
				gizmos.DrawLine(Transform.Position, Transform.Position - Transform.Up * endRadius + Transform.Forward, Color.Xyz);
			}

		}
		
		// ---- Serialization ----

		public override void ReadComponentXmlNode(XmlReader reader)
		{
			if (reader.Name.Equals("Type"))
				Type = (LightType)reader.ReadElementContentAsInt();
			else if (reader.Name.Equals("Color"))
				Color = XmlUtils.StringToVector4(reader.ReadElementContentAsString());
			else if (reader.Name.Equals("Intensity"))
				Intensity = reader.ReadElementContentAsFloat();
		}

		public override void WriteComponentXml(XmlWriter writer)
		{
			writer.WriteElementString("Type", ((int)Type).ToString());
			writer.WriteElementString("Color", XmlUtils.Vector4ToString(Color));
			writer.WriteElementString("Intensity", Intensity.ToString());
		}
	}
}
