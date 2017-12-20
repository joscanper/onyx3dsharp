
namespace Onyx3D
{

	public class Onyx3DEngine : Singleton<Onyx3DEngine>
	{
		public RenderManager Render = new RenderManager();
		public ResourceManager Content = new ResourceManager();

		public void Init()
		{
			Render.Init();
			Content.Init();
		}
		
	}

}
