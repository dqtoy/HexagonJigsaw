using System;
using lib;
using UnityEngine;
using UnityEngine.UI;

public class TextExd : MonoBehaviour
{
    private Text txt;

    private void Awake()
    {
        GameVO.Instance.language.AddListener(lib.Event.CHANGE, Change);
        Change();
    }

    private void Change(lib.Event e = null)
    {
        if(!txt)
        {
            txt = gameObject.GetComponent<Text>();
        }
        LanguageConfig lc = LanguageConfig.GetConfig(languageId);
        Type t = typeof(LanguageConfig);
        txt.text = (string)t.GetField(LanguageTypeConfig.GetConfig(GameVO.Instance.language.value).name).GetValue(lc);
    }

    public int _langueId;
    public int languageId
    {
        get { return _langueId; }
        set { _langueId = value; Change(); }
    }
}