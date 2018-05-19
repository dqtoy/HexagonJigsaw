using System;
using System.Collections.Generic;

namespace lib
{
    public class Expr
    {
        public string type;
        public object expr1;
        public object expr2;
        public object expr3;

        public Expr(string type, object expr1 = null, object expr2 = null, object expr3 = null)
        {
            this.type = type;
            this.expr1 = expr1;
            this.expr2 = expr2;
            this.expr3 = expr3;
            if (type == "int")
            {
                this.expr1 = StringUtils.ToNumber((string)expr1);
            }
            if (type == "string")
            {
                this.expr1 = StringUtils.Slice((string)this.expr1, 1, ((string)this.expr1).Length - 1);
            }
        }

        public void checkPropertyBinding(CommonInfo commonInfo)
        {
            if (this.type == "Atr")
            {
                Grammar.Call(this.expr1, "checkPropertyBinding", new object[] { commonInfo });
            }
            else if (this.expr1 != null && (this.expr1 is Expr || this.expr1 is ExprAtr)) {
                Grammar.Call(this.expr1, "checkPropertyBinding", new object[] { commonInfo });
            }
            if (this.type == "spfor")
            {
                //commonInfo.specialFor = this.expr1.getValue();
            }
            if (this.expr2 != null && (this.expr2 is Expr || this.expr2 is ExprAtr)) {
                Grammar.Call(this.expr2, "checkPropertyBinding", new object[] { commonInfo });
            }
            if (this.expr3 != null && (this.expr3 is Expr || this.expr3 is ExprAtr)) {
                Grammar.Call(this.expr3, "checkPropertyBinding", new object[] { commonInfo });
            }
            if (this.type == "spfor")
            {
                commonInfo.specialFor = null;
            }
        }

