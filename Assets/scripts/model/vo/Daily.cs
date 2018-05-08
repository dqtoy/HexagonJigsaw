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

    public bool checkAll = false;

    private long createTime;

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
    
    public void Awake()
    {
        HttpRequest request = new HttpRequest();
        request.AddListener(lib.Event.COMPLETE, Init2);
        StartCoroutine(request.Get("http://hexfit.hundredcent.com/time"));
    }

    private void Init2(lib.Event e)
    {
        long time = Convert.ToInt64((string)e.Data);
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

    private void CreateNewLevels(long time)
    {
        createTime = time;
        //临时代码 生成每日挑战数据
        //随机10个关卡，1-999 6  1000-1999 3  2000 1
        //levels = new List<LevelConfig>();
        List<int> list = new List<int>();
        List<int> list2 = new List<int>();
        List<int> list3 = new List<int>();
        for (int i = 0; i < LevelConfig.Configs.Count; i++)
        {
            if (LevelConfig.Configs[i].id < 1000)
            {
                list.Add(i);
            }
            else if (LevelConfig.Configs[i].id < 2000)
            {
                list2.Add(i);
            }
            else
            {
                list3.Add(i);
            }
        }
        int len = 0;
        while (len < 6)
        {
            int index = (int)Math.Floor(UnityEngine.Random.Range(0, 1f) * list.Count);
            int ind = list[index];
            list.RemoveAt(index);
            DailyLevelVO levelvo = new DailyLevelVO();
            levelvo.config = LevelConfig.Configs[ind];
            levels.Add(levelvo);
            len++;
        }
        len = 0;
        while (len < 3)
        {
            int index = (int)Math.Floor(UnityEngine.Random.Range(0, 1f) * list2.Count);
            int ind = list2[index];
            list2.RemoveAt(index);
            DailyLevelVO levelvo = new DailyLevelVO();
            levelvo.config = LevelConfig.Configs[ind];
            levels.Add(levelvo);
            len++;
        }
        len = 0;
        while (len < 1)
        {
            int index = (int)Math.Floor(UnityEngine.Random.Range(0, 1f) * list3.Count);
            int ind = list3[index];
            list3.RemoveAt(index);
            DailyLevelVO levelvo = new DailyLevelVO();
            levelvo.config = LevelConfig.Configs[ind];
            levels.Add(levelvo);
            len++;
        }
        all.value = levels.length;

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
        int count = 0;
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
        List<object> list = data["levels"] as List<object>;
        for(int i = 0; i < list.Count; i++)
        {
            Dictionary<string, object> item = list[i] as Dictionary<string, object>;
            DailyLevelVO levelvo = new DailyLevelVO();
            levelvo.config = LevelConfig.GetConfig((int)item["id"]);
            levelvo.pass = (bool)item["pass"];
            levelvo.time = (int)item["time"];
            if(levelvo.pass)
            {
                count++;
            }
            levels.Add(levelvo);
        }
        all.value = levels.length;
        progress.value = count;

    }
}