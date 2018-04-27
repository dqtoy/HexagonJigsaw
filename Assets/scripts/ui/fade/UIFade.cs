using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using lib;
using UnityEngine.UI;
using DG.Tweening;

public abstract class UIFade : MonoBehaviour , IEventDispatcher {

    protected EventDispatcher dispatcher;

    abstract public void FadeOut(ModuleName name);
    abstract public void FadeIn(ModuleName name);

    public float outTime = 0.5f;
    public float inTime = 0.5f;

    public UIFade()
    {
        dispatcher = new EventDispatcher(this);
    }

    public void AddListener(string type, listenerBack listener)
    {
        dispatcher.AddListener(type, listener);
    }

    public void RemoveListener(string type, listenerBack listener)
    {
        dispatcher.RemoveListener(type, listener);
    }

    public void Dispatch(lib.Event e)
    {
        dispatcher.Dispatch(e);
    }

    public void DispatchWith(string type, object data = null)
    {
        dispatcher.DispatchWith(type, data);
    }

    protected void DoEffect(UIFadeType type, Image obj, float startTime,bool reverse = false)
    {
        if(type == UIFadeType.SuoFang)
        {
            if(reverse)
            {
                SuoFangReverse(obj, startTime);
            }
            else
            {
                SuoFang(obj, startTime);
            }
        }
        else if (type == UIFadeType.XuanZhuanTiaoYue)
        {
            if (reverse)
            {
                XuanZhuanTiaoYueReverse(obj, startTime);
            }
            else
            {
                XuanZhuanTiaoYue(obj, startTime);
            }
        }
    }

    protected void DoEffect(UIFadeType type, Text obj, float startTime, bool reverse = false)
    {
        if (type == UIFadeType.SuoFang)
        {
            if (reverse)
            {
                SuoFangReverse(obj, startTime);
            }
            else
            {
                SuoFang(obj, startTime);
            }
        }
        else if (type == UIFadeType.XuanZhuanTiaoYue)
        {
            if (reverse)
            {
                XuanZhuanTiaoYueReverse(obj, startTime);
            }
            else
            {
                XuanZhuanTiaoYue(obj, startTime);
            }
        }
    }

    /// <summary>
    /// 自由选择面板的 easy 效果
    /// </summary>
    /// <param name="bg"></param>
    /// <param name="startTime"></param>
    /*protected void BaoShan(Image bg,float startTime)
    {
        Sequence mySequence = DOTween.Sequence();
        bg.color = new Color(1, 1, 1, 0);
        mySequence.Append(bg.DOColor(new Color(1, 1, 1, 0), startTime));
        mySequence.Append(bg.DOColor(new Color(1, 1, 1, 1), 0.6f));
        mySequence.Append(bg.DOColor(new Color(1, 1, 1, 0), 1.3f - 0.6f));
        mySequence.Append(bg.DOColor(new Color(1, 1, 1, 1), 1.7f - 1.3f));
        mySequence.Append(bg.DOColor(new Color(1, 1, 1, 0), 2.3f - 1.7f));
        mySequence.Append(bg.DOColor(new Color(1, 1, 1, 1), 2.4f - 2.3f));
        mySequence.Append(bg.DOColor(new Color(1, 1, 1, 0), 2.5f - 2.4f));
        mySequence.Append(bg.DOColor(new Color(1, 1, 1, 1), 2.6f - 2.5f));
    }

    protected void BaoShan(Text txt, float startTime)
    {
        Sequence mySequence = DOTween.Sequence();
        float r = txt.color.r;
        float g = txt.color.g;
        float b = txt.color.b;
        mySequence = DOTween.Sequence();
        txt.color = new Color(r, g, b, 0);
        mySequence.Append(txt.DOColor(new Color(r, g, b, 0), startTime));
        mySequence.Append(txt.DOColor(new Color(r, g, b, 1), 0.6f));
        mySequence.Append(txt.DOColor(new Color(r, g, b, 0), 1.3f - 0.6f));
        mySequence.Append(txt.DOColor(new Color(r, g, b, 1), 1.7f - 1.3f));
        mySequence.Append(txt.DOColor(new Color(r, g, b, 0), 2.3f - 1.7f));
        mySequence.Append(txt.DOColor(new Color(r, g, b, 1), 2.4f - 2.3f));
        mySequence.Append(txt.DOColor(new Color(r, g, b, 0), 2.5f - 2.4f));
        mySequence.Append(txt.DOColor(new Color(r, g, b, 1), 2.6f - 2.5f));
    }*/

