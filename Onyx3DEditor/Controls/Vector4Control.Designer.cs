namespace Onyx3DEditor
{
	partial class Vector4Control
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.textBoxX = new System.Windows.Forms.TextBox();
			this.textBoxY = new System.Windows.Forms.TextBox();
			this.textBoxZ = new System.Windows.Forms.TextBox();
			this.textBoxW = new System.Windows.Forms.TextBox();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 4;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel1.Controls.Add(this.textBoxX, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.textBoxY, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.textBoxZ, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.textBoxW, 3, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(187, 27);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// textBoxX
			// 
			this.textBoxX.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxX.Location = new System.Drawing.Point(3, 3);
			this.textBoxX.Name = "textBoxX";
			this.textBoxX.Size = new System.Drawing.Size(40, 20);
			this.textBoxX.TabIndex = 1;
			// 
			// textBoxY
			// 
			this.textBoxY.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxY.Location = new System.Drawing.Point(49, 3);
			this.textBoxY.Name = "textBoxY";
			this.textBoxY.Size = new System.Drawing.Size(40, 20);
			this.textBoxY.TabIndex = 3;
			// 
			// textBoxZ
			// 
			this.textBoxZ.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxZ.Location = new System.Drawing.Point(95, 3);
			this.textBoxZ.Name = "textBoxZ";
			this.textBoxZ.Size = new System.Drawing.Size(40, 20);
			this.textBoxZ.TabIndex = 6;
			// 
			// textBoxW
			// 
			this.textBoxW.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxW.Location = new System.Drawing.Point(141, 3);
			this.textBoxW.Name = "textBoxW";
			this.textBoxW.Size = new System.Drawing.Size(43, 20);
			this.textBoxW.TabIndex = 8;
			// 
			// Vector4Control
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "Vector4Control";
			this.Size = new System.Drawing.Size(187, 27);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TextBox textBoxX;
		private System.Windows.Forms.TextBox textBoxY;
		private System.Windows.Forms.TextBox textBoxZ;
		private System.Windows.Forms.TextBox textBoxW;
	}
}
