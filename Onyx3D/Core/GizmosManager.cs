
using System.Collections.Generic;
using OpenTK;

namespace Onyx3D
{
    public class GizmosManager : EngineComponent
    {
        private List<MeshRenderer> mRenderers = new List<MeshRenderer>();
		private List<MeshRenderer> mPooledRenderers = new List<MeshRenderer>();
		private SceneObject mRoot;

        public override void Init(Onyx3DInstance onyx3D)
        {
			base.Init(onyx3D);
            mRoot = new SceneObject("Gizmos");
        }

        public void DrawLine(Vector3 from, Vector3 to, Vector3 color)
        {
            LineRenderer myLine = GetComponent<LineRenderer>();
            myLine.Material = Onyx3D.Resources.GetMaterial(BuiltInMaterial.UnlitVertexColor);
            myLine.Set(from, to, color);
			mRenderers.Add(myLine);
		}

		private T GetComponent<T>() where T : MeshRenderer, new()
		{

			for (int i = 0; i<mPooledRenderers.Count; ++i)
			{
				T component = (T)mPooledRenderers[i];
				if (component != null)
				{
					mPooledRenderers.Remove(component);
					return component;
				}
			}

			return mRoot.AddComponent<T>();
		}

        public void Render(Camera cam)
        {
            for(int i = 0; i< mRenderers.Count; ++i)
            {
				MeshRenderer r = mRenderers[i];
                Onyx3D.Renderer.Render(r, cam);
				mPooledRenderers.Add(r);
				mRenderers.Remove(r);
				--i;
            }

			mRoot.RemoveAllComponents();
        }
    }
}
