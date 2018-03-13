using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Onyx3D;

public static class Selection
{
	public static Action<List<SceneObject>> OnSelectionChanged;

	private static List<SceneObject> mSelected = new List<SceneObject>();

	public static List<SceneObject> Selected
	{
		get { return mSelected; }
		set
		{
			mSelected = value;
			if (OnSelectionChanged != null)
				OnSelectionChanged(Selected);
		}
	}

	public static SceneObject ActiveObject
	{
		set
		{
			Selected = new List<SceneObject>() { value };
			if (OnSelectionChanged != null)
				OnSelectionChanged(Selected);
		}
		get { return Selected.Count > 0 ? Selected[0] : null; }
	}

}

