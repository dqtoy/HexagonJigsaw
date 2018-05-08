using System;
using System.Collections;

namespace lib
{
    public class Bool : SimpleValue
    {
        public Bool(object value = null)
        {
            this.val = value ?? 0;
        }

        /// <summary>
        /// 设置当前值
        /// </summary>
        /// <param name="val"></param>
        protected override void SetValue(object val)
        {
            if (!(val is bool))
            {
                val = Convert.ToBoolean(val);
            }
            if (this.val == val)
            {
                return;
            }
            oldValue = this.val;
            this.val = val;
            DispatchWith(Event.CHANGE);
        }

        /// <summary>
        /// 当前值
        /// </summary>
        public new bool value
        {
            get { return (bool)val; }
            set { SetValue(value); }
        }

        /// <summary>
        /// 上一次的值
        /// </summary>
        public new bool old
        {
            get { return (bool)oldValue; }
        }
    }
}