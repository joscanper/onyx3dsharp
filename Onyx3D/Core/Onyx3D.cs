
namespace Onyx3D
{

	public class Onyx3DEngine : Singleton<Onyx3DEngine>
	{
		public RenderManager Render = new RenderManager();
		public ResourcesManager Resources = new ResourcesManager();

		public void Init()
		{
			Render.Init();
			Resources.Init();
		}
		
	}

}
