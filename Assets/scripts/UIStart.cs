using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStart : MonoBehaviour {

    public Text Txt;
    public Camera camera;

	// Use this for initialization
	void Start () {
        Vector3 size = new Vector3();
        size = camera.ViewportToWorldPoint(size);
        Txt.text = "像素大小:" + camera.pixelWidth + "," + camera.pixelHeight + "\n世界坐标大小:" + Mathf.Abs(2 * size.x) + "," + Mathf.Abs(2 * size.y);
    }
	
	// Update is called once per frame
	void Update ()
    {

    }
}
