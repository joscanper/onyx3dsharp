
namespace Onyx3D
{
	public class Onyx3DInstance
	{
		public SceneManager Scenes = new SceneManager();
		public RenderManager Renderer = new RenderManager();
		public ResourcesManager Resources = new ResourcesManager();
		public GizmosManager Gizmos = new GizmosManager();

		public void Init()
		{
			Renderer.Init(this);
			Resources.Init(this);
			Gizmos.Init(this);
		}
	}
}
