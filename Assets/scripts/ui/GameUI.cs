using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using hexjig;
using lib;
using System;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

    public Text timeTxt;
    public Text modelTxt;

    //临时测试
    public Text levelTxt;

    private void Awake()
    {
        ButtonClick.dispatcher.AddListener("quitGame", OnQuit);
        ButtonClick.dispatcher.AddListener("restart", OnRestart);
        ButtonClick.dispatcher.AddListener("tip", OnTip);
        MainData.Instance.dispatcher.AddListener(hexjig.EventType.FINISH_LEVEL, OnFinshLevel);
        MainData.Instance.time.AddListener(lib.Event.CHANGE, OnTimeChange);
    }

    private void OnTip(lib.Event e)
    {
        MainData.Instance.dispatcher.DispatchWith(hexjig.EventType.SHOW_TIP);
    }

    private void OnTimeChange(lib.Event e)
    {
        timeTxt.text = StringUtils.TimeToMS(MainData.Instance.time.value);
    }

    /// <summary>
    /// 完成关卡，显示结果
    /// </summary>
    /// <param name="e"></param>
    private void OnFinshLevel(lib.Event e)
    {
        if(GameVO.Instance.model == GameModel.Daily)
        {
            //修改记录
            GameVO.Instance.daily.Finish(MainData.Instance.levelId.value,MainData.Instance.time.value);
        }
        else
        {

        }
        MainData.Instance.dispatcher.DispatchWith(hexjig.EventType.QUIT_LEVEL);
        GameVO.Instance.ShowModule(ModuleName.Result, e.Data);
    }

    private void OnRestart(lib.Event e)
    {
        MainData.Instance.dispatcher.DispatchWith(hexjig.EventType.RESTART);
    }

    private void OnQuit(lib.Event e)
    {
        MainData.Instance.dispatcher.DispatchWith(hexjig.EventType.QUIT_LEVEL);
        if(GameVO.Instance.model == GameModel.Daily)
        {
            GameVO.Instance.ShowModule(ModuleName.Daily);
        }
        else
        {
            GameVO.Instance.ShowModule(ModuleName.Freedom);
        }
    }

    private void OnEnable()
    {
        int level = 0;
        if (GameVO.Instance.model == GameModel.Daily)
        {
            level = (int)GameVO.Instance.moduleData;
        }
        else if (GameVO.Instance.model == GameModel.Freedom)
        {
            List<int> list = new List<int>();
            List<int> list2 = new List<int>();
            List<int> list3 = new List<int>();
            for (int i = 0; i < LevelConfig.Configs.Count; i++)
            {
                if (LevelConfig.Configs[i].id < 1000)
                {
                    list.Add(LevelConfig.Configs[i].id);
                }
                else if (LevelConfig.Configs[i].id < 2000)
                {
                    list2.Add(LevelConfig.Configs[i].id);
                }
                else
                {
                    list3.Add(LevelConfig.Configs[i].id);
                }
            }
            if(GameVO.Instance.difficulty == DifficultyMode.Easy)
            {
                level = list[(int)(UnityEngine.Random.Range(0, 1.0f) * list.Count)];
            }
            else if (GameVO.Instance.difficulty == DifficultyMode.Normal)
            {
                level = list2[(int)(UnityEngine.Random.Range(0, 1.0f) * list2.Count)];
            }
            else if (GameVO.Instance.difficulty == DifficultyMode.Hard)
            {
                level = list3[(int)(UnityEngine.Random.Range(0, 1.0f) * list3.Count)];
                level = 2000;
            }
        }
        new StartGameCommand(level);

        levelTxt.text = level + "";

        modelTxt.text = GameVO.Instance.model == GameModel.Daily ? "Challenge model" : "Freedom model";
    }
}
