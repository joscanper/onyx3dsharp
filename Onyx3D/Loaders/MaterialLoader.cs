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

        public static void Save(Material material, string path)
        {
            XmlWriter xmlWriter = XmlWriter.Create(ProjectContent.GetAbsolutePath(path), ProjectContent.DefaultXMLSettings);
            material.WriteXml(xmlWriter);
            xmlWriter.Close();
        }

    }
}
