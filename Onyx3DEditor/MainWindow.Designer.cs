namespace Onyx3DEditor
{
	partial class MainWindow
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


		private void InitializeCanvas()
		{
			// 
			// renderCanvas
			// 
			this.renderCanvas.BackColor = System.Drawing.Color.Black;
			this.renderCanvas.Name = "renderCanvas";;
			this.renderCanvas.VSync = false;
			this.renderCanvas.Load += new System.EventHandler(this.renderCanvas_Load);
			this.renderCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.renderCanvas_Paint);

		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.renderCanvas = new OpenTK.GLControl();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButtonNewProject = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonOpenProject = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonSaveProject = new System.Windows.Forms.ToolStripButton();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.toolStrip3 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.toolStrip3.SuspendLayout();
			this.SuspendLayout();
			// 
			// renderCanvas
			// 
			this.renderCanvas.BackColor = System.Drawing.Color.Black;
			this.renderCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
			this.renderCanvas.Location = new System.Drawing.Point(0, 0);
			this.renderCanvas.Name = "renderCanvas";
			this.renderCanvas.Size = new System.Drawing.Size(757, 520);
			this.renderCanvas.TabIndex = 0;
			this.renderCanvas.VSync = false;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonNewProject,
            this.toolStripButtonOpenProject,
            this.toolStripButtonSaveProject});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(979, 25);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripButtonNewProject
			// 
			this.toolStripButtonNewProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonNewProject.Image = global::Onyx3DEditor.Properties.Resources.if_gtk_new_20536;
			this.toolStripButtonNewProject.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonNewProject.Name = "toolStripButtonNewProject";
			this.toolStripButtonNewProject.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonNewProject.Text = "New Project";
			this.toolStripButtonNewProject.Click += new System.EventHandler(this.toolStripButtonNewProject_Click);
			// 
			// toolStripButtonOpenProject
			// 
			this.toolStripButtonOpenProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonOpenProject.Image = global::Onyx3DEditor.Properties.Resources.if_folder_open_21164;
			this.toolStripButtonOpenProject.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonOpenProject.Name = "toolStripButtonOpenProject";
			this.toolStripButtonOpenProject.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonOpenProject.Text = "Open Project";
			this.toolStripButtonOpenProject.Click += new System.EventHandler(this.toolStripButtonOpenProject_Click);
			// 
			// toolStripButtonSaveProject
			// 
			this.toolStripButtonSaveProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonSaveProject.Image = global::Onyx3DEditor.Properties.Resources.if_stock_save_20659;
			this.toolStripButtonSaveProject.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonSaveProject.Name = "toolStripButtonSaveProject";
			this.toolStripButtonSaveProject.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonSaveProject.Text = "Save Project";
			this.toolStripButtonSaveProject.Click += new System.EventHandler(this.toolStripButtonSaveProject_Click);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 25);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.toolStrip3);
			this.splitContainer1.Panel1.Controls.Add(this.treeView1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.splitContainer1.Panel2.Controls.Add(this.renderCanvas);
			this.splitContainer1.Size = new System.Drawing.Size(979, 520);
			this.splitContainer1.SplitterDistance = 218;
			this.splitContainer1.TabIndex = 2;
			// 
			// toolStrip3
			// 
			this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton2,
            this.toolStripButton1});
			this.toolStrip3.Location = new System.Drawing.Point(0, 0);
			this.toolStrip3.Name = "toolStrip3";
			this.toolStrip3.Size = new System.Drawing.Size(218, 25);
			this.toolStrip3.TabIndex = 2;
			this.toolStrip3.Text = "toolStrip3";
			// 
			// toolStripButton2
			// 
			this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton2.Image = global::Onyx3DEditor.Properties.Resources.stock_3d_texture;
			this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton2.Name = "toolStripButton2";
			this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton2.Text = "toolStripButtonTextures";
			this.toolStripButton2.Click += new System.EventHandler(this.toolStripButtonTextures_Click);
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton1.Image = global::Onyx3DEditor.Properties.Resources.stock_3d_texture_and_shading;
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton1.Text = "toolStripButtonMaterials";
			this.toolStripButton1.Click += new System.EventHandler(this.toolStripButtonMaterials_Click);
			// 
			// treeView1
			// 
			this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView1.Location = new System.Drawing.Point(0, 0);
			this.treeView1.Name = "treeView1";
			this.treeView1.Size = new System.Drawing.Size(218, 520);
			this.treeView1.TabIndex = 0;
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(979, 545);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.toolStrip1);
			this.Name = "MainWindow";
			this.Text = "MainWindow";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.toolStrip3.ResumeLayout(false);
			this.toolStrip3.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButtonNewProject;
		private System.Windows.Forms.ToolStripButton toolStripButtonOpenProject;
		private System.Windows.Forms.ToolStripButton toolStripButtonSaveProject;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.ToolStrip toolStrip3;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.ToolStripButton toolStripButton2;
		private OpenTK.GLControl renderCanvas;
	}
}