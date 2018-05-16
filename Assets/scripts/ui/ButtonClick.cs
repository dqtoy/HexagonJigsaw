using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using lib;
using System;

public class ButtonClick : MonoBehaviour, IPointerClickHandler, IPointerDownHandler
{
    /// <summary>  
    /// 定义事件代理  
    /// </summary>  
    /// <param name="gb"></param>  
    public delegate void UIEventProxy(GameObject gb);

    /// <summary>  
    /// 鼠标点击事件  
    /// </summary>  
    public event UIEventProxy OnClick;

    private DateTime lastTime;

    /*public void OnClick(GameObject obj)
    {
        Debug.Log("click");
    }*/

    public void OnPointerDown(PointerEventData eventData)
    {
        //ResourceManager.PlaySound("sound/click", false, GameVO.Instance.soundVolumn.value / 100.0f);
        _dispatcher.DispatchWith(gameObject.name + "_Down", gameObject);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        double time = System.DateTime.Now.Subtract(lastTime).TotalMilliseconds;
        if(time < 250)
        {
            return;
        }
        lastTime = System.DateTime.Now;
        _dispatcher.DispatchWith(gameObject.name, gameObject);
        ResourceManager.PlaySound("sound/click", false, GameVO.Instance.soundVolumn.value / 100.0f);
    }


    private static EventDispatcher _dispatcher = new EventDispatcher();
    public static EventDispatcher dispatcher
    {
        get { return _dispatcher; }
    }
}
