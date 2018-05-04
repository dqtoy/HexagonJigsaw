using System;
using System.Collections.Generic;

namespace lib
{
    public class Binding
    {
        bool singleValue;
        List<object> list;
        List<object> stmts;
        object thisObj;
        string property;
        string content;
        List<object> checks;
        bool hasDispose = false;

        public Binding(object thisObj,List<object> checks,string property,string content)
        {
            this.thisObj = thisObj;
            this.checks = checks == null ?  new List<object>() : checks;
            this.property = property;
            this.content = content;
            if (checks != null && content.IndexOf("data") != -1)
            {
                for (int i = 0; i < checks.Count; i++)
                {
                    object display = checks[i];
                    if (Grammar.HasProperty(display,"id"))
                    {
                        int id = (int)Grammar.GetProperty(display, "id");
                        if (!Binding.changeList.ContainsKey(id))
                        {
                            Binding.changeList[id] = new List<Binding>();
                        }
                        Binding.changeList[id].Add(this);
                    }
                }
            }
            this.__bind(thisObj, ListUtils.Clone<object>(checks), property, content);
        }

        public void reset()
        {
            for (var i = 0; i < this.list.Count; i++)
            {
                (this.list[i] as IEventDispatcher).RemoveListener(Event.CHANGE, this.update);
            }
            this.__bind(this.thisObj, ListUtils.Clone<object>(checks), this.property, this.content);
        }

        


        public void addValueListener(ValueBase value)
        {
            value.AddListener(Event.CHANGE, this.update);
        }

        public void removeValueListener(ValueBase value)
        {
            value.RemoveListener(Event.CHANGE, this.update);
        }

        private void SetValue(object thisObj,string property, object value)
        {
            object p = Grammar.GetProperty(thisObj, property);
            if(p is Int)
            {
                (p as Int).value = Convert.ToInt32(value);
            }
            else if(p is Float)
            {
                (p as Float).value = Convert.ToInt32(value);
            }
            else if(p is String)
            {
                (p as String).value = Convert.ToString(value);
            }
            else if (p is Bool)
            {
                (p as Bool).value = Convert.ToBoolean(value);
            }
            else
            {
                Grammar.SetProperty(thisObj, property, value);
            }
        }

        public void update(Event e = null)
        {
            object value = null;
            if (this.singleValue)
            {
                try
                {
                    value = (this.stmts[0] as Stmts).getValue();
                }
                catch
                {
                    value = null;
                }
                SetValue(thisObj, property, value);
            }
            else
            {
                string str = "";
                for (var i = 0; i < this.stmts.Count; i++)
                {
                    object expr = this.stmts[i];
                    if (expr is Stmts) {
                        try
                        {
                            str += (expr as Stmts).getValue();
                        }
                        catch
                        {
                            str += "null";
                        }

                    } else {
                        str += expr;
                    }
                }
                SetValue(thisObj, property, str);
            }
        }

        public void dispose()
        {
            this.hasDispose = true;
            for (var i = 0; i < this.list.Count; i++)
            {
                (this.list[i] as IEventDispatcher).RemoveListener(Event.CHANGE, this.update);
            }
        }

        public static List<object> bindingChecks = new List<object>();

        public static void addBindingCheck(object check)
        {
            for (var i = 0; i < Binding.bindingChecks.Count; i++)
            {
                if (Binding.bindingChecks[i] == check)
                {
                    return;
                }
            }
            Binding.bindingChecks.Add(check);
        }

        public static Dictionary<int, List<Binding>> changeList = new Dictionary<int, List<Binding>>();

        public static void changeData(object display)
        {
            int id = (int)Grammar.GetProperty(display, "id");
            List<Binding> list = Binding.changeList[id];
            if (list != null)
            {
                for (var i = 0; i < list.Count; i++)
                {
                    list[i].reset();
                }
            }
        }

        public static void removeChangeObject(object display)
        {
            int id = (int)Grammar.GetProperty(display, "id");
            changeList.Remove(id);
        }

        public static void clearBindingChecks()
        {
            bindingChecks = null;
            changeList = new Dictionary<int, List<Binding>>();
        }

        public void __bind(object thisObj, List<object> checks, string property, string content)
        {
            this.list = new List<object>();
            this.stmts = new List<object>();
            this.singleValue = false;
            int i;
            if (checks == null)
            {
                checks = ListUtils.Clone<object>(bindingChecks);
            }
            else
            {
                for (i = 0; i < Binding.bindingChecks.Count; i++)
                {
                    checks.Add(Binding.bindingChecks[i]);
                }
            }
            checks.Add(thisObj);
            int lastEnd = 0;
            bool parseError = false;
            for (i = 0; i < content.Length; i++)
            {
                if (content[i] == '{')
                {
                    for (int j = i + 1; j < content.Length; j++)
                    {
                        if (content[j] == '{')
                        {
                            break;
                        }
                        if (content[j] == '}')
                        {
                            var bindContent = StringUtils.Slice(content, i + 1, j);
                            if (i == 0 && j == content.Length - 1)
                            {
                                this.singleValue = true;
                            }
                            if (lastEnd < i)
                            {
                                this.stmts.Add(StringUtils.Slice(content, lastEnd, i));
                            }
                            lastEnd = j + 1;
                            Stmts stmt = (Stmts)Compiler.parserExprStatic(bindContent, checks, new Dictionary<string, object>() { { "this", thisObj } },
                                new Dictionary<string, object>(){}, list,this);
                            if (stmt == null)
                            {
                                parseError = true;
                                break;
                            }
                            this.stmts.Add(stmt);
                            i = j;
                            break;
                        }
                    }
                }
            }
            if (parseError) {
                Grammar.SetProperty(thisObj, property, content);
                return;
            }
            if (lastEnd < content.Length) {
                this.stmts.Add(StringUtils.Slice(content, lastEnd, content.Length));
            }
            this.thisObj = thisObj;
            this.property = property;
            for (i = 0; i< this.list.Count; i++) {
                for (int j = 0; j < this.list.Count; j++)
                {
                    if (i != j && this.list[i] == this.list[j])
                    {
                        this.list.RemoveAt(j);
                        i = -1;
                        break;
                    }
                }
            }
            for (i = 0; i < this.list.Count; i++) {
                (this.list[i] as IEventDispatcher).AddListener(Event.CHANGE, this.update);
            }
            this.update();
        }
    }
}