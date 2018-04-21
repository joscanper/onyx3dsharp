namespace Onyx3DEditor
{
    partial class Onyx3DControl
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

        private void InitializeCanvas()
        {
            // 
            // renderCanvas
            // 
            this.renderCanvas = new OpenTK.GLControl();
            
            this.renderCanvas.Name = "renderCanvas";
            this.renderCanvas.VSync = false;
            this.renderCanvas.Load += new System.EventHandler(this.renderCanvas_Load);
            this.renderCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.renderCanvas_Paint);

            this.renderCanvas.BackColor = System.Drawing.Color.Magenta;
            this.renderCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.renderCanvas.Location = new System.Drawing.Point(0, 0);
            this.renderCanvas.Size = new System.Drawing.Size(600, 520);
            this.renderCanvas.TabIndex = 0;

            this.Controls.Add(this.renderCanvas);
        }

  
        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.renderTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // renderTimer
            // 
            this.renderTimer.Interval = 16;
            this.renderTimer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // Onyx3DControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Magenta;
            this.Name = "Onyx3DControl";
            this.ResumeLayout(false);

        }

        #endregion

        private OpenTK.GLControl renderCanvas;
        private System.Windows.Forms.Timer renderTimer;
    }
}
