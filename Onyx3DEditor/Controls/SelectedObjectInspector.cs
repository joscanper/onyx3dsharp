using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Onyx3D;
using Onyx3DEditor.Controls;
using Onyx3DEditor.Controls.Inspector;
using System.Reflection;

namespace Onyx3DEditor
{
	public partial class SelectedObjectInspector : UserControl
	{
		public EventHandler InspectorChanged;

        private Dictionary<Type, Type> mInspectors = new Dictionary<Type, Type>();

        // --------------------------------------------------------------------

        public SelectedObjectInspector()
		{
			InitializeComponent();
			RegisterInspectors(this.GetType().Assembly);
		}

        // --------------------------------------------------------------------

        private void RegisterInspectors(Assembly assembly)
		{
			foreach (Type type in assembly.GetTypes())
			{
				object[] props = type.GetCustomAttributes(typeof(ComponentInspector), true);
				if (props.Length > 0)
				{
					ComponentInspector inspectorAtt = (ComponentInspector)props[0];
					mInspectors[inspectorAtt.ComponentType] = type;	
				}
			}
		}

        // --------------------------------------------------------------------

        public void Fill(SceneObject obj)
		{
			Clear();

            FillName(obj);
            FillTransformInspector(obj.Transform);

            FillObjectInspector(obj);

            FillComponentInspectors(obj);
		}

        // --------------------------------------------------------------------

        private void FillObjectInspector(SceneObject obj)
        {
            if (obj.GetType() == typeof(EntityProxy))
            {
                tableLayoutPanel.Controls.Add(CreatePropertyInspector(new EntityProxyInspector((EntityProxy)obj)));
            }
            else if (obj.GetType().IsSubclassOf(typeof(Camera)))
            {

                CameraInspectorControl camInspector = new CameraInspectorControl((Camera)obj);
                InspectorChanged += camInspector.OnObjectInspectorChanged;
                tableLayoutPanel.Controls.Add(camInspector);
            }


        }

        // --------------------------------------------------------------------

        public void Fill(Camera cam)
        {
            Clear();

            FillName(cam);
            FillTransformInspector(cam.Transform);
            FillComponentInspectors(cam);
        }

        // --------------------------------------------------------------------

        public void Fill(Scene scene)
		{

			Clear();

			// Name
			textBoxName.Visible = true;
			textBoxName.Text = "SCENE NAME HERE!";
			//tableLayoutPanel.Controls.Add(textBoxName, 0, 0);

			PropertyGrid sceneInspector = CreatePropertyInspector(new SceneInspector(scene));
			tableLayoutPanel.Controls.Add(sceneInspector);
		}

        // --------------------------------------------------------------------

        private void FillName(SceneObject obj)
        {
            // Name
            textBoxName.Visible = true;
            textBoxName.Text = obj.Id;
        }

        // --------------------------------------------------------------------

        private void FillTransformInspector(Transform transform)
        {
            PropertyGrid transformInspector = CreatePropertyInspector(new TransformInspector(transform));
            tableLayoutPanel.Controls.Add(transformInspector);
        }

        // --------------------------------------------------------------------

        private void FillComponentInspectors(SceneObject obj)
        {

            obj.ForEachComponent((c) =>
            {
                if (!mInspectors.ContainsKey(c.GetType()))
                    return;

                Type inspectorType = mInspectors[c.GetType()];
                if (inspectorType != null)
                {

                    object objectToInspect = Activator.CreateInstance(inspectorType, c);
                    InspectorControl inspectorControl = null;
                    if (objectToInspect is InspectorControl)
                        inspectorControl = (InspectorControl)Activator.CreateInstance(inspectorType, c);

                    Control control;
                    if (inspectorControl == null && objectToInspect is IPropertyInspector)
                    {
                        control = CreatePropertyInspector((IPropertyInspector)objectToInspect);
                    }
                    else
                    {
                        control = inspectorControl;
                        inspectorControl.InspectorChanged += OnObjectInspectorChanged;
                    }

                    if (control != null)
                        tableLayoutPanel.Controls.Add(control);
                }
            });

        }

        // --------------------------------------------------------------------

        private PropertyGrid CreatePropertyInspector(IPropertyInspector obj)
		{
			PropertyGrid propGrid = new PropertyGrid();
			propGrid.SelectedObject = obj;
			propGrid.PropertyValueChanged += OnObjectPropertyInspectorChanged;
			propGrid.HelpVisible = false;
			propGrid.Width = this.Width;
			propGrid.ToolbarVisible = false;
			propGrid.Height = obj.GetInspectorHeight();
			return propGrid;
		}

        // --------------------------------------------------------------------

        public void Clear()
		{
            InspectorChanged = null;

            while (tableLayoutPanel.Controls.Count > 0)
			{
				Control c = tableLayoutPanel.Controls[0];
				c.Dispose();
			}

			tableLayoutPanel.Controls.Clear();
			tableLayoutPanel.RowCount = 2;
			tableLayoutPanel.RowStyles.Clear();

			textBoxName.Visible = false;

		}

        // --------------------------------------------------------------------

        private void OnObjectInspectorChanged(object s, EventArgs e)
		{
			InspectorChanged?.Invoke(this, e);
		}

        // --------------------------------------------------------------------

        private void OnObjectPropertyInspectorChanged(object s, PropertyValueChangedEventArgs e)
		{
			PropertyGrid propGrid = s as PropertyGrid;
			IPropertyInspector inspector = propGrid.SelectedObject as IPropertyInspector;
			propGrid.Refresh();
			inspector.Apply();
			InspectorChanged?.Invoke(this, e);
		}

        // --------------------------------------------------------------------

        private void textBoxName_TextChanged(object sender, EventArgs e)
		{
			if (Selection.ActiveObject == null)
				return;

			Selection.ActiveObject.Id = textBoxName.Text;
			InspectorChanged?.Invoke(this, e);
		}
        
    }
}
