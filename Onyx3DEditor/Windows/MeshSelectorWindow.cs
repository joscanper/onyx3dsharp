using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Onyx3D;

namespace Onyx3DEditor
{
	public partial class MeshSelectorWindow : AssetSelector
	{
		
		public MeshSelectorWindow()
		{
			InitializeComponent();
			meshViewList.SelectedChanged += new EventHandler(OnSelectionChanged);
		}

		private void OnSelectionChanged(object sender, EventArgs e)
		{
			SelectedAsset = meshViewList.SelectedMesh;
			SelectedAssetChanged?.Invoke(this, e);
		}

	}
}
