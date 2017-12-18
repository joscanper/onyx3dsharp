using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

using OpenTK;

namespace Onyx3D
{
	public class SceneLoader
	{
		public static Scene Load(string path)
		{
			XmlReader xmlReader = XmlReader.Create(path);
			
			Scene scene = new Scene();
			xmlReader.ReadStartElement("Root");
			scene.Root = ObjectLoader.Load(scene, xmlReader, true);

			xmlReader.Close();
			
			return scene;
		}

		public static void Save(Scene myScene, string path)
		{

			XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
			xmlWriterSettings.NewLineOnAttributes = true;
			xmlWriterSettings.Indent = true;

			XmlWriter xmlWriter = XmlWriter.Create(path, xmlWriterSettings);

			xmlWriter.WriteStartDocument();

			ObjectLoader.Save(myScene.Root, xmlWriter, true);

			xmlWriter.WriteEndDocument();
			xmlWriter.Close();
		}
	}


	public class ObjectLoader
	{ 
		public static string XMLTAG = "SceneObject";

		public static SceneObject Load(Scene scene, XmlReader xmlReader, bool isRoot = false)
		{
			string id = "";
			int instanceId = 0;
			if (!isRoot)
			{ 
				id = xmlReader.GetAttribute("id");
				instanceId = Convert.ToInt32(xmlReader.GetAttribute("instanceId"));
			}

			SceneObject obj = new SceneObject(id, scene, instanceId);
			if (xmlReader.IsEmptyElement)
				return obj;

			while (xmlReader.Read())
			{
				switch (xmlReader.NodeType)
				{
					case XmlNodeType.Element:
							if (xmlReader.Name.Equals(XMLTAG))
								Load(scene, xmlReader).Parent = obj;
						if (xmlReader.Name.Equals("Transform"))
							LoadTransform(obj, xmlReader);
							break;
					case XmlNodeType.EndElement:
						if (xmlReader.Name.Equals(XMLTAG))
							return obj;
						break;
				}

			}
			
			return obj;
		}

		private static void LoadTransform(SceneObject obj, XmlReader xmlReader)
		{
			obj.Transform.LocalPosition = XmlUtils.StringToVector3(xmlReader.GetAttribute("position"));
			Vector4 rotation = XmlUtils.StringToVector4(xmlReader.GetAttribute("rotation"));
			obj.Transform.LocalRotation = Quaternion.FromAxisAngle(rotation.Xyz, rotation.W);
			obj.Transform.LocalScale = XmlUtils.StringToVector3(xmlReader.GetAttribute("scale"));
		}

		public static void Save(SceneObject obj, XmlWriter xmlWriter, bool isRoot = false)
		{
			if (isRoot)
			{
				xmlWriter.WriteStartElement("Root");
			}
			else
			{ 
				xmlWriter.WriteStartElement("SceneObject");
				xmlWriter.WriteAttributeString("id", obj.Id);
				xmlWriter.WriteAttributeString("instanceId", obj.InstanceId.ToString());
				
				
				xmlWriter.WriteStartElement("Transform");
				xmlWriter.WriteAttributeString("position", XmlUtils.Vector3ToString(obj.Transform.LocalPosition));
				xmlWriter.WriteAttributeString("rotation", XmlUtils.Vector4ToString(obj.Transform.LocalRotation.ToAxisAngle()));
				xmlWriter.WriteAttributeString("scale", XmlUtils.Vector3ToString(obj.Transform.LocalScale));
				xmlWriter.WriteEndElement();
			}

			for (int i = 0; i < obj.ChildCount; ++i)
				Save(obj.GetChild(i), xmlWriter);

			xmlWriter.WriteEndElement();
		}
	}
	/*
	public class ComponentLoader
	{
		public static Component Load()
		{
			SceneObject cmp = new SceneObject();
		}

		public static Save(Component cmp, XmlDocument xml)
		{

		}
	}
	*/
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
