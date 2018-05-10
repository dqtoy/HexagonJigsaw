using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using lib;
using System.IO;
using System.Reflection;

public class StartScript : MonoBehaviour {

    public Camera mainCamera;

    /// <summary>
    /// 编辑器是否开启
    /// </summary>
    public bool editor = false;

    public bool startFlag = false;

    public GameObject EditorMain;

    public A a;

    private void Awake()
    {

        /*A a = new A();
        Type t = a.GetType();
        PropertyInfo pa = t.GetProperty("a");
        MemberInfo ma = t.GetMember("a")[0];
        PropertyInfo pb = t.GetProperty("b");
        MemberInfo mb = t.GetMember("b")[0];*/

        data = new A();
        data.x.value = 2;
        data.y.value = 3;
        new Binding(data, new List<object> { data }, "area", "{x * y}");
        new Binding(this, new List<object> { this }, "text", "point area = {data.area}");
        data.x.value = 4;

        //设置窗体大小
        //Screen.SetResolution(720/2,1280/2,false);

        //读取配置
        ConfigDecode.Decode();

        BindingProperty.checks.Add(GameVO.Instance);
        BindingProperty.checks.Add(Language.instance);
        BindingProperty.checks.Add(StringUtils.instance);

        GameVO.Instance.editor = editor;

        GameVO.Instance.language.value = LanguageTypeConfig.GetConfigWidth("name", "en_us").id;

        //是否开启编辑器
        if (editor)
        {
            EditorMain.SetActive(true);
            return;
        }
        EditorMain.SetActive(false);
    }

    private void OnNetComplete(lib.Event e)
    {
        Debug.Log(e.Data);
    }

    public A data;

    private string _text;
    public string text
    {
        get { return _text; }
        set {
            _text = value;
            Debug.Log(_text);
        }
    }

    private void Start()
    {
        //存储屏幕信息
        Vector3 size = new Vector3();
        size = mainCamera.ViewportToWorldPoint(size);
        GameVO.Instance.Width = Mathf.Abs(size.x * 2);
        GameVO.Instance.Height = Mathf.Abs(size.y * 2);
        GameVO.Instance.PixelWidth = mainCamera.pixelWidth;
        GameVO.Instance.PixelHeight = mainCamera.pixelHeight;

        if (!editor)
        {
            gameObject.AddComponent<hexjig.Start>();
        }
    }
}

public class A
{
    public lib.String name = new lib.String();
    public Int x = new Int();
    public Int y = new Int();
    public Int area = new Int();
}

public class B : A
{
    public Int b = new Int();
}

public class C : A
{
    public Int c = new Int();
}