
namespace Onyx3D
{

	public class Onyx3DEngine : Singleton<Onyx3DEngine>
	{
		
		public RenderManager Render = new RenderManager();
		public ContentManager Content = new ContentManager();

		public void Init()
		{
			Render.Init();
			Content.Init();
		}
		
	}

}
