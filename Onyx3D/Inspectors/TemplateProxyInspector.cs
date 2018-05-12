
using System.ComponentModel;

namespace Onyx3D
{
    public class TemplateProxyInspector : Inspector<TemplateProxy>
    {
        public TemplateProxyInspector(TemplateProxy obj) : base(obj)
        {
        }

        [Category("TemplateProxy")]
        public int Template
        {
            get { return mObject.Template != null ? mObject.Template.LinkedProjectAsset.Guid : 0; }
            set
            {
                Template m = Onyx3DEngine.Instance.Resources.GetTemplate(value);
                if (m != null)
                    mObject.Template = m;
            }
        }
        
    }
}

