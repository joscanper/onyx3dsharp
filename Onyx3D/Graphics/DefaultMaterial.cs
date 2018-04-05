using System.Xml;

namespace Onyx3D
{
	public class DefaultMaterial : Material
	{ 
		public DefaultMaterial()
		{
			XmlReader xmlReader = XmlReader.Create(ProjectManager.Instance.Content.GetAsset(BuiltInMaterial.Default).Path);
			this.ReadXml(xmlReader);
		}
	}
}
