using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Onyx3D
{
	public abstract class Object
	{
		private static int mLastGeneratedInstanceId = 0;

		private int mInstanceId = 0;

		public Object(int instanceId)
		{
			mInstanceId = instanceId;
			CheckInstanceId();
		}

		private void CheckInstanceId()
		{
			if (mInstanceId == 0)
			{
				if (mLastGeneratedInstanceId == 0)
					mLastGeneratedInstanceId = new Random(System.DateTime.Now.Millisecond).Next();

				mInstanceId = mLastGeneratedInstanceId + 1;
				mLastGeneratedInstanceId = mInstanceId;

				// TODO - Check collitions
			}
		}

		public int InstanceId
		{
			get {
				CheckInstanceId();
				return mInstanceId;
			}
		}

	}
}
