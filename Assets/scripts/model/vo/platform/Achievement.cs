using lib;
using UnityEngine;

public class Achievement
{

    /// <summary>
    /// 显示成就面板
    /// </summary>
    public void ShowAchievementView()
    {
        if(GameVO.Instance.googlePlatform.hasLogin == false)
        {
            GameVO.Instance.googlePlatform.Login();
            new WaitEvent(GameVO.Instance.googlePlatform, GooglePlay.LOGIN_SUCCESS, GameVO.Instance.googlePlatform, "ShowAchievementView");
        }
        else
        {
            GameVO.Instance.googlePlatform.ShowAchievementView();
        }
    }

    /// <summary>
    /// 解锁成就 
    /// </summary>
    /// <param name="id">成就id 例如CgkIgZjNrfYeEAIQAQ</param>
    public void UnlockAchievement(string id)
    {
        if (GameVO.Instance.googlePlatform.hasLogin == false)
        {
            GameVO.Instance.googlePlatform.Login();
            new WaitEvent(GameVO.Instance.googlePlatform, GooglePlay.LOGIN_SUCCESS, GameVO.Instance.googlePlatform, "UploadAchievement", new object[] { id });
        }
        else
        {
            GameVO.Instance.googlePlatform.UploadAchievement(id);
        }
    }

    /// <summary>
    /// 是否解锁成就
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool IsUnlockAchievement(string id)
    {
        return GameVO.Instance.googlePlatform.IsUnlockAchievement(id);
    }

    /// <summary>
    /// 通过关卡
    /// </summary>
    /// <param name="config">关卡配置</param>
    /// <param name="timeSecond">通过耗时(秒)</param>
    /// <param name="score">获得分数</param>
    public void FinishLevel(LevelConfig config,int timeSecond,float score)
    {
        if (config.id == 23 && IsUnlockAchievement("CgkInbOFtb4REAIQDg") == false)//love
            UnlockAchievement("CgkInbOFtb4REAIQDg");

        if (config.id == 28 && IsUnlockAchievement("CgkInbOFtb4REAIQDw") == false)//love
            UnlockAchievement("CgkInbOFtb4REAIQDw");

        if (config.id == 42 && IsUnlockAchievement("CgkInbOFtb4REAIQEA") == false)//love
            UnlockAchievement("CgkInbOFtb4REAIQEA");

        if (config.id == 47 && IsUnlockAchievement("CgkInbOFtb4REAIQEQ") == false)//love
            UnlockAchievement("CgkInbOFtb4REAIQEQ");

        if (config.id == 56 && IsUnlockAchievement("CgkInbOFtb4REAIQEg") == false)//love
            UnlockAchievement("CgkInbOFtb4REAIQEg");

        if (config.id == 68 && IsUnlockAchievement("CgkInbOFtb4REAIQEw") == false)//love
            UnlockAchievement("CgkInbOFtb4REAIQEw");

        if (config.id == 65 && IsUnlockAchievement("CgkInbOFtb4REAIQFA") == false)//love
            UnlockAchievement("CgkInbOFtb4REAIQFA");

        if (config.id == 82 && IsUnlockAchievement("CgkInbOFtb4REAIQFQ") == false)//love
            UnlockAchievement("CgkInbOFtb4REAIQFQ");
    }

    /// <summary>
    /// 通过每日挑战
    /// </summary>
    /// <param name="timeSecond">耗时(秒)</param>
    public void FinishDaily(int timeSecond)
    {
        if (timeSecond < 1200 && IsUnlockAchievement("CgkInbOFtb4REAIQCw") == false)
            UnlockAchievement("CgkInbOFtb4REAIQCw");

        if (timeSecond < 900 && IsUnlockAchievement("CgkInbOFtb4REAIQDA") == false)
            UnlockAchievement("CgkInbOFtb4REAIQDA");

        if (timeSecond < 600 && IsUnlockAchievement("CgkInbOFtb4REAIQDQ") == false)
            UnlockAchievement("CgkInbOFtb4REAIQDQ");
    }

    /// <summary>
    /// 登入游戏
    /// </summary>
    /// <param name="isDayFirstLogin">是否为当天第一次登录游戏</param>
    /// <param name="autoLoginDay">连续登录天数</param>
    public void LoginIn(bool isDayFirstLogin,int autoLoginDay)
    {
        if (!PlayerPrefs.HasKey("LoginCounter"))
        {
            PlayerPrefs.SetInt("LoginCounter", 1);
            UnlockAchievement("CgkInbOFtb4REAIQCA");//首次登陆
        }
        else {
            PlayerPrefs.SetInt("LoginCounter", PlayerPrefs.GetInt("LoginCounter") + 1);
            if (PlayerPrefs.GetInt("LoginCounter") == 2 && IsUnlockAchievement("CgkInbOFtb4REAIQCQ") == false)
                UnlockAchievement("CgkInbOFtb4REAIQCQ");//登陆2天
            if (PlayerPrefs.GetInt("LoginCounter") == 3 && IsUnlockAchievement("CgkInbOFtb4REAIQCg") == false)
                UnlockAchievement("CgkInbOFtb4REAIQCg");//登陆3天
            //if (PlayerPrefs.GetInt("LoginCounter") == 5)
            //    UnlockAchievement("XXXXXXXXXXX");//登陆5天
            //if (PlayerPrefs.GetInt("LoginCounter") == 7)
            //    UnlockAchievement("XXXXXXXXXXX");//登陆7天
        }
    }
}