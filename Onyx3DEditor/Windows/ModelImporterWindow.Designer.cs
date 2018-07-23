using System;
using System.Windows.Forms;

namespace Onyx3DEditor
{
	partial class ModelImporterWindow
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
			this.textBoxPath = new System.Windows.Forms.TextBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.labelType = new System.Windows.Forms.Label();
			this.textBoxNameId = new System.Windows.Forms.TextBox();
			this.labelName = new System.Windows.Forms.Label();
			this.comboBoxType = new System.Windows.Forms.ComboBox();
			this.buttonImport = new System.Windows.Forms.Button();
			this.panelGL = new System.Windows.Forms.Panel();
			this.onyx3DControl = new Onyx3DEditor.Onyx3DControl();
			this.buttonOpen = new System.Windows.Forms.Button();
			this.sceneHierarchyControl1 = new Onyx3DEditor.SceneHierarchyControl();
			this.tableLayoutPanel1.SuspendLayout();
			this.panelGL.SuspendLayout();
			this.SuspendLayout();
			// 
			// textBoxPath
			// 
			this.textBoxPath.Enabled = false;
			this.textBoxPath.Location = new System.Drawing.Point(47, 18);
			this.textBoxPath.Name = "textBoxPath";
			this.textBoxPath.Size = new System.Drawing.Size(303, 20);
			this.textBoxPath.TabIndex = 1;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.labelType, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.textBoxNameId, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.labelName, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.comboBoxType, 1, 1);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 44);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 4;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(341, 316);
			this.tableLayoutPanel1.TabIndex = 2;
			// 
			// labelType
			// 
			this.labelType.AutoSize = true;
			this.labelType.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelType.Location = new System.Drawing.Point(3, 25);
			this.labelType.Name = "labelType";
			this.labelType.Size = new System.Drawing.Size(74, 25);
			this.labelType.TabIndex = 2;
			this.labelType.Text = "Type";
			// 
			// textBoxNameId
			// 
			this.textBoxNameId.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxNameId.Location = new System.Drawing.Point(83, 3);
			this.textBoxNameId.Name = "textBoxNameId";
			this.textBoxNameId.Size = new System.Drawing.Size(255, 20);
			this.textBoxNameId.TabIndex = 0;
			this.textBoxNameId.TextChanged += new System.EventHandler(this.textBoxNameId_TextChanged);
			// 
			// labelName
			// 
			this.labelName.AutoSize = true;
			this.labelName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelName.Location = new System.Drawing.Point(3, 0);
			this.labelName.Name = "labelName";
			this.labelName.Size = new System.Drawing.Size(74, 25);
			this.labelName.TabIndex = 1;
			this.labelName.Text = "NameId";
			// 
			// comboBoxType
			// 
			this.comboBoxType.AllowDrop = true;
			this.comboBoxType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.comboBoxType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.comboBoxType.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxType.FormattingEnabled = true;
			this.comboBoxType.Items.AddRange(new object[] {
            "Static",
            "Item",
            "Activator"});
			this.comboBoxType.Location = new System.Drawing.Point(83, 28);
			this.comboBoxType.Name = "comboBoxType";
			this.comboBoxType.Size = new System.Drawing.Size(255, 21);
			this.comboBoxType.TabIndex = 3;
			// 
			// buttonImport
			// 
			this.buttonImport.Enabled = false;
			this.buttonImport.Location = new System.Drawing.Point(12, 397);
			this.buttonImport.Name = "buttonImport";
			this.buttonImport.Size = new System.Drawing.Size(341, 28);
			this.buttonImport.TabIndex = 3;
			this.buttonImport.Text = "Import";
			this.buttonImport.UseVisualStyleBackColor = true;
			this.buttonImport.Click += new System.EventHandler(this.buttonImport_Click);
			// 
			// panelGL
			// 
			this.panelGL.Controls.Add(this.onyx3DControl);
			this.panelGL.Location = new System.Drawing.Point(359, 18);
			this.panelGL.Name = "panelGL";
			this.panelGL.Size = new System.Drawing.Size(411, 407);
			this.panelGL.TabIndex = 5;
			// 
			// onyx3DControl
			// 
			this.onyx3DControl.BackColor = System.Drawing.Color.Magenta;
			this.onyx3DControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.onyx3DControl.Location = new System.Drawing.Point(0, 0);
			this.onyx3DControl.Name = "onyx3DControl";
			this.onyx3DControl.Size = new System.Drawing.Size(411, 407);
			this.onyx3DControl.TabIndex = 0;
			// 
			// buttonOpen
			// 
			this.buttonOpen.Image = global::Onyx3DEditor.Properties.Resources.if_folder_open_21164;
			this.buttonOpen.Location = new System.Drawing.Point(12, 12);
			this.buttonOpen.Name = "buttonOpen";
			this.buttonOpen.Size = new System.Drawing.Size(29, 26);
			this.buttonOpen.TabIndex = 0;
			this.buttonOpen.UseVisualStyleBackColor = true;
			this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
			// 
			// sceneHierarchyControl1
			// 
			this.sceneHierarchyControl1.Location = new System.Drawing.Point(776, 18);
			this.sceneHierarchyControl1.Name = "sceneHierarchyControl1";
			this.sceneHierarchyControl1.Size = new System.Drawing.Size(237, 407);
			this.sceneHierarchyControl1.TabIndex = 6;
			// 
			// ModelImporterWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1025, 437);
			this.Controls.Add(this.sceneHierarchyControl1);
			this.Controls.Add(this.panelGL);
			this.Controls.Add(this.buttonImport);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.textBoxPath);
			this.Controls.Add(this.buttonOpen);
			this.Name = "ModelImporterWindow";
			this.Text = "ImportModel";
			this.Shown += new System.EventHandler(this.ModelImporterWindow_Shown);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.panelGL.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonOpen;
		private System.Windows.Forms.TextBox textBoxPath;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.Panel panelGL;
        private Onyx3DControl onyx3DControl;
		private SceneHierarchyControl sceneHierarchyControl1;
		private TextBox textBoxNameId;
		private Label labelName;
		private Label labelType;
		private ComboBox comboBoxType;
	}
}