using Onyx3DEditor.Controls;

namespace Onyx3DEditor
{
	public partial class EntitySelectorWindow : AssetSelector<EntityViewList>
	{
		protected override string GetWindowName()
		{
			return "Entity Library";
		}
	}
}