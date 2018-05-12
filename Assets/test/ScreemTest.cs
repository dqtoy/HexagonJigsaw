using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreemTest : MonoBehaviour {

    public Camera c1;
    public Camera c2;

    // Use this for initialization
    void Start () {
        Text txt = GetComponent<Text>();
        txt.text = c1.pixelWidth + "," + c2.pixelHeight + "\n" +
            c2.scaledPixelWidth + "," + c2.pixelHeight;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
