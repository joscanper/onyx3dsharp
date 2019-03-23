using Onyx3D;
using System;
using System.Windows.Forms;

namespace Onyx3DEditor.Controls.Inspector
{

    public partial class CameraInspectorControl : UserControl
    {
        private FrameBuffer mFrameBuffer;
        private Camera mCamera;

        // --------------------------------------------------------------------

        public CameraInspectorControl(Camera cam)
        {
            InitializeComponent();

            mCamera = cam;
            mFrameBuffer = new FrameBuffer(pictureBox1.Width, pictureBox1.Height);
            Render();
        }

        // --------------------------------------------------------------------

        public void OnObjectInspectorChanged(object s, EventArgs e)
        {
            Render();
        }

        // --------------------------------------------------------------------

        public void Render()
        {
            mFrameBuffer.Bind();    
            Onyx3DEngine.Instance.Renderer.RenderScene(SceneManagement.ActiveScene, mCamera, mFrameBuffer.Width, mFrameBuffer.Height);
            mFrameBuffer.Unbind();
            pictureBox1.Image = mFrameBuffer.Texture.AsBitmap();
        }

        // --------------------------------------------------------------------

        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);
            mFrameBuffer.Dispose();
        }

    }
}
