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
	public partial class SelectedInspectorPanel : UserControl
	{
		public SelectedInspectorPanel()
		{
			InitializeComponent();
		}

		public void Fill(SceneObject obj)
		{
			tableLayoutPanel.Controls.Clear();

			tableLayoutPanel.RowCount = 2;
			tableLayoutPanel.RowStyles.Clear();

			
			// Transform inspector
			PropertyGrid propGrid = new PropertyGrid();
			propGrid.SelectedObject = new TransformInspector(obj.Transform);
			propGrid.PropertyValueChanged += OnInspectorChanged;
			propGrid.HelpVisible = false;
			propGrid.Width = this.Width;
			//propGrid.AutoScaleMode = AutoScaleMode.Inherit;
			tableLayoutPanel.Controls.Add(propGrid, 0, 0);
			//tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, propGrid.Size.Height));
		}

		public void Clear()
		{
			tableLayoutPanel.Controls.Clear();
		}

		private void OnInspectorChanged(object s, PropertyValueChangedEventArgs e)
		{
			PropertyGrid propGrid = s as PropertyGrid;
			IInspector inspector = propGrid.SelectedObject as IInspector;
			inspector.Apply();
		}
	}
}
