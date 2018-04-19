using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorTip : MonoBehaviour {

    private int showTime = 0;
    private List<string> tips = new List<string>();

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
            if(tips.Count > 0)
            {
                gameObject.GetComponent<Text>().text = tips[0];
                showTime = 120;
                tips.RemoveAt(0);
            }
        }
    }

    private void show(string txt)
    {
        if(tips.Count > 0 && tips[tips.Count - 1] == txt)
        {
            return;
        }
        tips.Add(txt);
    }

    private static EditorTip instance;

    public static void Show(string txt)
    {
        instance.show(txt);
    }
}
