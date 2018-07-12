
using OpenTK.Graphics;
using OpenTK.Platform;
using System;

namespace Onyx3D
{

	public class Onyx3DEngine
	{
        private static Onyx3DInstance mMainInstance;

		public static IGraphicsContext Context { get; private set; }
		public static IWindowInfo WinInfo { get; private set; }

		public static Onyx3DInstance Instance
		{
			get
			{
				if (mMainInstance == null)
					throw new Exception("Onyx3DEngine main instance hasn't been set");

				return mMainInstance;
			}
		}

		public static void InitMain(IGraphicsContext context, IWindowInfo winInfo)
		{
			mMainInstance = new Onyx3DInstance();
			Context = context;
			WinInfo = winInfo;
		}
		
	}

}
