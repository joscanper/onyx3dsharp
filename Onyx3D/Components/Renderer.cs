using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using OpenTK;

namespace Onyx3D
{
	public abstract class Renderer : Component
	{
		public Bounds Bounds { get; protected set; }

		protected abstract void UpdateBounds();

        public virtual void PreRender() { }

        public abstract void Render();

		public virtual bool IntersectsRay(Ray ray, out RaycastHit hit)
		{
			hit = new RaycastHit();
			return false;
		}

		// --------------------------------------------------------------------

		public override void OnDirtyTransform()
		{
			base.OnDirtyTransform();

			UpdateBounds();
		}

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
