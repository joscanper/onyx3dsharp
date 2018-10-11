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
	public partial class MaterialSelectorWindow : AssetSelector<MaterialViewList>
	{
		protected override string GetWindowName()
		{
			return "Material Library";
		}
	}
}
