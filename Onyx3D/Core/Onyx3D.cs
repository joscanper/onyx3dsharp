
namespace Onyx3D
{

	public class Onyx3DEngine
	{
        private static Onyx3DInstance mMainInstance;

		public static Onyx3DInstance Instance
		{
			get
			{
				if (mMainInstance == null)
					mMainInstance = new Onyx3DInstance();
				return mMainInstance;
			}
		}
		
	}

}
