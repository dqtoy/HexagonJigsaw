using System.Collections.Generic;

namespace lib
{
    public class ObjectAtr
    {
        public List<object> list;

        public ObjectAtr(List<object> list)
        {
            this.list = list;
            for (var i = 0; i < list.Count; i++)
            {
                //(list[i] as List<object>)[0] = list[i][0].getValue();
            }
        }

        /*checkPropertyBinding(commonInfo)
        {
            for (var i = 0; i < this.list.length; i++)
            {
                this.list[i][1].checkPropertyBinding(commonInfo);
            }
        }

        getValue()
        {
            var val = { };
            for (var i = 0; i < this.list.length; i++)
            {
                val[this.list[i][0]] = this.list[i][1].getValue();
            }
            return val;
        }*/
    }
}