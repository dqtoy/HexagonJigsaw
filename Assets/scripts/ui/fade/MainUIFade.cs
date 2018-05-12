using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using lib;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class MainUIFade : UIFade {

    public GameObject title;
    public Image line;
    public GameObject hex;
    public GameObject hex2;
    public Image freedomIcon;
    public GameObject freedomBg;
    public Image dailyIcon;
    public GameObject dailyBg;
    public GameObject setting;
    public GameObject shop;

    public RectTransform dailyLevelTxt;
    public Text dailyTimeTxt;
    public GameObject effect6;
    public GameObject effect9;
    public GameObject effect10;
    public GameObject hitEffect;

    private ModuleName moduleName;

    override public void FadeOut(ModuleName name)
    { 
        if(name == ModuleName.Freedom)
        {
            DOTween.To(() => hexjig.Start.backgroundInstance.bposition, x => hexjig.Start.backgroundInstance.bposition = x, 0.6f, outTime + inTime);
            DOTween.To(() => hexjig.Start.backgroundInstance.brotation, x => hexjig.Start.backgroundInstance.brotation = x, 127, outTime + inTime);
            line.DOFillAmount(0, outTime);
            title.transform.GetComponent<RectTransform>().DOLocalMoveX(-900, outTime);
            hex2.transform.GetComponent<RectTransform>().DORotate(new Vector3(0, 0, -180), outTime);
            freedomIcon.DOColor(new Color(1, 1, 1, 0), outTime);
            freedomBg.GetComponent<RectTransform>().DORotate(new Vector3(0, 0, 0), outTime);
            hex.transform.GetComponent<RectTransform>().DOLocalMoveY(807 + UIFix.GetDistanceToTop(), outTime).onComplete = TweenComplete;
        }
        else if (name == ModuleName.Setting)
        {
            DOTween.To(() => hexjig.Start.backgroundInstance.bposition, x => hexjig.Start.backgroundInstance.bposition = x, 0.48f, outTime + inTime);
            DOTween.To(() => hexjig.Start.backgroundInstance.brotation, x => hexjig.Start.backgroundInstance.brotation = x, 158, outTime + inTime);
            setting.transform.parent = hex.transform.parent;

            float offTime = 0.1f;
            
            line.DOFillAmount(0, outTime - offTime);
            title.transform.GetComponent<RectTransform>().DOLocalMoveX(-900, outTime - offTime);
            hex2.transform.GetComponent<RectTransform>().DORotate(new Vector3(0, 0, -180), outTime - offTime);
            freedomIcon.DOColor(new Color(1, 1, 1, 0), outTime - offTime);
            freedomBg.GetComponent<RectTransform>().DORotate(new Vector3(0, 0, 0), outTime - offTime);
            hex.transform.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, 0, 270), outTime - offTime);
            hex.transform.GetComponent<RectTransform>().DOLocalMoveY(1100 + UIFix.GetDistanceToTop(), outTime - offTime);
            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(setting.transform.GetComponent<RectTransform>().DOLocalMoveY(524 + UIFix.GetOffy(), outTime - offTime));
            mySequence.Append(setting.transform.GetComponent<RectTransform>().DOScaleX(0, offTime)).onComplete = TweenComplete;
        }
        else if(name == ModuleName.Daily)
        {
            DOTween.To(() => hexjig.Start.backgroundInstance.bposition, x => hexjig.Start.backgroundInstance.bposition = x, 0.58f, outTime + inTime);
            DOTween.To(() => hexjig.Start.backgroundInstance.brotation, x => hexjig.Start.backgroundInstance.brotation = x, -127, outTime + inTime);
            //205,112   120
            //-223,653   30
            float offTime = outTime * 0.5f;
            line.DOFillAmount(0, offTime);
            title.transform.GetComponent<RectTransform>().DOLocalMoveX(-900, offTime);
            hex2.transform.GetComponent<RectTransform>().DORotate(new Vector3(0, 0, -180), offTime);
            dailyIcon.DOColor(new Color(1, 1, 1, 0), outTime);
            //dailyBg.GetComponent<RectTransform>().DORotate(new Vector3(0, 0, 30), outTime);
            hex.transform.GetComponent<RectTransform>().DOLocalMove(new Vector3(-225, 652 + UIFix.GetDistanceToTop()), outTime);
            hex.transform.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, 0, 120), outTime).onComplete = TweenComplete;
        }
        else if (name == ModuleName.Shop)
        {
            DOTween.To(() => hexjig.Start.backgroundInstance.bposition, x => hexjig.Start.backgroundInstance.bposition = x, 0.48f, outTime + inTime);
            DOTween.To(() => hexjig.Start.backgroundInstance.brotation, x => hexjig.Start.backgroundInstance.brotation = x, -90, outTime + inTime);
            shop.transform.parent = hex.transform.parent;

            float offTime = 0.1f;

            line.DOFillAmount(0, outTime - offTime);
            title.transform.GetComponent<RectTransform>().DOLocalMoveX(-900, outTime - offTime);
            hex2.transform.GetComponent<RectTransform>().DORotate(new Vector3(0, 0, -180), outTime - offTime);
            freedomIcon.DOColor(new Color(1, 1, 1, 0), outTime - offTime);
            freedomBg.GetComponent<RectTransform>().DORotate(new Vector3(0, 0, 0), outTime - offTime);
            hex.transform.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, 0, 270), outTime - offTime);
            hex.transform.GetComponent<RectTransform>().DOLocalMoveY(1100 + UIFix.GetDistanceToTop(), outTime - offTime);
            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(shop.transform.GetComponent<RectTransform>().DOLocalMove(new Vector2(-271.7311f, 524 + UIFix.GetOffy()), outTime - offTime));
            mySequence.Append(shop.transform.GetComponent<RectTransform>().DOScaleX(0, offTime)).onComplete = TweenComplete;
        }
    }

    override public void FadeIn(ModuleName name)
    {
        hitEffect.SetActive(false);
        dailyLevelTxt.gameObject.SetActive(true);
        if (GameVO.Instance.daily.levels[9].pass)
        {
            dailyIcon.gameObject.SetActive(false);
            effect6.SetActive(false);
            effect9.SetActive(false);
            effect10.SetActive(true);
            dailyLevelTxt.localPosition = new Vector3(2, -25);
            dailyLevelTxt.gameObject.SetActive(false);
            dailyTimeTxt.gameObject.SetActive(true);
        }
        else if (GameVO.Instance.daily.levels[8].pass)
        {
            dailyIcon.gameObject.SetActive(false);
            effect6.SetActive(false);
            effect9.SetActive(true);
            effect10.SetActive(false);
            dailyTimeTxt.gameObject.SetActive(false);
        }
        else if (GameVO.Instance.daily.levels[5].pass)
        {
            dailyIcon.gameObject.SetActive(true);
            effect6.SetActive(true);
            effect9.SetActive(false);
            effect10.SetActive(false);
            dailyTimeTxt.gameObject.SetActive(false);
        }
        else
        {
            dailyIcon.gameObject.SetActive(true);
            effect6.SetActive(false);
            effect9.SetActive(false);
            effect10.SetActive(false);
            dailyTimeTxt.gameObject.SetActive(false);
        }
        if (name == ModuleName.Freedom || name == ModuleName.Result && GameVO.Instance.model == GameModel.Freedom)
        {
            line.fillAmount = 0;
            title.transform.GetComponent<RectTransform>().localPosition = new Vector3(-900, 479 + UIFix.GetDistanceToTop());
            hex2.transform.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, -180);
            freedomIcon.color = new Color(1, 1, 1, 0);
            freedomBg.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 0);
            hex.transform.GetComponent<RectTransform>().localPosition = new Vector3(205, 807 + UIFix.GetDistanceToTop());
            
            line.DOFillAmount(0.64f, inTime).onComplete = LineComplete;
            title.transform.GetComponent<RectTransform>().DOLocalMoveX(-359, inTime);
            hex2.transform.GetComponent<RectTransform>().DORotate(new Vector3(0, 0, 0), inTime);
            freedomIcon.DOColor(new Color(1, 1, 1, 1), inTime);
            freedomBg.GetComponent<RectTransform>().DORotate(new Vector3(0, 0, 0), inTime);
            hex.transform.GetComponent<RectTransform>().DOLocalMoveY(112, inTime);
        }
        else if (name == ModuleName.Setting || name == ModuleName.Shop || name == ModuleName.None)
        {
            float offTime = 0.1f;

            line.fillAmount = 0;
            title.transform.GetComponent<RectTransform>().localPosition = new Vector3(-900, 479 + UIFix.GetDistanceToTop());
            hex2.transform.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, -180);
            freedomIcon.color = new Color(1, 1, 1, 0);
            freedomBg.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 0);
            hex.transform.GetComponent<RectTransform>().localPosition = new Vector3(205, 1100 + UIFix.GetDistanceToTop());
            if(name == ModuleName.Setting)
            {
                setting.transform.GetComponent<RectTransform>().localPosition = new Vector3(setting.transform.GetComponent<RectTransform>().localPosition.x, 524 + UIFix.GetOffy());
                setting.transform.GetComponent<RectTransform>().DOScaleX(1, offTime).onComplete = FadeIn2;
            }
            else if(name == ModuleName.Shop)
            {
                shop.transform.GetComponent<RectTransform>().localPosition = new Vector3(shop.transform.GetComponent<RectTransform>().localPosition.x, 524 + UIFix.GetOffy());
                shop.transform.GetComponent<RectTransform>().DOScaleX(1, offTime).onComplete = FadeIn2;
            }
            else
            {
                FadeIn2();
            }
            moduleName = name;
        }
        else if(name == ModuleName.Daily || name == ModuleName.Result && GameVO.Instance.model == GameModel.Daily)
        {
            line.fillAmount = 0;
            title.transform.GetComponent<RectTransform>().localPosition = new Vector3(-900, 479 + UIFix.GetDistanceToTop());
            hex2.transform.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, -180);
            dailyIcon.color = new Color(1, 1, 1, 0);
            dailyBg.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 0);
            hex.transform.GetComponent<RectTransform>().localPosition = new Vector3(-225, 652 + UIFix.GetDistanceToTop());
            hex.transform.GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 0, 120);

            line.DOFillAmount(0.64f, inTime).onComplete = LineComplete;
            title.transform.GetComponent<RectTransform>().DOLocalMoveX(-359, inTime);
            hex2.transform.GetComponent<RectTransform>().DORotate(new Vector3(0, 0, 0), inTime);
            dailyIcon.DOColor(new Color(1, 1, 1, 1), inTime);
            dailyBg.GetComponent<RectTransform>().DORotate(new Vector3(0, 0, 0), inTime);
            hex.transform.GetComponent<RectTransform>().DOLocalMove(new Vector3(205,112), inTime);
            hex.transform.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, 0, 0), inTime);
        }
    }

    private void LineComplete()
    {
        hitEffect.SetActive(true);
    }

    private void FadeIn2()
    {
        if(moduleName == ModuleName.Setting || moduleName == ModuleName.Shop || moduleName == ModuleName.None)
        {
            float offTime = 0.1f;

            line.DOFillAmount(0.64f, inTime - offTime).onComplete = LineComplete;
            title.transform.GetComponent<RectTransform>().DOLocalMoveX(-359, inTime - offTime);
            hex2.transform.GetComponent<RectTransform>().DORotate(new Vector3(0, 0, 0), inTime - offTime);
            freedomIcon.DOColor(new Color(1, 1, 1, 1), inTime - offTime);
            freedomBg.GetComponent<RectTransform>().DORotate(new Vector3(0, 0, 0), inTime - offTime);
            hex.transform.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, 0, 0), inTime - offTime);
            hex.transform.GetComponent<RectTransform>().DOLocalMoveY(112, inTime - offTime);
            if (moduleName == ModuleName.Setting)
            {
                setting.transform.GetComponent<RectTransform>().DOLocalMove(new Vector3(-271.7311f, -508.8597f + UIFix.GetDistanceToBottom()), inTime - offTime).onComplete = FadeIn3;
            }
            else if(moduleName == ModuleName.Shop)
            {
                shop.transform.GetComponent<RectTransform>().DOLocalMove(new Vector3(-128.9411f, -552.4307f + UIFix.GetDistanceToBottom()), inTime - offTime).onComplete = FadeIn3;
            }
        }
    }

    private void FadeIn3()
    {
        if (moduleName == ModuleName.Setting)
        {
            setting.transform.parent = hex2.transform;
        }
        else if (moduleName == ModuleName.Shop)
        {
            shop.transform.parent = hex2.transform;
        }
    }

    private void TweenComplete()
    {
        dispatcher.DispatchWith(lib.Event.COMPLETE);
    }
}