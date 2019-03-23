using Onyx3D;
using System.Collections.Generic;
using System.Drawing;

namespace Onyx3DEditor.Controls
{
	public partial class EntityViewList : AssetViewList
	{
		public EntityViewList()
		{
			InitializeComponent();
		}

		// --------------------------------------------------------------------

		protected override PreviewRenderer InstantiatePreviewRenderer()
		{
			return new SingleEntityPreviewRenderer();
		}

		// --------------------------------------------------------------------

		protected override void AddBuiltIn(int selectedGuid)
		{
		}

		// --------------------------------------------------------------------

		protected override Bitmap GeneratePreview(int guid)
		{
			SingleEntityPreviewRenderer entityPreview = (SingleEntityPreviewRenderer)mPreview;
			entityPreview.SetEntity(guid);
			entityPreview.SetFloorActive(false);
			entityPreview.Render();
			return entityPreview.AsBitmap();
		}

		// --------------------------------------------------------------------

		protected override List<OnyxProjectAsset> GetAssets()
		{
			return ProjectManager.Instance.Content.Entities;
		}
	}
}