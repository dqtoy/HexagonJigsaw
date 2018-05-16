using GooglePlayGames;
using lib;
using UnityEngine;

public class GooglePlay : EventDispatcher {

    private bool initFlag = false;

    private void Init()
    {
        if(!initFlag)
        {
            initFlag = true;
            PlayGamesPlatform.Activate();
        }
    }

    public bool hasLogin
    {
        get
        {
            return Social.localUser.authenticated;
        }
    }

    bool isLogin = false;
    public void Login()
    {
        if (hasLogin) return;
        Init();
        if (isLogin) return;
        isLogin = true;

        Social.localUser.Authenticate((bool success) =>
        {
            isLogin = false;
            if (success)
            {
                this.DispatchWith(LOGIN_SUCCESS);
            }
            else
            {
                this.DispatchWith(LOGIN_FAIL);
            }
        });
    }

    public void TestLogin()
    {
        this.DispatchWith(LOGIN_SUCCESS);
    }

    public void ShowAchievementView()
    {
        PlayGamesPlatform.Instance.ShowAchievementsUI();
    }

    /// <summary>
    /// 显示排行榜
    /// </summary>
    /// <param name="id">排行榜id </param>
    public void ShowRankView(string id)
    {
        PlayGamesPlatform.Instance.ShowLeaderboardUI(id);
    }

    /// <summary>
    /// 上传排行榜分数
    /// </summary>
    /// <param name="score">分数</param>
    /// <param name="id">排行榜id</param>
    public void UploadRank(int score,string id)
    {
        Social.ReportScore(score, id, null);
    }


    /// <summary>
    /// 上传成就，完成成就
    /// </summary>
    /// <param name="id"> 成就id </param>
    public void UploadAchievement(string id)
    {
        PlayGamesPlatform.Instance.UnlockAchievement(id);
    }

    /// <summary>
    /// 时候解锁成就
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool IsUnlockAchievement(string id)
    {
        return PlayGamesPlatform.Instance.GetAchievement(id).IsUnlocked;
    }

    public void Buy(string buykey)
    {
        AndroidJavaClass m_unityPlayer = new AndroidJavaClass("com.BailiGame.HexagonJigsaw.UnityPlayerActivity");
        AndroidJavaObject m_curActivity = m_unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        m_curActivity.Call("Pay", buykey);

    }

    public static string LOGIN_SUCCESS = "login_success";
    public static string LOGIN_FAIL = "login_fail";

    public static string TotalTimeRankId = "CgkInbOFtb4REAIQBw";
    public static string DailyTimeRankId = "CgkInbOFtb4REAIQBg";
}
