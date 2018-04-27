using System.Collections.Generic;

namespace lib
{
    public class ExprAtr
    {
        public string type = "attribute";
        public List<ExprAtrItem> list;
        public object value;
        public object before;
        public bool beforeClass;
        public bool equalBefore;

        public ExprAtr()
        {
            list = new List<ExprAtrItem>();
            equalBefore = false;
        }

        public void addItem(ExprAtrItem item)
        {
            if (list.Count == 0 && item.type == "id" && (string)item.val == "this")
            {
                return;
            }
            if (list.Count == 0 && item.type == ".")
            {
                item.type = "id";
            }
            list.Add(item);
        }

        
    }
}