﻿using System;
using System.Collections;
using System.Collections.Generic;
using lib;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using hexjig;

public class GameUIFade : UIFade
{
    public RectTransform txt;
    public RectTransform operateBg;
    public RectTransform restart;
    public RectTransform restartBg;
    public RectTransform restartIcon;
    public RectTransform tip;
    public RectTransform tipBg;
    public RectTransform tipIcon;
    public RectTransform hex;
    private Tweener restartTween;
    private Tweener tipTween;
    private int restartEffectGap = 10;
    private int tipEffectGap = 10;

    public GameObject result;
    public GameObject effect1;
    public GameObject effect2;
    public GameObject effect3;
    [HideInInspector]
    public int effectCount = 0;

    private ModuleName moduleName;

    private void Awake()
    {
        MainData.Instance.dispatcher.AddListener(hexjig.EventType.SHOW_GAME_CHANGE_OUT_EFFECT0, OnShowGameChangeOut0);
        MainData.Instance.dispatcher.AddListener(hexjig.EventType.SHOW_GAME_CHANGE_OUT_EFFECT, OnShowGameChangeOut);
        MainData.Instance.dispatcher.AddListener(hexjig.EventType.SHOW_GAME_CHANGE_IN_EFFECT, OnShowGameChangeIn);
    }

    private void OnShowGameChangeOut0(lib.Event e)
    {
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(operateBg.DOScaleY(0, 0.4f));
        mySequence.Append(operateBg.DOScaleY(1, 0.4f)).onComplete = OnShowGameChangeInComplete0;
        txt.DOScaleX(0, 0.4f);
    }

    private void OnShowGameChangeOut(lib.Event e)
    {
        tip.gameObject.SetActive(true);
        restart.gameObject.SetActive(true);
        result.SetActive(false);
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(operateBg.DOScaleY(0, 0.4f));
        mySequence.Append(operateBg.DOScaleY(1, 0.4f)).onComplete = OnShowGameChangeInComplete;
        txt.DOScaleX(0, 0.4f);
    }

    private void OnShowGameChangeIn(lib.Event e)
    {
        txt.DOScaleX(1, 0.2f);
    }

    private void OnShowGameChangeInComplete0()
    {
        tip.gameObject.SetActive(false);
        restart.gameObject.SetActive(false);
        result.SetActive(true);
        effect2.SetActive(false);
        effect3.SetActive(false);
        if (effectCount > 1)
        {
            Invoke("playEffect2", 0.5f);
        }
        if (effectCount > 2)
        {
            Invoke("playEffect3", 1);
        }
    }

    private void OnShowGameChangeInComplete()
    {
        MainData.Instance.dispatcher.DispatchWith(hexjig.EventType.SHOW_GAME_CHANGE_OUT_EFFECT_COMPLETE);
    }

    private void playEffect2()
    {
        effect2.SetActive(true);
    }

    private void playEffect3()
    {
        effect3.SetActive(true);
    }

    override public void FadeOut(ModuleName name)
    {
        moduleName = name;
        if (name == ModuleName.Freedom || name == ModuleName.Daily)
        {
            restart.DOLocalMoveX(274 + 459 + 325, outTime * 0.5f);
            tip.DOLocalMoveX(-700, outTime * 0.5f).onComplete = FadeOut2;
            if (name == ModuleName.Daily)
            {
                DOTween.To(() => hexjig.Start.backgroundInstance.bposition, x => hexjig.Start.backgroundInstance.bposition = x, 0.58f, outTime + inTime);
                DOTween.To(() => hexjig.Start.backgroundInstance.brotation, x => hexjig.Start.backgroundInstance.brotation = x, -127, outTime + inTime);
            }
            else if (name == ModuleName.Freedom)
            {
                DOTween.To(() => hexjig.Start.backgroundInstance.bposition, x => hexjig.Start.backgroundInstance.bposition = x, 0.6f, outTime + inTime);
                DOTween.To(() => hexjig.Start.backgroundInstance.brotation, x => hexjig.Start.backgroundInstance.brotation = x, 127, outTime + inTime);
            }
        }
        else if(name == ModuleName.Result)
        {
            MainData.Instance.showCutRoot.transform.DOScale(new Vector3(0.5f,0.5f,1.0f), 0.26f).onComplete = FadeOut2;

            DOTween.To(() => hexjig.Start.backgroundInstance.bposition, x => hexjig.Start.backgroundInstance.bposition = x, 0.62f, outTime + inTime);
            DOTween.To(() => hexjig.Start.backgroundInstance.brotation, x => hexjig.Start.backgroundInstance.brotation = x, 67, outTime + inTime);
        }
    }

