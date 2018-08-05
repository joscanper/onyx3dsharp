using System;
using System.Windows.Forms;

namespace Onyx3DEditor
{
	partial class ModelImporterWindow
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModelImporterWindow));
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.buttonImport = new System.Windows.Forms.Button();
            this.panelGL = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.modelTreeView = new System.Windows.Forms.TreeView();
            this.treeViewImages = new System.Windows.Forms.ImageList(this.components);
            this.panelGL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxPath
            // 
            this.textBoxPath.Enabled = false;
            this.textBoxPath.Location = new System.Drawing.Point(47, 18);
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.Size = new System.Drawing.Size(303, 20);
            this.textBoxPath.TabIndex = 1;
            // 
            // buttonImport
            // 
            this.buttonImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonImport.Enabled = false;
            this.buttonImport.Location = new System.Drawing.Point(12, 397);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(341, 28);
            this.buttonImport.TabIndex = 3;
            this.buttonImport.Text = "Import";
            this.buttonImport.UseVisualStyleBackColor = true;
            this.buttonImport.Click += new System.EventHandler(this.buttonImport_Click);
            // 
            // panelGL
            // 
            this.panelGL.Controls.Add(this.pictureBox1);
            this.panelGL.Location = new System.Drawing.Point(12, 44);
            this.panelGL.Name = "panelGL";
            this.panelGL.Size = new System.Drawing.Size(338, 286);
            this.panelGL.TabIndex = 5;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(338, 286);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // buttonOpen
            // 
            this.buttonOpen.Image = global::Onyx3DEditor.Properties.Resources.if_folder_open_21164;
            this.buttonOpen.Location = new System.Drawing.Point(12, 12);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(29, 26);
            this.buttonOpen.TabIndex = 0;
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // modelTreeView
            // 
            this.modelTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.modelTreeView.ImageIndex = 0;
            this.modelTreeView.ImageList = this.treeViewImages;
            this.modelTreeView.Location = new System.Drawing.Point(359, 18);
            this.modelTreeView.Name = "modelTreeView";
            this.modelTreeView.SelectedImageIndex = 0;
            this.modelTreeView.Size = new System.Drawing.Size(249, 407);
            this.modelTreeView.TabIndex = 6;
            // 
            // treeViewImages
            // 
            this.treeViewImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("treeViewImages.ImageStream")));
            this.treeViewImages.TransparentColor = System.Drawing.Color.Transparent;
            this.treeViewImages.Images.SetKeyName(0, "add.png");
            this.treeViewImages.Images.SetKeyName(1, "update.png");
            // 
            // ModelImporterWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 433);
            this.Controls.Add(this.modelTreeView);
            this.Controls.Add(this.panelGL);
            this.Controls.Add(this.buttonImport);
            this.Controls.Add(this.textBoxPath);
            this.Controls.Add(this.buttonOpen);
            this.Name = "ModelImporterWindow";
            this.Text = "ImportModel";
            this.Shown += new System.EventHandler(this.ModelImporterWindow_Shown);
            this.panelGL.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonOpen;
		private System.Windows.Forms.TextBox textBoxPath;
		private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.Panel panelGL;
        private TreeView modelTreeView;
        private ImageList treeViewImages;
        private PictureBox pictureBox1;
    }
}