
namespace Onyx3D
{
	public class GameAsset
	{
        public bool IsDirty;

		public OnyxProjectAsset LinkedProjectAsset;

        public virtual void Copy(GameAsset other){}
	}
}
