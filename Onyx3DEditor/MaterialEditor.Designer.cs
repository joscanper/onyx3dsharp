using System;
using OpenTK.Graphics;

namespace Onyx3DEditor
{
	partial class MaterialEditor
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
			//this.renderCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
			//this.renderCanvas.Location = new System.Drawing.Point(0, 0);
			this.renderCanvas.Name = "renderCanvas";
			//this.renderCanvas.Size = new System.Drawing.Size(493, 572);
			//this.renderCanvas.TabIndex = 1;
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MaterialEditor));
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.panel1 = new System.Windows.Forms.Panel();
			this.groupBoxProperties = new System.Windows.Forms.GroupBox();
			this.materialPropertiesControl = new Onyx3DEditor.MaterialPropertiesControl();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripMaterialsComboBox = new System.Windows.Forms.ToolStripComboBox();
			this.tabControlMain = new System.Windows.Forms.TabControl();
			this.tabPreview = new System.Windows.Forms.TabPage();
			this.toolStrip2 = new System.Windows.Forms.ToolStrip();
			this.textBoxLog = new System.Windows.Forms.TextBox();
			this.renderCanvas = new OpenTK.GLControl();
			this.tabVertex = new System.Windows.Forms.TabPage();
			this.textBoxVertexCode = new System.Windows.Forms.TextBox();
			this.tabFragment = new System.Windows.Forms.TabPage();
			this.textBoxFragmentCode = new System.Windows.Forms.TextBox();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripNewMaterialButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonGrid = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonCube = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonSphere = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonCylinder = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonTorus = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonTeapot = new System.Windows.Forms.ToolStripButton();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.groupBoxProperties.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.tabControlMain.SuspendLayout();
			this.tabPreview.SuspendLayout();
			this.toolStrip2.SuspendLayout();
			this.tabVertex.SuspendLayout();
			this.tabFragment.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.panel1);
			this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.tabControlMain);
			this.splitContainer1.Size = new System.Drawing.Size(877, 572);
			this.splitContainer1.SplitterDistance = 250;
			this.splitContainer1.TabIndex = 0;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.groupBoxProperties);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 25);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(250, 547);
			this.panel1.TabIndex = 4;
			// 
			// groupBoxProperties
			// 
			this.groupBoxProperties.Controls.Add(this.materialPropertiesControl);
			this.groupBoxProperties.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBoxProperties.Location = new System.Drawing.Point(0, 0);
			this.groupBoxProperties.Name = "groupBoxProperties";
			this.groupBoxProperties.Size = new System.Drawing.Size(250, 547);
			this.groupBoxProperties.TabIndex = 1;
			this.groupBoxProperties.TabStop = false;
			this.groupBoxProperties.Text = "Material Properties";
			// 
			// materialPropertiesControl
			// 
			this.materialPropertiesControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.materialPropertiesControl.Location = new System.Drawing.Point(3, 16);
			this.materialPropertiesControl.Name = "materialPropertiesControl";
			this.materialPropertiesControl.Size = new System.Drawing.Size(244, 528);
			this.materialPropertiesControl.TabIndex = 2;
			this.materialPropertiesControl.PropertyChanged += new System.EventHandler(this.materialProperties_Changed);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMaterialsComboBox,
            this.toolStripNewMaterialButton});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(250, 25);
			this.toolStrip1.TabIndex = 3;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripMaterialsComboBox
			// 
			this.toolStripMaterialsComboBox.Items.AddRange(new object[] {
            "Material1Test",
            "Material2Test"});
			this.toolStripMaterialsComboBox.Name = "toolStripMaterialsComboBox";
			this.toolStripMaterialsComboBox.Size = new System.Drawing.Size(121, 25);
			this.toolStripMaterialsComboBox.Text = "Material1Test";
			// 
			// tabControlMain
			// 
			this.tabControlMain.Controls.Add(this.tabPreview);
			this.tabControlMain.Controls.Add(this.tabVertex);
			this.tabControlMain.Controls.Add(this.tabFragment);
			this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControlMain.Location = new System.Drawing.Point(0, 0);
			this.tabControlMain.Name = "tabControlMain";
			this.tabControlMain.SelectedIndex = 0;
			this.tabControlMain.Size = new System.Drawing.Size(623, 572);
			this.tabControlMain.TabIndex = 0;
			this.tabControlMain.SelectedIndexChanged += new System.EventHandler(this.tabControlMain_SelectedIndexChanged);
			// 
			// tabPreview
			// 
			this.tabPreview.Controls.Add(this.toolStrip2);
			this.tabPreview.Controls.Add(this.textBoxLog);
			this.tabPreview.Controls.Add(this.renderCanvas);
			this.tabPreview.Location = new System.Drawing.Point(4, 22);
			this.tabPreview.Name = "tabPreview";
			this.tabPreview.Size = new System.Drawing.Size(615, 546);
			this.tabPreview.TabIndex = 2;
			this.tabPreview.Text = "Preview";
			this.tabPreview.UseVisualStyleBackColor = true;
			// 
			// toolStrip2
			// 
			this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonGrid,
            this.toolStripSeparator1,
            this.toolStripButtonCube,
            this.toolStripButtonSphere,
            this.toolStripButtonCylinder,
            this.toolStripButtonTorus,
            this.toolStripButtonTeapot});
			this.toolStrip2.Location = new System.Drawing.Point(0, 0);
			this.toolStrip2.Name = "toolStrip2";
			this.toolStrip2.Size = new System.Drawing.Size(615, 25);
			this.toolStrip2.TabIndex = 2;
			this.toolStrip2.Text = "toolStrip2";
			// 
			// textBoxLog
			// 
			this.textBoxLog.BackColor = System.Drawing.SystemColors.InfoText;
			this.textBoxLog.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.textBoxLog.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBoxLog.ForeColor = System.Drawing.Color.Red;
			this.textBoxLog.Location = new System.Drawing.Point(0, 491);
			this.textBoxLog.Multiline = true;
			this.textBoxLog.Name = "textBoxLog";
			this.textBoxLog.Size = new System.Drawing.Size(615, 55);
			this.textBoxLog.TabIndex = 1;
			// 
			// renderCanvas
			// 
			this.renderCanvas.BackColor = System.Drawing.Color.Black;
			this.renderCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
			this.renderCanvas.Location = new System.Drawing.Point(0, 0);
			this.renderCanvas.Name = "renderCanvas";
			this.renderCanvas.Size = new System.Drawing.Size(615, 546);
			this.renderCanvas.TabIndex = 0;
			this.renderCanvas.VSync = false;
			// 
			// tabVertex
			// 
			this.tabVertex.Controls.Add(this.textBoxVertexCode);
			this.tabVertex.Location = new System.Drawing.Point(4, 22);
			this.tabVertex.Name = "tabVertex";
			this.tabVertex.Padding = new System.Windows.Forms.Padding(3);
			this.tabVertex.Size = new System.Drawing.Size(615, 546);
			this.tabVertex.TabIndex = 0;
			this.tabVertex.Text = "Vertex Shader";
			this.tabVertex.UseVisualStyleBackColor = true;
			// 
			// textBoxVertexCode
			// 
			this.textBoxVertexCode.AcceptsReturn = true;
			this.textBoxVertexCode.AcceptsTab = true;
			this.textBoxVertexCode.BackColor = System.Drawing.SystemColors.InfoText;
			this.textBoxVertexCode.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxVertexCode.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBoxVertexCode.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
			this.textBoxVertexCode.Location = new System.Drawing.Point(3, 3);
			this.textBoxVertexCode.Multiline = true;
			this.textBoxVertexCode.Name = "textBoxVertexCode";
			this.textBoxVertexCode.Size = new System.Drawing.Size(609, 540);
			this.textBoxVertexCode.TabIndex = 0;
			// 
			// tabFragment
			// 
			this.tabFragment.Controls.Add(this.textBoxFragmentCode);
			this.tabFragment.Location = new System.Drawing.Point(4, 22);
			this.tabFragment.Name = "tabFragment";
			this.tabFragment.Padding = new System.Windows.Forms.Padding(3);
			this.tabFragment.Size = new System.Drawing.Size(615, 546);
			this.tabFragment.TabIndex = 1;
			this.tabFragment.Text = "Fragment Shader";
			this.tabFragment.UseVisualStyleBackColor = true;
			// 
			// textBoxFragmentCode
			// 
			this.textBoxFragmentCode.AcceptsReturn = true;
			this.textBoxFragmentCode.AcceptsTab = true;
			this.textBoxFragmentCode.BackColor = System.Drawing.SystemColors.InfoText;
			this.textBoxFragmentCode.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxFragmentCode.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBoxFragmentCode.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
			this.textBoxFragmentCode.Location = new System.Drawing.Point(3, 3);
			this.textBoxFragmentCode.Multiline = true;
			this.textBoxFragmentCode.Name = "textBoxFragmentCode";
			this.textBoxFragmentCode.Size = new System.Drawing.Size(609, 540);
			this.textBoxFragmentCode.TabIndex = 1;
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 33;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripNewMaterialButton
			// 
			this.toolStripNewMaterialButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripNewMaterialButton.Image = ((System.Drawing.Image)(resources.GetObject("toolStripNewMaterialButton.Image")));
			this.toolStripNewMaterialButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripNewMaterialButton.Name = "toolStripNewMaterialButton";
			this.toolStripNewMaterialButton.Size = new System.Drawing.Size(23, 22);
			this.toolStripNewMaterialButton.Text = "toolStripButton1";
			this.toolStripNewMaterialButton.Click += new System.EventHandler(this.toolStripNewMaterialButton_Click);
			// 
			// toolStripButtonGrid
			// 
			this.toolStripButtonGrid.CheckOnClick = true;
			this.toolStripButtonGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonGrid.Image = global::Onyx3DEditor.Properties.Resources.apps_16;
			this.toolStripButtonGrid.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonGrid.Name = "toolStripButtonGrid";
			this.toolStripButtonGrid.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonGrid.Text = "toolStripButton1";
			this.toolStripButtonGrid.Click += new System.EventHandler(this.toolStripButtonGrid_Click);
			// 
			// toolStripButtonCube
			// 
			this.toolStripButtonCube.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonCube.Image = global::Onyx3DEditor.Properties.Resources.if_stock_draw_cube_21540;
			this.toolStripButtonCube.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonCube.Name = "toolStripButtonCube";
			this.toolStripButtonCube.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonCube.Text = "toolStripButton1";
			this.toolStripButtonCube.Click += new System.EventHandler(this.toolStripButtonCube_Click);
			// 
			// toolStripButtonSphere
			// 
			this.toolStripButtonSphere.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonSphere.Image = global::Onyx3DEditor.Properties.Resources.stock_draw_sphere__1_;
			this.toolStripButtonSphere.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonSphere.Name = "toolStripButtonSphere";
			this.toolStripButtonSphere.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonSphere.Text = "toolStripButton2";
			this.toolStripButtonSphere.Click += new System.EventHandler(this.toolStripButtonSphere_Click);
			// 
			// toolStripButtonCylinder
			// 
			this.toolStripButtonCylinder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonCylinder.Image = global::Onyx3DEditor.Properties.Resources.if_stock_draw_cylinder_21550;
			this.toolStripButtonCylinder.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonCylinder.Name = "toolStripButtonCylinder";
			this.toolStripButtonCylinder.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonCylinder.Text = "toolStripButton1";
			this.toolStripButtonCylinder.Click += new System.EventHandler(this.toolStripButtonCylinder_Click);
			// 
			// toolStripButtonTorus
			// 
			this.toolStripButtonTorus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonTorus.Image = global::Onyx3DEditor.Properties.Resources.stock_draw_torus;
			this.toolStripButtonTorus.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonTorus.Name = "toolStripButtonTorus";
			this.toolStripButtonTorus.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonTorus.Text = "toolStripButton1";
			this.toolStripButtonTorus.Click += new System.EventHandler(this.toolStripButtonTorus_Click);
			// 
			// toolStripButtonTeapot
			// 
			this.toolStripButtonTeapot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonTeapot.Image = global::Onyx3DEditor.Properties.Resources.if_teapot_93284;
			this.toolStripButtonTeapot.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonTeapot.Name = "toolStripButtonTeapot";
			this.toolStripButtonTeapot.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonTeapot.Text = "toolStripButton3";
			this.toolStripButtonTeapot.Click += new System.EventHandler(this.toolStripButtonTeapot_Click);
			// 
			// MaterialEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(877, 572);
			this.Controls.Add(this.splitContainer1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "MaterialEditor";
			this.Text = "Material Editor";
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.groupBoxProperties.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.tabControlMain.ResumeLayout(false);
			this.tabPreview.ResumeLayout(false);
			this.tabPreview.PerformLayout();
			this.toolStrip2.ResumeLayout(false);
			this.toolStrip2.PerformLayout();
			this.tabVertex.ResumeLayout(false);
			this.tabVertex.PerformLayout();
			this.tabFragment.ResumeLayout(false);
			this.tabFragment.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private OpenTK.GLControl renderCanvas;
		private System.Windows.Forms.TabControl tabControlMain;
		private System.Windows.Forms.TabPage tabVertex;
		private System.Windows.Forms.TabPage tabFragment;
		private System.Windows.Forms.TabPage tabPreview;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.TextBox textBoxVertexCode;
		private System.Windows.Forms.TextBox textBoxFragmentCode;
		private System.Windows.Forms.ToolStripComboBox toolStripMaterialsComboBox;
		private System.Windows.Forms.ToolStripButton toolStripNewMaterialButton;
        private System.Windows.Forms.TextBox textBoxLog;
		private System.Windows.Forms.ToolStrip toolStrip2;
		private System.Windows.Forms.ToolStripButton toolStripButtonCube;
		private System.Windows.Forms.ToolStripButton toolStripButtonSphere;
		private System.Windows.Forms.ToolStripButton toolStripButtonTeapot;
		private System.Windows.Forms.ToolStripButton toolStripButtonTorus;
		private System.Windows.Forms.ToolStripButton toolStripButtonCylinder;
		private System.Windows.Forms.GroupBox groupBoxProperties;
		private MaterialPropertiesControl materialPropertiesControl;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.ToolStripButton toolStripButtonGrid;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
	}
}

