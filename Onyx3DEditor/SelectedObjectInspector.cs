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
	public partial class SelectedObjectInspector : UserControl
	{
		public Action OnInspectorChanged;
		
		public SelectedObjectInspector()
		{
			InitializeComponent();
		}

		public void Fill(SceneObject obj)
		{

			Clear();


			// Name
			textBoxName.Visible = true;
			textBoxName.Text = obj.Id;
			tableLayoutPanel.Controls.Add(textBoxName, 0, 0);


			// Transform inspector
			PropertyGrid propGrid = new PropertyGrid();
			propGrid.SelectedObject = new TransformInspector(obj.Transform);
			propGrid.PropertyValueChanged += OnObjectInspectorChanged;
			propGrid.HelpVisible = false;
			propGrid.Width = this.Width;
			tableLayoutPanel.Controls.Add(propGrid, 0, 1);
			//tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, propGrid.Size.Height));



		}

		public void Clear()
		{
			tableLayoutPanel.Controls.Clear();
			tableLayoutPanel.RowCount = 2;
			tableLayoutPanel.RowStyles.Clear();

			textBoxName.Visible = false;
		}

		private void OnObjectInspectorChanged(object s, PropertyValueChangedEventArgs e)
		{
			PropertyGrid propGrid = s as PropertyGrid;
			IInspector inspector = propGrid.SelectedObject as IInspector;
			propGrid.Refresh();
			inspector.Apply();
			OnInspectorChanged();
		}

		private void textBoxName_TextChanged(object sender, EventArgs e)
		{
			Selection.ActiveObject.Id = textBoxName.Text;
			OnInspectorChanged();
		}
	}
}
