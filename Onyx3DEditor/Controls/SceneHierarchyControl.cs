using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using Onyx3D;



namespace Onyx3DEditor
{
	public class OnHierarchyEntityChange : EventArgs
	{
		public EntityProxy EntityProxy;
	}

	public partial class SceneHierarchyControl : UserControl
	{
        private Scene mScene;
		private TreeNode mPrevSelected;
		private EntityProxy mEntityProxy;
		private OnHierarchyEntityChange mEntityChangeEvent = new OnHierarchyEntityChange();

		public EventHandler<OnHierarchyEntityChange> OnEntityEditingChange;
		
		
		// --------------------------------------------------------------------

        public SceneHierarchyControl()
		{
			InitializeComponent();
			Selection.OnSelectionChanged += OnSelectionChanged;
		}

        // --------------------------------------------------------------------

        public void SetScene(Scene scene)
        {
            mScene = scene;
            UpdateScene();
        }

        // --------------------------------------------------------------------

        public void SetObject(SceneObject obj)
		{
			treeViewScene.Nodes.Clear();
			TreeNode root = new TreeNode(obj.Id);
			AddSceneObjectToTreeNode(root, obj, true);
			treeViewScene.Nodes.Add(root);
			treeViewScene.ExpandAll();
		}

        // --------------------------------------------------------------------

        public void UpdateScene()
		{
			treeViewScene.Nodes.Clear();
			TreeNode root = new TreeNode("Scene Name");
			if (mScene.Root.ChildCount > 0)
				AddSceneObjectToTreeNode(root, mScene.Root, true);
			treeViewScene.Nodes.Add(root);
			treeViewScene.ExpandAll();
		}

        // --------------------------------------------------------------------

        private void AddSceneObjectToTreeNode(TreeNode node, SceneObject sceneObject, bool skipAdd)
		{
			SceneTreeNode objectNode = new SceneTreeNode(sceneObject);
			if (!skipAdd)
				node.Nodes.Add(objectNode);
			for (int i = 0; i < sceneObject.ChildCount; ++i)
				AddSceneObjectToTreeNode(skipAdd ? node : objectNode, sceneObject.GetChild(i), false);

		}

        // --------------------------------------------------------------------

        private void OnSelectionChanged(List<SceneObject> obj)
		{
			mPrevSelected = treeViewScene.SelectedNode;

			if (treeViewScene.SelectedNode != null)
				treeViewScene.SelectedNode.BackColor = SystemColors.Window;

			if (Selection.ActiveObject == null)
			{ 
				treeViewScene.SelectedNode = null;
			}
			
			if (mScene != null && mScene.IsDirty)
				UpdateScene();

            if (treeViewScene.Nodes.Count > 0)
			    SearchAndHighlightObject(treeViewScene.Nodes[0]);

		}

        // --------------------------------------------------------------------

        private bool SearchAndHighlightObject(TreeNode tn)
		{
		
			foreach (SceneTreeNode node in tn.Nodes)
			{
				if (node.GetType() != typeof(SceneTreeNode))
					continue;

				if (node.BoundSceneObject == Selection.ActiveObject)
				{
					node.BackColor = Color.Orange;
					treeViewScene.SelectedNode = node;
				}
				else if(Selection.Selected.Contains(node.BoundSceneObject))
				{
					node.BackColor = Color.Gray;
				}
				else
				{
					node.BackColor = Color.Transparent;

				}

				SearchAndHighlightObject(node);
			}

			return false;

		}

        // --------------------------------------------------------------------

        private void treeViewScene_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			if (e.Node.GetType() != typeof(SceneTreeNode))
			{
				Selection.Clear();
				return;
			}
			
			SceneTreeNode sceneTreeNode = (SceneTreeNode)e.Node;
			if (sceneTreeNode != null)
			{
				mPrevSelected = sceneTreeNode;

				if (ModifierKeys.HasFlag(Keys.Control))
				{
					Selection.Add(sceneTreeNode.BoundSceneObject);
				}
				else
				{
					Selection.ActiveObject = sceneTreeNode.BoundSceneObject;
				}
			}

		}

        // --------------------------------------------------------------------

        protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);

			Selection.OnSelectionChanged -= OnSelectionChanged;
		}

        // --------------------------------------------------------------------

        private void treeViewScene_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			if (e.Node.GetType() != typeof(SceneTreeNode) && mEntityProxy != null)
			{
				ExitEntity();
				NotifyChange();
				return;
			}
			else
			{
				SceneTreeNode node = (SceneTreeNode)e.Node;
				EntityProxy proxy = node.BoundSceneObject as EntityProxy;
				if (proxy != null)
				{
					EnterEntity(proxy);
					NotifyChange();
				}
			}
			
		}

		// --------------------------------------------------------------------

		public void ExitEntity()
		{
			AssetLoader<Entity>.Save(mEntityProxy.EntityRef, mEntityProxy.EntityRef.LinkedProjectAsset.Path);
			mEntityProxy = null;
			SetScene(mScene);			
		}

		// --------------------------------------------------------------------

		public void EnterEntity(EntityProxy proxy)
		{
			mEntityProxy = proxy;
			SetObject(mEntityProxy.EntityRef.Root);
		}

		// --------------------------------------------------------------------

		private void NotifyChange()
		{
			mEntityChangeEvent.EntityProxy = mEntityProxy;
			OnEntityEditingChange(this, mEntityChangeEvent);
		}
	}
}
