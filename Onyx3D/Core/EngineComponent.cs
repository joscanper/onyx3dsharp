
namespace Onyx3D
{
	public class EngineComponent
	{
		public Onyx3DInstance Onyx3D;

		public virtual void Init(Onyx3DInstance onyx3D)
		{
			Onyx3D = onyx3D;
		}
	}
}
