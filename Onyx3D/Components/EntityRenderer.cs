using System.Collections.Generic;
using System.Xml;

using OpenTK;

namespace Onyx3D
{
	public class EntityRenderer : Renderer
	{
		
		public List<MeshRenderer> Renderers { get; } = new List<MeshRenderer>();

		public Entity Entity { get { return ((EntityProxy)SceneObject).EntityRef; } }

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

		public override void UpdateBounds()
		{
			mBounds.Clear();

			if (Entity == null)
				return;

			Entity.Root.Transform.SetModelMatrix(SceneObject.Transform.ModelMatrix);

			if (Renderers.Count > 0)
				mBounds = Renderers[0].Bounds;
			
			foreach(Renderer renderer in Renderers)
			{
				renderer.UpdateBounds();
				mBounds.Encapsulate(renderer.Bounds);
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
		
		public override bool IntersectsRay(Ray ray, out RaycastHit hit)
		{
			hit = new RaycastHit();

			Entity myEntity = Entity;
			if (myEntity == null)
				return false;

			myEntity.Root.Transform.SetModelMatrix(SceneObject.Transform.ModelMatrix);

			myEntity.Root.IntersectRay(ray, out hit);

			if (hit.Object != null)
				hit.Object = SceneObject;

			return hit.Object != null;
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
