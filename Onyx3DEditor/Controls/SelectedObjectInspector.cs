using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Onyx3D;
using Onyx3DEditor.Controls;
using System.Reflection;

namespace Onyx3DEditor
{
	public partial class SelectedObjectInspector : UserControl
	{
		public EventHandler InspectorChanged;
		private Dictionary<Type, Type> mInspectors = new Dictionary<Type, Type>();
		
		public SelectedObjectInspector()
		{
			InitializeComponent();
			RegisterInspectors(this.GetType().Assembly);
		}

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

		public void Fill(SceneObject obj)
		{
			Clear();

			// Name
			textBoxName.Visible = true;
			textBoxName.Text = obj.Id;
			tableLayoutPanel.Controls.Add(textBoxName, 0, 0);

            if (obj.GetType() == typeof(TemplateProxy))
                CreatePropertyInspector(new TemplateProxyInspector((TemplateProxy)obj));

            // Transform inspector
            PropertyGrid transformInspector = CreatePropertyInspector(new TransformInspector(obj.Transform));
			tableLayoutPanel.Controls.Add(transformInspector);

			obj.ForEachComponent((c) =>
			{
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

		public void Fill(Scene scene)
		{

			Clear();

			// Name
			textBoxName.Visible = true;
			textBoxName.Text = "SCENE NAME HERE!";
			tableLayoutPanel.Controls.Add(textBoxName, 0, 0);

			PropertyGrid sceneInspector = CreatePropertyInspector(new SceneInspector(scene));
			tableLayoutPanel.Controls.Add(sceneInspector);
		}

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
		
		public void Clear()
		{
			tableLayoutPanel.Controls.Clear();
			tableLayoutPanel.RowCount = 2;
			tableLayoutPanel.RowStyles.Clear();

			textBoxName.Visible = false;

		}

		private void OnObjectInspectorChanged(object s, EventArgs e)
		{
			InspectorChanged?.Invoke(this, e);
		}

		private void OnObjectPropertyInspectorChanged(object s, PropertyValueChangedEventArgs e)
		{
			PropertyGrid propGrid = s as PropertyGrid;
			IPropertyInspector inspector = propGrid.SelectedObject as IPropertyInspector;
			propGrid.Refresh();
			inspector.Apply();
			InspectorChanged?.Invoke(this, e);
		}

		private void textBoxName_TextChanged(object sender, EventArgs e)
		{
			Selection.ActiveObject.Id = textBoxName.Text;
			InspectorChanged?.Invoke(this, e);
		}
	}
}
