namespace Onyx3DEditor
{
	partial class MaterialPropertyControl
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
			this.labelPropertyName = new System.Windows.Forms.Label();
			this.panelPropertyValue = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// labelPropertyName
			// 
			this.labelPropertyName.AutoSize = true;
			this.labelPropertyName.Location = new System.Drawing.Point(4, 7);
			this.labelPropertyName.Name = "labelPropertyName";
			this.labelPropertyName.Size = new System.Drawing.Size(76, 13);
			this.labelPropertyName.TabIndex = 0;
			this.labelPropertyName.Text = "property Name";
			// 
			// panelPropertyValue
			// 
			this.panelPropertyValue.Location = new System.Drawing.Point(125, 0);
			this.panelPropertyValue.Name = "panelPropertyValue";
			this.panelPropertyValue.Size = new System.Drawing.Size(64, 27);
			this.panelPropertyValue.TabIndex = 1;
			// 
			// MaterialPropertyControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panelPropertyValue);
			this.Controls.Add(this.labelPropertyName);
			this.Name = "MaterialPropertyControl";
			this.Size = new System.Drawing.Size(194, 27);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labelPropertyName;
		private System.Windows.Forms.Panel panelPropertyValue;
	}
}
