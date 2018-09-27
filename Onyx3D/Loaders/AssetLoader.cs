using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Onyx3D
{
	public class AssetLoader<T> where T : IXmlSerializable
	{

		public static T Load(string path, bool relative, params object[] args)
		{

            path = relative ? ProjectContent.GetAbsolutePath(path) : path;

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Unable to open \"" + path + "\", file does not exist.");
            }
            
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

    // --------------------------------------------------------------------

    public class AssetStreamLoader<T>
    {

        public static T Load(string path, bool relative)
        {
            path = relative ? ProjectContent.GetAbsolutePath(path) : path;

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Unable to open \"" + path + "\", file does not exist.");
            }

            T mData;
            
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            StreamReader stream = new StreamReader(path);
            mData = (T)xmlSerializer.Deserialize(stream);

            stream.Close();

            return mData;
        }

        public static void Save(T data, string path, bool relative = true)
        {
            StreamWriter stream = null;
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                path = relative ? ProjectContent.GetAbsolutePath(path) : path;
                stream = new StreamWriter(path, false);
                xmlSerializer.Serialize(stream, data);
            }
            finally
            {
                if (null != stream)
                    stream.Close();
            }
        }


    }


}
