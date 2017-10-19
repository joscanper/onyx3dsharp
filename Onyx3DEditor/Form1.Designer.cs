namespace TriangleInOpenTkWinForms
{
    partial class Form1
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
            this.renderCanvas = new OpenTK.GLControl();
            this.SuspendLayout();
            // 
            // renderCanvas
            // 
            this.renderCanvas.BackColor = System.Drawing.Color.Black;
            this.renderCanvas.Location = new System.Drawing.Point(12, 12);
            this.renderCanvas.Name = "renderCanvas";
            this.renderCanvas.Size = new System.Drawing.Size(400, 400);
            this.renderCanvas.TabIndex = 0;
            this.renderCanvas.VSync = false;
            this.renderCanvas.Load += new System.EventHandler(this.renderCanvas_Load);
            this.renderCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.renderCanvas_Paint);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 420);
            this.Controls.Add(this.renderCanvas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Triangle";
            this.ResumeLayout(false);

        }

        #endregion

        private OpenTK.GLControl renderCanvas;
    }
}

