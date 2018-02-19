
using System.Collections.Generic;
using OpenTK;

namespace Onyx3D
{
    public class Gizmos
    {
        private List<MeshRenderer> mRenderers = new List<MeshRenderer>();
        private SceneObject mRoot;

        public void Init()
        {
            mRoot = new SceneObject("Gizmos");
        }

        public void DrawLine(Vector3 from, Vector3 to, Vector3 color)
        {
            LineRenderer myLine = mRoot.AddComponent<LineRenderer>();
           // myLine.Material = myOnyxInstance.Resources.GetMaterial(BuiltInMaterial.UnlitVertexColor);
            myLine.Set(new Vector3(0, 0, 0), new Vector3(10, 10, 10), new Vector3(1, 0, 0));
        }

        public void Render(RenderManager renderMgr, Camera cam)
        {
            foreach(MeshRenderer r in mRenderers)
            {
                renderMgr.Render(r, cam);
            }

            mRoot.RemoveAllComponents();
            mRenderers.Clear();
        }
    }
}
