namespace Onyx3DEditor
{
	partial class NewEntityWindow
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
			this.textBoxName = new System.Windows.Forms.TextBox();
			this.labelName = new System.Windows.Forms.Label();
			this.comboBoxType = new System.Windows.Forms.ComboBox();
			this.labelType = new System.Windows.Forms.Label();
			this.buttonApply = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textBoxName
			// 
			this.textBoxName.Location = new System.Drawing.Point(53, 12);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new System.Drawing.Size(233, 20);
			this.textBoxName.TabIndex = 0;
			// 
			// labelName
			// 
			this.labelName.AutoSize = true;
			this.labelName.Location = new System.Drawing.Point(12, 15);
			this.labelName.Name = "labelName";
			this.labelName.Size = new System.Drawing.Size(35, 13);
			this.labelName.TabIndex = 1;
			this.labelName.Text = "Name";
			// 
			// comboBoxType
			// 
			this.comboBoxType.FormattingEnabled = true;
			this.comboBoxType.Location = new System.Drawing.Point(53, 38);
			this.comboBoxType.Name = "comboBoxType";
			this.comboBoxType.Size = new System.Drawing.Size(233, 21);
			this.comboBoxType.TabIndex = 2;
			// 
			// labelType
			// 
			this.labelType.AutoSize = true;
			this.labelType.Location = new System.Drawing.Point(12, 41);
			this.labelType.Name = "labelType";
			this.labelType.Size = new System.Drawing.Size(31, 13);
			this.labelType.TabIndex = 3;
			this.labelType.Text = "Type";
			// 
			// buttonApply
			// 
			this.buttonApply.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonApply.Location = new System.Drawing.Point(211, 65);
			this.buttonApply.Name = "buttonApply";
			this.buttonApply.Size = new System.Drawing.Size(75, 23);
			this.buttonApply.TabIndex = 4;
			this.buttonApply.Text = "Apply";
			this.buttonApply.UseVisualStyleBackColor = true;
			// 
			// NewEntityWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(298, 99);
			this.Controls.Add(this.buttonApply);
			this.Controls.Add(this.labelType);
			this.Controls.Add(this.comboBoxType);
			this.Controls.Add(this.labelName);
			this.Controls.Add(this.textBoxName);
			this.Name = "NewEntityWindow";
			this.Text = "Create New Entity";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBoxName;
		private System.Windows.Forms.Label labelName;
		private System.Windows.Forms.ComboBox comboBoxType;
		private System.Windows.Forms.Label labelType;
		private System.Windows.Forms.Button buttonApply;
	}
}