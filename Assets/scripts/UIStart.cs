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

    private GameObject show;
    private ModuleName showModule;
    private ModuleName nextModule;

    // Use this for initialization
    void Start ()
    {
        //播放背景音乐
        GameVO.Instance.bgm = ResourceManager.PlaySound("music/1", false, GameVO.Instance.musicVolumn.value / 100.0f);
        GameVO.Instance.bgm.DOFade(GameVO.Instance.musicVolumn.value / 100.0f, 1f);
        GameVO.Instance.musicVolumn.AddListener(lib.Event.CHANGE, OnMusicVolumnChange);

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

    private void OnMusicVolumnChange(lib.Event e)
    {
        GameVO.Instance.bgm.volume = GameVO.Instance.musicVolumn.value / 100.0f;
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
