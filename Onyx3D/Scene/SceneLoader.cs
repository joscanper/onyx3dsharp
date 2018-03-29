using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

using OpenTK;
using System.Reflection;

namespace Onyx3D
{
	public class SceneLoader
	{
		public static Scene Load(string path)
		{
            XmlReader xmlReader = XmlReader.Create(path);
			
			Scene scene = new Scene();
			scene.ReadXml(xmlReader);

			xmlReader.Close();
			
			return scene;
		}

		public static void Save(Scene myScene, string path)
		{

			XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
			xmlWriterSettings.NewLineOnAttributes = true;
			xmlWriterSettings.Indent = true;

			XmlWriter xmlWriter = XmlWriter.Create(path, xmlWriterSettings);

			myScene.WriteXml(xmlWriter);

			xmlWriter.Close();
		}
	}
}


public class XmlUtils
{
	public static string Vector3ToString(Vector3 v)
	{
		return  v.X + ", " + v.Y + ", " + v.Z;
	}

	public static string Vector4ToString(Vector4 v)
	{
		return v.X + ", " + v.Y + ", " + v.Z + ", " + v.W;
	}

	public static Vector2 StringToVector2(string vs)
	{
		Vector2 v = new Vector2();
		string[] values = vs.Split(',');
		v.X = (float)Convert.ToDouble(values[0]);
		v.Y = (float)Convert.ToDouble(values[1]);
		return v;
	}


	public static Vector3 StringToVector3(string vs)
	{
		Vector3 v = new Vector3();
		string[] values =vs.Split(',');
		v.X = (float)Convert.ToDouble(values[0]);
		v.Y = (float)Convert.ToDouble(values[1]);
		v.Z = (float)Convert.ToDouble(values[2]);
		return v;
	}

	public static Vector4 StringToVector4(string vs)
	{
		Vector4 v = new Vector4();
		string[] values = vs.Split(',');
		v.X = (float)Convert.ToDouble(values[0]);
		v.Y = (float)Convert.ToDouble(values[1]);
		v.Z = (float)Convert.ToDouble(values[2]);
		v.W = (float)Convert.ToDouble(values[3]);
		return v;
	}
}
