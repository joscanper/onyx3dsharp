using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Onyx3D
{
	public class EntityRenderer : Renderer
	{
		
		public List<MeshRenderer> Renderers { get; } = new List<MeshRenderer>();

		private Entity Entity { get { return ((EntityProxy)SceneObject).EntityRef; } }

		public void UpdateRenderers(Entity e)
		{
			Renderers.Clear();
			if (e != null)
				e.Root.GetComponentsInChildren<MeshRenderer>(Renderers);
		}

		public override void Render()
		{
			Entity myEntity = Entity;
			if (myEntity == null)
				return;

			myEntity.Root.Transform.SetModelMatrix(SceneObject.Transform.ModelMatrix);
			foreach (MeshRenderer mr in Renderers)
			{
				mr.Render();
			}
		}

		// -----------------------------------------

		public override void ReadComponentXmlNode(XmlReader reader)
		{
			
		}

		public override void WriteComponentXml(XmlWriter writer)
		{
			
		}

	}
}
