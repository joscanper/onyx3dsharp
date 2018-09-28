
using Onyx3D;
using System.ComponentModel;

namespace Onyx3DEditor
{

	public class EntityProxyInspector : PropertyInspector<EntityProxy>
    {
        public EntityProxyInspector(EntityProxy obj) : base(obj)
        {
        }

        [Category("EntityProxy")]
        public string Entity
        {
            get { return mObject.EntityRef != null ? mObject.EntityRef.LinkedProjectAsset.Name : "[None]"; }
            set
            {
                OnyxProjectAsset t = ProjectManager.Instance.Content.GetEntityByName(value);
                if (t != null)
                    mObject.EntityRef = Onyx3DEngine.Instance.Resources.GetEntity(t.Guid);
            }
        }
        
    }
}

