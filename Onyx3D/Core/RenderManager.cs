using System.Drawing;
using System.Collections.Generic;

using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace Onyx3D
{
	public class RenderManager : EngineComponent
	{
		private List<MeshRenderer> mMeshRenderers = new List<MeshRenderer>();
		private List<EntityRenderer> mEntityRenderers = new List<EntityRenderer>();
		private List<ReflectionProbe> mReflectionProbes = new List<ReflectionProbe>();
		//private List<Entity> mEntities = new List<Entity>();

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
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

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
			GL.Clear(ClearBufferMask.DepthBufferBit);

			if (scene.IsDirty)
			{
                scene.SetDirty(false);
                mMeshRenderers = scene.Root.GetComponentsInChildren<MeshRenderer>();
				mEntityRenderers = scene.Root.GetComponentsInChildren<EntityRenderer>();
				mReflectionProbes = scene.Root.GetComponentsInChildren<ReflectionProbe>();
				//mEntities = scene.Root.GetEntitiesInChildren();

                BakeReflectionProbes(false);
            }

			PrepareMaterials(cam.UBO, scene.Lighting.UBO);

			RenderSky(scene, cam);
            RenderMeshes();
			RenderEntities();

            GL.Flush();
		}

		private void RenderSky(Scene scene, Camera cam)
		{
			scene.Sky.Prepare(scene.Context);

			if (scene.Sky.Type == Sky.ShadingType.Procedural)
			{ 
				GL.DepthMask(false);
				Render(scene.Sky.SkyMesh, cam);
				GL.DepthMask(true);
			}
			else
			{
				GL.ClearColor(scene.Sky.Color);
				GL.Clear(ClearBufferMask.ColorBufferBit);
			}
		}

        private void BakeReflectionProbes(bool forced)
        {
            for(int i = 0; i < mReflectionProbes.Count; i++)
            {
                if (!mReflectionProbes[i].IsBaked || forced)
                {

                    mReflectionProbes[i].Bake(this);
                    mReflectionProbes[i].Bake(this);
                }
            }
        }

		private HashSet<Material> GetMaterialsFromRenderers()
		{
			HashSet<Material> materials = new HashSet<Material>();
			for(int i = 0; i < mMeshRenderers.Count; ++i)
				materials.Add(mMeshRenderers[i].Material);
			return materials;
		}

        
		private void PrepareMaterials(UBO<CameraUBufferData> camUBO, UBO<LightingUBufferData> lightUBO)
		{
			HashSet<Material> materials = GetMaterialsFromRenderers();
			
			// TODO - improve this so we only check a template once!
			foreach(EntityRenderer er in mEntityRenderers)
			{
				foreach (MeshRenderer mr in er.Renderers)
					materials.Add(mr.Material);
			}

			foreach (Material m in materials)
			{
				if (m == null)
					return;

				m.Shader.BindUBO(camUBO);
				m.Shader.BindUBO(lightUBO);
			}

			
		}

		private void RenderMeshes()
		{
			for (int i = 0; i < mMeshRenderers.Count; ++i)
			{
				SetUpReflectionProbe(mMeshRenderers[i]);
				mMeshRenderers[i].Render();
			}
		}

		private void RenderEntities()
		{
			for (int i = 0; i < mEntityRenderers.Count; ++i)
			{
				// TODO - Maybe better to get the right reflectionprobe considering only the entityProxy position and not for all its renderers
				foreach (MeshRenderer mr in mEntityRenderers[i].Renderers)
					SetUpReflectionProbe(mr);

				mEntityRenderers[i].Render();
			}
		}

		private void SetUpReflectionProbe(MeshRenderer renderer)
		{
			if (mReflectionProbes.Count == 0)
				return;

			CubemapMaterialProperty cubemapProp = renderer.Material.GetProperty<CubemapMaterialProperty>("environment_map");
			if (cubemapProp == null)
				return;

			ReflectionProbe reflectionProbe = GetClosestReflectionProbe(renderer.Transform.Position);
			cubemapProp.Data = reflectionProbe.Cubemap.Id;
		}

		private ReflectionProbe GetClosestReflectionProbe(Vector3 toPosition)
		{
			ReflectionProbe reflectionProbe = null;
			float candidateDist = float.MaxValue;
			for (int i = 0; i < mReflectionProbes.Count; ++i)
			{
				float sqrDist = mReflectionProbes[i].Transform.Position.SqrDistance(toPosition);
				if (sqrDist < candidateDist)
				{
					candidateDist = sqrDist;
					reflectionProbe = mReflectionProbes[i];
				}
			}

			return reflectionProbe;
		}

		public void Render(MeshRenderer r, Camera cam)
		{
			r.Material.Shader.BindUBO(cam.UBO);
            r.Render();
		}

		public void RefreshReflectionProbes()
		{
			BakeReflectionProbes(true);
		}
	}
}
