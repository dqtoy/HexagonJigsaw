using lib;

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

    }

    /// <summary>
    /// 通过每日挑战
    /// </summary>
    /// <param name="timeSecond">耗时(秒)</param>
    public void FinishDaily(int timeSecond)
    {

    }

    /// <summary>
    /// 玩家进入游戏
    /// </summary>
    public void LoginIn()
    {

    }
}