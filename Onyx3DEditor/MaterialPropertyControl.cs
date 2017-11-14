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
		public Action OnPropertyChanged;
		private MaterialProperty mProperty;
		private TextureSelector mTextureSelector;

		public MaterialPropertyControl()
		{
			InitializeComponent();
		}

		public void Fill(string key, MaterialProperty prop)
		{
			mProperty = prop;

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
			pic.Click += OnPictureBoxClicked;
		}

		private void SetTextBoxLayout(MaterialProperty prop, string data)
		{
			TextBox tb = new TextBox();
			tb.Text = data + "";
			tb.Dock = DockStyle.Fill;
			tb.TextChanged += OnTextBoxChanged;
			panelPropertyValue.Controls.Add(tb);

		}

		private void OnPictureBoxClicked(object sender, EventArgs e)
		{
			mTextureSelector = new TextureSelector();
			mTextureSelector.TextureSelected += OnTextureSelected;
			mTextureSelector.Show();
		}

		private void OnTextureSelected(object sender, EventArgs e)
		{
			
			TextureMaterialProperty tmp = (TextureMaterialProperty)mProperty;
			tmp.Texture = mTextureSelector.SelectedTexture;
			tmp.Data = tmp.Texture.Id;
			OnPropertyChanged();
		}

		private void OnTextBoxChanged(object sender, EventArgs e)
		{
			TextBox tb = sender as TextBox;
			
			try
			{
				switch (mProperty.Type)
				{
					case MaterialPropertyType.Float:
						mProperty.Data = (float)Convert.ToDouble(tb.Text);
						break;
				}
				tb.BackColor = Color.WhiteSmoke;
				OnPropertyChanged();
			}
			catch (Exception exc)
			{
				tb.BackColor = Color.IndianRed;
			}
		}
	}
}
