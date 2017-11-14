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
		public event EventHandler PropertyChanged;

		public MaterialPropertiesControl()
		{
			InitializeComponent();
		}

		public void Fill(Material mat)
		{
			tableLayoutPanelProperties.Controls.Clear();

			tableLayoutPanelProperties.RowCount = mat.Properties.Count;
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
	}
}
