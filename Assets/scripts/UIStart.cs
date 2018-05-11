using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using hexjig;
using lib;
using System;
using DG.Tweening;

public class UIStart : MonoBehaviour {

    public GameObject mainUI;
    public GameObject dailyUI;
    public GameObject freedomUI;
    public GameObject gameUI;
    public GameObject resultUI;
    public GameObject settingUI;
    public GameObject shopUI;

    public GameObject bg1;
    public GameObject bg2;
    public GameObject bg3;
    public GameObject bg4;
    public GameObject bg5;

    public RectTransform border1;
    public RectTransform border2;
    public RectTransform border3;
    public RectTransform border4;

    private GameObject show;
    private ModuleName showModule;
    private ModuleName nextModule;

    public Transform backgrodunEffect;

    // Use this for initialization
    void Start ()
    {
        border1.sizeDelta = new Vector2(border1.sizeDelta.x, GameVO.Instance.PixelHeight);
        border2.sizeDelta = new Vector2(border1.sizeDelta.x, GameVO.Instance.PixelHeight);
        UIFix.SetDistanceToBottom(border3);
        UIFix.SetDistanceToTop(border4);
        backgrodunEffect.transform.position = new Vector3(0, -GameVO.Instance.Height * 0.5f, 100);

        (Grammar.GetProperty(this, "bg" + UnityEngine.Random.Range(1, 6)) as GameObject).SetActive(true);


        gameObject.transform.localScale = new Vector3(GameVO.Instance.PixelWidth/720f,GameVO.Instance.PixelWidth / 720f);
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

    private void OnFadeOut(lib.Event e)
    {
        ResourceManager.PlaySound("sound/changeScene", false, GameVO.Instance.soundVolumn.value / 100.0f);
        ModuleName old = showModule;
        show.SetActive(false);
        show = null;
        showModule = nextModule;
        switch (nextModule)
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
            case ModuleName.Setting:
                settingUI.SetActive(true);
                show = settingUI;
                break;
            case ModuleName.Shop:
                shopUI.SetActive(true);
                show = shopUI;
                break;
        }
        if(old == ModuleName.Result && showModule == ModuleName.Main)
        {
            if(GameVO.Instance.model == GameModel.Daily)
            {
                dailyUI.GetComponent<UIFade>().FadeIn(ModuleName.Game);
            }
            if (GameVO.Instance.model == GameModel.Freedom)
            {
                dailyUI.GetComponent<UIFade>().FadeIn(ModuleName.Game);
            }
        }
        show.GetComponent<UIFade>().FadeIn(old);
    }

    private void OnShowModule(lib.Event e)
    {
        ModuleEventData d = e.Data as ModuleEventData;
        GameVO.Instance.moduleData = d.value;
        if (show != null)
        {
            nextModule = d.name;
            show.GetComponent<UIFade>().FadeOut(nextModule);
            show.GetComponent<UIFade>().AddListener(lib.Event.COMPLETE, OnFadeOut);
            return;
        }
        showModule = d.name;
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
            case ModuleName.Setting:
                settingUI.SetActive(true);
                show = settingUI;
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
