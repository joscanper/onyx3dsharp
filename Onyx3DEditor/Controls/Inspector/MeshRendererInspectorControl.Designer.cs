namespace Onyx3DEditor.Controls.Inspector
{
	partial class MeshRendererInspectorControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.materialPreviewPictureBox = new System.Windows.Forms.PictureBox();
			this.meshAssetField = new Onyx3DEditor.Controls.Inspector.AssetField();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.buttonEditMaterial = new System.Windows.Forms.Button();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.materialAssetField = new Onyx3DEditor.Controls.Inspector.AssetField();
			((System.ComponentModel.ISupportInitialize)(this.materialPreviewPictureBox)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// materialPreviewPictureBox
			// 
			this.materialPreviewPictureBox.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.materialPreviewPictureBox.Location = new System.Drawing.Point(6, 19);
			this.materialPreviewPictureBox.Name = "materialPreviewPictureBox";
			this.materialPreviewPictureBox.Size = new System.Drawing.Size(100, 100);
			this.materialPreviewPictureBox.TabIndex = 1;
			this.materialPreviewPictureBox.TabStop = false;
			this.materialPreviewPictureBox.Click += new System.EventHandler(this.materialPreviewPictureBox_Click);
			// 
			// meshAssetField
			// 
			this.meshAssetField.AutoSize = true;
			this.meshAssetField.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.meshAssetField.Location = new System.Drawing.Point(3, 3);
			this.meshAssetField.Name = "meshAssetField";
			this.meshAssetField.Size = new System.Drawing.Size(374, 26);
			this.meshAssetField.TabIndex = 2;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.buttonEditMaterial);
			this.groupBox1.Controls.Add(this.tableLayoutPanel1);
			this.groupBox1.Controls.Add(this.materialPreviewPictureBox);
			this.groupBox1.Location = new System.Drawing.Point(4, 68);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(363, 126);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Material Details";
			// 
			// buttonEditMaterial
			// 
			this.buttonEditMaterial.BackgroundImage = global::Onyx3DEditor.Properties.Resources.stock_3d_texture_and_shading;
			this.buttonEditMaterial.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.buttonEditMaterial.Location = new System.Drawing.Point(8, 21);
			this.buttonEditMaterial.Name = "buttonEditMaterial";
			this.buttonEditMaterial.Size = new System.Drawing.Size(23, 23);
			this.buttonEditMaterial.TabIndex = 3;
			this.buttonEditMaterial.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.32787F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.67213F));
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(113, 19);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 4;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(244, 100);
			this.tableLayoutPanel1.TabIndex = 2;
			this.tableLayoutPanel1.Visible = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Location = new System.Drawing.Point(3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(68, 25);
			this.label1.TabIndex = 0;
			this.label1.Text = "GUID";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Location = new System.Drawing.Point(3, 25);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(68, 25);
			this.label2.TabIndex = 1;
			this.label2.Text = "Shader";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label3.Location = new System.Drawing.Point(3, 50);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(68, 25);
			this.label3.TabIndex = 2;
			this.label3.Text = "label3";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.label3.Visible = false;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label4.Location = new System.Drawing.Point(3, 75);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(68, 25);
			this.label4.TabIndex = 3;
			this.label4.Text = "label4";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.label4.Visible = false;
			// 
			// materialAssetField
			// 
			this.materialAssetField.AutoSize = true;
			this.materialAssetField.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.materialAssetField.Location = new System.Drawing.Point(4, 35);
			this.materialAssetField.Name = "materialAssetField";
			this.materialAssetField.Size = new System.Drawing.Size(374, 26);
			this.materialAssetField.TabIndex = 3;
			// 
			// MeshRendererInspectorControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.materialAssetField);
			this.Controls.Add(this.meshAssetField);
			this.Name = "MeshRendererInspectorControl";
			this.Size = new System.Drawing.Size(381, 197);
			this.Load += new System.EventHandler(this.MeshRendererInspectorControl_Load);
			((System.ComponentModel.ISupportInitialize)(this.materialPreviewPictureBox)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.PictureBox materialPreviewPictureBox;
		private AssetField meshAssetField;
		private System.Windows.Forms.GroupBox groupBox1;
		private AssetField materialAssetField;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button buttonEditMaterial;
	}
}
