using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using hexjig;
using lib;
using System;

public class UIStart : MonoBehaviour {

    public Text levelTxt;

    private List<LevelConfig> levels;

	// Use this for initialization
	void Start () {
        if(GameVO.Instance.editor == false)
        {
            gameObject.SetActive(true);

            //监听是否开启一个新的关卡
            MainData.Instance.dispatcher.AddListener(hexjig.EventType.START_LEVEL, OnStartLevel);

            //监听关卡是否完成
            MainData.Instance.dispatcher.AddListener(hexjig.EventType.FINISH_LEVEL, OnFinishLevel);

            //随机10个关卡，1-999 6  1000-1999 3  2000 1
            levels = new List<LevelConfig>();
            List<int> list = new List<int>();
            List<int> list2 = new List<int>();
            List<int> list3 = new List<int>();
            for (int i = 0; i < LevelConfig.Configs.Count; i++)
            {
                if(LevelConfig.Configs[i].id < 1000)
                {
                    list.Add(i);
                }
                else if(LevelConfig.Configs[i].id < 2000)
                {
                    list2.Add(i);
                }
                else
                {
                    list3.Add(i);
                }
            }
            int len = 0;
            while(len < 6)
            {
                int index = (int)Math.Floor(UnityEngine.Random.Range(0, 1f) * list.Count);
                int ind = list[index];
                list.RemoveAt(index);
                levels.Add(LevelConfig.Configs[ind]);
                len++;
            }
            len = 0;
            while (len < 3)
            {
                int index = (int)Math.Floor(UnityEngine.Random.Range(0, 1f) * list2.Count);
                int ind = list2[index];
                list2.RemoveAt(index);
                levels.Add(LevelConfig.Configs[ind]);
                len++;
            }
            len = 0;
            while (len < 1)
            {
                int index = (int)Math.Floor(UnityEngine.Random.Range(0, 1f) * list3.Count);
                int ind = list3[index];
                list3.RemoveAt(index);
                levels.Add(LevelConfig.Configs[ind]);
                len++;
            }
            StartNextLevel();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void StartNextLevel()
    {
        int index = (int)Math.Floor(UnityEngine.Random.Range(0, 1f) * levels.Count);
        LevelConfig config = levels[index];
        levels.RemoveAt(index);
        //开启一个关卡
        new StartGameCommand(config.id);
    }

    /// <summary>
    /// 一个新的关卡开始
    /// </summary>
    /// <param name="e"></param>
    private void OnStartLevel(lib.Event e)
    {
        levelTxt.text = "当前关卡:" + MainData.Instance.levelId.value;
    }

    /// <summary>
    /// 完成关卡
    /// </summary>
    /// <param name="e"></param>
    private void OnFinishLevel(lib.Event e)
    {
        //删除之前的游戏
        new DisposeGameCommand();

        //完成关卡后跳到下一个关卡，如果没有，则不用管
        StartNextLevel();
    }

    // Update is called once per frame
    void Update ()
    {

    }
}
