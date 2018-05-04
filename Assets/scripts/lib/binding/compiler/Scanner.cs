using System.Collections.Generic;

namespace lib
{
    public class Scanner
    {
        public int start;
        public Dictionary<string, object> moves;
        public Dictionary<string, object> endInfos;
        public Dictionary<string, Dictionary<string,object>> befores;
        public Dictionary<string, object> inputs;
        public int tokenPos;
        public string tokenContent;
        public int tokenContentLength;
        public CommonInfo commonInfo;
        public object lastToken;

        public Scanner()
        {
            this.start = ScannerTable.start;
            this.moves = ScannerTable.moves;
            this.endInfos = ScannerTable.endInfos;
            this.befores = ScannerTable.befores;
            this.inputs = ScannerTable.inputs;
            this.tokenPos = 0;
            this.tokenContent = null;
            this.tokenContentLength = 0;
            this.commonInfo = null;
            this.lastToken = null;
        }

        public void setCommonInfo(CommonInfo info)
        {
            this.commonInfo = info;
        }

        public void setTokenContent(string content)
        {
            content += "\r\n";
            this.tokenContent = content;
            this.tokenPos = 0;
            this.tokenContentLength = content.Length;
            this.lastToken = null;
        }

        public string getNextToken()
        {
            if (this.tokenContentLength == 0)
            {
                return null;
            }
            int recordPos = this.tokenPos;
            int ch;
            int findStart = this.tokenPos;
            int state = this.start;
            List<List<int>> receiveStack = new List<List<int>>();
            int lastEndPos = -1;
            int lastEndState = -1;
            while (this.tokenPos < this.tokenContentLength)
            {
                ch = this.tokenContent[this.tokenPos];
                if (ch == 92 && this.tokenPos < this.tokenContent.Length)
                {
                    this.tokenPos++;
                }
                if (this.inputs.ContainsKey(ch + "") == false)
                {
                    ch = 20013;
                }
                if (this.moves.ContainsKey(state + "") == false || (this.moves[state + ""] as Dictionary<string, object>).ContainsKey(ch + "") == false)
                    break;
                state = (int)(this.moves[state + ""] as Dictionary<string,object>)[ch + ""];
                if (this.endInfos.ContainsKey(state + "") == true)
                {
                    lastEndPos = this.tokenPos;
                    lastEndState = state;
                    receiveStack.Add(new List<int> { this.tokenPos, state });
                    if ((bool)this.endInfos[state + ""] == true)
                        break;
                }
                this.tokenPos++;
            }
            List<int> last;
            if (receiveStack.Count > 0)
            {
                while (receiveStack.Count > 0)
                {
                    last = receiveStack[receiveStack.Count - 1];
                    receiveStack.RemoveAt(receiveStack.Count - 1);
                    lastEndPos = last[0];
                    lastEndState = last[1];
                    if (this.lastToken == null || this.befores.ContainsKey(lastEndState + "") == false || (this.befores.ContainsKey(lastEndState + "") != false && this.befores[lastEndState + ""].ContainsKey(this.lastToken + "") != false))
                    {
                        this.tokenPos = lastEndPos + 1;
                        string str = StringUtils.Slice(tokenContent, findStart, this.tokenPos);
                        string result = this.getTokenComplete(lastEndState, str);
                        if (result == null)
                            return this.getNextToken();
                        this.commonInfo.tokenPos = findStart;
                        if (TokenType.TokenTrans.ContainsKey(result) != false)
                            this.lastToken = this.commonInfo.tokenValue;
                        else
                            this.lastToken = result;
                        return result;
                    }
                }
            }
            if (this.tokenPos < this.tokenContent.Length)
            {
            }
            else
            {
                this.commonInfo.tokenValue = null;
                return (string)TokenType.Type["endSign"];
            }
            return null;
        }

        public CompilerId installId(CommonInfo commonInfo,string content)
        {
            if (commonInfo.ids.ContainsKey(content))
            {
                return commonInfo.ids[content];
            }
            var id = new CompilerId(){ name =  content };
            commonInfo.ids[content] = id;
            return id;
        }

