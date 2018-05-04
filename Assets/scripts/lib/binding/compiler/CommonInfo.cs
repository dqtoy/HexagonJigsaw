using System.Collections.Generic;

namespace lib
{
    public class CommonInfo
    {
        /// <summary>
        /// 需要解析的内容比如 data.x * data.y
        /// </summary>
        public string content;

        public Dictionary<string, object> objects;

        public Dictionary<string, object> classes;

        public List<object> checks;

        public Dictionary<string, CompilerId> ids = new Dictionary<string, CompilerId>();

        public object tokenValue;

        public Scanner scanner;

        public List<CompilerNode> nodeStack;

        public int tokenPos;

        public int tokenCount;

        public int lastTokenPos;

        public List<object> specialFor;

        public List<object> bindList = new List<object>();

        public Binding binding;

        public List<object> result;

        public Stmts expr;

        public CompilerNode newNode;

        public bool parserError = false;
    }

}