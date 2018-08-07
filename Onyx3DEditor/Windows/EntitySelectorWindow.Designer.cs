namespace Onyx3DEditor
{
	partial class EntitySelectorWindow
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
			this.tableEntities = new System.Windows.Forms.TableLayoutPanel();
			this.SuspendLayout();
			// 
			// tableEntities
			// 
			this.tableEntities.ColumnCount = 1;
			this.tableEntities.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableEntities.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableEntities.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableEntities.Location = new System.Drawing.Point(0, 0);
			this.tableEntities.Name = "tableEntities";
			this.tableEntities.RowCount = 2;
			this.tableEntities.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableEntities.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableEntities.Size = new System.Drawing.Size(328, 450);
			this.tableEntities.TabIndex = 0;
			// 
			// EntitySelectorWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(328, 450);
			this.Controls.Add(this.tableEntities);
			this.Name = "EntitySelectorWindow";
			this.Text = "EntitySelectorWindow";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableEntities;
	}
}