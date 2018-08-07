using System.Collections.Generic;
using System.Xml;

namespace Onyx3D
{
	public class EntityRenderer : Renderer
	{
		
		public List<MeshRenderer> Renderers { get; } = new List<MeshRenderer>();

		private Entity Entity { get { return ((EntityProxy)SceneObject).EntityRef; } }

		// --------------------------------------------------------------------

		public void UpdateRenderers(Entity e)
		{
			Renderers.Clear();
			if (e != null)
			{
				e.Root.GetComponentsInChildren<MeshRenderer>(Renderers);
			}

			UpdateBounds();
		}
		
		// --------------------------------------------------------------------

		protected override void UpdateBounds()
		{
			Bounds.Clear();

			if (Renderers.Count > 0)
				Bounds = Renderers[0].Bounds;

			foreach(Renderer renderer in Renderers)
			{
				Bounds.Encapsulate(renderer.Bounds);
			}
		}

        // --------------------------------------------------------------------

        public override void PreRender()
        {
            Entity myEntity = Entity;
            if (myEntity == null)
                return;

            myEntity.Root.Transform.SetModelMatrix(SceneObject.Transform.ModelMatrix);
        }

        // --------------------------------------------------------------------

        public override void Render()
		{
			foreach (MeshRenderer mr in Renderers)
			{
				mr.Render();
			}
		}

		// --------------------------------------------------------------------
		// --------- Serialization --------------
		// --------------------------------------------------------------------

		public override void ReadComponentXmlNode(XmlReader reader)
		{
			
		}

		// --------------------------------------------------------------------

		public override void WriteComponentXml(XmlWriter writer)
		{
			
		}

	}
}
