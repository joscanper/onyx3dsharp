using System.Xml;

namespace Onyx3D
{
    public class MaterialLoader
    {

        public static Material Load(string path)
        {
            XmlReader xmlReader = XmlReader.Create(path);
            Material m = new Material();
            m.ReadXml(xmlReader);
            xmlReader.Close();
            return m;
        }

        public static void Save(Material material, string path, bool relative = true)
        {
            XmlWriter xmlWriter = XmlWriter.Create(relative ? ProjectContent.GetAbsolutePath(path) : path, ProjectContent.DefaultXMLSettings);
            material.WriteXml(xmlWriter);
            xmlWriter.Close();
        }

    }
}
