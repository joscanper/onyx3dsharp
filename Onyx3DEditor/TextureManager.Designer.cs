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
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("");
			this.pictureBoxPreview = new System.Windows.Forms.PictureBox();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButtonOpen = new System.Windows.Forms.ToolStripButton();
			this.textBoxFilePath = new System.Windows.Forms.TextBox();
			this.textBoxId = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.listViewTextures = new System.Windows.Forms.ListView();
			this.buttonOpen = new System.Windows.Forms.Button();
			this.buttonApply = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).BeginInit();
			this.toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBoxPreview
			// 
			this.pictureBoxPreview.BackColor = System.Drawing.SystemColors.WindowText;
			this.pictureBoxPreview.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pictureBoxPreview.Location = new System.Drawing.Point(0, 232);
			this.pictureBoxPreview.Name = "pictureBoxPreview";
			this.pictureBoxPreview.Size = new System.Drawing.Size(331, 300);
			this.pictureBoxPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pictureBoxPreview.TabIndex = 0;
			this.pictureBoxPreview.TabStop = false;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonOpen});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(355, 25);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
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
			this.textBoxFilePath.Location = new System.Drawing.Point(72, 12);
			this.textBoxFilePath.Name = "textBoxFilePath";
			this.textBoxFilePath.ReadOnly = true;
			this.textBoxFilePath.Size = new System.Drawing.Size(206, 20);
			this.textBoxFilePath.TabIndex = 2;
			// 
			// textBoxId
			// 
			this.textBoxId.Location = new System.Drawing.Point(72, 38);
			this.textBoxId.Name = "textBoxId";
			this.textBoxId.Size = new System.Drawing.Size(247, 20);
			this.textBoxId.TabIndex = 3;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(11, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(29, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Path";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(11, 41);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(16, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "Id";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.listViewTextures);
			this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.buttonOpen);
			this.splitContainer1.Panel2.Controls.Add(this.buttonApply);
			this.splitContainer1.Panel2.Controls.Add(this.label2);
			this.splitContainer1.Panel2.Controls.Add(this.label1);
			this.splitContainer1.Panel2.Controls.Add(this.textBoxId);
			this.splitContainer1.Panel2.Controls.Add(this.textBoxFilePath);
			this.splitContainer1.Panel2.Controls.Add(this.pictureBoxPreview);
			this.splitContainer1.Size = new System.Drawing.Size(690, 532);
			this.splitContainer1.SplitterDistance = 355;
			this.splitContainer1.TabIndex = 6;
			// 
			// listViewTextures
			// 
			this.listViewTextures.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewTextures.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
			this.listViewTextures.Location = new System.Drawing.Point(0, 25);
			this.listViewTextures.MultiSelect = false;
			this.listViewTextures.Name = "listViewTextures";
			this.listViewTextures.Size = new System.Drawing.Size(355, 507);
			this.listViewTextures.TabIndex = 0;
			this.listViewTextures.UseCompatibleStateImageBehavior = false;
			this.listViewTextures.View = System.Windows.Forms.View.List;
			this.listViewTextures.SelectedIndexChanged += new System.EventHandler(this.listViewTextures_SelectedIndexChanged);
			// 
			// buttonOpen
			// 
			this.buttonOpen.BackgroundImage = global::Onyx3DEditor.Properties.Resources.if_folder_open_21164;
			this.buttonOpen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.buttonOpen.Location = new System.Drawing.Point(284, 3);
			this.buttonOpen.Name = "buttonOpen";
			this.buttonOpen.Size = new System.Drawing.Size(35, 29);
			this.buttonOpen.TabIndex = 7;
			this.buttonOpen.UseVisualStyleBackColor = true;
			this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
			// 
			// buttonApply
			// 
			this.buttonApply.Location = new System.Drawing.Point(244, 203);
			this.buttonApply.Name = "buttonApply";
			this.buttonApply.Size = new System.Drawing.Size(75, 23);
			this.buttonApply.TabIndex = 6;
			this.buttonApply.Text = "Apply";
			this.buttonApply.UseVisualStyleBackColor = true;
			this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
			// 
			// TextureManager
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(690, 532);
			this.Controls.Add(this.splitContainer1);
			this.Name = "TextureManager";
			this.Text = "TextureManager";
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).EndInit();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBoxPreview;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.TextBox textBoxFilePath;
		private System.Windows.Forms.TextBox textBoxId;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.ListView listViewTextures;
		private System.Windows.Forms.Button buttonApply;
		private System.Windows.Forms.Button buttonOpen;
		private System.Windows.Forms.ToolStripButton toolStripButtonOpen;
	}
}