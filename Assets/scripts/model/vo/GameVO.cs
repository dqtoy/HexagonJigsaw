using lib;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameVO
{

    public float Width;
    public float Height;
    public float PixelWidth;
    public float PixelHeight;
    public float scale;

    public Int bgmId = new Int(1);
    public AudioSource bgm;

    /// <summary>
    /// 背景音乐音量
    /// </summary>
    public Int musicVolumn = new Int(60);

    /// <summary>
    /// 音效音量
    /// </summary>
    public Int soundVolumn = new Int(100);

    /// <summary>
    /// 背景音乐是否在编辑状态
    /// </summary>
    public Bool musicEditor = new Bool(false);

    /// <summary>
    /// 音效是否在编辑状态
    /// </summary>
    public Bool soundEditor = new Bool(false);

    /// <summary>
    /// 当前语言
    /// </summary>
    public Int language = new Int();

    public Bool hasBuyMusic = new Bool(false);

    /// <summary>
    /// 是否处于编辑模式
    /// </summary>
    public bool editor;

    /// <summary>
    /// 模块名称堆栈
    /// </summary>
    public List<ModuleName> lastModules = new List<ModuleName>();

    private ModuleName showModule = ModuleName.None;

    /// <summary>
    /// 参考 GameEvent
    /// </summary>
    public EventDispatcher dispatcher = new EventDispatcher();

    /// <summary>
    /// 每日挑战
    /// </summary>
    public Daily daily;

    public object moduleData;

    /// <summary>
    /// 当前选择的游戏模式
    /// </summary>
    public GameModel model;

    public lib.Int modelCount = new lib.Int();

    /// <summary>
    /// 过关评价
    /// </summary>
    public PassScoreConfig passScore;

    public lib.Int totalTime = new lib.Int();

    public lib.String totalTimeString = new lib.String();

    /// <summary>
    /// 自由模式下的游戏难度
    /// </summary>
    public DifficultyMode difficulty;

    public StorageVO storage;

    public GooglePlay googlePlatform =  new GooglePlay();

    public Achievement achievement = new Achievement();

    public Rank rank = new Rank();

    public bool isLoading = true;

    public GameVO()
    {
        instance = this;

        storage = new StorageVO();

        GameObject obj = new GameObject();
        daily = obj.AddComponent<Daily>();

        musicVolumn.value = 0;
        soundVolumn.value = 0;


        //播放背景音乐
        bgm = ResourceManager.PlaySound("music/" + bgmId.value, true, musicVolumn.value / 100.0f);

        bgmId.AddListener(lib.Event.CHANGE, OnBgmChange);
        soundVolumn.AddListener(lib.Event.CHANGE, OnSoundVolumnChange);
        musicVolumn.AddListener(lib.Event.CHANGE, OnMusicVolumnChange);

        language.AddListener(lib.Event.CHANGE, OnLanguageChange);
    }

    public void LoadingComplete()
    {
        isLoading = false;
        musicVolumn.value = PlayerPrefs.HasKey("musicVolumn")? PlayerPrefs.GetInt("musicVolumn") : 60;
        soundVolumn.value = PlayerPrefs.HasKey("soundVolumn") ? PlayerPrefs.GetInt("soundVolumn") : 60;
        bgmId.value = 1;
    }

    private void OnLanguageChange(lib.Event e)
    {
        PlayerPrefs.SetInt("language", language.value);
    }

    private void OnBgmChange(lib.Event e)
    {
        if(isLoading)
        {
            return;
        }

        //Debug.Log("bgm:" + bgmId.value);

        if(bgm)
        {
            bgm.Stop();
            StartUp.Destroy(bgm.gameObject);
        }

        //播放背景音乐
        bgm = ResourceManager.PlaySound("music/" + bgmId.value, true, GameVO.Instance.musicVolumn.value / 100.0f);
    }


    private void OnSoundVolumnChange(lib.Event e)
    {
        if (isLoading)
        {
            return;
        }
        PlayerPrefs.SetInt("soundVolumn", soundVolumn.value);
    }

    private void OnMusicVolumnChange(lib.Event e)
    {
        if(isLoading)
        {
            return;
        }
        PlayerPrefs.SetInt("musicVolumn", musicVolumn.value);
        bgm.volume = musicVolumn.value / 100.0f;
    }


    /// <summary>
    /// 显示模块
    /// </summary>
    /// <param name="name"></param>
    /// <param name="data"></param>
    public void ShowModule(ModuleName name,object data = null)
    {
        if(showModule != ModuleName.None)
        {
            lastModules.Add(showModule);
        }
        showModule = name;
        dispatcher.DispatchWith(GameEvent.SHOW_MODULE, new ModuleEventData(name, data));
    }

    private static GameVO instance;


    public static GameVO Instance
    {
        get
        {
            if (instance == null)
            {
                new GameVO();
            }
            return instance;
        }
    }
}

public class GameEvent
{
    public static string SHOW_MODULE = "show_module";
    public static string READY_SHOW_MODULE = "ready_show_module";
    public static string SHOW_MODULE_COMPLETE = "show_module_complete";

}

public enum GameModel
{
    Daily = 1,
    Freedom = 2
}

public enum ModuleName
{
    None = 0,
    Main = 1,
    Daily = 2,
    Freedom = 3,
    Game = 4,
    Result = 5,
    Setting = 6,
    Shop = 7
}

public enum DifficultyMode
{
    Easy = 1,
    Normal = 2,
    Hard = 3
}

public class ModuleEventData
{
    public ModuleName name;

    public object value;

    public ModuleEventData(ModuleName name,object value = null)
    {
        this.name = name;
        this.value = value;
    }
}