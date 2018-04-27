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

            DoEffect(easy, easyBg, 0, true);
            DoEffect(easy, easyIcon, 0, true);
            DoEffect(easy, easyTxt, 0, true);

            DoEffect(normal, normalBg, 0, true);
            DoEffect(normal, normalIcon, 0, true);
            DoEffect(normal, normalTxt, 0, true);

            DoEffect(hard, hardBg, 0, true);
            DoEffect(hard, hardIcon, 0, true);
            DoEffect(hard, hardTxt, 0, true);

            DoEffect(quit, quitButton, 0, true);
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

            DoEffect(easy, easyBg, easyStart);
            DoEffect(easy, easyIcon, easyStart);
            DoEffect(easy, easyTxt, easyStart);

            DoEffect(normal, normalBg, normalStart);
            DoEffect(normal, normalIcon, normalStart);
            DoEffect(normal, normalTxt, normalStart);

            DoEffect(hard, hardBg, hardStart);
            DoEffect(hard, hardIcon, hardStart);
            DoEffect(hard, hardTxt, hardStart);

            DoEffect(quit, quitButton, quitStart);
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