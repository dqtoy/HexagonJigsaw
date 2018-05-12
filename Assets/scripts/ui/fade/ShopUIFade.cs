﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using System;
using UnityEngine.UI;

public class ShopUIFade : UIFade
{
    private bool fadeOutFlag = false;
    private DateTime fadeOutTime;
    private DOTweenAnimation[] animations;
    private bool first = true;

    public Image line1;
    public Image line2;
    public RectTransform quit;
    public Transform hit1;
    public Transform hit2;

    private void Awake()
    {
        UIFix.SetDistanceToTop(line2.rectTransform);
        UIFix.SetDistanceToTop(hit2);
        UIFix.SetDistanceToTop(quit);
        UIFix.SetDistanceToBottom(line1.rectTransform);
        UIFix.SetDistanceToBottom(hit1);


        animations = GetComponentsInChildren<DOTweenAnimation>();
    }


    override public void FadeIn(ModuleName name)
    {
        if(first)
        {
            first = false;
            return;
        }
        for (int i = 0; i < animations.Length; i++)
        {
            animations[i].DOPlayForward();
        }

        hit1.gameObject.SetActive(false);
        hit2.gameObject.SetActive(false);
        line1.fillAmount = 0;
        line2.fillAmount = 0;
        quit.localScale = new Vector3(0, 1);
        quit.DOScaleX(1, 0.1f).onComplete = FadeIn2;
    }

    private void FadeIn2()
    {
        line1.DOFillAmount(0.61f, 0.3f);
        line2.DOFillAmount(0.61f, 0.3f).onComplete = FadeIn3;
    }

    private void FadeIn3()
    {
        hit1.gameObject.SetActive(true);
        hit2.gameObject.SetActive(true);
    }

    override public void FadeOut(ModuleName name)
    {
        DOTween.To(() => hexjig.Start.backgroundInstance.bposition, x => hexjig.Start.backgroundInstance.bposition = x, 0.57f, outTime + inTime);
        DOTween.To(() => hexjig.Start.backgroundInstance.brotation, x => hexjig.Start.backgroundInstance.brotation = x, -18, outTime + inTime);

        line1.DOFillAmount(0, 0.3f);
        line2.DOFillAmount(0, 0.3f);
        quit.DOScaleX(0, 0.1f);

        fadeOutFlag = true;
        fadeOutTime = System.DateTime.Now;
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
