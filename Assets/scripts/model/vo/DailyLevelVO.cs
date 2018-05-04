using lib;

public class DailyLevelVO
{
    //关卡配置
    public LevelConfig config;

    //是否通关
    public bool pass = false;

    //通关时间 秒
    public int time = 0;

    /// <summary>
    /// 通关是否检测过
    /// </summary>
    public bool hasCheck = false;
}