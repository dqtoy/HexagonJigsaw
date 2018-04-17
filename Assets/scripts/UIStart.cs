using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using hexjig;
using lib;
using System;

public class UIStart : MonoBehaviour {

    public Text levelTxt;

	// Use this for initialization
	void Start () {
        if(GameVO.Instance.editor == false)
        {
            gameObject.SetActive(true);

            //监听是否开启一个新的关卡
            MainData.Instance.dispatcher.AddListener(hexjig.EventType.START_LEVEL, OnStartLevel);

            //监听关卡是否完成
            MainData.Instance.dispatcher.AddListener(hexjig.EventType.FINISH_LEVEL, OnFinishLevel);

            //开启一个关卡
            new StartGameCommand(5);
        }
        else
        {
            gameObject.SetActive(false);
        }
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
        for(int i = 0; i < LevelConfig.Configs.Count; i++)
        {
            if(LevelConfig.Configs[i].id == MainData.Instance.levelId.value)
            {
                if(i < LevelConfig.Configs.Count - 1)
                {
                    new StartGameCommand(LevelConfig.Configs[i + 1].id);
                    return;
                }
            }
        }
    }

    // Update is called once per frame
    void Update ()
    {

    }
}
