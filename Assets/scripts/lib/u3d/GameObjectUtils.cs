using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace lib
{
    public class GameObjectUtils
    {
        public static void DisableComponentAllChildren<T>(GameObject obj)where T : class
        {
           T[] ps = obj.GetComponentsInChildren<T>();
            for(int i = 0; i < ps.Length; i++)
            {
                (ps[i] as MonoBehaviour).enabled = false;
            }
        }
        public static void EnableComponentAllChildren<T>(GameObject obj) where T : class
        {
            T[] ps = obj.GetComponentsInChildren<T>();
            for (int i = 0; i < ps.Length; i++)
            {
                (ps[i] as MonoBehaviour).enabled = true;
            }
        }
    }
}
