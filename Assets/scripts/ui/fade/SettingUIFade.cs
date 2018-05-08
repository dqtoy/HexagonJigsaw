using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using lib;
using DG.Tweening;
using UnityEngine.UI;

public class SettingUIFade : UIFade
{
    public Image line;
    public RectTransform hex;
    public RectTransform quit;
    public RectTransform button;

    private ModuleName moduleName;
    private float offTime = 0.1f;

    override public void FadeOut(ModuleName name)
    {
        if(name == ModuleName.Main)
        {
            DOTween.To(() => hexjig.Start.backgroundInstance.bposition, x => hexjig.Start.backgroundInstance.bposition = x, 0.57f, outTime + inTime);
            DOTween.To(() => hexjig.Start.backgroundInstance.brotation, x => hexjig.Start.backgroundInstance.brotation = x, -18, outTime + inTime);
            line.DOFillAmount(0, outTime - offTime);
            button.DOScale(0, outTime - offTime);
            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(hex.DOLocalMoveX(770, outTime - offTime));
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
            line.fillAmount = 0;
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
            line.DOFillAmount(1, inTime - offTime);
            button.DOScale(1, inTime - offTime);
            hex.DOLocalMoveX(205, inTime - offTime);
        }
    }
}
