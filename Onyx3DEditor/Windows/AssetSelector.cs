using Onyx3D;
using System;
using System.Windows.Forms;

namespace Onyx3DEditor
{

	public class AssetSelector : Form
	{
		public EventHandler SelectedAssetChanged;
		public OnyxProjectAsset SelectedAsset;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		public AssetSelector()
		{
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// AssetSelector
			// 
			this.ClientSize = new System.Drawing.Size(284, 727);
			this.Name = "AssetSelector";
			this.ResumeLayout(false);

		}
	}

	public class AssetSelector<T> : AssetSelector where T : AssetViewList, new()
	{
		
		private T mViewList;

		public AssetSelector()
		{
			
			mViewList = Activator.CreateInstance<T>();
			Controls.Add(mViewList);
			mViewList.Dock = DockStyle.Fill;
			mViewList.SelectedChanged += new EventHandler(OnSelectionChanged);

			Text = GetWindowName();
			
		}

		// --------------------------------------------------------------------

		protected virtual string GetWindowName()
		{
			return "Asset Library";
		}

		// --------------------------------------------------------------------

		private void OnSelectionChanged(object sender, EventArgs e)
		{
			SelectedAsset = mViewList.SelectedAsset;
			SelectedAssetChanged?.Invoke(this, e);
		}
	}
}
