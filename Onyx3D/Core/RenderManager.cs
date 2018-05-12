using System.Drawing;
using System.Collections.Generic;

using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace Onyx3D
{
	public class RenderManager : EngineComponent
	{
		private List<MeshRenderer> mSceneRenderers;
		private List<ReflectionProbe> mReflectionProbes;


		public GizmosManager Gizmos { get; private set; }

		public Vector2 ScreenSize = new Vector2(800,600);

		public override void Init(Onyx3DInstance onyx3d)
		{
			base.Init(onyx3d);

			GL.Enable(EnableCap.CullFace);
			GL.Enable(EnableCap.DepthTest);

			GL.Enable(EnableCap.Multisample);
            
			GL.Hint(HintTarget.MultisampleFilterHintNv, HintMode.Nicest);

			GL.Enable(EnableCap.LineSmooth);
			GL.Hint(HintTarget.LineSmoothHint, HintMode.Nicest);

			GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            GL.Enable(EnableCap.TextureCubeMapSeamless);
            
            GL.ClearColor(Color.SlateGray);

			Gizmos = new GizmosManager();
			Gizmos.Init(onyx3d);
		}

		public void Render(Scene scene)
		{
			Render(scene, scene.ActiveCamera, (int)ScreenSize.X, (int)ScreenSize.Y);
		}

		public void Render(Scene scene, Camera cam, int w, int h)
		{
			
			cam.UpdateUBO();
			scene.Lighting.UpdateUBO(scene);


			GL.Viewport(0, 0, w, h);
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

			if (scene.IsDirty) {
                scene.SetDirty(false);
                mSceneRenderers = scene.Root.GetComponentsInChildren<MeshRenderer>();
				mReflectionProbes = scene.Root.GetComponentsInChildren<ReflectionProbe>();

                BakeReflectionProbes();
            }
			PrepareMaterials(mSceneRenderers, cam.UBO, scene.Lighting.UBO);

            if (scene.Sky != null)
            {
                GL.DepthMask(false);
                Render(scene.Sky, cam);
                GL.DepthMask(true);
            }


            Render(mSceneRenderers);

            GL.Flush();
		}

        private void BakeReflectionProbes()
        {
            for(int i = 0; i < mReflectionProbes.Count; i++)
            {
                if (!mReflectionProbes[i].IsBaked)
                {

                    mReflectionProbes[i].Bake(this);
                    mReflectionProbes[i].Bake(this);
                }
            }
        }

		private HashSet<Material> GetMaterialsFromRenderers(List<MeshRenderer> renderers)
		{
			HashSet<Material> materials = new HashSet<Material>();
			for(int i = 0; i < renderers.Count; ++i)
				materials.Add(renderers[i].Material);
			return materials;
		}
        
		private void PrepareMaterials(List<MeshRenderer> renderers, UBO<CameraUBufferData> camUBO, UBO<LightingUBufferData> lightUBO)
		{
			HashSet<Material> materials = GetMaterialsFromRenderers(renderers);
			foreach (Material m in materials)
			{ 
				m.Shader.BindUBO(camUBO);
				m.Shader.BindUBO(lightUBO);
			}
		}

		private void Render(List<MeshRenderer> renderers)
		{
			for (int i = 0; i < renderers.Count; ++i)
			{
				SetUpReflectionProbe(renderers[i]);
				renderers[i].Render();
			}
		}

		private void SetUpReflectionProbe(MeshRenderer renderer)
		{
			if (mReflectionProbes.Count == 0)
				return;

			CubemapMaterialProperty cubemapProp = renderer.Material.GetProperty<CubemapMaterialProperty>("environment_map");
			if (cubemapProp == null)
				return;

			ReflectionProbe reflectionProbe = null;
			float candidateDist = float.MaxValue;
			for(int i=0; i < mReflectionProbes.Count; ++i)
			{
				float sqrDist = mReflectionProbes[i].Transform.Position.SqrDistance(renderer.Transform.Position);
				if (sqrDist < candidateDist)
				{
					candidateDist = sqrDist;
					reflectionProbe = mReflectionProbes[i];
				}
			}

			if (reflectionProbe != null)
				cubemapProp.Data = reflectionProbe.Cubemap.Id;
		}

		public void Render(MeshRenderer r, Camera cam)
		{
			r.Material.Shader.BindUBO(cam.UBO);
            r.Render();
		}
	}
}
