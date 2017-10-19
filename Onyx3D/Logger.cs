using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Onyx3D
{
	public class Logger
	{
		public static string logFileName = "info.txt";

		/// <summary>
		/// Write a message to a log file
		/// </summary>
		/// <param name="message">a message that will append to a log file</param>
		public static void Append(string message)
		{
			File.AppendAllText(logFileName, message + Environment.NewLine);
		}
	}
}
