using System;
using System.Collections;
using System.Collections.Generic;
using lib;
using UnityEngine;

public class GameGrid : MonoBehaviour {

    private SpriteRenderer sprite;

    public GridVO vo;

	// Use this for initialization
	void Start () {
        sprite = gameObject.GetComponent<SpriteRenderer>();

        vo.color.AddListener(lib.Event.CHANGE, OnColorChange);

        sprite.color = new Color(1, 1, 1, 0.3f);
    }

    private void OnColorChange(lib.Event e)
    {
        sprite.sprite = EditorVO.Instance.colors[vo.color.value].image;
        sprite.color = new Color(1, 1, 1, vo.color.value==0?0.3f:1f);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
