using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Onyx3D;

namespace Onyx3DEditor.Controls
{
	public partial class EntityViewList : AssetViewList
	{
		public EntityViewList()
		{
			InitializeComponent();
		}

		// --------------------------------------------------------------------

		protected override void AddBuiltIn(int selectedGuid)
		{
			//throw new NotImplementedException();
		}

		// --------------------------------------------------------------------

		protected override Bitmap GeneratePreview(int guid)
		{
			//throw new NotImplementedException();
			return new Bitmap(64, 64);
		}

		// --------------------------------------------------------------------

		protected override List<OnyxProjectAsset> GetAssets()
		{
			return ProjectManager.Instance.Content.Entities;
		}
	}
}
