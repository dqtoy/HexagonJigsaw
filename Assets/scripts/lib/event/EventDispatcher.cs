using System.Collections;
using System.Collections.Generic;

namespace lib
{

    /*
     * @Example 
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using lib;

    public abstract class UIFade : MonoBehaviour , IEventDispatcher {

        private EventDispatcher dispatcher;

        abstract public void FadeOut();
        abstract public void FadeIn();

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
    }
     */
    public class EventDispatcher : IEventDispatcher
    {
        private Dictionary<string, ArrayList> listeners = new Dictionary<string, ArrayList>();

        private IEventDispatcher target;

        public EventDispatcher(IEventDispatcher target = null)
        {
            this.target = target == null ? this : target;
        }

        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="type"> 事件类型 </param>
        /// <param name="listener"> 回调函数 </param>
        public void AddListener(string type,listenerBack listener)
        {
            if(!listeners.ContainsKey(type))
            {
                listeners.Add(type, new ArrayList());
            }
            if(listeners[type].Contains(listener) == false)
            {
                listeners[type].Add(listener);
            }
        }

        /// <summary>
        /// 移除事件
        /// </summary>
        /// <param name="type"> 事件类型 </param>
        /// <param name="listener"> 回调函数 </param>
        public void RemoveListener(string type,listenerBack listener)
        {
            if(listeners.ContainsKey(type))
            {
                ArrayList list = listeners[type];
                foreach (listenerBack item in list)
                {
                    if (item == listener)
                    {
                        list.Remove(item);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 抛出事件
        /// </summary>
        /// <param name="e"> 事件 </param>
        public void Dispatch(Event e)
        {
            e.target = target;
            if (listeners.ContainsKey(e.Type))
            {
                ArrayList list = listeners[e.Type].Clone() as ArrayList;
                for(int i = 0, len = list.Count; i < len; i++)
                {
                    (list[i] as listenerBack)(e);
                }
            }
        }

        /// <summary>
        /// 抛出事件，无需创建事件对象，只需要传递事件类型和相关内容即可
        /// </summary>
        /// <param name="type"> 事件类型 </param>
        /// <param name="data"> 事件内容 </param>
        public void DispatchWith(string type,object data = null)
        {
            Event e = Event.Create(type, data);
            Dispatch(e);
        }

        public void RemoveAll()
        {
            listeners = new Dictionary<string, ArrayList>();
        }
    }
}

