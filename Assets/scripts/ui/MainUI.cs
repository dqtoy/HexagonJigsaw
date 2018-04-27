using System;
using System.Collections;
using System.Collections.Generic;
using lib;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MainUI : MonoBehaviour {

    public Text dailyProgressTxt;
    public Transform progressBg;
    public Image progress;
    public Image progressLess;

    private void Awake()
    {
        ButtonClick.dispatcher.AddListener("daily", OnShowDaily);
        ButtonClick.dispatcher.AddListener("freedom", OnShowFreedom);
        ButtonClick.dispatcher.AddListener("setting", OnShowSetting);

        GameVO.Instance.daily.progress.AddListener(lib.Event.CHANGE, OnDailyProgressChange);
    }

    private void OnDailyProgressChange(lib.Event e = null)
    {
        dailyProgressTxt.text = GameVO.Instance.daily.progress.value + "/" + GameVO.Instance.daily.all.value;
        progress.fillAmount = 1.0f * GameVO.Instance.daily.progress.value / GameVO.Instance.daily.all.value;
        progressLess.fillAmount = 1 - 1.0f * GameVO.Instance.daily.progress.value / GameVO.Instance.daily.all.value;
    }

    private void OnEnable()
    {
        OnDailyProgressChange();
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
