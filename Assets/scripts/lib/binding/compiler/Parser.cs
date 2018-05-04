using System.Collections.Generic;

namespace lib
{
    public class Parser
    {
        public Dictionary<string, object> action;
        public Dictionary<string, object> go;
        public CommonInfo commonInfo;

        public Parser()
        {
            this.action = ParserTable.action;
            this.go = ParserTable.go;
            this.commonInfo = null;
        }

        public void setCommonInfo(CommonInfo info)
        {
            this.commonInfo = info;
            this.commonInfo.tokenCount = 0;
        }

        
        public object parser(string content)
        {
            CommonInfo commonInfo = this.commonInfo;
            Scanner scanner = this.commonInfo.scanner;
            scanner.setTokenContent(content);
            object token;
            commonInfo.lastTokenPos = 0;
            token = scanner.getNextToken();
            CompilerNode newNode = new CompilerNode() { type = "leaf" , token = (string)token, value  = commonInfo.tokenValue };
            if (TokenType.TokenTrans.ContainsKey((string)token))
            {
                token = commonInfo.tokenValue;
            }
            commonInfo.tokenCount++;
            if (token == null) {
                return null;
            }
            int state = 1;
            List<int> stack = new List<int>() { state };
            List<CompilerNode> nodeStack = new List<CompilerNode>();
            commonInfo.nodeStack = nodeStack;
            int i;
            Dictionary<string,object> action;
            List<CompilerNode> popNodes;
            var commonDebug = new CompilerDebug(){ file = content };
            while (true) {
                if ((this.action[state + ""] as Dictionary<string,object>).ContainsKey((string)token) == false) {
                    //throw new System.Exception("3008");
                    //flower.sys.$error(3008, content, this.getFilePosInfo(content, commonInfo.lastTokenPos));
                    commonInfo.parserError = true;
                    return false;
                }
                action = (Dictionary<string, object>)(this.action[state + ""] as Dictionary<string,object>)[(string)token];
                if ((int)action["a"] == 0) {
                    break;
                }
                else if ((int)action["a"] == 1) {
                    popNodes = new List<CompilerNode>();
                    i = (int)(action["c"] as Dictionary<string,object>)["exp"];
                    while (i != 0) {
                        stack.RemoveAt(stack.Count - 1);
                        popNodes.Add(nodeStack[nodeStack.Count - 1]);
                        nodeStack.RemoveAt(nodeStack.Count - 1);
                        i--;
                    }
                    popNodes.Reverse();
                    commonInfo.newNode = new CompilerNode() {
                        type = "node",
                        create = (int)(action["c"] as Dictionary<string,object>)["id"],
                        nodes = popNodes,
                        tokenPos = popNodes[0].tokenPos,
                        debug = popNodes[0].debug
                    };
                    if ((bool)(action["c"] as Dictionary<string, object>)["code"]) {
                        this.runProgrammer((int)(action["c"] as Dictionary<string, object>)["id"], commonInfo.newNode, popNodes);
                    }
                    state = stack[stack.Count - 1];
                    state = (int)(this.go[state + ""] as Dictionary<string,object>)[(string)(action["c"] as Dictionary<string, object>)["head"]];
                    stack.Add(state);
                    nodeStack.Add(commonInfo.newNode);
                }
                else {
                    state = (int)((this.action[state + ""] as Dictionary<string,object>)[(string)token] as Dictionary<string,object>)["to"];
                    stack.Add(state);
                    nodeStack.Add(newNode);
                    token = null;
                    newNode = null;
                }
                if (token == null && (string)token != "$") {
                    commonInfo.lastTokenPos = commonInfo.tokenPos;
                    token = scanner.getNextToken();
                    commonInfo.tokenCount++;
                    if (token == null)
                        return false;
                    else
                        newNode = new CompilerNode(){
                            type = "leaf",
                            token = (string)token,
                            value = commonInfo.tokenValue,
                            tokenPos = commonInfo.tokenPos,
                            debug = commonDebug
                        };
                    if (TokenType.TokenTrans.ContainsKey((string)token))
                    {
                        token = commonInfo.tokenValue;
                    }
                }
            }
            return true;
        }

