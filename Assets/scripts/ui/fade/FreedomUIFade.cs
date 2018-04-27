using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using lib;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class FreedomUIFade : UIFade
{
    public UIFadeType quit = UIFadeType.XuanZhuanTiaoYue;
    public UIFadeType easy = UIFadeType.SuoFang;
    public UIFadeType normal = UIFadeType.SuoFang;
    public UIFadeType hard = UIFadeType.SuoFang;

    public float quitStart = 0.0f;
    public float easyStart = 0.0f;
    public float normalStart = 0.0f;
    public float hardStart = 0.0f;

    public Image quitButton;
    public GameObject hex;
    public GameObject line1;
    public GameObject line2;

    public RectTransform easyTrans;
    public RectTransform normalTrans;
    public RectTransform hardTrans;

    public Image easyBg;
    public Image easyIcon;
    public Text easyTxt;

    public Image normalBg;
    public Image normalIcon;
    public Text normalTxt;

    public Image hardBg;
    public Image hardIcon;
    public Text hardTxt;

    private ModuleName moduleName;

    override public void FadeOut(ModuleName name)
    {
        if(name == ModuleName.Main)
        {
            line1.transform.GetComponent<RectTransform>().DOSizeDelta(new Vector2(0, 9), outTime);
            line2.transform.GetComponent<RectTransform>().DOSizeDelta(new Vector2(0, 9), outTime).onComplete = TweenComplete;
        }
        else if(name == ModuleName.Game)
        {
            moduleName = name;
            quitButton.GetComponent<RectTransform>().DOScaleX(0, 0.1f).onComplete = FadeOut2;
        }
    }

    private void FadeOut2()
    {
        if (moduleName == ModuleName.Game)
        {
            line1.GetComponent<RectTransform>().DOScaleX(0, 0.1f);
            line2.GetComponent<RectTransform>().DOScaleX(0, 0.1f).onComplete = FadeOut3;
        }
    }

    private void FadeOut3()
    {
        if (moduleName == ModuleName.Game)
        {
            hex.GetComponent<RectTransform>().DOLocalMoveY(1100, outTime - 0.2f);
            if (GameVO.Instance.difficulty == DifficultyMode.Easy)
            {
                easyTrans.DOLocalRotate(new Vector3(0, 0, 30), 0.1f).onComplete = FadeOut4;
                normalTrans.DOScaleX(0, 0.1f);
                hardTrans.DOScaleX(0, 0.1f);
            }
            else if (GameVO.Instance.difficulty == DifficultyMode.Normal)
            {
                normalTrans.DOLocalRotate(new Vector3(0, 0, 30), 0.1f).onComplete = FadeOut4;
                easyTrans.DOScaleX(0, 0.1f);
                hardTrans.DOScaleX(0, 0.1f);
            }
            else if (GameVO.Instance.difficulty == DifficultyMode.Hard)
            {
                hardTrans.DOLocalRotate(new Vector3(0, 0, 30), 0.1f).onComplete = FadeOut4;
                easyTrans.DOScaleX(0, 0.1f);
                normalTrans.DOScaleX(0, 0.1f);
            }
        }
    }

    private void FadeOut4()
    {
        if (moduleName == ModuleName.Game)
        {
            if (GameVO.Instance.difficulty == DifficultyMode.Easy)
            {
                easyTrans.DOLocalMoveX(-500, outTime - 0.3f).onComplete = TweenComplete;
            }
            else if (GameVO.Instance.difficulty == DifficultyMode.Normal)
            {
                normalTrans.DOLocalMoveX(-500, outTime - 0.3f).onComplete = TweenComplete;
            }
            else if (GameVO.Instance.difficulty == DifficultyMode.Hard)
            {
                hardTrans.DOLocalMoveX(520, outTime - 0.3f).onComplete = TweenComplete;
            }
        }
    }

    private void TweenComplete()
    {
        dispatcher.DispatchWith(lib.Event.COMPLETE);
    }


    override public void FadeIn(ModuleName name)
    {
        moduleName = name;
        if(name == ModuleName.Main)
        {
            line1.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 9);
            line2.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 9);
            hex.transform.GetComponent<RectTransform>().localPosition = new Vector3(205, 773);

            line1.transform.GetComponent<RectTransform>().DOSizeDelta(new Vector2(400, 9), inTime);
            line2.transform.GetComponent<RectTransform>().DOSizeDelta(new Vector2(1016, 9), inTime);

            easyTxt.rectTransform.localScale = new Vector3(0, 0);
            easyTxt.rectTransform.DOScale(1, 0.6f).SetDelay(0.5f);

            normalTxt.rectTransform.localScale = new Vector3(0, 0);
            normalTxt.rectTransform.DOScale(1, 0.6f).SetDelay(0.5f);

            hardTxt.rectTransform.localScale = new Vector3(0, 0);
            hardTxt.rectTransform.DOScale(1, 0.6f).SetDelay(0.5f);

            easyIcon.rectTransform.localScale = new Vector3(0, 0);
            easyIcon.rectTransform.DOScale(1, 0.6f).SetDelay(0.5f);

            normalIcon.rectTransform.localScale = new Vector3(0, 0);
            normalIcon.rectTransform.DOScale(1, 0.6f).SetDelay(0.5f);

            hardIcon.rectTransform.localScale = new Vector3(0, 0);
            hardIcon.rectTransform.DOScale(1, 0.6f).SetDelay(1.2f);

            easyBg.rectTransform.localScale = new Vector3(0, 0);
            easyBg.rectTransform.DOScale(1, 0.6f).SetDelay(0.5f);
            easyBg.rectTransform.DOLocalRotate(new Vector3(0, -720, 0), 1.2f).SetDelay(0.8f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.InOutBounce);

            normalBg.rectTransform.localScale = new Vector3(0, 0);
            normalBg.rectTransform.DOScale(1, 0.6f).SetDelay(0.5f);
            normalBg.rectTransform.DOLocalRotate(new Vector3(0, -720, 0), 1.2f).SetDelay(0.8f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.InOutBounce);

            hardBg.rectTransform.localScale = new Vector3(0, 0);
            hardBg.rectTransform.DOScale(1, 0.6f).SetDelay(1.33f);
            hardBg.rectTransform.DOLocalRotate(new Vector3(0, -720, 0), 1.2f).SetDelay(1.33f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.InOutBounce);
        }
        else if(name == ModuleName.Game || name == ModuleName.Result)
        {
            if (GameVO.Instance.difficulty == DifficultyMode.Easy)
            {
                easyTrans.DOLocalMoveX(-188, inTime - 0.3f).onComplete = FadeIn2;
            }
            else if (GameVO.Instance.difficulty == DifficultyMode.Normal)
            {
                normalTrans.DOLocalMoveX(-59, inTime - 0.3f).onComplete = FadeIn2;
            }
            else if (GameVO.Instance.difficulty == DifficultyMode.Hard)
            {
                hardTrans.DOLocalMoveX(208, inTime - 0.3f).onComplete = FadeIn2;
            }
        }
    }

    private void FadeIn2()
    {
        if (moduleName == ModuleName.Game || moduleName == ModuleName.Result)
        {
            hex.GetComponent<RectTransform>().DOLocalMoveY(773, inTime - 0.2f);
            if (GameVO.Instance.difficulty == DifficultyMode.Easy)
            {
                easyTrans.DOLocalRotate(new Vector3(0, 0, 0), 0.1f).onComplete = FadeIn3;
                normalTrans.DOScaleX(1, 0.1f);
                hardTrans.DOScaleX(1, 0.1f);
            }
            else if (GameVO.Instance.difficulty == DifficultyMode.Normal)
            {
                normalTrans.DOLocalRotate(new Vector3(0, 0, 0), 0.1f).onComplete = FadeIn3;
                easyTrans.DOScaleX(1, 0.1f);
                hardTrans.DOScaleX(1, 0.1f);
            }
            else if (GameVO.Instance.difficulty == DifficultyMode.Hard)
            {
                hardTrans.DOLocalRotate(new Vector3(0, 0, 0), 0.1f).onComplete = FadeIn3;
                easyTrans.DOScaleX(1, 0.1f);
                normalTrans.DOScaleX(1, 0.1f);
            }
        }
    }

    private void FadeIn3()
    {
        if (moduleName == ModuleName.Game || moduleName == ModuleName.Result)
        {
            line1.GetComponent<RectTransform>().DOScaleX(1, 0.1f);
            line2.GetComponent<RectTransform>().DOScaleX(1, 0.1f).onComplete = FadeIn4;
        }
    }

    private void FadeIn4()
    {
        if (moduleName == ModuleName.Game || moduleName == ModuleName.Result)
        {
            quitButton.GetComponent<RectTransform>().DOScaleX(1, 0.1f);
        }
    }
}