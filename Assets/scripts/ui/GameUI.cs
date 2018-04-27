using hexjig;
using lib;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameUI : MonoBehaviour {

    public Text timeTxt;
    public TextExd modelTxt;
    public GameObject flush;

    //临时测试
    public Text levelTxt;

    private void Awake()
    {
        ButtonClick.dispatcher.AddListener("quitGame", OnQuit);
        ButtonClick.dispatcher.AddListener("restart", OnRestart);
        ButtonClick.dispatcher.AddListener("tip", OnTip);
        MainData.Instance.dispatcher.AddListener(hexjig.EventType.FINISH_LEVEL, OnFinshLevel);
        MainData.Instance.time.AddListener(lib.Event.CHANGE, OnTimeChange); 
        MainData.Instance.dispatcher.AddListener(hexjig.EventType.SET_PIECE, OnSetPiece);
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
            if(GameVO.Instance.daily.HasNextLevel(MainData.Instance.levelId.value))
            {

                new StartGameCommand(GameVO.Instance.daily.GetNextLevel(MainData.Instance.levelId.value));
                levelTxt.text = GameVO.Instance.daily.GetNextLevel(MainData.Instance.levelId.value) + "";
            }
            else
            {
                ResourceManager.PlaySound("sound/camera", false, GameVO.Instance.soundVolumn.value / 100.0f);
                MainData.Instance.dispatcher.DispatchWith(hexjig.EventType.SHOW_CUT);
                MainData.Instance.dispatcher.DispatchWith(hexjig.EventType.QUIT_LEVEL);
                GameVO.Instance.ShowModule(ModuleName.Result, e.Data);
            }
        }
        else
        {
            StartFreedomLevel();
        }
    }

    private void OnRestart(lib.Event e)
    {
        MainData.Instance.dispatcher.DispatchWith(hexjig.EventType.RESTART);
    }

    private void OnQuit(lib.Event e)
    {
        flush.GetComponent<RectTransform>().sizeDelta = new Vector2(GameVO.Instance.PixelWidth, GameVO.Instance.PixelHeight);
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(flush.GetComponent<Image>().DOColor(new Color(1, 1, 1, 1), 0.15f));
        mySequence.Append(flush.GetComponent<Image>().DOColor(new Color(1, 1, 1, 0), 0.1f));
        ResourceManager.PlaySound("sound/camera", false, GameVO.Instance.soundVolumn.value / 100.0f);
        MainData.Instance.dispatcher.DispatchWith(hexjig.EventType.SHOW_CUT);
        MainData.Instance.dispatcher.DispatchWith(hexjig.EventType.QUIT_LEVEL);
        GameVO.Instance.ShowModule(ModuleName.Result, MainData.Instance.time.value);
        /*if(GameVO.Instance.model == GameModel.Daily)
        {
            GameVO.Instance.ShowModule(ModuleName.Daily);
        }
        else
        {
            GameVO.Instance.ShowModule(ModuleName.Freedom);
        }*/
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
