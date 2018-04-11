using System;
using System.Windows.Forms;
using OpenTK.Graphics;

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.renderCanvas = new OpenTK.GLControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButtonNewProject = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonOpenProject = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSaveProject = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.sceneHierarchy = new Onyx3DEditor.SceneHierarchyControl();
            this.toolStripScene = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonChangeScene = new System.Windows.Forms.ToolStripButton();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.pictureBoxTest4 = new System.Windows.Forms.PictureBox();
            this.pictureBoxTest3 = new System.Windows.Forms.PictureBox();
            this.pictureBoxTest2 = new System.Windows.Forms.PictureBox();
            this.pictureBoxTest = new System.Windows.Forms.PictureBox();
            this.labelLoggerOutput = new System.Windows.Forms.Label();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonMove = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonScale = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRotate = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripCreateQuad = new System.Windows.Forms.ToolStripButton();
            this.toolStripCreateCube = new System.Windows.Forms.ToolStripButton();
            this.toolStripCreateCylinder = new System.Windows.Forms.ToolStripButton();
            this.toolStripCreateSphere = new System.Windows.Forms.ToolStripButton();
            this.toolStripCreateTeapot = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripCreateLight = new System.Windows.Forms.ToolStripButton();
            this.selectedObjectInspector = new Onyx3DEditor.SelectedObjectInspector();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStripScene.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTest4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTest3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTest2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTest)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.SuspendLayout();
            // 
            // renderCanvas
            // 
            this.renderCanvas.BackColor = System.Drawing.Color.Magenta;
            this.renderCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.renderCanvas.Location = new System.Drawing.Point(0, 0);
            this.renderCanvas.Name = "renderCanvas";
            this.renderCanvas.Size = new System.Drawing.Size(600, 520);
            this.renderCanvas.TabIndex = 0;
            this.renderCanvas.VSync = false;
            this.renderCanvas.Click += new System.EventHandler(this.renderCanvas_Click);
            this.renderCanvas.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainWindow_KeyDown);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.toolStripButtonNewProject,
            this.toolStripButtonOpenProject,
            this.toolStripButtonSaveProject});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(979, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(44, 22);
            this.toolStripLabel2.Text = "Project";
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
            this.splitContainer1.Panel1.Controls.Add(this.sceneHierarchy);
            this.splitContainer1.Panel1.Controls.Add(this.toolStripScene);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip3);
            this.splitContainer1.Size = new System.Drawing.Size(979, 520);
            this.splitContainer1.SplitterDistance = 158;
            this.splitContainer1.TabIndex = 2;
            // 
            // sceneHierarchy
            // 
            this.sceneHierarchy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sceneHierarchy.Location = new System.Drawing.Point(0, 0);
            this.sceneHierarchy.Name = "sceneHierarchy";
            this.sceneHierarchy.Size = new System.Drawing.Size(158, 495);
            this.sceneHierarchy.TabIndex = 4;
            // 
            // toolStripScene
            // 
            this.toolStripScene.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStripScene.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonChangeScene});
            this.toolStripScene.Location = new System.Drawing.Point(0, 495);
            this.toolStripScene.Name = "toolStripScene";
            this.toolStripScene.Size = new System.Drawing.Size(158, 25);
            this.toolStripScene.TabIndex = 3;
            this.toolStripScene.Text = "toolStrip2";
            // 
            // toolStripButtonChangeScene
            // 
            this.toolStripButtonChangeScene.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonChangeScene.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonChangeScene.Image")));
            this.toolStripButtonChangeScene.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonChangeScene.Name = "toolStripButtonChangeScene";
            this.toolStripButtonChangeScene.Size = new System.Drawing.Size(86, 22);
            this.toolStripButtonChangeScene.Text = "Change Scene";
            this.toolStripButtonChangeScene.Click += new System.EventHandler(this.toolStripButtonChangeScene_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(24, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.pictureBoxTest4);
            this.splitContainer2.Panel1.Controls.Add(this.pictureBoxTest3);
            this.splitContainer2.Panel1.Controls.Add(this.pictureBoxTest2);
            this.splitContainer2.Panel1.Controls.Add(this.pictureBoxTest);
            this.splitContainer2.Panel1.Controls.Add(this.labelLoggerOutput);
            this.splitContainer2.Panel1.Controls.Add(this.toolStrip2);
            this.splitContainer2.Panel1.Controls.Add(this.renderCanvas);
            this.splitContainer2.Panel1MinSize = 600;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.splitContainer2.Panel2.Controls.Add(this.selectedObjectInspector);
            this.splitContainer2.Panel2MinSize = 100;
            this.splitContainer2.Size = new System.Drawing.Size(793, 520);
            this.splitContainer2.SplitterDistance = 600;
            this.splitContainer2.SplitterWidth = 1;
            this.splitContainer2.TabIndex = 3;
            // 
            // pictureBoxTest4
            // 
            this.pictureBoxTest4.Location = new System.Drawing.Point(387, 28);
            this.pictureBoxTest4.Name = "pictureBoxTest4";
            this.pictureBoxTest4.Size = new System.Drawing.Size(128, 128);
            this.pictureBoxTest4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxTest4.TabIndex = 6;
            this.pictureBoxTest4.TabStop = false;
            // 
            // pictureBoxTest3
            // 
            this.pictureBoxTest3.Location = new System.Drawing.Point(259, 28);
            this.pictureBoxTest3.Name = "pictureBoxTest3";
            this.pictureBoxTest3.Size = new System.Drawing.Size(128, 128);
            this.pictureBoxTest3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxTest3.TabIndex = 5;
            this.pictureBoxTest3.TabStop = false;
            // 
            // pictureBoxTest2
            // 
            this.pictureBoxTest2.Location = new System.Drawing.Point(131, 28);
            this.pictureBoxTest2.Name = "pictureBoxTest2";
            this.pictureBoxTest2.Size = new System.Drawing.Size(128, 128);
            this.pictureBoxTest2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxTest2.TabIndex = 4;
            this.pictureBoxTest2.TabStop = false;
            // 
            // pictureBoxTest
            // 
            this.pictureBoxTest.Location = new System.Drawing.Point(3, 28);
            this.pictureBoxTest.Name = "pictureBoxTest";
            this.pictureBoxTest.Size = new System.Drawing.Size(128, 128);
            this.pictureBoxTest.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxTest.TabIndex = 3;
            this.pictureBoxTest.TabStop = false;
            // 
            // labelLoggerOutput
            // 
            this.labelLoggerOutput.AutoSize = true;
            this.labelLoggerOutput.BackColor = System.Drawing.Color.Black;
            this.labelLoggerOutput.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelLoggerOutput.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelLoggerOutput.ForeColor = System.Drawing.Color.Red;
            this.labelLoggerOutput.Location = new System.Drawing.Point(0, 507);
            this.labelLoggerOutput.Name = "labelLoggerOutput";
            this.labelLoggerOutput.Size = new System.Drawing.Size(57, 13);
            this.labelLoggerOutput.TabIndex = 2;
            this.labelLoggerOutput.Text = "Label Test";
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonMove,
            this.toolStripButtonScale,
            this.toolStripButtonRotate,
            this.toolStripSeparator1,
            this.toolStripCreateQuad,
            this.toolStripCreateCube,
            this.toolStripCreateCylinder,
            this.toolStripCreateSphere,
            this.toolStripCreateTeapot,
            this.toolStripSeparator2,
            this.toolStripCreateLight});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(600, 25);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripButtonMove
            // 
            this.toolStripButtonMove.Checked = true;
            this.toolStripButtonMove.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripButtonMove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonMove.Image = global::Onyx3DEditor.Properties.Resources.if_move_118639;
            this.toolStripButtonMove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonMove.Name = "toolStripButtonMove";
            this.toolStripButtonMove.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonMove.Text = "toolStripButton3";
            this.toolStripButtonMove.Click += new System.EventHandler(this.toolStripButtonMove_Click);
            // 
            // toolStripButtonScale
            // 
            this.toolStripButtonScale.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonScale.Image = global::Onyx3DEditor.Properties.Resources.if_fullscreen_118670;
            this.toolStripButtonScale.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonScale.Name = "toolStripButtonScale";
            this.toolStripButtonScale.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonScale.Text = "toolStripButton4";
            this.toolStripButtonScale.Click += new System.EventHandler(this.toolStripButtonScale_Click);
            // 
            // toolStripButtonRotate
            // 
            this.toolStripButtonRotate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRotate.Image = global::Onyx3DEditor.Properties.Resources.if_object_rotate_right_23488;
            this.toolStripButtonRotate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRotate.Name = "toolStripButtonRotate";
            this.toolStripButtonRotate.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonRotate.Text = "toolStripButton3";
            this.toolStripButtonRotate.Click += new System.EventHandler(this.toolStripButtonRotate_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripCreateQuad
            // 
            this.toolStripCreateQuad.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripCreateQuad.Image = ((System.Drawing.Image)(resources.GetObject("toolStripCreateQuad.Image")));
            this.toolStripCreateQuad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripCreateQuad.Name = "toolStripCreateQuad";
            this.toolStripCreateQuad.Size = new System.Drawing.Size(23, 22);
            this.toolStripCreateQuad.Text = "toolStripButton3";
            this.toolStripCreateQuad.Click += new System.EventHandler(this.toolStripCreateQuad_Click);
            // 
            // toolStripCreateCube
            // 
            this.toolStripCreateCube.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripCreateCube.Image = global::Onyx3DEditor.Properties.Resources.if_stock_draw_cube_21540;
            this.toolStripCreateCube.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripCreateCube.Name = "toolStripCreateCube";
            this.toolStripCreateCube.Size = new System.Drawing.Size(23, 22);
            this.toolStripCreateCube.Text = "toolStripButton3";
            this.toolStripCreateCube.ToolTipText = "Create Cube";
            this.toolStripCreateCube.Click += new System.EventHandler(this.toolStripCreateCube_Click);
            // 
            // toolStripCreateCylinder
            // 
            this.toolStripCreateCylinder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripCreateCylinder.Image = global::Onyx3DEditor.Properties.Resources.if_stock_draw_cylinder_21550;
            this.toolStripCreateCylinder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripCreateCylinder.Name = "toolStripCreateCylinder";
            this.toolStripCreateCylinder.Size = new System.Drawing.Size(23, 22);
            this.toolStripCreateCylinder.Text = "toolStripButton3";
            this.toolStripCreateCylinder.ToolTipText = "Create Cylinder";
            this.toolStripCreateCylinder.Click += new System.EventHandler(this.toolStripCreateCylinder_Click);
            // 
            // toolStripCreateSphere
            // 
            this.toolStripCreateSphere.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripCreateSphere.Image = global::Onyx3DEditor.Properties.Resources.stock_draw_sphere__1_;
            this.toolStripCreateSphere.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripCreateSphere.Name = "toolStripCreateSphere";
            this.toolStripCreateSphere.Size = new System.Drawing.Size(23, 22);
            this.toolStripCreateSphere.Text = "toolStripButton4";
            this.toolStripCreateSphere.ToolTipText = "Create Sphere";
            this.toolStripCreateSphere.Click += new System.EventHandler(this.toolStripCreateSphere_Click);
            // 
            // toolStripCreateTeapot
            // 
            this.toolStripCreateTeapot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripCreateTeapot.Image = global::Onyx3DEditor.Properties.Resources.if_teapot_93284;
            this.toolStripCreateTeapot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripCreateTeapot.Name = "toolStripCreateTeapot";
            this.toolStripCreateTeapot.Size = new System.Drawing.Size(23, 22);
            this.toolStripCreateTeapot.Text = "toolStripButton3";
            this.toolStripCreateTeapot.ToolTipText = "Create Teapot";
            this.toolStripCreateTeapot.Click += new System.EventHandler(this.toolStripCreateTeapot_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripCreateLight
            // 
            this.toolStripCreateLight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripCreateLight.Image = global::Onyx3DEditor.Properties.Resources.light_bulb;
            this.toolStripCreateLight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripCreateLight.Name = "toolStripCreateLight";
            this.toolStripCreateLight.Size = new System.Drawing.Size(23, 22);
            this.toolStripCreateLight.Text = "toolStripButton3";
            this.toolStripCreateLight.ToolTipText = "Create Light";
            this.toolStripCreateLight.Click += new System.EventHandler(this.toolStripCreateLight_Click);
            // 
            // selectedObjectInspector
            // 
            this.selectedObjectInspector.AutoScroll = true;
            this.selectedObjectInspector.AutoSize = true;
            this.selectedObjectInspector.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.selectedObjectInspector.BackColor = System.Drawing.SystemColors.Control;
            this.selectedObjectInspector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectedObjectInspector.Location = new System.Drawing.Point(0, 0);
            this.selectedObjectInspector.Name = "selectedObjectInspector";
            this.selectedObjectInspector.Size = new System.Drawing.Size(192, 520);
            this.selectedObjectInspector.TabIndex = 0;
            // 
            // toolStrip3
            // 
            this.toolStrip3.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton2,
            this.toolStripButton1});
            this.toolStrip3.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolStrip3.Location = new System.Drawing.Point(0, 0);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(24, 520);
            this.toolStrip3.TabIndex = 2;
            this.toolStrip3.Text = "toolStripContent";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::Onyx3DEditor.Properties.Resources.stock_3d_texture;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(21, 20);
            this.toolStripButton2.Text = "toolStripButtonTextures";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButtonTextures_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::Onyx3DEditor.Properties.Resources.stock_3d_texture_and_shading;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(21, 20);
            this.toolStripButton1.Text = "toolStripButtonMaterials";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButtonMaterials_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 14;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(979, 545);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "MainWindow";
            this.Text = "Onyx3DSharp";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStripScene.ResumeLayout(false);
            this.toolStripScene.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTest4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTest3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTest2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTest)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
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
		private System.Windows.Forms.ToolStripButton toolStripButton2;
		private OpenTK.GLControl renderCanvas;
		private System.Windows.Forms.ToolStrip toolStripScene;
		private ToolStripLabel toolStripLabel2;
		private ToolStripButton toolStripButtonChangeScene;
		private Timer timer1;
		private SplitContainer splitContainer2;
		private SelectedObjectInspector selectedObjectInspector;
		private ToolStrip toolStrip2;
		private ToolStripButton toolStripCreateCube;
		private ToolStripButton toolStripCreateTeapot;
		private ToolStripButton toolStripButtonMove;
		private ToolStripButton toolStripButtonScale;
		private ToolStripButton toolStripButtonRotate;
		private ToolStripSeparator toolStripSeparator1;
		private ToolStripButton toolStripCreateCylinder;
		private ToolStripButton toolStripCreateSphere;
		private SceneHierarchyControl sceneHierarchy;
		private ToolStripSeparator toolStripSeparator2;
		private ToolStripButton toolStripCreateLight;
        private ToolStripButton toolStripCreateQuad;
        private Label labelLoggerOutput;
		private PictureBox pictureBoxTest;
		private PictureBox pictureBoxTest2;
		private PictureBox pictureBoxTest3;
		private PictureBox pictureBoxTest4;
	}
}