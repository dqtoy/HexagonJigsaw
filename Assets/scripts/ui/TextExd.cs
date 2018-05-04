using System;
using lib;
using UnityEngine;
using UnityEngine.UI;

public class TextExd : MonoBehaviour
{
    private Text txt;

    private void Start()
    {
        if(!txt)
        {
            txt = gameObject.GetComponent<Text>();
        }
        GameVO.Instance.language.AddListener(lib.Event.CHANGE, Change);
        Change();
        ChangeBinding();
    }

    private void Change(lib.Event e = null)
    {
        if (!txt)
        {
            txt = gameObject.GetComponent<Text>();
        }
        if (_langueId == 0)
        {
            return;
        }
        txt.text = Language.instance.GetLanguage(GameVO.Instance.language.value,_langueId);
    }

    private Binding binding;


    public int _langueId = 0;
    public int languageId
    {
        get { return _langueId; }
        set { _langueId = value; Change(); }
    }

    private void ChangeBinding()
    {
        if (binding != null)
        {
            binding.dispose();
            binding = null;
        }
        if (_bindText != "")
        {
            binding = new Binding(this.txt, new System.Collections.Generic.List<object>() { GameVO.Instance } , "text", _bindText);
        }
    }

    public string _bindText = "";
    public string bindText
    {
        get { return _bindText; }
        set { _bindText = value; ChangeBinding(); }
    }

    private void OnDestroy()
    {
        if (binding != null)
        {
            binding.dispose();
            binding = null;
        }
    }
}