using hexjig;
using lib;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class GameUI : MonoBehaviour {

    public Text timeTxt;
    public TextExd modelTxt;
    public GameObject flush;

    public Text time2;
    public Text txt1;
    public Text txt2;
    public Text txt3;
    public Text txt4;

    public GameObject hex;

    //临时测试
    public Text levelTxt;

    public RectTransform hexTrans;
    public RectTransform sure;
    public RectTransform cancel;

    private void Awake()
    {
        UIFix.SetDistanceToTop(hexTrans);

        sure.localScale = new Vector3(0, 1);
        cancel.localScale = new Vector3(0, 1);

        flush.SetActive(false);
        ButtonClick.dispatcher.AddListener("quitGame", OnQuit);
        ButtonClick.dispatcher.AddListener("restart", OnRestart);
        ButtonClick.dispatcher.AddListener("nextGame", OnNextGame);
        ButtonClick.dispatcher.AddListener("gameSure", OnQuitSure);
        ButtonClick.dispatcher.AddListener("gameCancel", OnQuitCancel);
        ButtonClick.dispatcher.AddListener("tip", OnTip);
        MainData.Instance.dispatcher.AddListener(hexjig.EventType.FINISH_LEVEL, OnFinshLevel);
        MainData.Instance.dispatcher.AddListener(hexjig.EventType.SHOW_CUT_COMPLETE, ShowFlush);
        MainData.Instance.time.AddListener(lib.Event.CHANGE, OnTimeChange);
        MainData.Instance.dispatcher.AddListener(hexjig.EventType.SET_PIECE, OnSetPiece);
        MainData.Instance.dispatcher.AddListener(hexjig.EventType.SHOW_GAME_CHANGE_OUT_EFFECT_COMPLETE, OnFinshLevel2);
    }

    private void OnQuitCancel(lib.Event e)
    {
        sure.DOScaleX(0, 0.2f);
        cancel.DOScaleX(0, 0.2f);
    }

    private void OnQuitSure(lib.Event e)
    {
        sure.DOScaleX(0, 0.2f);
        cancel.DOScaleX(0, 0.2f);
        ResourceManager.PlaySound("sound/camera", false, GameVO.Instance.soundVolumn.value / 100.0f);
        hex.SetActive(false);
        MainData.Instance.dispatcher.DispatchWith(hexjig.EventType.SHOW_CUT);
        MainData.Instance.dispatcher.DispatchWith(hexjig.EventType.QUIT_LEVEL);
        GameVO.Instance.ShowModule(ModuleName.Result, MainData.Instance.time.value);
    }

    private void OnNextGame(lib.Event e)
    {
        if (GameVO.Instance.model == GameModel.Daily)
        {
            if (GameVO.Instance.daily.HasNextLevel(MainData.Instance.levelId.value))
            {
                MainData.Instance.dispatcher.DispatchWith(hexjig.EventType.SHOW_GAME_CHANGE_OUT_EFFECT);
            }
            else
            {
                ResourceManager.PlaySound("sound/camera", false, GameVO.Instance.soundVolumn.value / 100.0f);
                hex.SetActive(false);
                MainData.Instance.dispatcher.DispatchWith(hexjig.EventType.SHOW_CUT);
                MainData.Instance.dispatcher.DispatchWith(hexjig.EventType.QUIT_LEVEL);
                GameVO.Instance.ShowModule(ModuleName.Result, e.Data);
            }
        }
        else
        {
            MainData.Instance.dispatcher.DispatchWith(hexjig.EventType.SHOW_GAME_CHANGE_OUT_EFFECT);
        }
    }

    private void OnSetPiece(lib.Event e)
    {
        ResourceManager.PlaySound("sound/setpiece", false, GameVO.Instance.soundVolumn.value / 100.0f);
    }

    private void OnTip(lib.Event e)
    {
        MainData.Instance.dispatcher.DispatchWith(hexjig.EventType.SHOW_TIP);
    }

    private void OnTimeChange(lib.Event e)
    {
        timeTxt.text = StringUtils.TimeToMS(MainData.Instance.time.value);
        time2.text = StringUtils.TimeToMS(MainData.Instance.time.value);
    }

    /// <summary>
    /// 完成关卡，显示结果
    /// </summary>
    /// <param name="e"></param>
    private void OnFinshLevel(lib.Event e)
    {
        txt1.GetComponent<TextExd>().languageId = UnityEngine.Random.Range(1001, 1007);
        int score = UnityEngine.Random.Range(60, 101);
        if(score < 80)
        {
            GetComponent<GameUIFade>().effectCount = 1;
        }
        else if(score < 90)
        {
            GetComponent<GameUIFade>().effectCount = 2;
        }
        else
        {
            GetComponent<GameUIFade>().effectCount = 3;
        }
        txt3.text = score + "%";
        if (GameVO.Instance.model == GameModel.Daily)
        {
            //修改记录
            GameVO.Instance.daily.Finish(MainData.Instance.levelId.value,MainData.Instance.time.value);
            MainData.Instance.dispatcher.DispatchWith(hexjig.EventType.SHOW_GAME_CHANGE_OUT_EFFECT0);
        }
        else
        {
            //先播放之前关卡的退场动画
            MainData.Instance.dispatcher.DispatchWith(hexjig.EventType.SHOW_GAME_CHANGE_OUT_EFFECT0);
        }
    }


    private void OnFinshLevel2(lib.Event e)
    {
        if(GameVO.Instance.model == GameModel.Daily)
        {
            //修改记录
            GameVO.Instance.daily.Finish(MainData.Instance.levelId.value, MainData.Instance.time.value);
            if (GameVO.Instance.daily.HasNextLevel(MainData.Instance.levelId.value))
            {
                new StartGameCommand(GameVO.Instance.daily.GetNextLevel(MainData.Instance.levelId.value));
                levelTxt.text = GameVO.Instance.daily.GetNextLevel(MainData.Instance.levelId.value) + "";
                MainData.Instance.dispatcher.DispatchWith(hexjig.EventType.SHOW_GAME_CHANGE_IN_EFFECT);
            }
        }
        else
        {
            StartFreedomLevel();
            MainData.Instance.dispatcher.DispatchWith(hexjig.EventType.SHOW_GAME_CHANGE_IN_EFFECT);
        }
    }

    private void OnRestart(lib.Event e)
    {
        //MainData.Instance.dispatcher.DispatchWith(hexjig.EventType.RESTART);
        MainData.Instance.dispatcher.DispatchWith(hexjig.EventType.BACK_STEP);
    }

    private void OnQuit(lib.Event e)
    {
        sure.DOScaleX(1, 0.2f);
        cancel.DOScaleX(1, 0.2f);
    }

    private void ShowFlush(lib.Event e)
    {
        hex.SetActive(true);
        flush.SetActive(true);
        flush.GetComponent<RectTransform>().sizeDelta = new Vector2(GameVO.Instance.PixelWidth, GameVO.Instance.PixelHeight);
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(flush.GetComponent<Image>().DOColor(new Color(1, 1, 1, 1), 0.15f));
        mySequence.Append(flush.GetComponent<Image>().DOColor(new Color(1, 1, 1, 0), 0.1f)).onComplete = FlushComplete;
    }

    private void FlushComplete()
    {
        flush.SetActive(false);
    }

    private void OnEnable()
    {
        if (GameVO.Instance.model == GameModel.Daily)
        {
            int level = (int)GameVO.Instance.moduleData;
            new StartGameCommand(level);
            levelTxt.text = level + "";
        }
        else if (GameVO.Instance.model == GameModel.Freedom)
        {
            StartFreedomLevel();
        }
        modelTxt.languageId = GameVO.Instance.model == GameModel.Daily ? 10 : 9;
    }

    private void StartFreedomLevel()
    {
        int level = 0;
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
        if (GameVO.Instance.difficulty == DifficultyMode.Easy)
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
        }
        new StartGameCommand(level);
        levelTxt.text = level + "";
    }
}