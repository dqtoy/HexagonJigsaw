using System.Collections;
using System.Collections.Generic;

namespace lib
{
    public class Array<T> : ValueBase
    {
        private List<T> list = new List<T>();
        private Int len = new Int();

        public Array(T[] init = null)
        {
            if(init != null)
            {
                for(int i = 0; i < init.Length; i++)
                {
                    this.Add(init[i]);
                }
            }
        }

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
                    DispatchWith(Event.CHANGE);
                }
                else if (index == list.Count)
                {
                    list.Add(value);
                    len.value++;
                    DispatchWith(Event.CHANGE);
                }
            }
        }

        public T Add(T item)
        {
            list.Add(item);
            len.value++;
            DispatchWith(Event.CHANGE);
            return item;
        }

        public T AddAt(T item, int index)
        {
            list.Insert(index, item);
            len.value++;
            DispatchWith(Event.CHANGE);
            return item;
        }

        public T Remove(T item)
        {
            list.Remove(item);
            len.value--;
            DispatchWith(Event.CHANGE);
            return item;
        }

        public T RemoveAt(int index)
        {
            T item = list[index];
            list.RemoveAt(index);
            len.value--;
            DispatchWith(Event.CHANGE);
            return item;
        }

        public T Pop()
        {
            T item = list[len.value - 1];
            list.RemoveAt(len.value - 1);
            len.value--;
            DispatchWith(Event.CHANGE);
            return item;
        }

        public T Shift()
        {
            T item = list[len.value - 1];
            list.RemoveAt(len.value - 1);
            len.value--;
            DispatchWith(Event.CHANGE);
            return item;
        }

        public void Reverse()
        {
            list.Reverse();
            DispatchWith(Event.CHANGE);
        }

        public int GetItemIndex(T item)
        {
            return list.IndexOf(item);
        }

        public int length
        {
            get { return len.value; }
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
