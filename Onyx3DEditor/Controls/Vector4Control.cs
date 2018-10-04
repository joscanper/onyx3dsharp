using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenTK;

namespace Onyx3DEditor
{
	public partial class Vector4Control : UserControl
	{

		public float X { get { return SafeGet(textBoxX.Text); } }
		public float Y { get { return SafeGet(textBoxY.Text); } }
		public float Z { get { return SafeGet(textBoxZ.Text); } }
		public float W { get { return SafeGet(textBoxW.Text); } }

		// --------------------------------------------------------------------

		public Vector4Control()
		{
			InitializeComponent();

			textBoxX.TextChanged += OnTextBoxChanged;
			textBoxY.TextChanged += OnTextBoxChanged;
			textBoxZ.TextChanged += OnTextBoxChanged;
			textBoxW.TextChanged += OnTextBoxChanged;
		}

		// --------------------------------------------------------------------

		public void Fill(Vector4 v, int size)
		{
			textBoxX.Text = v.X.ToString();
			textBoxY.Text = v.Y.ToString();
			textBoxZ.Text = v.Z.ToString();
			textBoxW.Text = v.W.ToString();

			if (size < 4)
				textBoxW.Visible = false;
			if (size < 3)
				textBoxZ.Visible = false;
			if (size < 2)
				textBoxY.Visible = false;
			if (size < 1)
				textBoxX.Visible = false;

		}

		// --------------------------------------------------------------------

		private void OnTextBoxChanged(object sender, EventArgs e)
		{
			OnTextChanged(e);
		}
		
		// --------------------------------------------------------------------

		private float SafeGet(string value)
		{
			try
			{
				float val = (float)Convert.ToDouble(value);
				return val;
			}catch (Exception e)
			{
				return 0;
			}
		}
		
	}
}
