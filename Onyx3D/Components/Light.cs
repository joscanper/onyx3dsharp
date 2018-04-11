using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using OpenTK;

namespace Onyx3D
{
	
	public enum LightType
	{
		None,
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
        }

		public override Type GetInspectorType()
		{
			return typeof(LightInspector);
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
