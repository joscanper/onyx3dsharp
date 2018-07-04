
using System;

namespace Onyx3D
{
	public class Onyx3DInstance : IDisposable
	{
		//public SceneManager Scenes = new SceneManager();
		public RenderManager Renderer = new RenderManager();
		public ResourcesManager Resources = new ResourcesManager();
		public GizmosManager Gizmos = new GizmosManager();

		public Onyx3DInstance()
		{
			Init();
		}

		private void Init()
		{
			Renderer.Init(this);
			Resources.Init(this);
			Gizmos.Init(this);
		}

		public void Dispose()
		{

			Dispose();
		}
	}
}
