using System;

namespace Onyx3DEditor
{
	partial class MaterialSelectorWindow
	{
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.materialViewList = new Onyx3DEditor.MaterialViewList();
			this.SuspendLayout();
			// 
			// materialViewList
			// 
			this.materialViewList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.materialViewList.Location = new System.Drawing.Point(0, 0);
			this.materialViewList.Name = "materialViewList";
			this.materialViewList.SelectedIndex = 0;
			this.materialViewList.Size = new System.Drawing.Size(284, 461);
			this.materialViewList.TabIndex = 0;
			// 
			// MaterialSelectorWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 461);
			this.Controls.Add(this.materialViewList);
			this.Name = "MaterialSelectorWindow";
			this.Text = "Material Selector";
			this.ResumeLayout(false);
            this.Load += MaterialSelectorWindow_Load;

		}

        #endregion

        private MaterialViewList materialViewList;
	}
}