        public void runProgrammer(int id, CompilerNode node, List<CompilerNode> nodes)
        {
            CommonInfo common = this.commonInfo;
            switch (id)
            {
                case 1: node.expval = nodes[0].expval; break;
                case 3: node.expval = new Stmts(); (node.expval as Stmts).addStmt(nodes[0].expval as ExprStmt); break;
                case 4: node.expval = new ExprStmt(nodes[0].expval as Expr); break;
                case 5: node.expval = new DeviceStmt(); break;
                case 46: node.expval = new Expr("Atr", nodes[0].expval); break;
                case 47:
                case 67: node.expval = new Expr("int", nodes[0].value); break;
                case 48:
                case 68: node.expval = new Expr("0xint", nodes[0].value); break;
                case 49:
                case 69: node.expval = new Expr("number", nodes[0].value); break;
                case 50:
                case 70: node.expval = new Expr("string", nodes[0].value); break;
                case 55: node.expval = new ExprAtr(); (node.expval as ExprAtr).addItem(new ExprAtrItem("string", nodes[0].value)); break;
                case 51: node.expval = new Expr("boolean", "true"); break;
                case 52: node.expval = new Expr("boolean", "false"); break;
                case 53: node.expval = new Expr("null"); break;
                case 56: node.expval = new ExprAtr();
                    (node.expval as ExprAtr).addItem(new ExprAtrItem("id", Grammar.GetProperty(nodes[0].value,"name")));
                    break;
                case 57: node.expval = new ExprAtr(); (node.expval as ExprAtr).addItem(new ExprAtrItem("object", nodes[0].expval)); break;
                case 2: node.expval = nodes[1].expval; (node.expval as Stmts).addStmtAt(nodes[0].expval as ExprStmt, 0); break;
                case 6: node.expval = new Expr("-a", nodes[1].expval); break;
                case 7: node.expval = new Expr("+a", nodes[1].expval); break;
                case 8: node.expval = new Expr("!", nodes[1].expval); break;
                case 27: node.expval = new Expr("~", nodes[1].expval); break;
                case 60: node.expval = nodes[0].expval; (node.expval as ExprAtr).addItem(new ExprAtrItem("call", nodes[1].expval)); break;
                case 61: node.expval = new ExprAtr(); (node.expval as ExprAtr).addItem(new ExprAtrItem("id", Grammar.GetProperty(nodes[1].value, "name"), true)); break;
                case 66: node.expval = new Expr("string", Grammar.GetProperty(nodes[0].value, "name")); break;
                case 84:
                case 62: node.expval = new ObjectAtr(nodes.Count == 2?new List<object>(): (nodes[1].expval as List<object>)); break;
                case 13: node.expval = new Expr("-", nodes[0].expval, nodes[2].expval); break;
                case 12: node.expval = new Expr("+", nodes[0].expval, nodes[2].expval); break;
                case 9: node.expval = new Expr("*", nodes[0].expval, nodes[2].expval); break;
                case 10: node.expval = new Expr("/", nodes[0].expval, nodes[2].expval); break;
                case 11: node.expval = new Expr("%", nodes[0].expval, nodes[2].expval); break;
                case 14: node.expval = new Expr("<<", nodes[0].expval, nodes[2].expval); break;
                case 15: node.expval = new Expr(">>", nodes[0].expval, nodes[2].expval); break;
                case 16: node.expval = new Expr("<<<", nodes[0].expval, nodes[2].expval); break;
                case 17: node.expval = new Expr(">>>", nodes[0].expval, nodes[2].expval); break;
                case 18: node.expval = new Expr(">", nodes[0].expval, nodes[2].expval); break;
                case 19: node.expval = new Expr("<", nodes[0].expval, nodes[2].expval); break;
                case 32: node.expval = new Expr("=", nodes[0].expval, nodes[2].expval); break;
                case 26: node.expval = new Expr("&", nodes[0].expval, nodes[2].expval); break;
                case 28: node.expval = new Expr("^", nodes[0].expval, nodes[2].expval); break;
                case 29: node.expval = new Expr("|", nodes[0].expval, nodes[2].expval); break;
                case 30: node.expval = new Expr("&&", nodes[0].expval, nodes[2].expval); break;
                case 31: node.expval = new Expr("||", nodes[0].expval, nodes[2].expval); break;
                case 54: node.expval = new ExprAtr(); (node.expval as ExprAtr).addItem(new ExprAtrItem("()", nodes[1].expval)); break;
                case 73: node.expval = new CallParams(); (node.expval as CallParams).addParam(nodes[0].expval as Expr); break;
                case 85:
                case 71: node.expval = nodes.Count == 2 ? new CallParams() : nodes[1].expval; break;
                case 58: node.expval = nodes[0].expval; (node.expval as ExprAtr).addItem(new ExprAtrItem(".", Grammar.GetProperty(nodes[2].value, "name"))); break;
                case 38: node.expval = new Expr("-=", nodes[0].expval, nodes[3].expval); break;
                case 37: node.expval = new Expr("+=", nodes[0].expval, nodes[3].expval); break;
                case 25: node.expval = new Expr("!=", nodes[0].expval, nodes[3].expval); break;
                case 33: node.expval = new Expr("*=", nodes[0].expval, nodes[3].expval); break;
                case 34: node.expval = new Expr("/=", nodes[0].expval, nodes[3].expval); break;
                case 35: node.expval = new Expr("%=", nodes[0].expval, nodes[3].expval); break;
                case 40: node.expval = new Expr("<<=", nodes[0].expval, nodes[3].expval); break;
                case 41: node.expval = new Expr(">>=", nodes[0].expval, nodes[3].expval); break;
                case 20: node.expval = new Expr(">=", nodes[0].expval, nodes[3].expval); break;
                case 21: node.expval = new Expr("<=", nodes[0].expval, nodes[3].expval); break;
                case 22: node.expval = new Expr("==", nodes[0].expval, nodes[3].expval); break;
                case 36: node.expval = new Expr("&=", nodes[0].expval, nodes[3].expval); break;
                case 42: node.expval = new Expr("^=", nodes[0].expval, nodes[3].expval); break;
                case 43: node.expval = new Expr("|=", nodes[0].expval, nodes[3].expval); break;
                case 39: node.expval = new Expr("||=", nodes[0].expval, nodes[3].expval); break;
                case 86:
                case 72: node.expval = nodes[2].expval; (node.expval as CallParams).addParamAt(nodes[0].expval as Expr, 0); break;
                case 59: node.expval = nodes[0].expval; (node.expval as ExprAtr).addItem(new ExprAtrItem(".", Grammar.GetProperty(nodes[3].value, "name"), true)); break;
                case 64: node.expval = new List<object> { new List<object> { nodes[0].expval, nodes[2].expval } }; break;
                case 24: node.expval = new Expr("!==", nodes[0].expval, nodes[4].expval); break;
                case 23: node.expval = new Expr("===", nodes[0].expval, nodes[4].expval); break;
                case 44: node.expval = new Expr("?:", nodes[0].expval, nodes[2].expval, nodes[4].expval); break;
                case 87:
                case 63: node.expval = new List<object> { new List<object> { nodes[0].expval, nodes[2].expval } }; (node.expval as List<object>).AddRange(nodes.Count == 4?new List<object> { null } :(nodes[4].expval as List<object>)); break;
                case 45: node.expval = new Expr("spfor", nodes[2].expval, nodes[4].expval); break;
            }
        }
    }

    public class CompilerNode
    {
        public string type = "node";
        public int create;
        public List<CompilerNode> nodes;
        public int tokenPos;
        public object debug;
        public object expval;
        public string token;
        public object value;
    }

    public class CompilerDebug
    {
        public string file;
    }

}