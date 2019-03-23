namespace Onyx3DEditor.Controls.Inspector
{
	partial class EntityRendererInspectorControl
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
			this.entityAssetField = new Onyx3DEditor.Controls.Inspector.AssetField();
			this.SuspendLayout();
			// 
			// entityAssetField
			// 
			this.entityAssetField.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.entityAssetField.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.entityAssetField.Location = new System.Drawing.Point(3, 3);
			this.entityAssetField.Name = "entityAssetField";
			this.entityAssetField.Size = new System.Drawing.Size(340, 26);
			this.entityAssetField.TabIndex = 3;
			// 
			// EntityRendererInspectorControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.AutoSize = true;
			this.Controls.Add(this.entityAssetField);
			this.Name = "EntityRendererInspectorControl";
			this.Size = new System.Drawing.Size(381, 197);
			this.Load += new System.EventHandler(this.EntityRendererInspectorControl_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private AssetField entityAssetField;
	}
}
