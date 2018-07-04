namespace Onyx3DEditor
{
	partial class MeshSelectorWindow
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
			this.meshViewList = new Onyx3DEditor.MeshViewList();
			this.SuspendLayout();
			// 
			// materialViewList
			// 
			this.meshViewList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.meshViewList.Location = new System.Drawing.Point(0, 0);
			this.meshViewList.Name = "materialViewList";
			this.meshViewList.SelectedIndex = 0;
			this.meshViewList.Size = new System.Drawing.Size(284, 461);
			this.meshViewList.TabIndex = 0;
			// 
			// MaterialSelectorWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 461);
			this.Controls.Add(this.meshViewList);
			this.Name = "MaterialSelectorWindow";
			this.Text = "Material Selector";
			this.ResumeLayout(false);

		}

		#endregion

		private MeshViewList meshViewList;
	}
}