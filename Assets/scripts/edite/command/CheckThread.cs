using System;
using lib;

public class CheckThread : Thread {

    public CheckThread() : base(false)
    {
        Instance = this;

        AddListener("check", OnCheck);
    }

    private void OnCheck(Event e)
    {
        new GameCheck(e.Data as LevelConfig);
    }

    public static CheckThread Instance;

    public static int ThreadId
    {
        get { return Instance.Id; }
    }
}
