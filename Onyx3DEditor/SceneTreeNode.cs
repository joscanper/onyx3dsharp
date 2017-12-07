
using Onyx3D;
using System.Windows.Forms;

namespace Onyx3DEditor
{
	public class SceneTreeNode : TreeNode
	{
		public SceneObject BoundSceneObject { get; private set; }

		public SceneTreeNode(SceneObject sceneObject) : base(sceneObject.Id)
		{
			BoundSceneObject = sceneObject;
		}
	}
}
