using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Onyx3D
{
	public class Logger : Singleton<Logger>
	{
		public const string LogFileName = "info.txt";
        private string mCurrentContent = "";

        public string Content
        {
            get { return mCurrentContent; } 
        }

		/// <summary>
		/// Write a message to a log file
		/// </summary>
		/// <param name="message">a message that will append to a log file</param>
		public void Append(string message)
		{
			File.AppendAllText(LogFileName, message + Environment.NewLine);
            mCurrentContent += message + Environment.NewLine;
        }

        public void Clear()
        {
            File.WriteAllText(LogFileName, "");
            mCurrentContent = "";
        }
	}
}
