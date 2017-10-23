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


		private void InitializeGL()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MaterialEditor));
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.panel1 = new System.Windows.Forms.Panel();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripMaterialsComboBox = new System.Windows.Forms.ToolStripComboBox();
			this.toolStripNewMaterialButton = new System.Windows.Forms.ToolStripButton();
			this.tabControlMain = new System.Windows.Forms.TabControl();
			this.tabPreview = new System.Windows.Forms.TabPage();
			this.textBoxLog = new System.Windows.Forms.TextBox();
			this.renderCanvas = new OpenTK.GLControl();
			this.tabVertex = new System.Windows.Forms.TabPage();
			this.textBoxVertexCode = new System.Windows.Forms.TextBox();
			this.tabFragment = new System.Windows.Forms.TabPage();
			this.textBoxFragmentCode = new System.Windows.Forms.TextBox();
			this.trackBarRotation = new System.Windows.Forms.TrackBar();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.tabControlMain.SuspendLayout();
			this.tabPreview.SuspendLayout();
			this.tabVertex.SuspendLayout();
			this.tabFragment.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBarRotation)).BeginInit();
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
			this.panel1.Controls.Add(this.trackBarRotation);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 25);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(250, 547);
			this.panel1.TabIndex = 4;
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
			this.tabPreview.Controls.Add(this.textBoxLog);
			this.tabPreview.Controls.Add(this.renderCanvas);
			this.tabPreview.Location = new System.Drawing.Point(4, 22);
			this.tabPreview.Name = "tabPreview";
			this.tabPreview.Size = new System.Drawing.Size(615, 546);
			this.tabPreview.TabIndex = 2;
			this.tabPreview.Text = "Preview";
			this.tabPreview.UseVisualStyleBackColor = true;
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
			this.textBoxVertexCode.ForeColor = System.Drawing.SystemColors.InactiveCaption;
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
			this.textBoxFragmentCode.ForeColor = System.Drawing.SystemColors.InactiveCaption;
			this.textBoxFragmentCode.Location = new System.Drawing.Point(3, 3);
			this.textBoxFragmentCode.Multiline = true;
			this.textBoxFragmentCode.Name = "textBoxFragmentCode";
			this.textBoxFragmentCode.Size = new System.Drawing.Size(609, 540);
			this.textBoxFragmentCode.TabIndex = 1;
			// 
			// trackBarRotation
			// 
			this.trackBarRotation.Location = new System.Drawing.Point(3, 3);
			this.trackBarRotation.Maximum = 360;
			this.trackBarRotation.Name = "trackBarRotation";
			this.trackBarRotation.Size = new System.Drawing.Size(244, 45);
			this.trackBarRotation.TabIndex = 0;
			this.trackBarRotation.ValueChanged += new System.EventHandler(this.trackBarRotation_ValueChanged);
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
			this.panel1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.tabControlMain.ResumeLayout(false);
			this.tabPreview.ResumeLayout(false);
			this.tabPreview.PerformLayout();
			this.tabVertex.ResumeLayout(false);
			this.tabVertex.PerformLayout();
			this.tabFragment.ResumeLayout(false);
			this.tabFragment.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBarRotation)).EndInit();
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
		private System.Windows.Forms.TrackBar trackBarRotation;
	}
}

