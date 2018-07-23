
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
				foreach(OnyxProjectAsset t in ProjectManager.Instance.Content.Entities)
					if (t.Name == value)
						mObject.EntityRef = Onyx3DEngine.Instance.Resources.GetEntity(t.Guid);
            }
        }
        
    }
}

