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
		private Shader myShader;
        private int nVertices;

        public MaterialEditor()
        {
            InitializeComponent();
			InitializeGL();
        }

        private void renderCanvas_Load(object sender, EventArgs e)
        {
			// Load shaders from files
			myShader = new Shader("./Shaders/VertexShader.glsl", "./Shaders/FragmentShader.glsl");

			// Write the positions of vertices to a vertex shader
			nVertices = InitVertexBuffers();
            if (nVertices <= 0)
            {
                Logger.Append("Failed to write the positions of vertices to a vertex shader");
                return;
            }

            // Specify the color for clearing
            GL.ClearColor(Color.DarkSlateBlue);

            canDraw = true;
        }

        private int InitVertexBuffers()
        {
            float[] vertices = new float[] { 0f, 0.5f, -0.5f, -0.5f, 0.5f, -0.5f };

            // Create a buffer object
            int vertexBuffer;
            GL.GenBuffers(1, out vertexBuffer);

            // Bind the buffer object to target
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer);

            // Write data into the buffer object
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            // Get the storage location of a_Position
            int a_Position = GL.GetAttribLocation(myShader.Program, "a_Position");
            if (a_Position < 0)
            {
                Logger.Append("Failed to get the storage location of a_Position");
                return -1;
            }

            // Assign the buffer object to a_Position variable
            GL.VertexAttribPointer(a_Position, 2, VertexAttribPointerType.Float, false, 0, 0);

            // Enable the assignment to a_Position variable
            GL.EnableVertexAttribArray(a_Position);

            return vertices.Length / 2;
        }

        private void renderCanvas_Paint(object sender, PaintEventArgs e)
        {
			//RenderManager.Instance.Render();

			GL.Viewport(0, 0, renderCanvas.Width, renderCanvas.Height);

			// Clear the render canvas with the current color
			GL.Clear(ClearBufferMask.ColorBufferBit);

			if (canDraw)
			{
				// Draw a triangle
				GL.DrawArrays(PrimitiveType.Triangles, 0, nVertices);
			}

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
