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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPreview = new System.Windows.Forms.TabPage();
            this.renderCanvas = new OpenTK.GLControl();
            this.tabVertex = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabFragment = new System.Windows.Forms.TabPage();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPreview.SuspendLayout();
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
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(877, 572);
            this.splitContainer1.SplitterDistance = 250;
            this.splitContainer1.TabIndex = 0;
            // 
            // panel1
            // 
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
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPreview);
            this.tabControl1.Controls.Add(this.tabVertex);
            this.tabControl1.Controls.Add(this.tabFragment);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(623, 572);
            this.tabControl1.TabIndex = 0;
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
            this.tabVertex.Controls.Add(this.textBox1);
            this.tabVertex.Location = new System.Drawing.Point(4, 22);
            this.tabVertex.Name = "tabVertex";
            this.tabVertex.Padding = new System.Windows.Forms.Padding(3);
            this.tabVertex.Size = new System.Drawing.Size(615, 546);
            this.tabVertex.TabIndex = 0;
            this.tabVertex.Text = "Vertex Shader";
            this.tabVertex.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(3, 3);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(609, 540);
            this.textBox1.TabIndex = 0;
            // 
            // tabFragment
            // 
            this.tabFragment.Controls.Add(this.textBox2);
            this.tabFragment.Location = new System.Drawing.Point(4, 22);
            this.tabFragment.Name = "tabFragment";
            this.tabFragment.Padding = new System.Windows.Forms.Padding(3);
            this.tabFragment.Size = new System.Drawing.Size(615, 546);
            this.tabFragment.TabIndex = 1;
            this.tabFragment.Text = "Fragment Shader";
            this.tabFragment.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox2.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(3, 3);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(609, 540);
            this.textBox2.TabIndex = 1;
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
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPreview.ResumeLayout(false);
            this.tabPreview.PerformLayout();
            this.tabVertex.ResumeLayout(false);
            this.tabVertex.PerformLayout();
            this.tabFragment.ResumeLayout(false);
            this.tabFragment.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private OpenTK.GLControl renderCanvas;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabVertex;
		private System.Windows.Forms.TabPage tabFragment;
		private System.Windows.Forms.TabPage tabPreview;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.ToolStripComboBox toolStripMaterialsComboBox;
		private System.Windows.Forms.ToolStripButton toolStripNewMaterialButton;
        private System.Windows.Forms.TextBox textBoxLog;
    }
}

