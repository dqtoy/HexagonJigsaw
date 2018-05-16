using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using hexjig;
using lib;
using System;
using DG.Tweening;

public class UIStart : MonoBehaviour {

    public Transform uicamera;

    public Text log;

    void Messgae(string message)

    {

        log.text += message + "\n";

    }

    public GameObject loading;
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

    private int loadingStep = 0;

    public bool needLoading = true;

    // Use this for initialization
    void Start ()
    {
        border1.sizeDelta = new Vector2(border1.sizeDelta.x, GameVO.Instance.PixelHeight * GameVO.Instance.scale);
        border2.sizeDelta = new Vector2(border1.sizeDelta.x, GameVO.Instance.PixelHeight * GameVO.Instance.scale);
        UIFix.SetDistanceToBottom(border3);
        UIFix.SetDistanceToTop(border4);
        backgrodunEffect.transform.localPosition = new Vector3(0, -GameVO.Instance.Height * 0.5f, backgrodunEffect.transform.localPosition.z);

        (Grammar.GetProperty(this, "bg" + UnityEngine.Random.Range(1, 6)) as GameObject).SetActive(true);


        gameObject.transform.localScale = new Vector3(GameVO.Instance.PixelWidth/720f,GameVO.Instance.PixelWidth / 720f);
        if (GameVO.Instance.editor == false)
        {
            gameObject.SetActive(true);

            GameVO.Instance.dispatcher.AddListener(GameEvent.SHOW_MODULE,OnShowModule);

            GameVO.Instance.ShowModule(ModuleName.Main);

            loadingStep = 5;
            if (needLoading)
            {
                uicamera.localPosition = new Vector3(0, 0, -1000);
                loading.SetActive(true);
                GameVO.Instance.dispatcher.AddListener(GameEvent.SHOW_MODULE_COMPLETE, OnLoadingStep);
                GameVO.Instance.dispatcher.AddListener(GameEvent.READY_SHOW_MODULE, OnReadyShowStep);
            }
            else
            {
                uicamera.localPosition = new Vector3(0, 0, -50);
                GameVO.Instance.LoadingComplete();
                loading.SetActive(false);
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void OnReadyShowStep(lib.Event e)
    {
        if (loadingStep == 11)
        {
            LoadingComplete();
        }
    }

    private void OnLoadingStep(lib.Event e)
    {
        if(loadingStep == 1)
        {
            GameVO.Instance.ShowModule(ModuleName.Setting);
            loadingStep = 2;
        }
        else if(loadingStep == 2)
        {
            GameVO.Instance.ShowModule(ModuleName.Main);
            loadingStep = 3;
        }
        else if(loadingStep == 3)
        {
            GameVO.Instance.ShowModule(ModuleName.Shop);
            loadingStep = 4;
        }
        else if (loadingStep == 4)
        {
            GameVO.Instance.ShowModule(ModuleName.Main);
            loadingStep = 5;
        }
        else if (loadingStep == 5)
        {
            GameVO.Instance.ShowModule(ModuleName.Daily);
            loadingStep = 6;
        }
        else if (loadingStep == 6)
        {
            GameVO.Instance.ShowModule(ModuleName.Main);
            loadingStep = 7;
        }
        else if(loadingStep == 7)
        {
            GameVO.Instance.ShowModule(ModuleName.Freedom);
            loadingStep = 8;
        }
        else if(loadingStep == 8)
        {
            GameVO.Instance.model = GameModel.Freedom;
            GameVO.Instance.difficulty = DifficultyMode.Easy;
            GameVO.Instance.ShowModule(ModuleName.Game);
            loadingStep = 9;
        }
        else if(loadingStep == 9)
        {
            Invoke("LoadingGameStep", 0.1f);
        }
        else if(loadingStep == 10)
        {
            GameVO.Instance.ShowModule(ModuleName.Main);
            loadingStep = 11;
        }

    }

    public void LoadingGameStep()
    {
        gameUI.GetComponent<GameUI>().LoadingChangeOut();
        loadingStep = 10;
    }

    private void LoadingComplete()
    {
        uicamera.localPosition = new Vector3(0, 0, -50);
        GameVO.Instance.dispatcher.RemoveListener(GameEvent.SHOW_MODULE_COMPLETE, OnLoadingStep);
        GameVO.Instance.dispatcher.RemoveListener(GameEvent.READY_SHOW_MODULE, OnReadyShowStep);
        GameVO.Instance.LoadingComplete();
        MainData.Instance.isLoading = false;
        loading.SetActive(false);

        GameVO.Instance.achievement.LoginIn();
        //show = null;
        //GameVO.Instance.ShowModule(ModuleName.Main);
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
        show.GetComponent<UIFade>().FadeIn(ModuleName.None);
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
