using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

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
    }
}

