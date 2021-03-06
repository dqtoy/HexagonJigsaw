﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using lib;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class FreedomUIFade : UIFade
{
    //public UIFadeType quit = UIFadeType.XuanZhuanTiaoYue;
    //public UIFadeType easy = UIFadeType.SuoFang;
    //public UIFadeType normal = UIFadeType.SuoFang;
    //public UIFadeType hard = UIFadeType.SuoFang;

    //public float quitStart = 0.0f;
    //public float easyStart = 0.0f;
    //public float normalStart = 0.0f;
    //public float hardStart = 0.0f;

    public RectTransform quitButton;
    public GameObject hex;
    public Image line1;
    public Image line2;

    public RectTransform easyTrans;
    public RectTransform normalTrans;
    public RectTransform hardTrans;

    public Image easyBg;
    public Image easyIcon;
    public Text easyTxt;
    private float easyX;

    public Image normalBg;
    public Image normalIcon;
    public Text normalTxt;
    private float normalX;

    public Image hardBg;
    public Image hardIcon;
    public Text hardTxt;
    private float hardX;

    public GameObject hit1;
    public GameObject hit2;

    private ModuleName moduleName;

    private void Awake()
    {
        easyX = easyTrans.localPosition.x;
        normalX = normalTrans.localPosition.x;
        hardX = hardTrans.localPosition.x;
    }

    override public void FadeOut(ModuleName name)
    {
        if(name == ModuleName.Main)
        {
            quitButton.DOScaleX(0, 0.1f);
            line1.DOFillAmount(0, outTime);
            line2.DOFillAmount(0, outTime).onComplete = TweenComplete;

            DOTween.To(() => hexjig.Start.backgroundInstance.bposition, x => hexjig.Start.backgroundInstance.bposition = x, 0.57f, outTime + inTime);
            DOTween.To(() => hexjig.Start.backgroundInstance.brotation, x => hexjig.Start.backgroundInstance.brotation = x, -18, outTime + inTime);
        }
        else if(name == ModuleName.Game)
        {
            moduleName = name;
            quitButton.DOScaleX(0, 0.1f).onComplete = FadeOut2;

            DOTween.To(() => hexjig.Start.backgroundInstance.bposition, x => hexjig.Start.backgroundInstance.bposition = x, 0.5f, outTime + inTime);
            DOTween.To(() => hexjig.Start.backgroundInstance.brotation, x => hexjig.Start.backgroundInstance.brotation = x, -22, outTime + inTime);
        }
    }

    private void FadeOut2()
    {
        if (moduleName == ModuleName.Game)
        {
            line1.DOFillAmount(0, 0.1f);
            line2.DOFillAmount(0, 0.1f).onComplete = FadeOut3;
        }
    }

    private void FadeOut3()
    {
        if (moduleName == ModuleName.Game)
        {
            hex.GetComponent<RectTransform>().DOLocalMoveY(1100 + UIFix.GetDistanceToTop(), outTime - 0.2f);
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
        hit1.SetActive(false);
        hit2.SetActive(false);
        if (name == ModuleName.Main)
        {
            line1.fillAmount = 0;
            line2.fillAmount = 0;
            hex.transform.GetComponent<RectTransform>().localPosition = new Vector3(205, 807 + UIFix.GetDistanceToTop());

            line1.DOFillAmount(0.56f, inTime).onComplete = LineComplete;
            line2.DOFillAmount(0.83f, inTime);

            easyTrans.localPosition = new Vector3(-188, easyTrans.localPosition.y, easyTrans.localPosition.z);
            normalTrans.localPosition = new Vector3(-82, normalTrans.localPosition.y, normalTrans.localPosition.z);
            hardTrans.localPosition = new Vector3(38, hardTrans.localPosition.y, hardTrans.localPosition.z);
            easyTrans.localScale = new Vector3(1, 1);
            normalTrans.localScale = new Vector3(1, 1);
            hardTrans.localScale = new Vector3(1, 1);

            quitButton.localScale = new Vector3(0, 1);
            quitButton.DOScaleX(1, 0.1f);

            easyTxt.rectTransform.localScale = new Vector3(0, 0);
            easyTxt.rectTransform.DOScale(1, 0.4f).SetDelay(0.3f);
            //easyTxt.DOText("Easy", 2f);

            normalTxt.rectTransform.localScale = new Vector3(0, 0);
            normalTxt.rectTransform.DOScale(1, 0.4f).SetDelay(0.5f);

            hardTxt.rectTransform.localScale = new Vector3(0, 0);
            hardTxt.rectTransform.DOScale(1, 0.4f).SetDelay(0.8f);

            easyIcon.rectTransform.localScale = new Vector3(0, 0);
            easyIcon.rectTransform.DOScale(1, 0.4f).SetDelay(0.2f);

            normalIcon.rectTransform.localScale = new Vector3(0, 0);
            normalIcon.rectTransform.DOScale(1, 0.4f).SetDelay(0.2f);

            hardIcon.rectTransform.localScale = new Vector3(0, 0);
            hardIcon.rectTransform.DOScale(1, 0.4f).SetDelay(0.5f).onComplete = OnFadeInComplete;

            easyBg.rectTransform.localScale = new Vector3(0, 0);
            easyBg.rectTransform.DOScale(1, 0.4f).SetDelay(0.2f);
            easyBg.rectTransform.DOLocalRotate(new Vector3(0, -720, 0), 0.8f).SetDelay(0.5f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.InOutBounce);

            normalBg.rectTransform.localScale = new Vector3(0, 0);
            normalBg.rectTransform.DOScale(1, 0.4f).SetDelay(0.2f);
            normalBg.rectTransform.DOLocalRotate(new Vector3(0, -720, 0), 0.8f).SetDelay(0.5f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.InOutBounce);

            hardBg.rectTransform.localScale = new Vector3(0, 0);
            hardBg.rectTransform.DOScale(1, 0.4f).SetDelay(0.5f);
            hardBg.rectTransform.DOLocalRotate(new Vector3(0, -720, 0), 0.8f).SetDelay(0.5f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.InOutBounce);
        }
        else if(name == ModuleName.Game || name == ModuleName.Result)
        {
            quitButton.localScale = new Vector3(0, 1);
            if (GameVO.Instance.difficulty == DifficultyMode.Easy)
            {
                easyTrans.DOLocalMoveX(easyX, inTime - 0.3f).onComplete = FadeIn2;
            }
            else if (GameVO.Instance.difficulty == DifficultyMode.Normal)
            {
                normalTrans.DOLocalMoveX(normalX, inTime - 0.3f).onComplete = FadeIn2;
            }
            else if (GameVO.Instance.difficulty == DifficultyMode.Hard)
            {
                hardTrans.DOLocalMoveX(hardX, inTime - 0.3f).onComplete = FadeIn2;
            }
        }
    }

    private void OnFadeInComplete()
    {
        GameVO.Instance.dispatcher.DispatchWith(GameEvent.SHOW_MODULE_COMPLETE, ModuleName.Freedom);
    }

    private void LineComplete()
    {
        hit1.SetActive(true);
        hit2.SetActive(true);
    }

    private void FadeIn2()
    {
        if (moduleName == ModuleName.Game || moduleName == ModuleName.Result)
        {
            hex.GetComponent<RectTransform>().DOLocalMoveY(807 + UIFix.GetDistanceToTop(), inTime - 0.2f);
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
            line1.DOFillAmount(0.56f, 0.1f);
            line2.DOFillAmount(0.83f, 0.1f).onComplete = FadeIn4;
        }
    }

    private void FadeIn4()
    {
        if (moduleName == ModuleName.Game || moduleName == ModuleName.Result)
        {
            hit1.SetActive(true);
            hit2.SetActive(true);
            quitButton.DOScaleX(1, 0.1f);
        }
    }
}