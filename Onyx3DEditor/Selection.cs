using System;
using System.Collections.Generic;

using Onyx3D;
using OpenTK;

public static class Selection
{
	public static Action<List<SceneObject>> OnSelectionChanged;

    // --------------------------------------------------------------------

    public static List<SceneObject> Selected { get; } = new List<SceneObject>();

    // --------------------------------------------------------------------

    public static SceneObject ActiveObject
	{
		set
		{
			Selected.Clear();
			Selected.Add(value);
			OnSelectionChanged?.Invoke(Selected);
		}
		get { return Selected != null && Selected.Count > 0 ? Selected[Selected.Count - 1] : null; }
	}

    // --------------------------------------------------------------------

    public static void Add(SceneObject obj)
	{
		Selected.Remove(obj);
		Selected.Add(obj);
		OnSelectionChanged?.Invoke(Selected);
	}

    // --------------------------------------------------------------------

    public static void Clear()
	{
		Selected.Clear();
		OnSelectionChanged?.Invoke(Selected);
	}

    // --------------------------------------------------------------------

    public static void Set(List<SceneObject> selected)
    {
        Selected.Clear();
        Selected.AddRange(selected);
        OnSelectionChanged?.Invoke(Selected);
    }

    // --------------------------------------------------------------------

    public static Vector3 MiddlePoint()
	{
		Vector3 position = Vector3.Zero;
		foreach (SceneObject obj in Selected)
		{
			position += obj.Transform.Position;
		}
		position /= Selected.Count;
		return position;
	}
    
}

