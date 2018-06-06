namespace Onyx3DEditor
{
	partial class MaterialViewList
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.listViewMaterials = new System.Windows.Forms.ListView();
			this.SuspendLayout();
			// 
			// listViewMaterials
			// 
			this.listViewMaterials.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewMaterials.GridLines = true;
			this.listViewMaterials.Location = new System.Drawing.Point(0, 0);
			this.listViewMaterials.MultiSelect = false;
			this.listViewMaterials.Name = "listViewMaterials";
			this.listViewMaterials.Size = new System.Drawing.Size(307, 347);
			this.listViewMaterials.TabIndex = 0;
			this.listViewMaterials.UseCompatibleStateImageBehavior = false;
			this.listViewMaterials.SelectedIndexChanged += new System.EventHandler(this.ListViewMaterials_SelectedIndexChanged);
			// 
			// MaterialViewList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.listViewMaterials);
			this.Name = "MaterialViewList";
			this.Size = new System.Drawing.Size(307, 347);
			this.Load += new System.EventHandler(this.MaterialViewList_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView listViewMaterials;
	}
}
