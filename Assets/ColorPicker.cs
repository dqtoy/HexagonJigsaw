using System;
using System.Collections;
using System.Collections.Generic;
using lib;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour {

    public GameObject color0;
    public GameObject color1;
    public GameObject color2;
    public GameObject color3;
    public GameObject color4;
    public GameObject color5;
    public GameObject color6;
    public GameObject color7;
    public GameObject color8;
    public GameObject color9;
    public GameObject color10;
    public GameObject color11;
    public GameObject color12;

    private List<GameObject> list;

    // Use this for initialization
    void Start () {
        list = new List<GameObject>();
        list.Add(color0);
        list.Add(color1);
        list.Add(color2);
        list.Add(color3);
        list.Add(color4);
        list.Add(color5);
        list.Add(color6);
        list.Add(color7);
        list.Add(color8);
        list.Add(color9);
        list.Add(color10);
        list.Add(color11);
        list.Add(color12);
        for (int i = 0; i < list.Count; i++)
        {
            EditorVO.Instance.dispatcher.AddListener("UIcolor" + i + "Handle", UIcolorHandler);
        }
        EditorVO.Instance.color.AddListener(lib.Event.CHANGE, ChangeColor);
        ChangeColor();
    }

    private void UIcolorHandler(lib.Event e)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if(list[i] == e.Data as GameObject)
            {
                EditorVO.Instance.color.value = i;
            }
        }
    }

    void ChangeColor(lib.Event e=null)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (EditorVO.Instance.color.value == i)
            {
                list[i].GetComponent<Image>().color = new Color(list[i].GetComponent<Image>().color.r, list[i].GetComponent<Image>().color.g, list[i].GetComponent<Image>().color.b, 1);
            }
            else
            {
                list[i].GetComponent<Image>().color = new Color(list[i].GetComponent<Image>().color.r, list[i].GetComponent<Image>().color.g, list[i].GetComponent<Image>().color.b, 0.3f);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
