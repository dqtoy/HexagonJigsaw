using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using System;

public class ShopUIFade : UIFade
{
    private bool fadeOutFlag = false;
    private DateTime fadeOutTime;
    private DOTweenAnimation[] animations;
    private bool first = true;

    private void Awake()
    {
        animations = GetComponentsInChildren<DOTweenAnimation>();
    }


    override public void FadeIn(ModuleName name)
    {
        if(first)
        {
            first = false;
            return;
        }
        Debug.Log("FadeIn");
        for (int i = 0; i < animations.Length; i++)
        {
            animations[i].DOPlayForward();
        }
    }

    override public void FadeOut(ModuleName name)
    {
        fadeOutFlag = true;
        fadeOutTime = System.DateTime.Now;
        Debug.Log("FadeOut");
        for (int i = 0; i < animations.Length; i++)
        {
            animations[i].DOPlayBackwards();
        }
    }

    private void TweenComplete()
    {
        dispatcher.DispatchWith(lib.Event.COMPLETE);
    }

    private void Update()
    {
        int time = GameVO.Instance.hasBuyMusic.value ? 500 : 1000;
        if (fadeOutFlag && System.DateTime.Now.Subtract(fadeOutTime).TotalMilliseconds > time)
        {
            fadeOutFlag = false;
            TweenComplete();
        }
    }
}
