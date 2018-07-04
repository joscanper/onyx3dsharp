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
		protected static Queue<int> mAvailableBindingPoints = new Queue<int>();
		protected static int mLastGeneratedBindingPoint;

		protected int mUniformBufferObject;
		protected int mBindingPoint;
		protected string mBlockName;

		public string BlockName { get { return mBlockName; } }
		public int BindingPoint { get { return mBindingPoint; } }
	}

	public class UBO<T> : UBO, IDisposable
	{

	
		public UBO(T data, string blockName)
		{

			GL.GenBuffers(1, out mUniformBufferObject);
			mBlockName = blockName;
			mBindingPoint = GetBindingPoint();
			// Fill the buffer with data at the chosen binding point
			GL.BindBufferBase(BufferRangeTarget.UniformBuffer, mBindingPoint, mUniformBufferObject);

			int dataSize = Marshal.SizeOf(data);
			IntPtr pnt = Marshal.AllocHGlobal(dataSize);
			Marshal.StructureToPtr(data, pnt, false);

			GL.BufferData(BufferTarget.UniformBuffer, dataSize, pnt, BufferUsageHint.StaticDraw);

			Marshal.FreeHGlobal(pnt);
		}

		private int GetBindingPoint()
		{
			if (mAvailableBindingPoints.Any())
				return mAvailableBindingPoints.Dequeue();
			else
				return ++mLastGeneratedBindingPoint;
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
	
		public void Dispose()
		{
			
			mAvailableBindingPoints.Enqueue(mBindingPoint);
			GL.DeleteBuffer(mUniformBufferObject);
		}
	}
}