        public string getTokenComplete(int token, string content)
        {
            this.commonInfo.tokenValue = null;
            switch (token)
            {
                case 1: return null;
                case 39: return (string)TokenType.Type["null"];
                case 27: return (string)TokenType.Type["as"];
                case 28: return (string)TokenType.Type["is"];
                case 40: return (string)TokenType.Type["true"];
                case 41: return (string)TokenType.Type["false"];
                case 36: return (string)TokenType.Type["for"];
                case 3: this.commonInfo.tokenValue = content; return (string)TokenType.Type["op"];
                case 4: this.commonInfo.tokenValue = content; return (string)TokenType.Type["op"];
                case 5: this.commonInfo.tokenValue = content; return (string)TokenType.Type["op"];
                case 6: this.commonInfo.tokenValue = content; return (string)TokenType.Type["op"];
                case 7: this.commonInfo.tokenValue = content; return (string)TokenType.Type["op"];
                case 8: this.commonInfo.tokenValue = content; return (string)TokenType.Type["op"];
                case 9: this.commonInfo.tokenValue = content; return (string)TokenType.Type["op"];
                case 10: this.commonInfo.tokenValue = content; return (string)TokenType.Type["op"];
                case 11: this.commonInfo.tokenValue = content; return (string)TokenType.Type["op"];
                case 12: this.commonInfo.tokenValue = content; return (string)TokenType.Type["op"];
                case 13: this.commonInfo.tokenValue = content; return (string)TokenType.Type["op"];
                case 14: this.commonInfo.tokenValue = content; return (string)TokenType.Type["op"];
                case 15: this.commonInfo.tokenValue = content; return (string)TokenType.Type["op"];
                case 16: this.commonInfo.tokenValue = content; return (string)TokenType.Type["op"];
                case 31: this.commonInfo.tokenValue = content; return (string)TokenType.Type["op"];
                case 32: this.commonInfo.tokenValue = content; return (string)TokenType.Type["op"];
                case 19: this.commonInfo.tokenValue = content; return (string)TokenType.Type["op"];
                case 17: this.commonInfo.tokenValue = content; return (string)TokenType.Type["op"];
                case 18: this.commonInfo.tokenValue = content; return (string)TokenType.Type["op"];
                case 20: this.commonInfo.tokenValue = content; return (string)TokenType.Type["op"];
                case 30: this.commonInfo.tokenValue = content; return (string)TokenType.Type["op"];
                case 29: this.commonInfo.tokenValue = content; return (string)TokenType.Type["op"];
                case 38: this.commonInfo.tokenValue = content; return (string)TokenType.Type["op"];
                case 37: this.commonInfo.tokenValue = content; return (string)TokenType.Type["op"];
                case 21: this.commonInfo.tokenValue = content; return (string)TokenType.Type["op"];
                case 22: this.commonInfo.tokenValue = content; return (string)TokenType.Type["op"];
                case 23: this.commonInfo.tokenValue = content; return (string)TokenType.Type["op"];
                case 24: this.commonInfo.tokenValue = content; return (string)TokenType.Type["op"];
                case 25: this.commonInfo.tokenValue = content; return (string)TokenType.Type["op"];
                case 26:
                case 44: this.commonInfo.tokenValue = content; return (string)TokenType.Type["valueInt"];
                case 34: this.commonInfo.tokenValue = content; return (string)TokenType.Type["valueOxInt"];
                case 33: this.commonInfo.tokenValue = content; return (string)TokenType.Type["valueNumber"];
                case 35: this.commonInfo.tokenValue = content; return (string)TokenType.Type["valueString"];
                case 2:
                case 43:
                case 46:
                case 47:
                case 48:
                case 49:
                case 50:
                case 51:
                case 53:
                case 54:
                case 55:
                case 56:
                case 57:
                case 58: this.commonInfo.tokenValue = this.installId(this.commonInfo, content); return (string)TokenType.Type["id"];
            }
            return null;
        }
    }

    public class CompilerId
    {
        public string name;
    }
}