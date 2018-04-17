using System.Collections;
using System.Collections.Generic;

namespace lib
{
    public class Array<T> : ValueBase
    {
        private List<T> list = new List<T>();

        public T this[int index]
        {
            get
            {
                return list[index];
            }
            set
            {
                if (index < list.Count)
                {
                    list[index] = value;
                }
                else if (index == list.Count)
                {
                    list.Add(value);
                }
                else
                {
                    list[index] = value;
                }
            }
        }

        public T Add(T item)
        {
            list.Add(item);
            DispatchWith(Event.CHANGE);
            return item;
        }

        public T AddAt(T item, int index)
        {
            list.Insert(index, item);
            DispatchWith(Event.CHANGE);
            return item;
        }

        public T Remove(T item)
        {
            list.Remove(item);
            DispatchWith(Event.CHANGE);
            return item;
        }

        public T RemoveAt(int index)
        {
            T item = list[index];
            list.RemoveAt(index);
            DispatchWith(Event.CHANGE);
            return item;
        }

        public int GetIndex(T item)
        {
            return list.IndexOf(item);
        }

        public int length
        {
            get { return list.Count; }
        }

        public static Array<T> ListTo(List<T> list)
        {
            Array<T> array = new Array<T>();
            for (int i = 0, len = list.Count; i < len; i++)
            {
                array.list.Add(list[i]);
            }
            return array;
        }
    }
}
