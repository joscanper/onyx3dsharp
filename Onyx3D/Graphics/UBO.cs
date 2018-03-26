using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Onyx3D
{
	public class UBO
	{
		protected int mUniformBufferObject;
		protected int mBindingPoint;
		protected string mBlockName;

		public string BlockName { get { return mBlockName; } }
		public int BindingPoint { get { return mBindingPoint; } }

		public static int CurrentBindingPoint;
	}

	public class UBO<T> : UBO
	{
	
		public UBO(T data, string blockName)
		{
			CurrentBindingPoint++;

			GL.GenBuffers(1, out mUniformBufferObject);
			mBlockName = blockName;
			mBindingPoint = CurrentBindingPoint;
			// Fill the buffer with data at the chosen binding point
			GL.BindBufferBase(BufferRangeTarget.UniformBuffer, mBindingPoint, mUniformBufferObject);

			int dataSize = Marshal.SizeOf(data);
			IntPtr pnt = Marshal.AllocHGlobal(dataSize);
			Marshal.StructureToPtr(data, pnt, false);

			GL.BufferData(BufferTarget.UniformBuffer, dataSize, pnt, BufferUsageHint.StaticDraw);

			Marshal.FreeHGlobal(pnt);
		}
		
		public void Update(T data)
		{
			int dataSize = Marshal.SizeOf(data);
			IntPtr pnt = Marshal.AllocHGlobal(dataSize);
			Marshal.StructureToPtr(data, pnt, false);

			GL.BindBuffer(BufferTarget.UniformBuffer, mUniformBufferObject);
			GL.BufferSubData(BufferTarget.UniformBuffer, (IntPtr)0, dataSize, pnt);
			Marshal.FreeHGlobal(pnt);
			GL.UnmapBuffer(BufferTarget.UniformBuffer);
		}
		
	}
}
