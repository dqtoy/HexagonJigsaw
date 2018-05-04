using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using lib;
using DG.Tweening;
using UnityEngine.UI;

public class MainUIFade : UIFade {

    public GameObject title;
    public GameObject line;
    public GameObject hex;
    public GameObject hex2;
    public Image freedomIcon;
    public GameObject freedomBg;
    public Image dailyIcon;
    public GameObject dailyBg;
    public GameObject setting;

    public RectTransform dailyLevelTxt;
    public Text dailyTimeTxt;
    public GameObject effect6;
    public GameObject effect9;
    public GameObject effect10;

    private ModuleName moduleName;

    override public void FadeOut(ModuleName name)
    { 
        if(name == ModuleName.Freedom)
        {
            line.transform.GetComponent<RectTransform>().DOSizeDelta(new Vector2(0, 9), outTime);
            title.transform.GetComponent<RectTransform>().DOLocalMoveX(-900, outTime);
            hex2.transform.GetComponent<RectTransform>().DORotate(new Vector3(0, 0, -180), outTime);
            freedomIcon.DOColor(new Color(1, 1, 1, 0), outTime);
            freedomBg.GetComponent<RectTransform>().DORotate(new Vector3(0, 0, 30), outTime);
            hex.transform.GetComponent<RectTransform>().DOLocalMoveY(773, outTime).onComplete = TweenComplete;
        }
        else if (name == ModuleName.Setting)
        {
            setting.transform.parent = hex.transform.parent;

            float offTime = 0.1f;

            line.transform.GetComponent<RectTransform>().DOSizeDelta(new Vector2(0, 9), outTime - offTime);
            title.transform.GetComponent<RectTransform>().DOLocalMoveX(-900, outTime - offTime);
            hex2.transform.GetComponent<RectTransform>().DORotate(new Vector3(0, 0, -180), outTime - offTime);
            freedomIcon.DOColor(new Color(1, 1, 1, 0), outTime - offTime);
            freedomBg.GetComponent<RectTransform>().DORotate(new Vector3(0, 0, 30), outTime - offTime);
            hex.transform.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, 0, 270), outTime - offTime);
            hex.transform.GetComponent<RectTransform>().DOLocalMoveY(1100, outTime - offTime);
            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(setting.transform.GetComponent<RectTransform>().DOLocalMoveY(524,outTime - offTime));
            mySequence.Append(setting.transform.GetComponent<RectTransform>().DOScaleX(0, offTime)).onComplete = TweenComplete;
        }
        else if(name == ModuleName.Daily)
        {
            //205,112   120
            //-223,653   30
            float offTime = outTime * 0.5f;
            line.transform.GetComponent<RectTransform>().DOSizeDelta(new Vector2(0, 9), offTime);
            title.transform.GetComponent<RectTransform>().DOLocalMoveX(-900, offTime);
            hex2.transform.GetComponent<RectTransform>().DORotate(new Vector3(0, 0, -180), offTime);
            dailyIcon.DOColor(new Color(1, 1, 1, 0), outTime);
            dailyBg.GetComponent<RectTransform>().DORotate(new Vector3(0, 0, 30), outTime);
            hex.transform.GetComponent<RectTransform>().DOLocalMove(new Vector3(-223, 653), outTime);
            hex.transform.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, 0, 120), outTime).onComplete = TweenComplete;
        }
    }

    override public void FadeIn(ModuleName name)
    {
        if(GameVO.Instance.daily.levels[9].pass)
        {
            dailyIcon.gameObject.SetActive(false);
            effect6.SetActive(false);
            effect9.SetActive(false);
            effect10.SetActive(true);
            dailyLevelTxt.localPosition = new Vector3(2, -25);
            dailyTimeTxt.gameObject.SetActive(true);
            dailyTimeTxt.text = StringUtils.TimeToMS(GameVO.Instance.daily.levels[9].time);
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
            line.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 9);
            title.transform.GetComponent<RectTransform>().localPosition = new Vector3(-900, 479);
            hex2.transform.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, -180);
            freedomIcon.color = new Color(1, 1, 1, 0);
            freedomBg.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 30);
            hex.transform.GetComponent<RectTransform>().localPosition = new Vector3(205, 773);

            line.transform.GetComponent<RectTransform>().DOSizeDelta(new Vector2(835, 9), inTime);
            title.transform.GetComponent<RectTransform>().DOLocalMoveX(-359, inTime);
            hex2.transform.GetComponent<RectTransform>().DORotate(new Vector3(0, 0, 0), inTime);
            freedomIcon.DOColor(new Color(1, 1, 1, 1), inTime);
            freedomBg.GetComponent<RectTransform>().DORotate(new Vector3(0, 0, 0), inTime);
            hex.transform.GetComponent<RectTransform>().DOLocalMoveY(112, inTime);
        }
        else if (name == ModuleName.Setting)
        {
            float offTime = 0.1f;

            line.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 9);
            title.transform.GetComponent<RectTransform>().localPosition = new Vector3(-900, 479);
            hex2.transform.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, -180);
            freedomIcon.color = new Color(1, 1, 1, 0);
            freedomBg.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 30);
            hex.transform.GetComponent<RectTransform>().localPosition = new Vector3(205, 1100);
            setting.transform.GetComponent<RectTransform>().localPosition = new Vector3(setting.transform.GetComponent<RectTransform>().localPosition.x, 524);

            setting.transform.GetComponent<RectTransform>().DOScaleX(1, offTime).onComplete = FadeIn2;
            moduleName = name;
        }
        else if(name == ModuleName.Daily || name == ModuleName.Result && GameVO.Instance.model == GameModel.Daily)
        {
            line.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 9);
            title.transform.GetComponent<RectTransform>().localPosition = new Vector3(-900, 479);
            hex2.transform.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, -180);
            dailyIcon.color = new Color(1, 1, 1, 0);
            dailyBg.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 30);
            hex.transform.GetComponent<RectTransform>().localPosition = new Vector3(-223, 653);
            hex.transform.GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 0, 120);

            line.transform.GetComponent<RectTransform>().DOSizeDelta(new Vector2(835, 9), inTime);
            title.transform.GetComponent<RectTransform>().DOLocalMoveX(-359, inTime);
            hex2.transform.GetComponent<RectTransform>().DORotate(new Vector3(0, 0, 0), inTime);
            dailyIcon.DOColor(new Color(1, 1, 1, 1), inTime);
            dailyBg.GetComponent<RectTransform>().DORotate(new Vector3(0, 0, 0), inTime);
            hex.transform.GetComponent<RectTransform>().DOLocalMove(new Vector3(205,112), inTime);
            hex.transform.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, 0, 0), inTime);
        }
    }

    private void FadeIn2()
    {
        if(moduleName == ModuleName.Setting)
        {
            float offTime = 0.1f;

            line.transform.GetComponent<RectTransform>().DOSizeDelta(new Vector2(835, 9), inTime - offTime);
            title.transform.GetComponent<RectTransform>().DOLocalMoveX(-359, inTime - offTime);
            hex2.transform.GetComponent<RectTransform>().DORotate(new Vector3(0, 0, 0), inTime - offTime);
            freedomIcon.DOColor(new Color(1, 1, 1, 1), inTime - offTime);
            freedomBg.GetComponent<RectTransform>().DORotate(new Vector3(0, 0, 0), inTime - offTime);
            hex.transform.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, 0, 0), inTime - offTime);
            hex.transform.GetComponent<RectTransform>().DOLocalMoveY(112, inTime - offTime);
            setting.transform.GetComponent<RectTransform>().DOLocalMove(new Vector3(-271.7311f, -508.8597f), inTime - offTime).onComplete = FadeIn3;
        }
    }

    private void FadeIn3()
    {
        if (moduleName == ModuleName.Setting)
        {
            setting.transform.parent = hex2.transform;
        }
    }

    private void TweenComplete()
    {
        dispatcher.DispatchWith(lib.Event.COMPLETE);
    }
}