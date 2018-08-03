namespace Onyx3DEditor.Controls.Inspector
{
	partial class AssetField
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
            this.assetNameLabel = new System.Windows.Forms.Label();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.assetRefField = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Controls.Add(this.assetNameLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonSearch, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.assetRefField, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(297, 26);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // assetNameLabel
            // 
            this.assetNameLabel.AutoSize = true;
            this.assetNameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.assetNameLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.assetNameLabel.Location = new System.Drawing.Point(3, 0);
            this.assetNameLabel.Name = "assetNameLabel";
            this.assetNameLabel.Size = new System.Drawing.Size(74, 26);
            this.assetNameLabel.TabIndex = 2;
            this.assetNameLabel.Text = "Field Text ";
            this.assetNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonSearch
            // 
            this.buttonSearch.BackColor = System.Drawing.SystemColors.Control;
            this.buttonSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonSearch.FlatAppearance.BorderSize = 0;
            this.buttonSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSearch.Image = global::Onyx3DEditor.Properties.Resources.toggle_plus;
            this.buttonSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSearch.Location = new System.Drawing.Point(270, 3);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(24, 20);
            this.buttonSearch.TabIndex = 1;
            this.buttonSearch.UseVisualStyleBackColor = false;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // assetRefField
            // 
            this.assetRefField.Location = new System.Drawing.Point(83, 3);
            this.assetRefField.Name = "assetRefField";
            this.assetRefField.ReadOnly = true;
            this.assetRefField.Size = new System.Drawing.Size(181, 20);
            this.assetRefField.TabIndex = 0;
            // 
            // AssetField
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "AssetField";
            this.Size = new System.Drawing.Size(300, 29);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TextBox assetRefField;
		private System.Windows.Forms.Button buttonSearch;
		private System.Windows.Forms.Label assetNameLabel;
	}
}
