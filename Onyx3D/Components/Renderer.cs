using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Onyx3D
{
	public abstract class Renderer : Component
	{
		public Bounds Bounds { get; protected set; }

		public abstract void Render();

		protected abstract void UpdateBounds();


		// --------------------------------------------------------------------
		// --------------------- Serialization ----------------------
		// --------------------------------------------------------------------

		public override void ReadComponentXmlNode(XmlReader reader)
		{
			throw new NotImplementedException();
		}

		// --------------------------------------------------------------------

		public override void WriteComponentXml(XmlWriter writer)
		{
			throw new NotImplementedException();
		}
	}
}
