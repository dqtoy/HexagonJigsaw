using System.Collections.Generic;

namespace lib
{
    class CallParams
    {
        public string type = "callParams";
        public List<Expr> list = new List<Expr>();

        public void addParam(Expr expr)
        {
            this.list.Add(expr);
        }

        public void addParamAt(Expr expr,int index)
        {
            this.list.Insert(index, expr);
        }

        public void checkPropertyBinding(CommonInfo commonInfo)
        {
            for (var i = 0; i < this.list.Count; i++)
            {
                this.list[i].checkPropertyBinding(commonInfo);
            }
        }

        public object[] getValueList()
        {
            List<object> param = new List<object>();
            for (var i = 0; i < this.list.Count; i++)
            {
                param.Add((this.list[i]).getValue());
            }
            return param.ToArray();
        }
    }
}