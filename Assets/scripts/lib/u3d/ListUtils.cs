using System.Collections.Generic;

namespace lib
{
    public class ListUtils
    {
        public static List<T> Clone<T>(List<T> obj) where T : class
        {
            List<T> copy = new List<T>();
            for(int i = 0; i < obj.Count; i++)
            {
                copy.Add(obj[i]);
            }
            return copy;
        }
    }
}