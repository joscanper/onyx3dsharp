﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using Onyx3D;
using OpenTK;

namespace Onyx3DEditor
{
	public partial class MaterialPropertyControl : UserControl
	{
		public Action OnPropertyChanged;

		private Control mPropertyControl;
		private MaterialProperty mProperty;
		private TextureSelectorWindow mTextureSelector;

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
				case MaterialPropertyType.Color:
					SetColorLayout(prop);
					break;
				case MaterialPropertyType.Sampler2D:
					SetTextureLayout(prop);
					break;
				case MaterialPropertyType.Float:
					SetTextBoxLayout(prop, ((float)prop.Data).ToString());
					break;
				case MaterialPropertyType.Vector2:
					SetVectorLayout(prop, 2, new Vector4(((Vector2)prop.Data)));
					break;
			}
		}
		private void SetVectorLayout(MaterialProperty prop, int size, Vector4 data)
		{
			
			Vector4Control tb = new Vector4Control();
			tb.Fill(data, size);
			tb.Dock = DockStyle.Fill;
			tb.TextChanged += OnVectorChanged;
			panelPropertyValue.Controls.Add(tb);

			mPropertyControl = tb;
		}


		private void SetColorLayout(MaterialProperty prop)
		{
			Vector4 color = (Vector4)prop.Data;
			Color systemColor = Color.FromArgb((int)(color.X * 255), (int)(color.Y * 255), (int)(color.Z * 255));
			
			
			PictureBox colorBox = new PictureBox();
			colorBox.Size = new Size(64, 20);
			colorBox.BackColor = systemColor;
			colorBox.BorderStyle = BorderStyle.FixedSingle;
			panelPropertyValue.Controls.Add(colorBox);
			panelPropertyValue.Size = new Size(panelPropertyValue.Size.Width, 70);
			//this.Size = new Size(this.Size.Width, 70);
			colorBox.Click += OnColorBoxClicked;

			mPropertyControl = colorBox;
		}

		private void SetTextureLayout(MaterialProperty prop)
		{
			TextureMaterialProperty tmp = (TextureMaterialProperty)prop;
			PictureBox pic = new PictureBox();
			pic.Size = new Size(64, 64);
			pic.BackColor = Color.Black;

            OnyxProjectAsset asset = ProjectManager.Instance.Content.GetAsset(tmp.TextureGuid);
			pic.Image = new Bitmap(asset.AbsolutePath).GetThumbnailImage(64, 64, null, IntPtr.Zero);
			panelPropertyValue.Controls.Add(pic);
			panelPropertyValue.Size = new Size(panelPropertyValue.Size.Width, 70);
			this.Size = new Size(this.Size.Width, 70);
			pic.Click += OnPictureBoxClicked;

			mPropertyControl = pic;
		}

		private void SetTextBoxLayout(MaterialProperty prop, string data)
		{
			TextBox tb = new TextBox();
			tb.Text = data + "";
			tb.Dock = DockStyle.Fill;
			tb.TextChanged += OnTextBoxChanged;
			panelPropertyValue.Controls.Add(tb);

			mPropertyControl = tb;
		}


		private void OnColorBoxClicked(object sender, EventArgs e)
		{
			ColorDialog colorPicker = new ColorDialog();
			colorPicker.SolidColorOnly = false;
			colorPicker.FullOpen = true;
			Vector4 currentColor = (Vector4)mProperty.Data;
			colorPicker.Color = currentColor.ToColor();

			if (colorPicker.ShowDialog() == DialogResult.OK)
			{
				Color c = colorPicker.Color;
				mProperty.Data = c.ToVector();

				PictureBox cb = (PictureBox)mPropertyControl;
				cb.BackColor = c;

				OnPropertyChanged();
			}
		}

		private void OnVectorChanged(object sender, EventArgs e)
		{
			Vector4Control control = (Vector4Control)sender;
			object data = mProperty.Data;
			switch (mProperty.Type)
			{
				case MaterialPropertyType.Vector2:
					data = new Vector2(control.X, control.Y);
					break;
				case MaterialPropertyType.Vector3:
					data = new Vector3(control.X, control.Y, control.Z);
					break;
				case MaterialPropertyType.Vector4:
					data = new Vector4(control.X, control.Y, control.Z, control.W);
					break;
			}
			mProperty.Data = data;
		}

		private void OnPictureBoxClicked(object sender, EventArgs e)
		{
			mTextureSelector = new TextureSelectorWindow();
			mTextureSelector.TextureSelected += OnTextureSelected;
			mTextureSelector.Show();
		}

		private void OnTextureSelected(object sender, EventArgs e)
		{
			TextureMaterialProperty tmp = (TextureMaterialProperty)mProperty;
			tmp.TextureGuid = mTextureSelector.SelectedTexture.Guid;
			
			PictureBox pb = (PictureBox)mPropertyControl;
            OnyxProjectAsset asset = ProjectManager.Instance.Content.GetAsset(tmp.TextureGuid);
            pb.Image = new Bitmap(asset.AbsolutePath).GetThumbnailImage(64, 64, null, IntPtr.Zero);

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
