namespace Onyx3DEditor
{
	partial class TextureSelector
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
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("");
			this.listViewTextures = new System.Windows.Forms.ListView();
			this.SuspendLayout();
			// 
			// listViewTextures
			// 
			this.listViewTextures.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewTextures.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
			this.listViewTextures.Location = new System.Drawing.Point(0, 0);
			this.listViewTextures.MultiSelect = false;
			this.listViewTextures.Name = "listViewTextures";
			this.listViewTextures.Size = new System.Drawing.Size(284, 461);
			this.listViewTextures.TabIndex = 1;
			this.listViewTextures.UseCompatibleStateImageBehavior = false;
			this.listViewTextures.View = System.Windows.Forms.View.List;
			this.listViewTextures.DoubleClick += OnTextureSelected;
			// 
			// TextureSelector
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 461);
			this.Controls.Add(this.listViewTextures);
			this.Name = "TextureSelector";
			this.Text = "TextureSelector";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView listViewTextures;
	}
}