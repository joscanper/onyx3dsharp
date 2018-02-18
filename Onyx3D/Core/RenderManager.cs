using System.Drawing;
using System.Collections.Generic;

using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace Onyx3D
{
	public class RenderManager
	{
		private SceneObject mRoot;

		public Vector2 ScreenSize = new Vector2(800,600);

		public void Init()
		{
			GL.Enable(EnableCap.CullFace);
			GL.Enable(EnableCap.DepthTest);

			GL.Enable(EnableCap.Multisample);
			GL.Hint(HintTarget.MultisampleFilterHintNv, HintMode.Nicest);

			GL.Enable(EnableCap.LineSmooth);
			GL.Hint(HintTarget.LineSmoothHint, HintMode.Nicest);

			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

			GL.ClearColor(Color.SlateGray);
		}

		public void Render(Scene scene)
		{
			Render(scene, scene.ActiveCamera, (int)ScreenSize.X, (int)ScreenSize.Y);
		}

		public void Render(Scene scene, Camera cam, int w, int h)
		{
			cam.UpdateUBO();
			
			GL.Viewport(0, 0, w, h);
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

			List<MeshRenderer> renderers = GetSceneRenderers(scene);
			PrepareMaterials(renderers, cam);
			Render(renderers);
			
			GL.Flush();
			
		}
		
		private List<MeshRenderer> GetSceneRenderers(Scene scene)
		{
			List<MeshRenderer> rendereres = new List<MeshRenderer>();
			
			if (scene.Root == null)
				return rendereres;

            Queue<SceneObject> objects = new Queue<SceneObject>();
            objects.Enqueue(scene.Root);
			SceneObject s;
			do
			{
				s = objects.Dequeue();
				List<MeshRenderer> objRenderers = s.GetComponents<MeshRenderer>();
				if (objRenderers.Count > 0)
					rendereres.AddRange(objRenderers);

				for (int i = 0; i < s.ChildCount; i++)
				{
					objects.Enqueue(s.GetChild(i));
				}

				
			} while (objects.Count > 0);

			return rendereres;
		}

		private HashSet<Material> GetMaterialsFromRenderers(List<MeshRenderer> renderers)
		{
			HashSet<Material> materials = new HashSet<Material>();
			for(int i = 0; i < renderers.Count; ++i)
				materials.Add(renderers[i].Material);
			return materials;
		}


		private void PrepareMaterials(List<MeshRenderer> renderers, Camera cam)
		{
			HashSet<Material> materials = GetMaterialsFromRenderers(renderers);
			foreach (Material m in materials)
				m.Shader.BindUBO(cam.UBO);
		}

		private void Render(List<MeshRenderer> renderers)
		{
			for (int i = 0; i < renderers.Count; ++i)
				renderers[i].Render();
		}

		public void Render(MeshRenderer r, Camera cam)
		{
			r.Material.Shader.BindUBO(cam.UBO);
			r.Render();
		}
	}
}
