
using System.Collections.Generic;

public class LevelList
{
    private List<int> list = new List<int>();
    private List<int> list2 = new List<int>();
    private List<int> list3 = new List<int>();
    private bool firstRankEasy = true;
    private bool firstRankNormal = true;
    private bool firstRankHard = true;

    public int GetEasyLevelId(bool first = false)
    {
        int level = 0;
        if (first && !(GameVO.Instance.isFirstGame && firstRankEasy))
        {
            List<int> llist = new List<int>();
            for (int i = 0; i < LevelConfig.Configs.Count; i++)
            {
                if (LevelConfig.Configs[i].pieces.Count + LevelConfig.Configs[i].pieces2.Count == 4)
                {
                    llist.Add(LevelConfig.Configs[i].id);
                }
            }
            level = llist[(int)(UnityEngine.Random.Range(0, 1.0f) * llist.Count)];
        }
        else
        {
            if(list.Count == 0)
            {
                List<int> firstLevels = new List<int>() { 1, 2, 3, 4, 5 };
                if (GameVO.Instance.isFirstGame)
                {
                    for (int i = 0; i < firstLevels.Count; i++)
                    {
                        list.Add(firstLevels[i]);
                    }
                }
                for (int i = 0; i < LevelConfig.Configs.Count; i++)
                {
                    if (GameVO.Instance.isFirstGame)
                    {
                        bool find = false;
                        for (int j = 0; j < firstLevels.Count; j++)
                        {
                            if (LevelConfig.Configs[i].id == firstLevels[j])
                            {
                                find = true;
                                break;
                            }
                        }
                        if (find)
                        {
                            continue;
                        }
                    }
                    if (LevelConfig.Configs[i].pieces.Count + LevelConfig.Configs[i].pieces2.Count >= 4 && LevelConfig.Configs[i].pieces.Count + LevelConfig.Configs[i].pieces2.Count <= 6)
                    {
                        list.Add(LevelConfig.Configs[i].id);
                    }
                }
                firstRankEasy = false;
            }
            int index = (int)(UnityEngine.Random.Range(0, 1.0f) * list.Count);
            level = list[index];
            list.RemoveAt(index);
        }
        return level;
    }


    public int GetNormalLevelId(bool first = false)
    {
        int level = 0;
        if (first && !(GameVO.Instance.isFirstGame && firstRankNormal))
        {
            List<int> llist = new List<int>();
            for (int i = 0; i < LevelConfig.Configs.Count; i++)
            {
                if (LevelConfig.Configs[i].pieces.Count + LevelConfig.Configs[i].pieces2.Count == 7)
                {
                    llist.Add(LevelConfig.Configs[i].id);
                }
            }
            level = llist[(int)(UnityEngine.Random.Range(0, 1.0f) * llist.Count)];
        }
        else
        {
            if (list2.Count == 0)
            {
                List<int> firstLevels = new List<int>() { 11, 12, 13, 14, 15 };
                if (GameVO.Instance.isFirstGame)
                {
                    for (int i = 0; i < firstLevels.Count; i++)
                    {
                        list2.Add(firstLevels[i]);
                    }
                }
                for (int i = 0; i < LevelConfig.Configs.Count; i++)
                {
                    if (GameVO.Instance.isFirstGame)
                    {
                        bool find = false;
                        for (int j = 0; j < firstLevels.Count; j++)
                        {
                            if (LevelConfig.Configs[i].id == firstLevels[j])
                            {
                                find = true;
                                break;
                            }
                        }
                        if (find)
                        {
                            continue;
                        }
                    }
                    if (LevelConfig.Configs[i].pieces.Count + LevelConfig.Configs[i].pieces2.Count >= 7 && LevelConfig.Configs[i].pieces.Count + LevelConfig.Configs[i].pieces2.Count <= 9)
                    {
                        list2.Add(LevelConfig.Configs[i].id);
                    }
                }
                firstRankNormal = false;
            }
            int index = (int)(UnityEngine.Random.Range(0, 1.0f) * list2.Count);
            level = list2[index];
            list2.RemoveAt(index);
        }
        return level;
    }

    public int GetHardLevelId(bool first = false)
    {
        int level = 0;
        if (first && !(GameVO.Instance.isFirstGame && firstRankHard))
        {
            List<int> llist = new List<int>();
            for (int i = 0; i < LevelConfig.Configs.Count; i++)
            {
                if (LevelConfig.Configs[i].pieces.Count + LevelConfig.Configs[i].pieces2.Count == 10)
                {
                    llist.Add(LevelConfig.Configs[i].id);
                }
            }
            level = llist[(int)(UnityEngine.Random.Range(0, 1.0f) * llist.Count)];
        }
        else
        {
            if (list3.Count == 0)
            {
                List<int> firstLevels = new List<int>() { 31, 32, 33, 34, 35 };
                if (GameVO.Instance.isFirstGame)
                {
                    for (int i = 0; i < firstLevels.Count; i++)
                    {
                        list2.Add(firstLevels[i]);
                    }
                }
                for (int i = 0; i < LevelConfig.Configs.Count; i++)
                {
                    if (GameVO.Instance.isFirstGame)
                    {
                        bool find = false;
                        for (int j = 0; j < firstLevels.Count; j++)
                        {
                            if (LevelConfig.Configs[i].id == firstLevels[j])
                            {
                                find = true;
                                break;
                            }
                        }
                        if (find)
                        {
                            continue;
                        }
                    }
                    if (LevelConfig.Configs[i].pieces.Count + LevelConfig.Configs[i].pieces2.Count >= 10)
                    {
                        list3.Add(LevelConfig.Configs[i].id);
                    }
                }
                firstRankHard = false;
            }
            int index = (int)(UnityEngine.Random.Range(0, 1.0f) * list3.Count);
            level = list3[index];
            list3.RemoveAt(index);
        }
        return level;
    }
}
