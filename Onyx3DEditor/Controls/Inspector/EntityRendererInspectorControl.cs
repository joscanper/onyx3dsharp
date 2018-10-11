using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Onyx3D;

namespace Onyx3DEditor.Controls.Inspector
{
	[ComponentInspector(typeof(EntityRenderer))]
	public partial class EntityRendererInspectorControl : InspectorControl
	{
		private EntityRenderer mRenderer;

		// --------------------------------------------------------------------

		public EntityRendererInspectorControl(EntityRenderer renderer)
		{
			InitializeComponent();

			mRenderer = renderer;
			entityAssetField.AssetChanged += new EventHandler(OnEntitySelected);
			//materialAssetField.AssetChanged += new EventHandler(OnMaterialSelected);
		}

		// --------------------------------------------------------------------

		private void UpdateEntity()
		{
			entityAssetField.Fill<EntitySelectorWindow>("Entity", mRenderer.Entity);
		}

		// --------------------------------------------------------------------

		private void OnEntitySelected(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		// --------------------------------------------------------------------

		private void EntityRendererInspectorControl_Load(object sender, EventArgs e)
		{
			UpdateEntity();
		}
	}
}
