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

		// ---- Serialization ----

		public override void ReadComponentXmlNode(XmlReader reader)
		{
			if (reader.Name.Equals("Type"))
				Type = (LightType)reader.ReadElementContentAsInt();
			else if (reader.Name.Equals("Color"))
				Color = XmlUtils.StringToVector4(reader.ReadElementContentAsString());
		}

		public override void WriteComponentXml(XmlWriter writer)
		{
			writer.WriteElementString("Type", ((int)Type).ToString());
			writer.WriteElementString("Color", XmlUtils.Vector4ToString(Color));
		}
	}
}
