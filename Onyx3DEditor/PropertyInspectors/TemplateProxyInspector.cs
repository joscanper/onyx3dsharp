
using Onyx3D;
using System.ComponentModel;

namespace Onyx3DEditor
{

	public class TemplateProxyInspector : PropertyInspector<TemplateProxy>
    {
        public TemplateProxyInspector(TemplateProxy obj) : base(obj)
        {
        }

        [Category("TemplateProxy")]
        public string Template
        {
            get { return mObject.Template != null ? mObject.Template.LinkedProjectAsset.Name : "[None]"; }
            set
            {
				foreach(OnyxProjectAsset t in ProjectManager.Instance.Content.Templates)
					if (t.Name == value)
						mObject.Template = Onyx3DEngine.Instance.Resources.GetTemplate(t.Guid);
            }
        }
        
    }
}

