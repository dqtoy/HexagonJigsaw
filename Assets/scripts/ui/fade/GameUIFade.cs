using System;
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
    public RectTransform tip;
    public RectTransform hex;

    private ModuleName moduleName;

    override public void FadeOut(ModuleName name)
    {
        moduleName = name;
        if (name == ModuleName.Freedom || name == ModuleName.Daily)
        {
            restart.DOLocalMoveX(274 + 459 + 325, outTime * 0.5f);
            tip.DOLocalMoveX(-700, outTime * 0.5f).onComplete = FadeOut2;
        }
        else if(name == ModuleName.Result)
        {
            MainData.Instance.showCutRoot.transform.DOScale(new Vector3(0.5f,0.5f,1.0f), 0.25f).onComplete = FadeOut2;
        }
    }

    private void FadeOut2()
    {
        if (moduleName == ModuleName.Freedom || moduleName == ModuleName.Daily)
        {
            txt.DOScaleX(0, outTime * 0.5f);
            operateBg.DOScaleY(0, outTime * 0.5f);
            hex.DOLocalMoveX(-700, outTime * 0.5f).onComplete = TweenComplete;
        }
        else if(moduleName == ModuleName.Result)
        {
            txt.DOScaleX(0, outTime  - 0.1f);
            operateBg.DOScaleY(0, outTime - 0.1f);
            hex.DOLocalMoveX(-700, outTime - 0.1f).onComplete = TweenComplete;
        }
    }

    private void TweenComplete()
    {
        dispatcher.DispatchWith(lib.Event.COMPLETE);
    }

    override public void FadeIn(ModuleName name)
    {
        moduleName = name;
        if (name == ModuleName.Freedom || name == ModuleName.Daily)
        {
            MainData.Instance.dispatcher.DispatchWith(hexjig.EventType.HIDE_GAME);

            txt.localScale = new Vector3(0, 1);
            operateBg.localScale = new Vector3(1, 0);
            restart.localPosition = new Vector3(459, restart.localPosition.y);
            tip.localPosition = new Vector3(274 + 459 + 325, restart.localPosition.y);
            hex.localPosition = new Vector3(-700, 723);

            txt.DOScaleX(1,inTime * 0.5f);
            operateBg.DOScaleY(1,inTime * 0.5f);
            hex.DOLocalMoveX(-582, inTime * 0.5f).onComplete = FadeIn2;
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
}