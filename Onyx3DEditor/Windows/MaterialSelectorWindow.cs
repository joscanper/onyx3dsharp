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
	public partial class MaterialSelectorWindow : AssetSelector
	{
		
		public MaterialSelectorWindow()
		{
			InitializeComponent();
			materialViewList.SelectedChanged += new EventHandler(OnSelectionChanged);
		}


        private void MaterialSelectorWindow_Load(object sender, EventArgs e)
        {
            materialViewList.UpdateMaterialList(-1, true);
        }


        private void OnSelectionChanged(object sender, EventArgs e)
		{
			SelectedAsset = materialViewList.SelectedMaterial;
			SelectedAssetChanged?.Invoke(this, e);
		}

	}
}
