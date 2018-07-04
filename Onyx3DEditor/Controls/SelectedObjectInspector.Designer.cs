namespace Onyx3DEditor
{
	partial class SelectedObjectInspector
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
			this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.topTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.textBoxName = new System.Windows.Forms.TextBox();
			this.topTableLayoutPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel
			// 
			this.tableLayoutPanel.AutoSize = true;
			this.tableLayoutPanel.ColumnCount = 1;
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel.Location = new System.Drawing.Point(3, 28);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 2;
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel.Size = new System.Drawing.Size(344, 69);
			this.tableLayoutPanel.TabIndex = 0;
			// 
			// topTableLayoutPanel
			// 
			this.topTableLayoutPanel.AutoSize = true;
			this.topTableLayoutPanel.ColumnCount = 1;
			this.topTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.topTableLayoutPanel.Controls.Add(this.tableLayoutPanel, 0, 1);
			this.topTableLayoutPanel.Controls.Add(this.textBoxName, 0, 0);
			this.topTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.topTableLayoutPanel.Name = "topTableLayoutPanel";
			this.topTableLayoutPanel.RowCount = 2;
			this.topTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.topTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.topTableLayoutPanel.Size = new System.Drawing.Size(350, 100);
			this.topTableLayoutPanel.TabIndex = 1;
			// 
			// textBoxName
			// 
			this.textBoxName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxName.Location = new System.Drawing.Point(3, 3);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new System.Drawing.Size(344, 20);
			this.textBoxName.TabIndex = 2;
			this.textBoxName.Visible = false;
			// 
			// SelectedObjectInspector
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.Controls.Add(this.topTableLayoutPanel);
			this.Name = "SelectedObjectInspector";
			this.Size = new System.Drawing.Size(353, 103);
			this.topTableLayoutPanel.ResumeLayout(false);
			this.topTableLayoutPanel.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
		private System.Windows.Forms.TableLayoutPanel topTableLayoutPanel;
		private System.Windows.Forms.TextBox textBoxName;
	}
}
