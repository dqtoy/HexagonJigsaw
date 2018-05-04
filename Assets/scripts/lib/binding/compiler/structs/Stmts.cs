using System.Collections.Generic;

namespace lib
{
    public class Stmts
    {
        public string type = "stmts";
        public List<ExprStmt> list = new List<ExprStmt>();

        public void addStmt(ExprStmt stmt)
        {
            this.list.Add(stmt);
        }

        public void addStmtAt(ExprStmt stmt,int index)
        {
            this.list.Insert(index,stmt);
        }

        public void checkPropertyBinding(CommonInfo commonInfo)
        {
            for (int i = 0; i < this.list.Count; i++)
            {
                this.list[i].checkPropertyBinding(commonInfo);
            }
        }

        public object getValue()
        {
            object value = null;
            for (var i = 0; i < this.list.Count; i++)
            {
                if (i == 0)
                {
                    value = this.list[i].getValue();
                }
                else
                {
                    this.list[i].getValue();
                }
            }
            return value;
        }
    }
}