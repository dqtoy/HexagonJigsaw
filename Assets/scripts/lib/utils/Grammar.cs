using System;
using System.Reflection;

namespace lib
{
    public class Grammar
    {
        public static bool HasProperty(object obj,string name)
        {
            Type t = obj.GetType();
            MemberInfo[] p = t.GetMember(name) as MemberInfo[];
            return p != null && p.Length > 0 && p[0] != null ? true : false;
        }

        public static object GetProperty(object obj,string name)
        {
            Type t = obj.GetType();
            MemberInfo m = t.GetMember(name)[0] as MemberInfo;
            if(m != null)
            {
                PropertyInfo p = m as PropertyInfo;
                if(p != null)
                {
                    return p.GetValue(obj, null);
                }
            }
            if(t.GetField(name) != null)
            {
                return t.GetField(name).GetValue(obj);
            }
            return t.GetMember(name)[0];
        }

        public static void SetProperty(object obj,string name,object value)
        {
            Type t = obj.GetType();
            MemberInfo m = t.GetMember(name)[0] as MemberInfo;
            if (m != null)
            {
                PropertyInfo p = m as PropertyInfo;
                if (p != null)
                {
                    p.SetValue(obj, value, null);
                    return;
                }
            }
            t.GetField(name).SetValue(obj, value);
        }

        public static object Call(object obj,string name,object[] param)
        {
            Type t = obj.GetType();
            MethodInfo p = t.GetMethod(name);
            return p.Invoke(obj, param);
        }
    }
}