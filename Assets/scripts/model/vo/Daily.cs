using lib;

public class Daily
{
    public Array<DailyLevelVO> levels = new Array<DailyLevelVO>();

    public void Finish(int levelId, int time)
    {
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
        }
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