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
			this.textBoxPath = new System.Windows.Forms.TextBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.groupBoxMaterials = new System.Windows.Forms.GroupBox();
			this.labelMaterials = new System.Windows.Forms.Label();
			this.groupBoxTextures = new System.Windows.Forms.GroupBox();
			this.labelTextures = new System.Windows.Forms.Label();
			this.groupBoxMeshes = new System.Windows.Forms.GroupBox();
			this.labelMeshes = new System.Windows.Forms.Label();
			this.labelTemplate = new System.Windows.Forms.Label();
			this.buttonImport = new System.Windows.Forms.Button();
			this.panelGL = new System.Windows.Forms.Panel();
			this.onyx3DControl = new Onyx3DEditor.Onyx3DControl();
			this.buttonOpen = new System.Windows.Forms.Button();
			this.sceneHierarchyControl1 = new Onyx3DEditor.SceneHierarchyControl();
			this.tableLayoutPanel1.SuspendLayout();
			this.groupBoxMaterials.SuspendLayout();
			this.groupBoxTextures.SuspendLayout();
			this.groupBoxMeshes.SuspendLayout();
			this.panelGL.SuspendLayout();
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
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.groupBoxMaterials, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.groupBoxTextures, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.groupBoxMeshes, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.labelTemplate, 0, 3);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 44);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 4;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 47.46544F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52.53456F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 111F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(341, 316);
			this.tableLayoutPanel1.TabIndex = 2;
			// 
			// groupBoxMaterials
			// 
			this.groupBoxMaterials.Controls.Add(this.labelMaterials);
			this.groupBoxMaterials.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBoxMaterials.Location = new System.Drawing.Point(3, 187);
			this.groupBoxMaterials.Name = "groupBoxMaterials";
			this.groupBoxMaterials.Size = new System.Drawing.Size(335, 105);
			this.groupBoxMaterials.TabIndex = 2;
			this.groupBoxMaterials.TabStop = false;
			this.groupBoxMaterials.Text = "Materials";
			// 
			// labelMaterials
			// 
			this.labelMaterials.AutoSize = true;
			this.labelMaterials.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelMaterials.Location = new System.Drawing.Point(3, 16);
			this.labelMaterials.Name = "labelMaterials";
			this.labelMaterials.Size = new System.Drawing.Size(35, 13);
			this.labelMaterials.TabIndex = 0;
			this.labelMaterials.Text = "label1";
			// 
			// groupBoxTextures
			// 
			this.groupBoxTextures.Controls.Add(this.labelTextures);
			this.groupBoxTextures.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBoxTextures.Location = new System.Drawing.Point(3, 90);
			this.groupBoxTextures.Name = "groupBoxTextures";
			this.groupBoxTextures.Size = new System.Drawing.Size(335, 91);
			this.groupBoxTextures.TabIndex = 1;
			this.groupBoxTextures.TabStop = false;
			this.groupBoxTextures.Text = "Textures";
			// 
			// labelTextures
			// 
			this.labelTextures.AutoSize = true;
			this.labelTextures.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelTextures.Location = new System.Drawing.Point(3, 16);
			this.labelTextures.Name = "labelTextures";
			this.labelTextures.Size = new System.Drawing.Size(35, 13);
			this.labelTextures.TabIndex = 0;
			this.labelTextures.Text = "label1";
			// 
			// groupBoxMeshes
			// 
			this.groupBoxMeshes.Controls.Add(this.labelMeshes);
			this.groupBoxMeshes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBoxMeshes.Location = new System.Drawing.Point(3, 3);
			this.groupBoxMeshes.Name = "groupBoxMeshes";
			this.groupBoxMeshes.Size = new System.Drawing.Size(335, 81);
			this.groupBoxMeshes.TabIndex = 0;
			this.groupBoxMeshes.TabStop = false;
			this.groupBoxMeshes.Text = "Meshes";
			// 
			// labelMeshes
			// 
			this.labelMeshes.AutoSize = true;
			this.labelMeshes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelMeshes.Location = new System.Drawing.Point(3, 16);
			this.labelMeshes.Name = "labelMeshes";
			this.labelMeshes.Size = new System.Drawing.Size(35, 13);
			this.labelMeshes.TabIndex = 0;
			this.labelMeshes.Text = "label1";
			// 
			// labelTemplate
			// 
			this.labelTemplate.AutoSize = true;
			this.labelTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelTemplate.Location = new System.Drawing.Point(3, 295);
			this.labelTemplate.Name = "labelTemplate";
			this.labelTemplate.Size = new System.Drawing.Size(335, 21);
			this.labelTemplate.TabIndex = 3;
			this.labelTemplate.Text = "label1";
			// 
			// buttonImport
			// 
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
			this.panelGL.Controls.Add(this.onyx3DControl);
			this.panelGL.Location = new System.Drawing.Point(359, 18);
			this.panelGL.Name = "panelGL";
			this.panelGL.Size = new System.Drawing.Size(411, 407);
			this.panelGL.TabIndex = 5;
			// 
			// onyx3DControl
			// 
			this.onyx3DControl.BackColor = System.Drawing.Color.Magenta;
			this.onyx3DControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.onyx3DControl.Location = new System.Drawing.Point(0, 0);
			this.onyx3DControl.Name = "onyx3DControl";
			this.onyx3DControl.Size = new System.Drawing.Size(411, 407);
			this.onyx3DControl.TabIndex = 0;
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
			// sceneHierarchyControl1
			// 
			this.sceneHierarchyControl1.Location = new System.Drawing.Point(776, 18);
			this.sceneHierarchyControl1.Name = "sceneHierarchyControl1";
			this.sceneHierarchyControl1.Size = new System.Drawing.Size(237, 407);
			this.sceneHierarchyControl1.TabIndex = 6;
			// 
			// ModelImporterWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1025, 437);
			this.Controls.Add(this.sceneHierarchyControl1);
			this.Controls.Add(this.panelGL);
			this.Controls.Add(this.buttonImport);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.textBoxPath);
			this.Controls.Add(this.buttonOpen);
			this.Name = "ModelImporterWindow";
			this.Text = "ImportModel";
			this.Shown += new System.EventHandler(this.ModelImporterWindow_Shown);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.groupBoxMaterials.ResumeLayout(false);
			this.groupBoxMaterials.PerformLayout();
			this.groupBoxTextures.ResumeLayout(false);
			this.groupBoxTextures.PerformLayout();
			this.groupBoxMeshes.ResumeLayout(false);
			this.groupBoxMeshes.PerformLayout();
			this.panelGL.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonOpen;
		private System.Windows.Forms.TextBox textBoxPath;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.GroupBox groupBoxMaterials;
		private System.Windows.Forms.GroupBox groupBoxTextures;
		private System.Windows.Forms.GroupBox groupBoxMeshes;
		private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.Label labelMaterials;
        private System.Windows.Forms.Label labelTextures;
        private System.Windows.Forms.Label labelMeshes;
        private System.Windows.Forms.Panel panelGL;
        private Onyx3DControl onyx3DControl;
        private Label labelTemplate;
		private SceneHierarchyControl sceneHierarchyControl1;
	}
}