using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using OpenTK.Graphics.OpenGL;

namespace Onyx3D
{
    public class Shader : GameAsset
    {
		private int mProgram;

		private string mVertexCode;
		private string mFragmentCode;

		public int Program
		{
			get { return mProgram; }
		}

		public string VertexCode
		{
			get { return mVertexCode;  }
		}

		public string FragmentCode
		{
			get { return mFragmentCode; }
		}

		public Shader(){}

		public Shader(string vFileName, string fFileName)
		{
			Load(vFileName, fFileName);
		}

		public void Load(string vFileName, string fFileName)
		{
			mVertexCode = LoadShaderCode(vFileName);
			mFragmentCode = LoadShaderCode(fFileName);
			InitProgram(mVertexCode, mFragmentCode);
		}

		
		public bool InitProgram(string vShaderSource, string fShaderSource)
		{
			mProgram = CreateProgram(vShaderSource, fShaderSource);
			if (mProgram == 0)
			{
				Logger.Instance.Append("Shader :: Failed to create program");
				return false;
			}

			//GL.UseProgram(mProgram);

			return true;
		}
		
		public string LoadShaderCode(string shaderFileName)
		{
			string shaderSource = null;

			if (!File.Exists(shaderFileName))
			{
				throw new FileNotFoundException("Unable to open \"" + shaderFileName + "\", does not exist.");
			}

			using (StreamReader sr = new StreamReader(shaderFileName))
			{
				shaderSource = sr.ReadToEnd();
			}

			return shaderSource;
		}

		private static int CreateProgram(string vShader, string fShader)
		{
			// Create shader object
			int vertexShader = LoadShader(ShaderType.VertexShader, vShader);
			int fragmentShader = LoadShader(ShaderType.FragmentShader, fShader);
			if (vertexShader == 0 || fragmentShader == 0)
			{
				return 0;
			}

			// Create a program object
			int program = GL.CreateProgram();
			if (program == 0)
			{
				return 0;
			}

			// Attach the shader objects
			GL.AttachShader(program, vertexShader);
			GL.AttachShader(program, fragmentShader);

			// Link the program object
			GL.LinkProgram(program);

			// Check the result of linking
			int status;
			GL.GetProgram(program, GetProgramParameterName.LinkStatus, out status);
			if (status == 0)
			{
				string errorString = string.Format("Shader :: Failed to link program: {0}" + Environment.NewLine, GL.GetProgramInfoLog(program));
                Logger.Instance.Append(errorString);
				GL.DeleteProgram(program);
				GL.DeleteShader(vertexShader);
				GL.DeleteShader(fragmentShader);
				return 0;
			}

			GL.DeleteShader(vertexShader);
			GL.DeleteShader(fragmentShader);


			return program;
		}

		private static int LoadShader(ShaderType shaderType, string shaderSource)
		{
			// Create shader object
			int shader = GL.CreateShader(shaderType);
			if (shader == 0)
			{
                Logger.Instance.Append("Shader :: Unable to create shader");
				return 0;
			}

			// Set the shader program
			GL.ShaderSource(shader, shaderSource);

			// Compile the shader
			GL.CompileShader(shader);

			// Check the result of compilation
			int status;
			GL.GetShader(shader, ShaderParameter.CompileStatus, out status);
			if (status == 0)
			{
				string errorString = string.Format("Shader :: Failed to compile {0} shader: {1}", shaderType.ToString(), GL.GetShaderInfoLog(shader));
                Logger.Instance.Append(errorString);
				GL.DeleteShader(shader);
				return 0;
			}

			return shader;
		}
		

		public void BindUBO<T>(UBO<T> ubo)
		{
			int idx = GL.GetUniformBlockIndex(Program, ubo.BlockName);
			GL.UniformBlockBinding(Program, idx, ubo.BindingPoint);
		}

	}
}
