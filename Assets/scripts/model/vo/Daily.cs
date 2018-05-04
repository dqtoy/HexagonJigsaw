using lib;

public class Daily
{
    public Array<DailyLevelVO> levels = new Array<DailyLevelVO>();

    //每日进度
    public Int progress = new Int();

    public Int all = new Int(10);

    public bool checkAll = false;

    public int GetCurrentLevel()
    {
        for (int i = 0; i < levels.length; i++)
        {
            if (levels[i].pass == false)
            {
                return i;
            }
        }
        return -1;
    }

    public void Finish(int levelId, int time)
    {
        int count = 0;
        for(int i = 0;i < levels.length; i++)
        {
            if(levels[i].config.id == levelId)
            {
                if(levels[i].pass == false)
                {
                    levels[i].pass = true;
                    levels[i].time = time;
                }
                else if(levels[i].time > time)
                {
                    levels[i].time = time;
                }
            }
            if(levels[i].pass)
            {
                count++;
            }
        }
        progress.value = count;
    }

    public bool HasAllPass()
    {
        for (int i = 0; i < levels.length; i++)
        {
            if(levels[i].pass == false)
            {
                return false;
            }
        }
        return true;
    }

    public bool HasNextLevel(int levelId)
    {
        for (int i = 0; i < levels.length; i++)
        {
            if (levels[i].config.id == levelId)
            {
                if (i < levels.length - 1) return true;
                else return false;
            }
        }
        return false;
    }

    public int GetNextLevel(int levelId)
    {
        for (int i = 0; i < levels.length; i++)
        {
            if (levels[i].config.id == levelId)
            {
                if (i < levels.length - 1) return levels[i + 1].config.id;
                else return 0;
            }
        }
        return 0;
    }
}