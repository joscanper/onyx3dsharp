
using System;
using System.Xml;
using OpenTK;

namespace Onyx3D
{
	public class ReflectionProbe : Component
	{
        public bool Dynamic;

        private int mSize;
        private Cubemap mCubemap;

        public bool IsBaked { get; private set; }
		public Cubemap Cubemap { get { return mCubemap; } }

        // --------------------------------------------------------------------

        public ReflectionProbe()
		{
			
		}
        
        // --------------------------------------------------------------------

        public void Init(int size)
		{
            mSize = size;
            mCubemap = new Cubemap();
		}

        // --------------------------------------------------------------------

        public void Bake(RenderManager renderer)
		{
            IsBaked = true;

            CubemapGenerator cubemapGenerator = new CubemapGenerator(mSize);
            cubemapGenerator.Generate(renderer, SceneObject.Scene, Transform.Position, ref mCubemap);
		}

        // --------------------------------------------------------------------

        public override Component Clone()
        {
            ReflectionProbe rp = new ReflectionProbe();
            rp.Init(mSize);
            rp.Dynamic = Dynamic;
            return rp;
        }

        // --------------------------------------------------------------------

        public override void OnDrawGizmos(GizmosManager gizmos)
		{

            Matrix4 rts = Transform.ModelMatrix;
            rts[0, 0] = 0.25f;
            rts[1, 1] = 0.25f;
            rts[2, 2] = 0.25f;

            Material mat = Onyx3DEngine.Instance.Resources.GetMaterial(BuiltInMaterial.ReflectionProbe);
            CubemapMaterialProperty cubemapProp = mat.GetProperty<CubemapMaterialProperty>("cubemap");
            cubemapProp.Data = mCubemap.Id;


            gizmos.DrawMesh(rts, Onyx3DEngine.Instance.Resources.GetMesh(BuiltInMesh.Sphere), mat);
		}

        // --------------------------------------------------------------------

        public override void OnDestroy()
		{
			base.OnDestroy();
			mCubemap.Dispose();
		}

        // --------------------------------------------------------------------

        public override void ReadComponentXmlNode(XmlReader reader)
		{
            if (reader.Name.Equals("Size"))
            {
                mSize = reader.ReadElementContentAsInt();
                Init(mSize);
            }
            else if (reader.Name.Equals("Dynamic"))
            {
                Dynamic = Convert.ToBoolean(reader.ReadElementContentAsString());
            }
        }

        // --------------------------------------------------------------------

        public override void WriteComponentXml(XmlWriter writer)
		{
            writer.WriteElementString("Size", mSize.ToString());
            writer.WriteElementString("Dynamic", Dynamic.ToString());
        }

	}
}
