using System.Collections.Generic;

namespace lib
{
    public class Compiler
    {
        public Scanner _scanner;
        public Parser _parser;

        public Compiler()
        {
            this._scanner = new Scanner();
            this._parser = new Parser();
        }

        public object parserExpr(string content, List<object> checks,Dictionary<string,object> objects, Dictionary<string,object> classes,List<object> result,Binding binding)
        {
            var scanner = new Scanner();
            var common = new CommonInfo() {
                content = content,
                objects = objects,
                classes = classes,
                checks = checks,
                ids = new Dictionary<string, CompilerId>(),
                tokenValue = null,
                scanner = this._scanner,
                nodeStack = null,
                bindList = new List<object>(),
                binding = binding
            };
            this._scanner.setCommonInfo(common);
            this._parser.setCommonInfo(common);
            this._parser.parser(content);
            if (common.parserError) {
                return null;
            }
            common.result = result;
            common.expr = common.newNode.expval as Stmts;
            common.expr.checkPropertyBinding(common);
            return common.expr;
        }

        public static Compiler ist;

        public static object parserExprStatic(string content, List<object> checks, Dictionary<string, object> objects, Dictionary<string, object> classes, List<object> result,Binding binding)
        {
            if (Compiler.ist == null)
            {
                Compiler.ist = new Compiler();
            }
            return Compiler.ist.parserExpr(content, checks, objects, classes, result, binding);
        }
    }
}