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
	public partial class NewEntityWindow : Form
	{
		public string EntityName { get { return textBoxName.Text; } }

		public NewEntityWindow()
		{
			InitializeComponent();
		}
        
    }
}
