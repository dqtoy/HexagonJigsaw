using lib;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Daily : MonoBehaviour
{
    public Array<DailyLevelVO> levels = new Array<DailyLevelVO>();

    //每日进度
    public Int progress = new Int();

    public Int all = new Int(10);

    public Int allTime = new Int();
    public lib.String allTimeString = new lib.String();

    public bool checkAll = false;

    private bool isFirstPassAll = true;

    public bool firstPassAll = false;

    private long createTime;

    public Int currentLevelIndex = new Int();

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
        int passTime = 0;
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
                    //战绩不覆盖
                    //levels[i].time = time;
                }
            }
            if(levels[i].pass)
            {
                count++;
                passTime += levels[i].time;
            }
        }
        if(isFirstPassAll && HasAllPass())
        {
            isFirstPassAll = false;
            firstPassAll = true;

            GameVO.Instance.rank.FinishDaily((int)(passTime / 1000.0f));
            GameVO.Instance.achievement.FinishDaily((int)(passTime / 1000.0f));
        }
        allTime.value = passTime;
        allTimeString.value = StringUtils.TimeToMS(allTime.value);
        progress.value = count;
        Save();
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

    public int GetLevelIndex(int levelId)
    {
        for (int i = 0; i < levels.length; i++)
        {
            if (levels[i].config.id == levelId)
            {
                return i;
            }
        }
        return 0;
    }

    public void Awake()
    {
        DateTime d = DateTime.Now;
        DateTime d2 = new DateTime(1970, 1, 1);
        double t = d.Subtract(d2).TotalMilliseconds;
        Init2((long)t);
        //HttpRequest request = new HttpRequest();
        //request.AddListener(lib.Event.COMPLETE, Init2);
        //StartCoroutine(request.Get("http://hexfit.hundredcent.com/time"));
    }

    private void Init2(long time)
    {
        //long time = Convert.ToInt64((string)e.Data);
        //读取缓存
        if (PlayerPrefs.HasKey("daily"))
        {
            ReadLevels(time);
        }
        else
        {
            CreateNewLevels(time);
        }
    }

    private bool firstGame = true;

    private void CreateNewLevels(long time)
    {
        isFirstPassAll = true;
        firstPassAll = false;
        createTime = time;
        //临时代码 生成每日挑战数据
        //随机10个关卡，1-999 6  1000-1999 3  2000 1
        //levels = new List<LevelConfig>();
        List<int> list = new List<int>();
        List<int> list2 = new List<int>();
        List<int> list3 = new List<int>();
        List<int> firstLevels = new List<int>() {};
        if(GameVO.Instance.isFirstGame && firstGame)
        {
            firstGame = false;
            firstLevels.Add(1);
            firstLevels.Add(2);
            firstLevels.Add(3);
            firstLevels.Add(4);
            firstLevels.Add(5);
            for(int i = 0; i < firstLevels.Count; i++)
            {
                DailyLevelVO levelvo = new DailyLevelVO();
                levelvo.config = LevelConfig.GetConfig(firstLevels[i]);
                levels.Add(levelvo);
            }
        }
        for (int i = 0; i < LevelConfig.Configs.Count; i++)
        {
            bool find = false;
            for(int j = 0; j < firstLevels.Count; j++)
            {
                if(firstLevels[j] == LevelConfig.Configs[i].id)
                {
                    find = true;
                    break;
                }
            }
            if (LevelConfig.Configs[i].pieces.Count + LevelConfig.Configs[i].pieces2.Count >= 4 && LevelConfig.Configs[i].pieces.Count + LevelConfig.Configs[i].pieces2.Count <= 6)
            {
                list.Add(i);
            }
            else if (LevelConfig.Configs[i].pieces.Count + LevelConfig.Configs[i].pieces2.Count >= 7 && LevelConfig.Configs[i].pieces.Count + LevelConfig.Configs[i].pieces2.Count <= 8)
            {
                list2.Add(i);
            }
            else if(LevelConfig.Configs[i].pieces.Count + LevelConfig.Configs[i].pieces2.Count >= 10)
            {
                list3.Add(i);
            }
        }
        while (levels.length < 6)
        {
            int index = (int)Math.Floor(UnityEngine.Random.Range(0, 1f) * list.Count);
            int ind = list[index];
            list.RemoveAt(index);
            DailyLevelVO levelvo = new DailyLevelVO();
            levelvo.config = LevelConfig.Configs[ind];
            levels.Add(levelvo);
        }
        while (levels.length < 9)
        {
            int index = (int)Math.Floor(UnityEngine.Random.Range(0, 1f) * list2.Count);
            int ind = list2[index];
            list2.RemoveAt(index);
            DailyLevelVO levelvo = new DailyLevelVO();
            levelvo.config = LevelConfig.Configs[ind];
            levels.Add(levelvo);
        }
        while (levels.length < 10)
        {
            int index = (int)Math.Floor(UnityEngine.Random.Range(0, 1f) * list3.Count);
            int ind = list3[index];
            list3.RemoveAt(index);
            DailyLevelVO levelvo = new DailyLevelVO();
            levelvo.config = LevelConfig.Configs[ind];
            levels.Add(levelvo);
        }
        all.value = levels.length;
        allTime.value = 0;

        //存储
        Save();
    }

    private void Save()
    {
        string content = "{";
        content += "\"create\":\"" + createTime + "\",";
        content += "\"levels\":[";
        for (int i = 0; i < levels.length; i++)
        {
            content += "{";
            content += "\"id\":" + levels[i].config.id + ",";
            content += "\"pass\":" + (levels[i].pass?"true":"false") + ",";
            content += "\"time\":" + levels[i].time;
            content += "}" + (i < levels.length - 1 ? "," : "");
        }
        content += "]}";
        PlayerPrefs.SetString("daily", content);
    }

    public void ReadLevels(long time)
    {
        string content = PlayerPrefs.GetString("daily");
        //Debug.Log(content);
        Dictionary<string, object> data;
        try
        {
            data = JSON.Parse(content) as Dictionary<string, object>;
        }
        catch
        {
            CreateNewLevels(time);
            return;
        }
        createTime = Convert.ToInt64(data["create"]);
        if (time - createTime > 2 * 3600 * 1000)
        {
            CreateNewLevels(time);
            return;
        }
        int count = 0;
        List<object> list = data["levels"] as List<object>;
        for (int i = 0; i < list.Count; i++)
        {
            Dictionary<string, object> item = list[i] as Dictionary<string, object>;
            DailyLevelVO levelvo = new DailyLevelVO();
            levelvo.config = LevelConfig.GetConfig((int)item["id"]);
            levelvo.pass = (bool)item["pass"];
            levelvo.time = (int)item["time"];
            if (levelvo.pass)
            {
                count++;
                allTime.value += levelvo.time;
                allTimeString.value = StringUtils.TimeToMS(allTime.value);
            }
            levels.Add(levelvo);
        }
        all.value = levels.length;
        progress.value = count;

        isFirstPassAll = true;
        firstPassAll = false;

        if (HasAllPass())
        {
            isFirstPassAll = false;
        }
    }
}