    protected void SuoFang(Image image, float startTime)
    {
        Sequence mySequence = DOTween.Sequence();
        image.gameObject.GetComponent<RectTransform>().localScale = new Vector3(0, 0);
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOScale(0.8f, startTime));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOScale(1.2f, 0.3f));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOScale(0.8f, 0.6f - 0.3f));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOScale(1.1f, 1.0f - 0.6f));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOScale(0.9f, 1.2f - 1.0f));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOScale(1.0f, 1.3f - 1.2f));
    }

    protected void SuoFang(Text image, float startTime)
    {
        Sequence mySequence = DOTween.Sequence();
        image.gameObject.GetComponent<RectTransform>().localScale = new Vector3(0, 0);
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOScale(0.8f, startTime));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOScale(1.2f, 0.3f));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOScale(0.8f, 0.6f - 0.3f));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOScale(1.1f, 1.0f - 0.6f));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOScale(0.9f, 1.2f - 1.0f));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOScale(1.0f, 1.3f - 1.2f));
    }

    protected void SuoFangReverse(Image image, float startTime)
    {
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOScale(0, 0));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOScale(0.8f, startTime));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOScale(1.2f, 0.3f));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOScale(0.8f, 0.6f - 0.3f));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOScale(1.1f, 1.0f - 0.6f));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOScale(0.9f, 1.2f - 1.0f));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOScale(1.0f, 1.3f - 1.2f));
        mySequence.PlayForward();
    }

    protected void SuoFangReverse(Text image, float startTime)
    {
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOScale(0, 0));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOScale(0.8f, startTime));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOScale(1.2f, 0.3f));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOScale(0.8f, 0.6f - 0.3f));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOScale(1.1f, 1.0f - 0.6f));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOScale(0.9f, 1.2f - 1.0f));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOScale(1.0f, 1.3f - 1.2f));
        mySequence.PlayForward();
    }

    protected void XuanZhuanTiaoYue(Image image,float startTime)
    {
        float x = image.gameObject.GetComponent<RectTransform>().localPosition.x;
        float y = image.gameObject.GetComponent<RectTransform>().localPosition.y;
        float z = image.gameObject.GetComponent<RectTransform>().localPosition.z;
        Sequence mySequence = DOTween.Sequence();
        Sequence mySequence2 = DOTween.Sequence();
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, 90, 0), 0));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, 90, 0), startTime));
        mySequence2.Append(image.gameObject.GetComponent<RectTransform>().DOLocalMove(new Vector3(x, y, z), startTime));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, 360, 0), 0.4f));
        mySequence2.Append(image.gameObject.GetComponent<RectTransform>().DOLocalMove(new Vector3(x, y, z), 0.4f));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, 0, 0), 0.8f));
        mySequence2.Append(image.gameObject.GetComponent<RectTransform>().DOLocalMove(new Vector3(x, y, z), 0.8f));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, -720, 0), 0.8f));
        mySequence2.Append(image.gameObject.GetComponent<RectTransform>().DOLocalMove(new Vector3(x, y, z), 0.8f));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, 0, 0), 0.4f));
        mySequence2.Append(image.gameObject.GetComponent<RectTransform>().DOLocalMove(new Vector3(x, y + 10, z), 0.4f));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, 0, 0), 0.1f));
        mySequence2.Append(image.gameObject.GetComponent<RectTransform>().DOLocalMove(new Vector3(x, y, z), 0.1f));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, 0, 0), 0.5f));
        mySequence2.Append(image.gameObject.GetComponent<RectTransform>().DOLocalMove(new Vector3(x, y + 5, z), 0.5f));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, 0, 0), 0.1f));
        mySequence2.Append(image.gameObject.GetComponent<RectTransform>().DOLocalMove(new Vector3(x, y, z), 0.1f));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, 0, 0), 0.2f));
        mySequence2.Append(image.gameObject.GetComponent<RectTransform>().DOLocalMove(new Vector3(x, y, z), 0.2f));
    }

    protected void XuanZhuanTiaoYue(Text image, float startTime)
    {
        float x = image.gameObject.GetComponent<RectTransform>().localPosition.x;
        float y = image.gameObject.GetComponent<RectTransform>().localPosition.y;
        float z = image.gameObject.GetComponent<RectTransform>().localPosition.z;
        Sequence mySequence = DOTween.Sequence();
        Sequence mySequence2 = DOTween.Sequence();
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, 90, 0), 0));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, 90, 0), startTime));
        mySequence2.Append(image.gameObject.GetComponent<RectTransform>().DOLocalMove(new Vector3(x, y, z), startTime));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, 360, 0), 0.4f));
        mySequence2.Append(image.gameObject.GetComponent<RectTransform>().DOLocalMove(new Vector3(x, y, z), 0.4f));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, 0, 0), 0.8f));
        mySequence2.Append(image.gameObject.GetComponent<RectTransform>().DOLocalMove(new Vector3(x, y, z), 0.8f));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, -720, 0), 0.8f));
        mySequence2.Append(image.gameObject.GetComponent<RectTransform>().DOLocalMove(new Vector3(x, y, z), 0.8f));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, 0, 0), 0.4f));
        mySequence2.Append(image.gameObject.GetComponent<RectTransform>().DOLocalMove(new Vector3(x, y + 10, z), 0.4f));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, 0, 0), 0.1f));
        mySequence2.Append(image.gameObject.GetComponent<RectTransform>().DOLocalMove(new Vector3(x, y, z), 0.1f));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, 0, 0), 0.5f));
        mySequence2.Append(image.gameObject.GetComponent<RectTransform>().DOLocalMove(new Vector3(x, y + 5, z), 0.5f));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, 0, 0), 0.1f));
        mySequence2.Append(image.gameObject.GetComponent<RectTransform>().DOLocalMove(new Vector3(x, y, z), 0.1f));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, 0, 0), 0.2f));
        mySequence2.Append(image.gameObject.GetComponent<RectTransform>().DOLocalMove(new Vector3(x, y, z), 0.2f));
    }

    protected void XuanZhuanTiaoYueReverse(Image image, float startTime)
    {
        float x = image.gameObject.GetComponent<RectTransform>().localPosition.x;
        float y = image.gameObject.GetComponent<RectTransform>().localPosition.y;
        float z = image.gameObject.GetComponent<RectTransform>().localPosition.z;
        Sequence mySequence = DOTween.Sequence();
        Sequence mySequence2 = DOTween.Sequence();
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, 90, 0), 0));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, 90, 0), startTime));
        mySequence2.Append(image.gameObject.GetComponent<RectTransform>().DOLocalMove(new Vector3(x, y, z), startTime));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, 360, 0), 0.4f));
        mySequence2.Append(image.gameObject.GetComponent<RectTransform>().DOLocalMove(new Vector3(x, y, z), 0.4f));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, 0, 0), 0.8f));
        mySequence2.Append(image.gameObject.GetComponent<RectTransform>().DOLocalMove(new Vector3(x, y, z), 0.8f));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, -720, 0), 0.8f));
        mySequence2.Append(image.gameObject.GetComponent<RectTransform>().DOLocalMove(new Vector3(x, y, z), 0.8f));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, 0, 0), 0.4f));
        mySequence2.Append(image.gameObject.GetComponent<RectTransform>().DOLocalMove(new Vector3(x, y + 10, z), 0.4f));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, 0, 0), 0.1f));
        mySequence2.Append(image.gameObject.GetComponent<RectTransform>().DOLocalMove(new Vector3(x, y, z), 0.1f));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, 0, 0), 0.5f));
        mySequence2.Append(image.gameObject.GetComponent<RectTransform>().DOLocalMove(new Vector3(x, y + 5, z), 0.5f));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, 0, 0), 0.1f));
        mySequence2.Append(image.gameObject.GetComponent<RectTransform>().DOLocalMove(new Vector3(x, y, z), 0.1f));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, 0, 0), 0.2f));
        mySequence2.Append(image.gameObject.GetComponent<RectTransform>().DOLocalMove(new Vector3(x, y, z), 0.2f));
        mySequence.PlayForward();
        mySequence2.PlayForward();
    }

    protected void XuanZhuanTiaoYueReverse(Text image, float startTime)
    {
        float x = image.gameObject.GetComponent<RectTransform>().localPosition.x;
        float y = image.gameObject.GetComponent<RectTransform>().localPosition.y;
        float z = image.gameObject.GetComponent<RectTransform>().localPosition.z;
        Sequence mySequence = DOTween.Sequence();
        Sequence mySequence2 = DOTween.Sequence();
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, 90, 0), 0));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, 90, 0), startTime));
        mySequence2.Append(image.gameObject.GetComponent<RectTransform>().DOLocalMove(new Vector3(x, y, z), startTime));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, 360, 0), 0.4f));
        mySequence2.Append(image.gameObject.GetComponent<RectTransform>().DOLocalMove(new Vector3(x, y, z), 0.4f));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, 0, 0), 0.8f));
        mySequence2.Append(image.gameObject.GetComponent<RectTransform>().DOLocalMove(new Vector3(x, y, z), 0.8f));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, -720, 0), 0.8f));
        mySequence2.Append(image.gameObject.GetComponent<RectTransform>().DOLocalMove(new Vector3(x, y, z), 0.8f));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, 0, 0), 0.4f));
        mySequence2.Append(image.gameObject.GetComponent<RectTransform>().DOLocalMove(new Vector3(x, y + 10, z), 0.4f));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, 0, 0), 0.1f));
        mySequence2.Append(image.gameObject.GetComponent<RectTransform>().DOLocalMove(new Vector3(x, y, z), 0.1f));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, 0, 0), 0.5f));
        mySequence2.Append(image.gameObject.GetComponent<RectTransform>().DOLocalMove(new Vector3(x, y + 5, z), 0.5f));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, 0, 0), 0.1f));
        mySequence2.Append(image.gameObject.GetComponent<RectTransform>().DOLocalMove(new Vector3(x, y, z), 0.1f));
        mySequence.Append(image.gameObject.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, 0, 0), 0.2f));
        mySequence2.Append(image.gameObject.GetComponent<RectTransform>().DOLocalMove(new Vector3(x, y, z), 0.2f));
        mySequence.PlayForward();
        mySequence2.PlayForward();
    }
}

public enum UIFadeType
{
    //BaoShan = 1,
    SuoFang = 2,
    XuanZhuanTiaoYue = 3
}
 