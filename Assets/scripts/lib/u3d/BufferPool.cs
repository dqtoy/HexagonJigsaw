using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace lib
{
    public class BufferPool
    {
        internal static GameObject bufferRoot;

        private static Dictionary<string, List<object>> pools = new Dictionary<string, List<object>>();

        public static void Add(string key,GameObject obj)
        {
            if(pools.ContainsKey(key) == false)
            {
                pools.Add(key, new List<object>());
            }
            pools[key].Add(obj);
            obj.transform.parent = bufferRoot.transform;
        }

        public static int GetLength(string key)
        {
            if (pools.ContainsKey(key) == true)
            {
                return pools[key].Count;
            }
            return 0;
        }

        public static T Get<T>(string key) where T : class
        {
            if(pools.ContainsKey(key) && pools[key].Count > 0)
            {
                object obj = pools[key][pools[key].Count - 1];
                pools[key].RemoveAt(pools[key].Count - 1);
                return obj as T;
            }
            return null;
        }
    }
}
