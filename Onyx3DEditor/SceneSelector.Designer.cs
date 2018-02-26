namespace Onyx3DEditor
{
	partial class SceneSelector
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
			this.tableLayoutPanelScenes = new System.Windows.Forms.TableLayoutPanel();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButtonNewScene = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonImportScene = new System.Windows.Forms.ToolStripButton();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanelScenes
			// 
			this.tableLayoutPanelScenes.ColumnCount = 4;
			this.tableLayoutPanelScenes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.tableLayoutPanelScenes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanelScenes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanelScenes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanelScenes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanelScenes.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanelScenes.Name = "tableLayoutPanelScenes";
			this.tableLayoutPanelScenes.RowCount = 2;
			this.tableLayoutPanelScenes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelScenes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanelScenes.Size = new System.Drawing.Size(483, 261);
			this.tableLayoutPanelScenes.TabIndex = 0;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Left;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonNewScene,
            this.toolStripButtonImportScene});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(24, 261);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripButtonNewScene
			// 
			this.toolStripButtonNewScene.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonNewScene.Image = global::Onyx3DEditor.Properties.Resources.if_gtk_new_20536;
			this.toolStripButtonNewScene.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonNewScene.Name = "toolStripButtonNewScene";
			this.toolStripButtonNewScene.Size = new System.Drawing.Size(21, 20);
			this.toolStripButtonNewScene.Text = "toolStripButton1";
			this.toolStripButtonNewScene.Click += new System.EventHandler(this.toolStripButtonNewScene_Click);
			// 
			// toolStripButtonImportScene
			// 
			this.toolStripButtonImportScene.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonImportScene.Image = global::Onyx3DEditor.Properties.Resources.if_folder_open_21164;
			this.toolStripButtonImportScene.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonImportScene.Name = "toolStripButtonImportScene";
			this.toolStripButtonImportScene.Size = new System.Drawing.Size(21, 20);
			this.toolStripButtonImportScene.Text = "toolStripButton2";
			this.toolStripButtonImportScene.Click += new System.EventHandler(this.toolStripButtonImportScene_Click);
			// 
			// SceneSelector
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(483, 261);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.tableLayoutPanelScenes);
			this.Name = "SceneSelector";
			this.Text = "SceneSelector";
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanelScenes;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButtonNewScene;
		private System.Windows.Forms.ToolStripButton toolStripButtonImportScene;
	}
}