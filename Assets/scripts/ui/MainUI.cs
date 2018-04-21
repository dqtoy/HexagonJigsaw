using System;
using System.Collections;
using System.Collections.Generic;
using lib;
using UnityEngine;

public class MainUI : MonoBehaviour {

    private void Awake()
    {
        ButtonClick.dispatcher.AddListener("daily", OnShowDaily);
        ButtonClick.dispatcher.AddListener("freedom", OnShowFreedom);
    }

    private void OnShowDaily(lib.Event e)
    {
        GameVO.Instance.ShowModule(ModuleName.Daily);
    }

    private void OnShowFreedom(lib.Event e)
    {
        GameVO.Instance.ShowModule(ModuleName.Freedom);
    }

    private void OnShow(lib.Event e)
    {
        gameObject.SetActive(true);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