    private void FadeOut2()
    {
        if (moduleName == ModuleName.Freedom || moduleName == ModuleName.Daily)
        {
            txt.DOScaleX(0, outTime * 0.5f);
            operateBg.DOScaleY(0, outTime * 0.5f);
            hex.DOLocalMoveX(-500, outTime * 0.5f).onComplete = TweenComplete;
        }
        else if(moduleName == ModuleName.Result)
        {
            txt.DOScaleX(0, outTime  - 0.1f);
            operateBg.DOScaleY(0, outTime - 0.1f);
            hex.DOLocalMoveX(-500, outTime - 0.1f).onComplete = TweenComplete;
        }
    }

    private void TweenComplete()
    {
        dispatcher.DispatchWith(lib.Event.COMPLETE);
    }

    override public void FadeIn(ModuleName name)
    {
        tip.gameObject.SetActive(true);
        restart.gameObject.SetActive(true);
        result.SetActive(false);
        PlayTipEffect();
        PlayRestartEffect();
        moduleName = name;
        if (name == ModuleName.Freedom || name == ModuleName.Daily)
        {
            MainData.Instance.dispatcher.DispatchWith(hexjig.EventType.HIDE_GAME);

            txt.localScale = new Vector3(0, 1);
            operateBg.localScale = new Vector3(1, 0);
            restart.localPosition = new Vector3(459, restart.localPosition.y);
            tip.localPosition = new Vector3(274 + 459 + 325, restart.localPosition.y);
            hex.localPosition = new Vector3(-500, hex.localPosition.y);

            txt.DOScaleX(1,inTime * 0.5f);
            operateBg.DOScaleY(1,inTime * 0.5f);
            hex.DOLocalMoveX(-335, inTime * 0.5f).onComplete = FadeIn2;
        }
    }

    private void Update()
    {
        tipEffectGap--;
        if(tipEffectGap == 0)
        {
            PlayTipEffect();
        }
        restartEffectGap--;
        if(restartEffectGap == 0)
        {
            PlayRestartEffect();
        }
    }

    private void FadeIn2()
    {
        if(moduleName == ModuleName.Freedom || moduleName == ModuleName.Daily)
        {
            restart.DOLocalMoveX(-325, inTime * 0.5f);
            tip.DOLocalMoveX(274, inTime * 0.5f);

            MainData.Instance.dispatcher.DispatchWith(hexjig.EventType.SHOW_START_EFFECT);
            
        }
    }

    private void PlayTipEffect()
    {
        tipTween = tipIcon.DOLocalMove(new Vector3(0, 25), 0.3f).SetDelay(1.5f).SetLoops(4, LoopType.Yoyo).SetEase(Ease.InCirc);
        tipTween.onComplete = PlayTipEffectComplete;
        tipBg.DOShakeScale(1.0f, new Vector3(0.3f, 0.3f), 4, 10).SetDelay(0.5f).SetEase(Ease.InOutQuart);
        tipEffectGap = 60 * UnityEngine.Random.Range(10, 20);
    }

    private void PlayTipEffectComplete()
    {
        tipTween = null;
    }

    private void PlayRestartEffect()
    {
        restartTween = restartIcon.DOLocalMove(new Vector3(0, 13), 0.3f).SetDelay(1.5f).SetLoops(4, LoopType.Yoyo).SetEase(Ease.InCirc);
        restartTween.onComplete = PlayRestartEffectComplete;
        restartBg.DOShakeScale(1.0f, new Vector3(0.1f, 0.1f), 4, 10).SetDelay(0.5f).SetEase(Ease.InOutQuart);
        restartEffectGap = 60 * UnityEngine.Random.Range(10, 20);
    }

    private void PlayRestartEffectComplete()
    {
        restartTween = null;
    }
}