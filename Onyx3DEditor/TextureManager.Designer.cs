namespace Onyx3DEditor
{
	partial class TextureManager
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
			this.pictureBoxPreview = new System.Windows.Forms.PictureBox();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripComboBoxTextures = new System.Windows.Forms.ToolStripComboBox();
			this.toolStripButtonOpen = new System.Windows.Forms.ToolStripButton();
			this.textBoxFilePath = new System.Windows.Forms.TextBox();
			this.textBoxId = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).BeginInit();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBoxPreview
			// 
			this.pictureBoxPreview.BackColor = System.Drawing.SystemColors.WindowText;
			this.pictureBoxPreview.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pictureBoxPreview.Location = new System.Drawing.Point(0, 232);
			this.pictureBoxPreview.Name = "pictureBoxPreview";
			this.pictureBoxPreview.Size = new System.Drawing.Size(332, 300);
			this.pictureBoxPreview.TabIndex = 0;
			this.pictureBoxPreview.TabStop = false;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBoxTextures,
            this.toolStripButtonOpen});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(332, 25);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripComboBoxTextures
			// 
			this.toolStripComboBoxTextures.Name = "toolStripComboBoxTextures";
			this.toolStripComboBoxTextures.Size = new System.Drawing.Size(121, 25);
			this.toolStripComboBoxTextures.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxTextures_SelectedIndexChanged);
			// 
			// toolStripButtonOpen
			// 
			this.toolStripButtonOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonOpen.Image = global::Onyx3DEditor.Properties.Resources.if_folder_open_21164;
			this.toolStripButtonOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonOpen.Name = "toolStripButtonOpen";
			this.toolStripButtonOpen.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonOpen.Text = "toolStripButton1";
			this.toolStripButtonOpen.Click += new System.EventHandler(this.toolStripButtonOpen_Click);
			// 
			// textBoxFilePath
			// 
			this.textBoxFilePath.Location = new System.Drawing.Point(73, 28);
			this.textBoxFilePath.Name = "textBoxFilePath";
			this.textBoxFilePath.ReadOnly = true;
			this.textBoxFilePath.Size = new System.Drawing.Size(247, 20);
			this.textBoxFilePath.TabIndex = 2;
			// 
			// textBoxId
			// 
			this.textBoxId.Location = new System.Drawing.Point(73, 54);
			this.textBoxId.Name = "textBoxId";
			this.textBoxId.Size = new System.Drawing.Size(247, 20);
			this.textBoxId.TabIndex = 3;
			this.textBoxId.TextChanged += new System.EventHandler(this.textBoxId_TextChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 31);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(29, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Path";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 57);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(16, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "Id";
			// 
			// TextureManager
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(332, 532);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBoxId);
			this.Controls.Add(this.textBoxFilePath);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.pictureBoxPreview);
			this.Name = "TextureManager";
			this.Text = "TextureManager";
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).EndInit();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBoxPreview;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButtonOpen;
		private System.Windows.Forms.TextBox textBoxFilePath;
		private System.Windows.Forms.TextBox textBoxId;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ToolStripComboBox toolStripComboBoxTextures;
	}
}