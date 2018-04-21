using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using lib;

public class ButtonClick : MonoBehaviour, IPointerClickHandler
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

    /*public void OnClick(GameObject obj)
    {
        Debug.Log("click");
    }*/

    public void OnPointerClick(PointerEventData eventData)
    {
        _dispatcher.DispatchWith(gameObject.name, gameObject);
    }


    private static EventDispatcher _dispatcher = new EventDispatcher();
    public static EventDispatcher dispatcher
    {
        get { return _dispatcher; }
    }
}
