using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Onyx3D;

namespace Onyx3DEditor
{
	public partial class MaterialPropertiesControl : UserControl
	{
		private Material mMaterial;
		public event EventHandler PropertyChanged;

		public MaterialPropertiesControl()
		{
			InitializeComponent();
		}

		public void Clear()
		{
			textBoxGuid.Text = "";
			textBoxName.Text = "";

			tableLayoutPanelProperties.Controls.Clear();
		}

		public void Fill(Material mat)
		{
			mMaterial = mat;

			OnyxProjectMaterialAsset matAsset = (OnyxProjectMaterialAsset)mat.LinkedProjectAsset;
			textBoxGuid.Text = matAsset.Guid.ToString();
			textBoxName.Text = matAsset.Name;

			tableLayoutPanelProperties.Controls.Clear();
			tableLayoutPanelProperties.RowCount = mat.Properties.Count + 2;
			tableLayoutPanelProperties.RowStyles.Clear();
			int currentRow = 0;
			foreach (KeyValuePair<string,MaterialProperty> prop in mat.Properties)
			{
				MaterialPropertyControl propControl = new MaterialPropertyControl();
				propControl.Fill(prop.Key, prop.Value);
				propControl.Dock = DockStyle.Fill;
				propControl.OnPropertyChanged += OnPropertyChangedListener;
				tableLayoutPanelProperties.Controls.Add(propControl, 0, currentRow);
				tableLayoutPanelProperties.RowStyles.Add(new RowStyle(SizeType.Absolute, propControl.Size.Height));
				currentRow++;
			}
		}

		private void OnPropertyChangedListener()
		{
			PropertyChanged.Invoke(this, null);
		}

		private void textBoxName_TextChanged(object sender, EventArgs e)
		{
			OnyxProjectMaterialAsset matAsset = (OnyxProjectMaterialAsset)mMaterial.LinkedProjectAsset;
			matAsset.Name = textBoxName.Text;
			//PropertyChanged.Invoke(this, null);
		}
	}
}
