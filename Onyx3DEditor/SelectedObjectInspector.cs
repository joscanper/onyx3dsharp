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
			AddInspector(new TransformInspector(obj.Transform));
			
			//tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, propGrid.Size.Height));


			obj.ForEachComponent((c) =>
			{
				Type inspectorType = c.GetInspectorType();
				if (inspectorType != null)
				{
					IInspector inspector = (IInspector)Activator.CreateInstance(inspectorType, c);
					AddInspector(inspector);
				}
			});

		}

		public void Fill(Scene scene)
		{

			Clear();

			// Name
			textBoxName.Visible = true;
			textBoxName.Text = "SCENE NAME HERE!";
			tableLayoutPanel.Controls.Add(textBoxName, 0, 0);

			AddInspector(new SceneInspector(scene));
		}

		private void AddInspector(IInspector inspector)
		{
			PropertyGrid propGrid = new PropertyGrid();
			propGrid.SelectedObject = inspector;
			propGrid.PropertyValueChanged += OnObjectInspectorChanged;
			propGrid.HelpVisible = false;
			propGrid.Width = this.Width;
			propGrid.ToolbarVisible = false;
			propGrid.Height = inspector.GetFieldCount() * 20;

			tableLayoutPanel.Controls.Add(propGrid);
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
