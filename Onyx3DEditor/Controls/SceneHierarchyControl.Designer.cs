namespace Onyx3DEditor
{
	partial class SceneHierarchyControl
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
			this.treeViewScene = new System.Windows.Forms.TreeView();
			this.SuspendLayout();
			// 
			// treeViewScene
			// 
			this.treeViewScene.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeViewScene.Location = new System.Drawing.Point(0, 0);
			this.treeViewScene.Name = "treeViewScene";
			this.treeViewScene.Size = new System.Drawing.Size(150, 150);
			this.treeViewScene.TabIndex = 0;
			this.treeViewScene.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewScene_NodeMouseClick);
			this.treeViewScene.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewScene_NodeMouseDoubleClick);
			// 
			// SceneHierarchyControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.treeViewScene);
			this.Name = "SceneHierarchyControl";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TreeView treeViewScene;
	}
}
