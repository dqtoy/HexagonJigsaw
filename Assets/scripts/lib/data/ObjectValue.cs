using System.Collections;
using System.Collections.Generic;

namespace lib
{
    public abstract class ObjectValue : ValueBase
    {
        public string Value
        {
            get { return Encode(); }
            set { Decode(JSON.Parse(value) as Dictionary<string, object>); }
        }

        /// <summary>
        /// 范序列化
        /// </summary>
        /// <param name="val"></param>
        protected abstract void Decode(Dictionary<string, object> val);

        /// <summary>
        /// 序列化
        /// </summary>
        /// <returns></returns>
        protected abstract string Encode();
    }
}