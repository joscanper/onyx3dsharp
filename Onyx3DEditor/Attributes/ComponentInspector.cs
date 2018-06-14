using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Onyx3DEditor
{
	public class ComponentInspector : Attribute
	{
		public Type ComponentType;

		public ComponentInspector(Type componentType)
		{
			ComponentType = componentType;
		}
	}
}
