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
		public Vector4 Color;

		// ---- Serialization ----


		public override void ReadComponentXmlNode(XmlReader writer)
		{
			throw new NotImplementedException();
		}

		public override void WriteComponentXml(XmlWriter writer)
		{
			throw new NotImplementedException();
		}
	}
}
