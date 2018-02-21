
using System.Collections.Generic;
using OpenTK;

namespace Onyx3D
{
    public class GizmosManager : EngineComponent
    {
        private List<MeshRenderer> mRenderers = new List<MeshRenderer>();
		private List<MeshRenderer> mPooledRenderers = new List<MeshRenderer>();
		//private SceneObject mRoot;

        public override void Init(Onyx3DInstance onyx3D)
        {
			base.Init(onyx3D);
        }

        public void DrawLine(Vector3 from, Vector3 to, Vector3 color)
        {
            LineRenderer myLine = GetComponent<LineRenderer>();
            myLine.Material = Onyx3D.Resources.GetMaterial(BuiltInMaterial.UnlitVertexColor);
            myLine.GenerateLine(from, to, color);
			mRenderers.Add(myLine);
		}

		public void DrawBox(Bounds box, Vector3 position)
		{
			BoxRenderer myBox = GetComponent<BoxRenderer>();
			myBox.Material = Onyx3D.Resources.GetMaterial(BuiltInMaterial.UnlitVertexColor);
			myBox.GenerateBox(box);
			mRenderers.Add(myBox);

			myBox.Transform.LocalPosition = position;
		}

		public void DrawAxis(Vector3 position)
		{
			AxisRenderer myAxis = GetComponent<AxisRenderer>();
			myAxis.Material = Onyx3D.Resources.GetMaterial(BuiltInMaterial.UnlitVertexColor);
			mRenderers.Add(myAxis);

			myAxis.Transform.LocalPosition = position;
		}


		private T GetComponent<T>() where T : MeshRenderer, new()
		{

			for (int i = 0; i<mPooledRenderers.Count; ++i)
			{
				if (mPooledRenderers[i].GetType() == typeof(T))
				{
					T component = (T)mPooledRenderers[i];
					mPooledRenderers.Remove(component);
					return component;
				}
			}

			SceneObject obj = new SceneObject("Gizmo");
			return obj.AddComponent<T>();
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

        }
    }
}
