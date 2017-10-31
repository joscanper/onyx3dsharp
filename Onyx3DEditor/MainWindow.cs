using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Onyx3DEditor
{
	public partial class MainWindow : Form
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void toolStripButtonSaveProject_Click(object sender, EventArgs e)
		{
			ProjectManager.Instance.Save();
		}

		private void toolStripButtonMaterials_Click(object sender, EventArgs e)
		{
			new MaterialEditor().Show();
		}

		private void toolStripButtonTextures_Click(object sender, EventArgs e)
		{
			new TextureManager().Show();
		}
	}
}
