using System;
using System.Drawing;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;

using Onyx3D;

namespace Onyx3DEditor
{
    public partial class MaterialEditor : Form
    {
        private bool canDraw = false;

        private SceneObject myObject;
		private Shader myShader;
        private MeshRenderer myRenderer;

        public MaterialEditor()
        {
            InitializeComponent();
			InitializeGL();
        }

        private void renderCanvas_Load(object sender, EventArgs e)
        {
            Logger.Instance.Clear();

            myObject = new SceneObject("BaseObject");

            // Load shaders from files
            myShader = new Shader("./Shaders/VertexShader.glsl", "./Shaders/FragmentShader.glsl");

            myRenderer = myObject.AddComponent<MeshRenderer>();
            myRenderer.Mesh = new CubeMesh();
            myRenderer.Material = new Material();
            myRenderer.Material.Shader = myShader;
            
            /*
            myRenderer.Mesh.Vertices.Add(new Vertex(new OpenTK.Vector3(0, 0.5f, -0.5f)));
            myRenderer.Mesh.Vertices.Add(new Vertex(new OpenTK.Vector3(0.5f, -0.5f, -0.5f)));
            myRenderer.Mesh.Vertices.Add(new Vertex(new OpenTK.Vector3(-0.5f, -0.5f, -0.5f)));
            myRenderer.Mesh.GenerateVAO();
            */

            textBoxLog.Text = Logger.Instance.Content;

            canDraw = true;
        }
     

        private void renderCanvas_Paint(object sender, PaintEventArgs e)
        {
            //RenderManager.Instance.Render();
            
            GL.ClearColor(Color.DarkBlue);
            GL.Viewport(0, 0, renderCanvas.Width, renderCanvas.Height);
			GL.Clear(ClearBufferMask.ColorBufferBit);
            
            myRenderer.Render();
            
			GL.Flush();
			renderCanvas.SwapBuffers();
        }

		private void toolStripNewMaterialButton_Click(object sender, EventArgs e)
		{
			toolStripMaterialsComboBox.Items.Add("New Material");
			toolStripMaterialsComboBox.SelectedIndex = toolStripMaterialsComboBox.Items.Count - 1;
		}
	}
}
