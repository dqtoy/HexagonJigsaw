using System;
using lib;

public class EditorMainThread : Thread
{
    public EditorMainThread() : base(false)
    {
        Instance = this;

        AddListener("tip", OnTip);
    }

    private void OnTip(Event e)
    {
        EditorTip.Show((string)e.Data);
    }

    public static EditorMainThread Instance;

    public static int ThreadId
    {
        get { return Instance.Id; }
    }
}