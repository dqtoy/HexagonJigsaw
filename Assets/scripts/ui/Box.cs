using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Box : MonoBehaviour {

    public Image HexIma;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetRotate() {
        //this.transform.Rotate(new Vector3(0,0,0));
        transform.DORotate(new Vector3(0, 0, 0),0.1f);
    }

    public void ShowHex() {
        HexIma.DOFillAmount(1, 0.6f);
    }
}
