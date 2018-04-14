using System;
using System.Collections;

namespace lib
{
    public class Int : SimpleValue
    {
        public Int(object value = null)
        {
            this.val = value ?? 0;
        }

        /// <summary>
        /// 设置当前值
        /// </summary>
        /// <param name="val"></param>
        protected override void SetValue(object val)
        {
            if (this.val == val)
            {
                return;
            }
            if(!(val is int))
            {
               val = (int)Convert.ToDouble(val);
            }
            old = this.val;
            if(defendFalsify) //加上防篡改功能
            {
                DefendEncode(val);
            }
            else
            {
                this.val = val;
            }
            DispatchWith(Event.CHANGE);
        }

        /// <summary>
        /// 防篡改反序列化
        /// </summary>
        protected override void DefendDecode()
        {
            ArrayList list = defendValue as ArrayList;
            string str = "";
            for (int i = 0, len = list.Count; i < len; i++)
            {
                str += list[i].ToString();
            }
            val = Convert.ToInt64(str);
        }

        /// <summary>
        /// 当前值
        /// </summary>
        public new int value
        {
            get { return (int)val; }
            set { SetValue(value); }
        }
    }
}