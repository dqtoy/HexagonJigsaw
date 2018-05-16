using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace lib
{

    public class LibData : MonoBehaviour
    {
        /// <summary>
        /// 程序执行的时间(毫秒)
        /// </summary>
        public Int timeMiniSecond = new Int();

        /// <summary>
        /// 程序执行的时间(秒)
        /// </summary>
        public Int timeSecond = new Int();

        /// <summary>
        /// 程序执行的时间(秒)(Float)
        /// </summary>
        public Float timeSecondF = new Float();

        public static LibData instance = new LibData();
    }

}