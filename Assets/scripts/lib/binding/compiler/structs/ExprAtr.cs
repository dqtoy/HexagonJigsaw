using System;
using System.Collections.Generic;
using System.Reflection;

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

        public void checkPropertyBinding(CommonInfo commonInfo)
        {
            object atr = null;
            bool getValue = false;
            if (this.list[0].type == "()")
            {
                Grammar.Call(this.list[0].val, "checkPropertyBinding",new object[] { commonInfo });
            }
            else if (this.list[0].type == "object")
            {
                Grammar.Call(this.list[0].val, "checkPropertyBinding", new object[] { commonInfo });
            }
            else if (this.list[0].type == "id")
            {
                if (commonInfo.specialFor != null && (string)this.list[0].val == "$i")
                {
                    //this.checkSpecialFor(commonInfo.specialFor, commonInfo.binding);
                }
                getValue = this.list[0].getValue;
                string name = this.list[0].val as string;
                if (name == "this")
                {
                    this.list.RemoveAt(0);
                }
                object thisObj = commonInfo.objects.ContainsKey("this") ? commonInfo.objects["this"] : null;
                if (thisObj != null && Grammar.HasProperty(thisObj, name))
                {
                    atr = Grammar.GetProperty(thisObj, name);
                    this.before = thisObj;
                }
                else if (commonInfo.objects.ContainsKey(name))
                {
                    this.before = commonInfo.objects[name];
                    this.beforeClass = false;
                    this.equalBefore = true;
                }
                else if (commonInfo.classes.ContainsKey(name))
                {
                    this.before = commonInfo.classes[name];
                    this.beforeClass = true;
                    this.equalBefore = true;
                }
                else if (commonInfo.checks != null)
                {
                    for (var c = 0; c < commonInfo.checks.Count; c++)
                    {
                        try
                        {
                            atr = Grammar.GetProperty(commonInfo.checks[c], name);
                            if (atr != null)
                            {
                                this.before = commonInfo.checks[c];
                            }
                        }
                        catch
                        {
                            atr = null;
                            this.before = null;
                        }

                        if (atr != null)
                        {
                            break;
                        }
                    }
                }
            }
            for (int i = 1; i < this.list.Count; i++)
            {
                if (this.list[i].type == ".")
                {
                    if (atr != null)
                    {
                        string atrName = (string)this.list[i].val;
                        getValue = this.list[i].getValue;
                        try
                        {
                            atr = Grammar.GetProperty(atr, atrName);
                        }
                        catch
                        {
                            atr = null;
                        }

                    }
                }
                else if (this.list[i].type == "call")
                {
                    atr = null;
                    Grammar.Call(this.list[i].val, "checkPropertyBinding", new object[] { commonInfo });
                }
            }
            if (atr != null && atr is ValueBase && !getValue) {
                this.value = atr;
                commonInfo.result.Add(atr);
            }
        }

        public object getValue(Dictionary<string,object> param = null)
        {
            if (this.value != null)
            {
                if(this.value is SimpleValue)
                {
                    return (this.value as SimpleValue).value;
                }
                else
                {
                    return this.value;
                }
            }
            bool getValue = false;
            object atr = null;
            object lastAtr = null;
            if (this.list[0].type == "()")
            {
                atr = Grammar.Call(this.list[0].val, "getValue",new object[] { param });
            }
            else if (this.list[0].type == "object")
            {
                atr = Grammar.Call(this.list[0].val, "getValue", new object[] { param });
            }
            else if (this.list[0].type == "id")
            {
                if (param != null && param.ContainsKey((string)this.list[0].val)) {
                    this.before = param;
                }
                getValue = this.list[0].getValue;

                atr = this.before;

                lastAtr = this.before;
                if (!this.equalBefore) {
                    try
                    {
                        atr = Grammar.GetProperty(atr, (string)this.list[0].val);
                    }
                    catch 
                    {
                        return null;
                    }
                }
            }
            for (var i = 1; i < this.list.Count; i++)
            { 
                try {
                    if (this.list[i].type == ".")
                    {
                        atr = Grammar.GetProperty(atr, (string)this.list[i].val);
                        getValue = this.list[i].getValue;
                    }
                    else if (this.list [i].type == "call") {
                        if (i == 2 && this.beforeClass)
                        {
                            atr = (atr as MethodInfo).Invoke(null, (this.list[i].val as CallParams).getValueList());
                        }
                        else
                        {
                            atr = (atr as MethodInfo).Invoke(lastAtr, (this.list[i].val as CallParams).getValueList());
                        }
                    }
                    if (i < this.list.Count - 1 && this.list [i + 1].type == "call") {
                        continue;
                    }
                    lastAtr = atr;
                }
                catch  {
                    return null;
                }
            }
            if (!getValue && atr is SimpleValue) {
                atr = (atr as SimpleValue).value;
            }
            return atr;
        }

        public object setValue(object val, Dictionary<string,object> param = null)
        {
            if (this.value != null)
            {
                Grammar.SetProperty(this.value, "value", val);
                return null;
            }
            object atr = null;
            if (this.list.Count > 1)
            {
                if (this.list[0].type == "id")
                {
                    if (param != null && param.ContainsKey((string)this.list[0].val)) {
                        atr = param[(string)this.list[0].val];
                    } else {
                        try {
                            atr = Grammar.GetProperty(this.before, (string)this.list[0].val);
                        } catch {
                            return null;
                        }
                    }
                }
            } else {
                if (this.list[0].type == "id")
                {
                    if (param != null && param.ContainsKey((string)this.list[0].val))
                    {
                        param[(string)this.list[0].val] = val;
                    }
                    else
                    {
                        try {
                            Grammar.SetProperty(this.before, (string)this.list[0].val, val);
                        } catch {
                            return null;
                        }
                    }
                }
                return null;
            }
            for (var i = 1; i< this.list.Count; i++) {
                try {
                    if (this.list[i].type == ".") {
                        if (i == this.list.Count - 1)
                        {
                            Grammar.SetProperty(atr, (string)this.list[i].val, val);
                        }
                        else
                        {
                            atr = Grammar.GetProperty(atr, (string)this.list[i].val);
                        }
                    }
                } catch {
                    return null;
                }
            }
            return null;
        }

        public object getAttribute(string name)
        {
            object val = this.getValue();
            return Grammar.GetProperty(val,name);
        }
    }
}