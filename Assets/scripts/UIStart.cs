using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using hexjig;
using lib;
using System;

public class UIStart : MonoBehaviour {

    public GameObject mainUI;
    public GameObject dailyUI;
    public GameObject freedomUI;
    public GameObject gameUI;
    public GameObject resultUI;

    private GameObject show;

    // Use this for initialization
    void Start ()
    {
        if (GameVO.Instance.editor == false)
        {
            gameObject.SetActive(true);

            GameVO.Instance.dispatcher.AddListener(GameEvent.SHOW_MODULE,OnShowModule);

            GameVO.Instance.ShowModule(ModuleName.Main);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void OnShowModule(lib.Event e)
    { 
        if (show != null)
        {
            show.SetActive(false);
            show = null;
        }
        ModuleEventData d = e.Data as ModuleEventData;
        GameVO.Instance.moduleData = d.value;
        switch (d.name)
        {
            case ModuleName.Main:
                mainUI.SetActive(true);
                show = mainUI;
                break;
            case ModuleName.Daily:
                dailyUI.SetActive(true);
                show = dailyUI;
                break;
            case ModuleName.Freedom:
                freedomUI.SetActive(true);
                show = freedomUI;
                break;
            case ModuleName.Game:
                gameUI.SetActive(true);
                show = gameUI;
                break;
            case ModuleName.Result:
                resultUI.SetActive(true);
                show = resultUI;
                break;
        }
    }

    private void OnRestartHandler(lib.Event e)
    {
        MainData.Instance.dispatcher.DispatchWith(hexjig.EventType.RESTART);
    }

    private void StartNextLevel()
    {
        //int index = (int)Math.Floor(UnityEngine.Random.Range(0, 1f) * levels.Count);
        //LevelConfig config = levels[index];
        //levels.RemoveAt(index);
        //开启一个关卡
        //new StartGameCommand(config.id);
    }

    /// <summary>
    /// 一个新的关卡开始
    /// </summary>
    /// <param name="e"></param>
    private void OnStartLevel(lib.Event e)
    {
        //levelTxt.text = "当前关卡:" + MainData.Instance.levelId.value;
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
        //StartNextLevel();
    }

    // Update is called once per frame
    void Update ()
    {

    }
}
