using admob;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdmobPlugin
{

    private Admob ad;

    public AdmobPlugin()
    {
        ad = Admob.Instance();
    }

    public void ShowBanner()
    {
        ad.initAdmob("ca-app-pub-6319110362714674/8855868924", "ca-app-pub-6319110362714674~2847782205");
        //ad.bannerEventHandler += onBannerEvent;
        //ad.rewardedVideoEventHandler += onRewardedVideoEvent;
        ad.setTesting(true);
        ad.setGender(AdmobGender.MALE);
        Admob.Instance().showBannerRelative(AdSize.SmartBanner, AdPosition.BOTTOM_CENTER, 0);
    }


    public void RemoveBanner()
    {
        Admob.Instance().removeBanner();
    }
}
