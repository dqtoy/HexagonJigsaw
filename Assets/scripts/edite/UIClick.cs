using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIClick : MonoBehaviour, IPointerClickHandler
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

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update() {

    }

    /*public void OnClick(GameObject obj)
    {
        Debug.Log("click");
    }*/

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("click:" + gameObject.name);
        if (OnClick != null)
            OnClick(this.gameObject);
    }
}
