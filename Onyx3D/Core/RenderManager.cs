using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;
using System.Drawing;

namespace Onyx3D
{
    public class RenderManager : EngineComponent
    {
        private const string sMainRenderTrace = "RenderManager/MainRender";

        // --------------------------------------------------------------------

        public GizmosManager Gizmos { get; private set; }
        public double RenderTime { get { return Profiler.Instance.GetTrace(sMainRenderTrace).Duration; } }
        public Vector2 ScreenSize = new Vector2(800, 600);

        // --------------------------------------------------------------------

        public override void Init(Onyx3DInstance onyx3d)
        {
            base.Init(onyx3d);

            GL.Enable(EnableCap.CullFace);
            GL.Enable(EnableCap.DepthTest);

            GL.Enable(EnableCap.Multisample);

            GL.Enable(EnableCap.LineSmooth);
            GL.Hint(HintTarget.LineSmoothHint, HintMode.Nicest);

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            GL.Enable(EnableCap.TextureCubeMapSeamless);

            GL.ClearColor(Color.SlateGray);

            Gizmos = new GizmosManager();
            Gizmos.Init(onyx3d);
        }

        // --------------------------------------------------------------------

        public void Render(Scene scene, Camera cam, int w, int h)
        {
            Profiler.Instance.StartTrace(sMainRenderTrace);
            Onyx3D.MakeCurrent();

            cam.UpdateUBO();

            scene.Lighting.UpdateUBO(scene);

            GL.Viewport(0, 0, w, h);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            if (scene.IsDirty)
            {
                scene.UpdateRenderData();
                //BakeReflectionProbes(scene, false);
            }

            PrepareMaterials(scene, cam.UBO, scene.Lighting.UBO);

            RenderSky(scene, cam);
            RenderMeshes(scene);
            RenderEntities(scene);

            GL.Flush();

            Profiler.Instance.EndTrace();
        }

        // --------------------------------------------------------------------

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

        // --------------------------------------------------------------------

        private void BakeReflectionProbes(Scene scene, bool forced)
        {
            for (int i = 0; i < scene.RenderData.ReflectionProbes.Count; i++)
            {
                if (!scene.RenderData.ReflectionProbes[i].IsBaked || forced)
                {
                    scene.RenderData.ReflectionProbes[i].Bake(this);
                    scene.RenderData.ReflectionProbes[i].Bake(this);
                }
            }
        }

        // --------------------------------------------------------------------

        private HashSet<Material> GetMaterialsFromRenderers(Scene scene)
        {
            HashSet<Material> materials = new HashSet<Material>();
            for (int i = 0; i < scene.RenderData.MeshRenderers.Count; ++i)
                materials.Add(scene.RenderData.MeshRenderers[i].Material);
            return materials;
        }

        // --------------------------------------------------------------------

        private void PrepareMaterials(Scene scene, UBO<CameraUBufferData> camUBO, UBO<LightingUBufferData> lightUBO)
        {
            HashSet<Material> materials = GetMaterialsFromRenderers(scene);

            // TODO - improve this so we only check a template once!
            foreach (EntityRenderer er in scene.RenderData.EntityRenderers)
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

        // --------------------------------------------------------------------

        private void RenderMeshes(Scene scene)
        {
            for (int i = 0; i < scene.RenderData.MeshRenderers.Count; ++i)
            {
                scene.RenderData.MeshRenderers[i].PreRender();
                SetUpReflectionProbe(scene, scene.RenderData.MeshRenderers[i]);
                scene.RenderData.MeshRenderers[i].Render();
            }
        }

        // --------------------------------------------------------------------

        private void RenderEntities(Scene scene)
        {
            for (int i = 0; i < scene.RenderData.EntityRenderers.Count; ++i)
            {
                scene.RenderData.EntityRenderers[i].PreRender();

                // TODO - Maybe better to get the right reflectionprobe considering only the entityProxy position and not for all its renderers
                foreach (MeshRenderer mr in scene.RenderData.EntityRenderers[i].Renderers)
                    SetUpReflectionProbe(scene, mr);

                scene.RenderData.EntityRenderers[i].Render();
            }
        }

        // --------------------------------------------------------------------

        private void SetUpReflectionProbe(Scene scene, MeshRenderer renderer)
        {
            if (scene.RenderData.ReflectionProbes.Count == 0)
                return;

            CubemapMaterialProperty cubemapProp = renderer.Material.GetProperty<CubemapMaterialProperty>("environment_map");
            if (cubemapProp == null)
                return;

            ReflectionProbe reflectionProbe = GetClosestReflectionProbe(scene, renderer.Transform.Position);
            cubemapProp.Data = reflectionProbe.Cubemap.Id;
        }

        // --------------------------------------------------------------------

        private ReflectionProbe GetClosestReflectionProbe(Scene scene, Vector3 toPosition)
        {
            ReflectionProbe reflectionProbe = null;
            float candidateDist = float.MaxValue;
            for (int i = 0; i < scene.RenderData.ReflectionProbes.Count; ++i)
            {
                float sqrDist = scene.RenderData.ReflectionProbes[i].Transform.Position.SqrDistance(toPosition);
                if (sqrDist < candidateDist)
                {
                    candidateDist = sqrDist;
                    reflectionProbe = scene.RenderData.ReflectionProbes[i];
                }
            }

            return reflectionProbe;
        }

        // --------------------------------------------------------------------

        public void Render(MeshRenderer r, Camera cam)
        {
            r.Material.Shader.BindUBO(cam.UBO);
            r.Render();
        }

        // --------------------------------------------------------------------

        public void RefreshReflectionProbes(Scene scene)
        {
            BakeReflectionProbes(scene, true);
        }

        // --------------------------------------------------------------------
    }
}