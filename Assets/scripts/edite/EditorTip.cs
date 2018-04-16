using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorTip : MonoBehaviour {

    private int showTime = 0;

	// Use this for initialization
	void Start () {
        instance = this;
    }

    // Update is called once per frame
    void Update() {
        showTime--;
        if (showTime <= 0)
        {
            showTime = 0;
            gameObject.GetComponent<Text>().text = "";
        }
    }

    private void show(string txt)
    {
        gameObject.GetComponent<Text>().text = txt;
        showTime = 120;
    }

    private static EditorTip instance;

    public static void Show(string txt)
    {
        instance.show(txt);
    }
}
