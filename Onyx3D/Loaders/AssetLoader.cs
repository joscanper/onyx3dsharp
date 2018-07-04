using System;
using System.Xml;
using System.Xml.Serialization;

namespace Onyx3D
{
	public class AssetLoader<T> where T : IXmlSerializable
	{

		public static T Load(string path, params object[] args)
		{
			XmlReader xmlReader = XmlReader.Create(path);
			T m = (T)Activator.CreateInstance(typeof(T), args);
			m.ReadXml(xmlReader);
			xmlReader.Close();
			return m;
		}

		public static void Save(T m, string path, bool relative = true)
		{
			XmlWriter xmlWriter = XmlWriter.Create(relative ? ProjectContent.GetAbsolutePath(path) : path, ProjectContent.DefaultXMLSettings);
			m.WriteXml(xmlWriter);
			xmlWriter.Close();
		}

	}
}
