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
	public partial class EntitySelectorWindow : Form
	{
		public EntitySelectorWindow()
		{
			InitializeComponent();

			FillTable();
		}

		// --------------------------------------------------------------------

		private void FillTable()
		{
			tableEntities.RowCount = ProjectManager.Instance.Content.Entities.Count;
			int row = 0;
			foreach (OnyxProjectAsset asset in ProjectManager.Instance.Content.Entities)
			{
				Button button = new Button();
				button.Text = asset.Name;
				button.Click += OnEntityClicked;
				button.Dock = DockStyle.Fill;
				tableEntities.SetRow(button, row);
				row++;
			}
		}

		// --------------------------------------------------------------------
		private void OnEntityClicked(object sender, EventArgs e)
		{
			//Button button = sender as Button;
			//EditorEntityUtils.Instantiate()
		}

	}
}
