
using System.ComponentModel;

namespace Onyx3D
{
    public class MeshRendererInspector : Inspector<MeshRenderer>
    {
        public MeshRendererInspector(MeshRenderer obj) : base(obj)
        {
        }

        [Category("MeshRenderer")]
        public int Material
        {
            get { return mObject.Material.LinkedProjectAsset.Guid; }
            set {
                Material m = Onyx3DEngine.Instance.Resources.GetMaterial(value);
                if (m != null)
                    mObject.Material = m;
            }
        }

		[Category("MeshRenderer")]
		public int Mesh
		{
			get { return mObject.Mesh.LinkedProjectAsset.Guid; }
			set
			{
				Mesh m = Onyx3DEngine.Instance.Resources.GetMesh(value);
				if (m != null)
					mObject.Mesh = m;
			}
		}
	}
}

