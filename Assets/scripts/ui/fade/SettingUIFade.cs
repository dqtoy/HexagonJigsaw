using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using lib;
using DG.Tweening;
using UnityEngine.UI;

public class SettingUIFade : UIFade
{
    public RectTransform line;
    public RectTransform hex;
    public RectTransform quit;
    public RectTransform button;

    private ModuleName moduleName;
    private float offTime = 0.1f;

    override public void FadeOut(ModuleName name)
    {
        if(name == ModuleName.Main)
        {
            line.DOScaleX(0, inTime - offTime);
            button.DOScale(0, inTime - offTime);
            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(hex.DOLocalMoveX(770, inTime - offTime));
            mySequence.Append(quit.DOScaleX(1,offTime)).onComplete = TweenComplete;
        }
    }

    private void TweenComplete()
    {
        dispatcher.DispatchWith(lib.Event.COMPLETE);
    }


    override public void FadeIn(ModuleName name)
    {
        if(name == ModuleName.Main)
        {
            quit.localScale = new Vector3(0, 1);
            line.localScale = new Vector3(0, 1);
            button.localScale = new Vector3(0, 0);
            hex.localPosition = new Vector3(770, -280);

            moduleName = name;
            quit.DOScaleX(1, offTime).onComplete = FadeIn2;

        }
    }

    private void FadeIn2()
    {
        if (moduleName == ModuleName.Main)
        {
            line.DOScaleX(1, inTime - offTime);
            button.DOScale(1, inTime - offTime);
            hex.DOLocalMoveX(205, inTime - offTime);
        }
    }
}
