
public interface IInspector
{
	void Apply();
}

public class Inspector<T> : IInspector
{
	protected T mObject;
	
	public T Object
	{
		get { return mObject; }
	}

	public Inspector(T obj)
	{
		mObject = obj;
	}

	public virtual void Apply() {}
}

