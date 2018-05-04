using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using lib;


public class AnimatorControl : MonoBehaviour ,IEventDispatcher {

    private EventDispatcher dispatcher;

    public AnimatorControl()
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

    // Update is called once per frame
    void Update ()
    {
        AnimatorStateInfo info = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
        // 判断动画是否播放完成
        if (info.normalizedTime >= 1.0f)
        {
            DispatchWith(lib.Event.COMPLETE);
        }
    }
}
