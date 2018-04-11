
using System;
using System.Xml;
using OpenTK;


namespace Onyx3D
{
	public class ReflectionProbe : Component
	{
		private Cubemap mCubemap;
		private CubemapGenerator mCubemapGenerator;

		public float Angle;
		
		public Cubemap Cubemap { get { return mCubemap; } }

		public ReflectionProbe()
		{
			
		}

		public void Init(int size)
		{
			mCubemap = new Cubemap();
			mCubemapGenerator = new CubemapGenerator(size);
		}

		public void Bake(RenderManager renderer)
		{
			mCubemapGenerator.Generate(renderer, SceneObject.Scene, Transform.Position, ref mCubemap, Angle);
		}

		
		public override void OnDrawGizmos(GizmosManager gizmos)
		{
			gizmos.DrawSphere(Transform.Position, 1, Vector3.One);
		}
		

		public override void ReadComponentXmlNode(XmlReader writer)
		{
			throw new NotImplementedException();
		}

		public override void WriteComponentXml(XmlWriter writer)
		{
			throw new NotImplementedException();
		}
	}
}
