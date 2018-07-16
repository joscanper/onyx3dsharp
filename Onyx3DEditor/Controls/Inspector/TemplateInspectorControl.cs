using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Onyx3D;

namespace Onyx3DEditor.Controls.Inspector
{
	[ComponentInspector(typeof(TemplateProxy))]
	public partial class TemplateInspectorControl : InspectorControl
	{
		public TemplateInspectorControl()
		{
			InitializeComponent();
		}
	}
}
