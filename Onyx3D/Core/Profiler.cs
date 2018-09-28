using System;
using System.Collections.Generic;
using System.Timers;
using Onyx3D;

public class Profiler : Singleton<Profiler>
{

	public struct ProfilerTrace
	{
		public string Tag;
		public double Init;
		public double End;

		public double Duration { get { return End - Init; } }
	}

	private List<ProfilerTrace> mTraces = new List<ProfilerTrace>(10);
	private Stack<ProfilerTrace> mUnfinishedTraces = new Stack<ProfilerTrace>(10);
	public static ProfilerTrace LastTrace;

	// --------------------------------------------------------------------

	public void StartTrace(string tag)
	{
		LastTrace = new ProfilerTrace()
		{
			Tag = tag,
			Init = DateTime.Now.TimeOfDay.TotalMilliseconds
		};
		mUnfinishedTraces.Push(LastTrace);
	}

	// --------------------------------------------------------------------

	public void EndTrace()
	{
		LastTrace.End = DateTime.Now.TimeOfDay.TotalMilliseconds;
		mTraces.Add(LastTrace);
        
		mUnfinishedTraces.Pop();
		if (mUnfinishedTraces.Count > 0)
			LastTrace = mUnfinishedTraces.Peek();
	}

	// --------------------------------------------------------------------

	public ProfilerTrace GetTrace(string tag)
	{
		return mTraces.Find((trace) => trace.Tag == tag);
	}

	// --------------------------------------------------------------------

	public void Clear()
	{
		mTraces.Clear();
	}

}

