namespace Onyx3DEditor
{
	partial class MeshViewList
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
			this.listViewMeshes = new System.Windows.Forms.ListView();
			this.SuspendLayout();
			// 
			// listViewMaterials
			// 
			this.listViewMeshes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewMeshes.GridLines = true;
			this.listViewMeshes.Location = new System.Drawing.Point(0, 0);
			this.listViewMeshes.MultiSelect = false;
			this.listViewMeshes.Name = "listViewMaterials";
			this.listViewMeshes.Size = new System.Drawing.Size(307, 347);
			this.listViewMeshes.TabIndex = 0;
			this.listViewMeshes.UseCompatibleStateImageBehavior = false;
			this.listViewMeshes.SelectedIndexChanged += new System.EventHandler(this.ListViewMeshes_SelectedIndexChanged);
			// 
			// MaterialViewList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.listViewMeshes);
			this.Name = "MaterialViewList";
			this.Size = new System.Drawing.Size(307, 347);
			this.Load += new System.EventHandler(this.MeshViewList_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView listViewMeshes;
	}
}
