using System;
using System.Collections;

namespace lib
{
    public class String : SimpleValue
    {
        public String(object value = null)
        {
            if (!(value is string))
            {
                value = Convert.ToString(val);
            }
            this.val = value;
        }

        /// <summary>
        /// 设置当前值
        /// </summary>
        /// <param name="val"></param>
        protected override void SetValue(object val)
        {
            if (!(val is string))
            {
                val = Convert.ToString(val);
            }
            if (this.val == val)
            {
                return;
            }
            oldValue = this.val;
            this.val = val;
            DispatchWith(Event.CHANGE);
        }
    }
}