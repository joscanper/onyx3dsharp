
using System;
using System.Xml;
using OpenTK;


namespace Onyx3D
{
	public class ReflectionProbe : Component
	{
		private Cubemap mCubemap;
		private CubemapGenerator mCubemapGenerator;

        private int mSize;

        public bool IsBaked { get; private set; }
		
		public Cubemap Cubemap { get { return mCubemap; } }

		public ReflectionProbe()
		{
			
		}

		public void Init(int size)
		{
            mSize = size;
            mCubemap = new Cubemap();
			mCubemapGenerator = new CubemapGenerator(size);
		}

		public void Bake(RenderManager renderer)
		{
            IsBaked = true;
            mCubemapGenerator.Generate(renderer, SceneObject.Scene, Transform.Position, ref mCubemap);
		}

		public override void OnDrawGizmos(GizmosManager gizmos)
		{
			gizmos.DrawSphere(Transform.Position, 0.5f, Vector3.One);
		}		

		public override void ReadComponentXmlNode(XmlReader reader)
		{
            if (reader.Name.Equals("Size"))
            {
                mSize = reader.ReadElementContentAsInt();
                Init(mSize);
            }
        }

		public override void WriteComponentXml(XmlWriter writer)
		{
            writer.WriteElementString("Size", mSize.ToString());
        }
	}
}
