using hexjig;
using lib;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour {


    public RectTransform line;
    public Transform hit1;

    public GameObject daily;
    public GameObject freedom;

    private void Awake()
    {
        line.sizeDelta = new Vector2(line.sizeDelta.x, GameVO.Instance.PixelHeight * GameVO.Instance.scale);
        UIFix.SetDistanceToBottom(line);
        UIFix.SetDistanceToBottom(hit1);

        ButtonClick.dispatcher.AddListener("quitResult", OnQuit);
        ButtonClick.dispatcher.AddListener("home", OnShowHome);
        ButtonClick.dispatcher.AddListener("next", OnNext);
    }

    private void OnNext(lib.Event e)
    {
        if(GameVO.Instance.model == GameModel.Daily)
        {
            GameVO.Instance.ShowModule(ModuleName.Game, GameVO.Instance.daily.GetNextLevel(MainData.Instance.levelId.value));
        }
        else if(GameVO.Instance.model == GameModel.Freedom)
        {
            GameVO.Instance.ShowModule(ModuleName.Game);
        }
    }

    private void OnShowHome(lib.Event e)
    {
        //GameVO.Instance.ShowModule(ModuleName.Game, MainData.Instance.levelId.value);
        GameVO.Instance.ShowModule(ModuleName.Main);
    }

    private void OnQuit(lib.Event e)
    {
        GameVO.Instance.bgmId.value = bgmId;
        if (GameVO.Instance.model == GameModel.Daily)
        {
            GameVO.Instance.ShowModule(ModuleName.Daily);
        }
        else
        {
            GameVO.Instance.ShowModule(ModuleName.Freedom);
        }
    }

    private int bgmId;


    private void OnEnable()
    {
        bgmId = GameVO.Instance.bgmId.value;
        GameVO.Instance.bgmId.value = 1001;
        /*nextButton.SetActive(true);
        if (GameVO.Instance.model == GameModel.Daily)
        {
            if(!GameVO.Instance.daily.HasNextLevel(MainData.Instance.levelId.value))
            {
                nextButton.SetActive(false);
            }
        }*/

        //timeTxt.text = StringUtils.TimeToMS((int)GameVO.Instance.moduleData);
        daily.SetActive(GameVO.Instance.model == GameModel.Daily ? true : false);
        freedom.SetActive(GameVO.Instance.model == GameModel.Freedom ? true : false);
    }
}
