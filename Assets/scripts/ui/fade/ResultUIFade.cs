using System;
using System.Collections;
using System.Collections.Generic;
using lib;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using hexjig;

public class ResultUIFade : UIFade {

    public RectTransform time;
    public RectTransform model;
    public Image line1;
    public Image line2;
    public RectTransform hex;
    public RectTransform quit;
    public RectTransform home;
    public RectTransform share;
    public RectTransform root;

    private ModuleName moduleName;

    public GameObject hit1;
    public GameObject hit2;

    override public void FadeOut(ModuleName name)
    {
        moduleName = name;
        MainData.Instance.showCutRoot.transform.DOMove(new Vector3(1.03f, -10, 100), 0.3f);
        MainData.Instance.showCutRoot.transform.DOLocalRotate(new Vector3(0, 0, -50), 0.3f);
        time.DOScaleX(0, 0.3f);
        model.DOScaleX(0, 0.3f);
        hex.DOScaleX(0, 0.3f);
        hex.DOScaleY(0, 0.3f);
        quit.DOScaleX(0, 0.3f).onComplete = TweenComplete;
        if(name == ModuleName.Main)
        {
            DOTween.To(() => hexjig.Start.backgroundInstance.bposition, x => hexjig.Start.backgroundInstance.bposition = x, 0.57f, outTime + inTime);
            DOTween.To(() => hexjig.Start.backgroundInstance.brotation, x => hexjig.Start.backgroundInstance.brotation = x, -18, outTime + inTime);
        }
        else if(name == ModuleName.Daily)
        {
            DOTween.To(() => hexjig.Start.backgroundInstance.bposition, x => hexjig.Start.backgroundInstance.bposition = x, 0.58f, outTime + inTime);
            DOTween.To(() => hexjig.Start.backgroundInstance.brotation, x => hexjig.Start.backgroundInstance.brotation = x, -127, outTime + inTime);
        }
        else if(name == ModuleName.Freedom)
        {
            DOTween.To(() => hexjig.Start.backgroundInstance.bposition, x => hexjig.Start.backgroundInstance.bposition = x, 0.6f, outTime + inTime);
            DOTween.To(() => hexjig.Start.backgroundInstance.brotation, x => hexjig.Start.backgroundInstance.brotation = x, 127, outTime + inTime);
        }
    }

    private void TweenComplete()
    {
        Destroy(MainData.Instance.showCutRoot);
        MainData.Instance.showCutRoot = null;
        dispatcher.DispatchWith(lib.Event.COMPLETE);
    }

    override public void FadeIn(ModuleName name)
    {
        hit1.SetActive(false);
        hit2.SetActive(false);
        time.localScale = new Vector3(0, 1);
        model.localScale = new Vector3(0, 1);
        line1.fillAmount = 0;
        line2.fillAmount = 0;
        hex.localScale = new Vector3(0, 0);
        quit.localScale = new Vector3(0, 1);
        home.localScale = new Vector3(0, 1);
        share.localScale = new Vector3(0, 0);

        MainData.Instance.showCutRoot.transform.localPosition = new Vector3(MainData.Instance.showCutRoot.transform.localPosition.x, MainData.Instance.showCutRoot.transform.localPosition.y,100);
        MainData.Instance.showCutRoot.transform.DOMove(new Vector3(0.8f, 2.26f,100), 0.4f);
        MainData.Instance.showCutRoot.transform.DOLocalRotate(new Vector3(0, 0, -17), 0.4f).onComplete = FadeIn2;
    }

    private void FadeIn2()
    {
        time.DOScaleX(1, 0.2f);
        model.DOScaleX(1, 0.2f).onComplete = FadeIn3;
    }

    private void FadeIn3()
    {
        line1.DOFillAmount(1, 0.2f);
        line2.DOFillAmount(1, 0.2f).onComplete = FadeIn4;
    }

    private void FadeIn4()
    {
        hit1.SetActive(true);
        hit2.SetActive(true);
        time.DOScaleX(1, 0.2f);
        model.DOScaleX(1, 0.2f);
        hex.DOScaleX(1, 0.2f);
        hex.DOScaleY(1, 0.2f);
        quit.DOScaleX(1, 0.2f);
        home.DOScaleX(1, 0.2f).onComplete = FadeIn5;
    }

    private void FadeIn5()
    {
        share.localScale = new Vector3(5, 5);
        share.DOScale(new Vector3(1, 1, 1), 0.2f).onComplete = FadeIn6;
    }

    private void FadeIn6()
    {
        ResourceManager.PlaySound("sound/gaizhang", false, GameVO.Instance.soundVolumn.value / 100.0f);
        Tweener tween = root.DOShakePosition(0.3f, 30);
        tween.onUpdate = FadeIn6Update;
        tween.onComplete = FadeInComplete;
        shootCutX = MainData.Instance.showCutRoot.transform.position.x;
        shootCutY = MainData.Instance.showCutRoot.transform.position.y;
    }

    private float shootCutX;
    private float shootCutY;
    private void FadeIn6Update()
    {
        MainData.Instance.showCutRoot.transform.position = new Vector3(shootCutX + root.position.x, shootCutY + root.position.y,100);
    }

    private void FadeInComplete()
    {
        GameVO.Instance.dispatcher.DispatchWith(GameEvent.SHOW_MODULE_COMPLETE, ModuleName.Result);
    }
}