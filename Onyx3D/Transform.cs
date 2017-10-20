using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK;

namespace Onyx3D
{
	class Transform
	{
		public SceneNode Node;
		public Transform Parent;
		public Vector3 Position;
		public Quaternion Rotation;
	}
}
