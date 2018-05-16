using System;
using System.Collections.Generic;

namespace lib
{
    public class WaitEvent
    {
        private static List<WaitEvent> list = new List<WaitEvent>();

        IEventDispatcher dispatcher;
        string EventType;
        object thisObj;
        string callName;
        object[] paramList;

        public WaitEvent(IEventDispatcher dispatcher, string EventType, object thisObj, string callName, object[] paramList = null, bool checkSame = true)
        {
            if(checkSame)
            {
                for(int i = 0; i < list.Count; i++)
                {
                    if(list[i].dispatcher == dispatcher && list[i].EventType == EventType && list[i].thisObj == thisObj && list[i].callName == callName)
                    {
                        return;
                    }
                }
            }
            this.dispatcher = dispatcher;
            this.EventType = EventType;
            this.thisObj = thisObj;
            this.callName = callName;
            this.paramList = paramList;
            dispatcher.AddListener(EventType, OnListenerBack);
            list.Add(this);
        }

        private void OnListenerBack(Event e)
        {
            Grammar.Call(thisObj, callName, paramList);
            list.Remove(this);
        }
    }
}