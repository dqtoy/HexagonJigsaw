using lib;
using UnityEngine;

public class Rank
{

    /// <summary>
    /// 上传排行榜分数
    /// </summary>
    /// <param name="score"></param>
    /// <param name="id"></param>
    public void UploadRank(int score,string id)
    {
        if (GameVO.Instance.googlePlatform.hasLogin == false)
        {
            GameVO.Instance.googlePlatform.Login();
            new WaitEvent(GameVO.Instance.googlePlatform, GooglePlay.LOGIN_SUCCESS, GameVO.Instance.googlePlatform, "UploadRank", new object[] { score, id });
        }
        else
        {
            GameVO.Instance.googlePlatform.UploadRank(score, id);
        }
    }

    /// <summary>
    /// 显示排行榜
    /// </summary>
    /// <param name="id"></param>
    public void ShowRankView(string id)
    {
        if (GameVO.Instance.googlePlatform.hasLogin == false)
        {
            GameVO.Instance.googlePlatform.Login();
            new WaitEvent(GameVO.Instance.googlePlatform, GooglePlay.LOGIN_SUCCESS, GameVO.Instance.googlePlatform, "ShowRankView",new object[] { id });
        }
        else
        {
            GameVO.Instance.googlePlatform.ShowRankView(id);
        }
    }

    /// <summary>
    /// 通过关卡
    /// </summary>
    /// <param name="config">关卡配置</param>
    /// <param name="timeSecond">通过耗时(秒)</param>
    /// <param name="score">获得分数</param>
    public void FinishLevel(LevelConfig config, int timeSecond, float score)
    {
        if(!PlayerPrefs.HasKey("TotalTime"))
        {
            PlayerPrefs.SetInt("TotalTime", timeSecond);
        }
        else
        {
            PlayerPrefs.SetInt("TotalTime", timeSecond + PlayerPrefs.GetInt("TotalTime"));
        }
        UploadRank(PlayerPrefs.GetInt("TotalTime"), GooglePlay.TotalTimeRankId);
    }

    /// <summary>
    /// 通过每日挑战
    /// </summary>
    /// <param name="timeSecond">耗时(秒)</param>
    public void FinishDaily(int timeSecond)
    {
        if (!PlayerPrefs.HasKey("DailyTime"))
        {
            PlayerPrefs.SetInt("DailyTime", timeSecond);
        }
        else
        {
            PlayerPrefs.SetInt("DailyTime", timeSecond + PlayerPrefs.GetInt("DailyTime"));
        }
        UploadRank(PlayerPrefs.GetInt("DailyTime"), GooglePlay.DailyTimeRankId);
    }
}