
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


		public void DrawBox(Vector3 position, Vector3 size, Vector3 color)
		{
			BoxRenderer myBox = GetComponent<BoxRenderer>();
			myBox.Material = Onyx3D.Resources.GetMaterial(BuiltInMaterial.UnlitVertexColor);
			myBox.GenerateBox(Vector3.Zero, size, color);
			mRenderers.Add(myBox);

			myBox.Transform.LocalPosition = position;
		}

		public void DrawCircle(Vector3 position, float radius, Vector3 color)
		{
			DrawCircle(position, radius, color, Vector3.UnitY);
		}

		public void DrawCircle(Vector3 position, float radius, Vector3 color, Vector3 up, int segments = 100)
		{

			CircleRenderer myCircle = GetComponent<CircleRenderer>();
			myCircle.Material = Onyx3D.Resources.GetMaterial(BuiltInMaterial.UnlitVertexColor);
			myCircle.GenerateCircle(radius, color, up, segments);
			mRenderers.Add(myCircle);

			myCircle.Transform.LocalPosition = position;

		}

		public void DrawWireSphere(Vector3 position, float radius, Vector3 color, int segments = 100)
		{
			DrawCircle(position, radius, color, Vector3.UnitY, segments);
			DrawCircle(position, radius, color, Vector3.UnitZ, segments);
			DrawCircle(position, radius, color, Vector3.UnitX, segments);
		}

		public void DrawSphere(Vector3 position, float radius, Vector3 color)
		{
			MeshRenderer mySphere = GetComponent<MeshRenderer>();
			mySphere.Mesh = Onyx3D.Resources.GetMesh(BuiltInMesh.Sphere);
			mySphere.Material = Onyx3D.Resources.GetMaterial(BuiltInMaterial.ReflectionProbe);
			mySphere.Transform.LocalScale = Vector3.One * radius;
			mySphere.Transform.LocalPosition = position;
			mRenderers.Add(mySphere);
		}

		public void DrawWireSphere(Sphere s, Vector3 color, int segments = 100)
		{
			DrawWireSphere(s.Position, s.Radius, color);
		}

		public void DrawAxis(Vector3 position, float scale = 1)
		{
			AxisRenderer myAxis = GetComponent<AxisRenderer>();
			myAxis.Material = Onyx3D.Resources.GetMaterial(BuiltInMaterial.UnlitVertexColor);
			mRenderers.Add(myAxis);

			myAxis.Transform.LocalScale = Vector3.One * scale;
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
        

        public void DrawComponentGizmos(Scene scene)
        {
            DrawComponentGizmos(scene.Root);
        }

        public void DrawComponentGizmos(SceneObject obj)
        {
            obj.ForEachComponent((comp) =>
            {
                comp.OnDrawGizmos(this);
            });

            obj.ForEachChild((child) =>
            {
                DrawComponentGizmos(child);
            });
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
