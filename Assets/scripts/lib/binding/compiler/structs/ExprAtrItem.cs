namespace lib
{
    public class ExprAtrItem
    {
        public string type;
        public object val;
        public bool getValue;

        public ExprAtrItem(string type,object val,bool getValue = false)
        {
            this.type = type;
            this.val = val;
            this.getValue = getValue;
        }
    }
}