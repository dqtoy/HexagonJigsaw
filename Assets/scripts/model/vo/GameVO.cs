using lib;
using System;
using System.Collections.Generic;

public class GameVO
{

    public float Width;
    public float Height;
    public float PixelWidth;
    public float PixelHeight;

    /// <summary>
    /// 背景音乐音量
    /// </summary>
    public Int musicVolumn = new Int(100);

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
    public Daily daily = new Daily();

    public object moduleData;

    /// <summary>
    /// 当前选择的游戏模式
    /// </summary>
    public GameModel model;

    /// <summary>
    /// 自由模式下的游戏难度
    /// </summary>
    public DifficultyMode difficulty;

    public GameVO()
    {
        instance = this;


        //临时代码 生成每日挑战数据
        //随机10个关卡，1-999 6  1000-1999 3  2000 1
        //levels = new List<LevelConfig>();
        List<int> list = new List<int>();
        List<int> list2 = new List<int>();
        List<int> list3 = new List<int>();
        for (int i = 0; i < LevelConfig.Configs.Count; i++)
        {
            if (LevelConfig.Configs[i].id < 1000)
            {
                list.Add(i);
            }
            else if (LevelConfig.Configs[i].id < 2000)
            {
                list2.Add(i);
            }
            else
            {
                list3.Add(i);
            }
        }
        int len = 0;
        while (len < 6)
        {
            int index = (int)Math.Floor(UnityEngine.Random.Range(0, 1f) * list.Count);
            int ind = list[index];
            list.RemoveAt(index);
            DailyLevelVO levelvo = new DailyLevelVO();
            levelvo.config = LevelConfig.Configs[ind];
            daily.levels.Add(levelvo);
            len++;
        }
        len = 0;
        while (len < 3)
        {
            int index = (int)Math.Floor(UnityEngine.Random.Range(0, 1f) * list2.Count);
            int ind = list2[index];
            list2.RemoveAt(index);
            DailyLevelVO levelvo = new DailyLevelVO();
            levelvo.config = LevelConfig.Configs[ind];
            daily.levels.Add(levelvo);
            len++;
        }
        len = 0;
        while (len < 1)
        {
            int index = (int)Math.Floor(UnityEngine.Random.Range(0, 1f) * list3.Count);
            int ind = list3[index];
            list3.RemoveAt(index);
            DailyLevelVO levelvo = new DailyLevelVO();
            levelvo.config = LevelConfig.Configs[ind];
            daily.levels.Add(levelvo);
            len++;
        }
        daily.all.value = daily.levels.length;
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
    Setting = 6
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