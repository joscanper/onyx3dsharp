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
	public partial class AssetField : UserControl
	{

		public EventHandler AssetChanged;
		public int SelectedAssetGuid;

		private GameAsset mAsset;
		private Type mSelectorType;

		public AssetField()
		{
			InitializeComponent();
		}

		public void Fill<T>(string labelName, GameAsset asset) where T : AssetSelector
		{
			mAsset = asset;
			mSelectorType = typeof(T);
			assetNameLabel.Text = labelName;
			assetRefField.Text = asset.LinkedProjectAsset.Name != null ? asset.LinkedProjectAsset.Name : asset.LinkedProjectAsset.Guid.ToString();
		}

		private void buttonSearch_Click(object sender, EventArgs e)
		{
			SearchAsset();
		}

		public void SearchAsset()
		{
			
			AssetSelector selector = (AssetSelector)Activator.CreateInstance(mSelectorType);
			selector.Show();
			selector.SelectedAssetChanged += (comp, args) =>
			{
				SelectedAssetGuid = selector.SelectedAsset.Guid;
				AssetChanged?.Invoke(this, args);
			};
			
		}
	}
}
