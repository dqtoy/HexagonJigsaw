using System;
using System.Collections;
using System.Collections.Generic;
using lib;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MainUI : MonoBehaviour {

    //public Text dailyProgressTxt;
    public Transform progressBg;

    public RectTransform hex;
    public RectTransform hex2;
    public RectTransform line;
    public RectTransform title;

    private void Awake()
    {
        UIFix.SetDistanceToTop(title);
        UIFix.SetDistanceToBottom(hex2);

        hexjig.Start.backgroundInstance.bposition = 0.57f;
        hexjig.Start.backgroundInstance.brotation = -18;
        ButtonClick.dispatcher.AddListener("daily", OnShowDaily);
        ButtonClick.dispatcher.AddListener("freedom", OnShowFreedom);
        ButtonClick.dispatcher.AddListener("setting", OnShowSetting);
        ButtonClick.dispatcher.AddListener("shop", OnShowShop);
        ButtonClick.dispatcher.AddListener("favriate", OnLoginTest);
        ButtonClick.dispatcher.AddListener("honor", OnShowHonor);
    }

    private void OnLoginTest(lib.Event e)
    {
        GameVO.Instance.rank.ShowRankView(UnityEngine.Random.Range(0, 1.0f) < 0.5f ? GooglePlay.TotalTimeRankId : GooglePlay.DailyTimeRankId);
    }

    private void OnShowHonor(lib.Event e)
    {
        GameVO.Instance.achievement.ShowAchievementView();
    }

    private void OnShowShop(lib.Event e)
    {
        GameVO.Instance.ShowModule(ModuleName.Shop);
    }

    private void OnShowSetting(lib.Event e)
    {
        GameVO.Instance.ShowModule(ModuleName.Setting);
    }

    private void OnShowDaily(lib.Event e)
    {
        GameVO.Instance.ShowModule(ModuleName.Daily);
    }


    private void OnShowFreedom(lib.Event e)
    {
        GameVO.Instance.ShowModule(ModuleName.Freedom);
    }

    private void OnShow(lib.Event e)
    {
        gameObject.SetActive(true);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
