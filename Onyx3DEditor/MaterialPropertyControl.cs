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
	public partial class MaterialPropertyControl : UserControl
	{
		public MaterialPropertyControl()
		{
			InitializeComponent();
		}

		public void Fill(string key, MaterialProperty prop)
		{
			labelPropertyName.Text = key;

			switch (prop.Type)
			{
				case MaterialPropertyType.Sampler2D:
					SetTextureLayout(prop);
					break;
				case MaterialPropertyType.Float:
					SetTextBoxLayout(prop, ((float)prop.Data).ToString());
					break;
			}
		}


		private void SetTextureLayout(MaterialProperty prop)
		{
			TextureMaterialProperty tmp = (TextureMaterialProperty)prop;
			PictureBox pic = new PictureBox();
			pic.Size = new Size(64, 64);
			pic.BackColor = Color.Black;
			pic.Image = new Bitmap(tmp.Texture.Path).GetThumbnailImage(64, 64, null, IntPtr.Zero);
			panelPropertyValue.Controls.Add(pic);
			panelPropertyValue.Size = new Size(panelPropertyValue.Size.Width, 70);
			this.Size = new Size(this.Size.Width, 70);
		}

		private void SetTextBoxLayout(MaterialProperty prop, string data)
		{
			TextBox tb = new TextBox();
			tb.Text = data + "";
			tb.Dock = DockStyle.Fill;
			panelPropertyValue.Controls.Add(tb);
		}
	}
}
