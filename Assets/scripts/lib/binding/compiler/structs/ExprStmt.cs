namespace lib
{
    public class ExprStmt
    {
        public string type = "stmt_expr";
        public Expr expr;

        public ExprStmt(Expr expr)
        {
            this.expr = expr;
        }

        public void checkPropertyBinding(CommonInfo commonInfo)
        {
            this.expr.checkPropertyBinding(commonInfo);
        }

        public object getValue()
        {
            return this.expr.getValue();
        }
    }
}