        public object getValue(Dictionary<string, object> param = null)
        {
            if (this.type == "Atr")
            {
                return Grammar.Call(this.expr1, "getValue", new object[] { param });
            }
            if (this.type == "int")
            {
                return this.expr1;
            }
            if (this.type == "0xint")
            {
                return this.expr1;
            }
            if (this.type == "number")
            {
                return this.expr1;
            }
            if (this.type == "boolean")
            {
                return this.expr1;
            }
            if (this.type == "string")
            {
                return this.expr1;
            }
            if (this.type == "+a")
            {
                return Convert.ToDouble(Grammar.Call(this.expr1, "getValue", new object[] { param }));
            }
            if (this.type == "-a")
            {
                return -(float)Convert.ToDouble(Grammar.Call(this.expr1, "getValue", new object[] { param }));
            }
            if (this.type == "!")
            {
                return !Convert.ToBoolean(Grammar.Call(this.expr1, "getValue", new object[] { param }));
            }
            if (this.type == "*")
            {
                return (float)Convert.ToDouble(Grammar.Call(this.expr1, "getValue", new object[] { param })) * (float)Convert.ToDouble(Grammar.Call(this.expr2, "getValue", new object[] { param }));
            }
            if (this.type == "/")
            {
                return (float)Convert.ToDouble(Grammar.Call(this.expr1, "getValue", new object[] { param })) / (float)Convert.ToDouble(Grammar.Call(this.expr2, "getValue", new object[] { param }));
            }
            if (this.type == "%")
            {
                return (float)Convert.ToDouble(Grammar.Call(this.expr1, "getValue", new object[] { param })) % (float)Convert.ToDouble(Grammar.Call(this.expr2, "getValue", new object[] { param }));
            }
            if (this.type == "+")
            {
                object val1 = Grammar.Call(this.expr1, "getValue", new object[] { param });
                object val2 = Grammar.Call(this.expr2, "getValue", new object[] { param });
                if(val1 is string)
                {
                    return (string)val1 + val2;
                }
                else if(val2 is string)
                {
                    return val1 + (string)val2;
                }
                return (float)Convert.ToDouble(val1) + (float)Convert.ToDouble(val2);
            }
            if (this.type == "-")
            {
                return (float)Convert.ToDouble(Grammar.Call(this.expr1, "getValue", new object[] { param })) - (float)Convert.ToDouble(Grammar.Call(this.expr2, "getValue", new object[] { param }));
            }
            if (this.type == ">")
            {
                return (float)Convert.ToDouble(Grammar.Call(this.expr1, "getValue", new object[] { param })) > (float)Convert.ToDouble(Grammar.Call(this.expr2, "getValue", new object[] { param }));
            }
            if (this.type == "<")
            {
                return (float)Convert.ToDouble(Grammar.Call(this.expr1, "getValue", new object[] { param })) < (float)Convert.ToDouble(Grammar.Call(this.expr2, "getValue", new object[] { param }));
            }
            if (this.type == ">=")
            {
                return (float)Convert.ToDouble(Grammar.Call(this.expr1, "getValue", new object[] { param })) >= (float)Convert.ToDouble(Grammar.Call(this.expr2, "getValue", new object[] { param }));
            }
            if (this.type == "<=")
            {
                return (float)Convert.ToDouble(Grammar.Call(this.expr1, "getValue", new object[] { param })) <= (float)Convert.ToDouble(Grammar.Call(this.expr2, "getValue", new object[] { param }));
            }
            if (this.type == "==")
            {
                return (float)Convert.ToDouble(Grammar.Call(this.expr1, "getValue", new object[] { param })) == (float)Convert.ToDouble(Grammar.Call(this.expr2, "getValue", new object[] { param }));
            }
            if (this.type == "!=")
            {
                return (float)Convert.ToDouble(Grammar.Call(this.expr1, "getValue", new object[] { param })) != (float)Convert.ToDouble(Grammar.Call(this.expr2, "getValue", new object[] { param }));
            }
            if (this.type == "&&")
            {
                return Convert.ToBoolean(Grammar.Call(this.expr1, "getValue", new object[] { param })) && Convert.ToBoolean(Grammar.Call(this.expr2, "getValue", new object[] { param }));
            }
            if (this.type == "||")
            {
                return Convert.ToBoolean(Grammar.Call(this.expr1, "getValue", new object[] { param })) || Convert.ToBoolean(Grammar.Call(this.expr2, "getValue", new object[] { param }));
            }
            if (this.type == "=")
            {
                Grammar.Call(this.expr1, "setValue",new object[] { Grammar.Call(this.expr2, "getValue", new object[] { param}),param });
                return Grammar.Call(this.expr1, "getValue", new object[] { param });
            }
            if (this.type == "*=")
            {
                Grammar.Call(this.expr1, "setValue", new object[] { (float)Convert.ToDouble(Grammar.Call(this.expr1, "getValue", new object[] { param })) * (float)Convert.ToDouble(Grammar.Call(this.expr2, "getValue", new object[] { param })), param });
                return Grammar.Call(this.expr1, "getValue", new object[] { param });
            }
            if (this.type == "/=")
            {
                Grammar.Call(this.expr1, "setValue", new object[] { (float)Convert.ToDouble(Grammar.Call(this.expr1, "getValue", new object[] { param })) / (float)Convert.ToDouble(Grammar.Call(this.expr2, "getValue", new object[] { param })), param });
                return Grammar.Call(this.expr1, "getValue", new object[] { param });
            }
            if (this.type == "%=")
            {
                Grammar.Call(this.expr1, "setValue", new object[] { (float)Convert.ToDouble(Grammar.Call(this.expr1, "getValue", new object[] { param })) % (float)Convert.ToDouble(Grammar.Call(this.expr2, "getValue", new object[] { param })), param });
                return Grammar.Call(this.expr1, "getValue", new object[] { param });
            }
            if (this.type == "+=")
            {
                Grammar.Call(this.expr1, "setValue", new object[] { (float)Convert.ToDouble(Grammar.Call(this.expr1, "getValue", new object[] { param })) + (float)Convert.ToDouble(Grammar.Call(this.expr2, "getValue", new object[] { param })), param });
                return Grammar.Call(this.expr1, "getValue", new object[] { param });
            }
            if (this.type == "-=")
            {
                Grammar.Call(this.expr1, "setValue", new object[] { (float)Convert.ToDouble(Grammar.Call(this.expr1, "getValue", new object[] { param })) - (float)Convert.ToDouble(Grammar.Call(this.expr2, "getValue", new object[] { param })), param });
                return Grammar.Call(this.expr1, "getValue", new object[] { param });
            }
            if (this.type == "?:")
            {
                return Convert.ToBoolean(Grammar.Call(this.expr1, "getValue", new object[] { param })) ? Grammar.Call(this.expr2, "getValue", new object[] { param }) : Grammar.Call(this.expr3, "getValue", new object[] { param });
            }
            if (this.type == "spfor")
            {
                Dictionary<string,object> info = param;
                if(info == null)
                {
                    info = new Dictionary<string, object>();
                }
                info["$s"] = 0;
                info["$len"] = Grammar.Call(this.expr1, "getAttribute", new object[] { "length" });
                info["$i"] = null;
                for (var i = 0; i < (int)info["$len"]; i++)
                {
                    info["$i"] = Grammar.Call(this.expr1, "getAttribute", new object[] { i });
                    Grammar.Call(this.expr2, "getValue",new object[] { info});
                }
                return info["$s"];
            }
            return null;
        }
    }
}