using System.Xml;

namespace Onyx3D
{
    public class TemplateLoader
    {
        public static Template Load(string path)
        {
            XmlReader xmlReader = XmlReader.Create(path);
            Template template = new Template();
            template.ReadXml(xmlReader);
            xmlReader.Close();
            return template;
        }

        public static void Save(Template template, string path)
        {
            XmlWriter xmlWriter = XmlWriter.Create(path, ProjectContent.DefaultXMLSettings);
            template.WriteXml(xmlWriter);
            xmlWriter.Close();
        }
    }
}
