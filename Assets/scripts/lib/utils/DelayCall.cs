using System.Collections.Generic;

namespace lib
{
    public class DelayCall
    {
        float endTime;
        object thisObj;
        string callName;
        object[] param;
        bool isDispose = false;


        public DelayCall(float delaySecond,object thisObj, string callName, object[] param = null)
        {
            endTime = LibData.instance.timeSecondF.value + delaySecond;
            this.thisObj = thisObj;
            this.callName = callName;
            this.param = param;
            list.Add(this);
        }

        void Call()
        {
            Grammar.Call(thisObj, callName, param);
            thisObj = null;
            param = null;
            isDispose = true;
        }



        private static List<DelayCall> list = new List<DelayCall>();

        public static void Update()
        {
            int len = list.Count;
            for (int i = 0; i < len; i++)
            {
                if (list[i].endTime < LibData.instance.timeSecondF.value)
                {
                    list[i].Call();
                }
            }
            for (int i = 0; i < len; i++)
            {
                if(list[i].isDispose)
                {
                    list.RemoveAt(i);
                }
            }
        }
    }
}