using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using lib;
using System.IO;

public class StartScript : MonoBehaviour {

    public Camera mainCamera;

    /// <summary>
    /// 编辑器是否开启
    /// </summary>
    public bool editor = false;

    public GameObject EditorMain;

    private void Awake()
    {
        //存储屏幕信息
        Vector3 size = new Vector3();
        size = mainCamera.ViewportToWorldPoint(size);
        GameVO.Instance.Width = Mathf.Abs(size.x * 2);
        GameVO.Instance.Height = Mathf.Abs(size.y * 2);
        GameVO.Instance.PixelWidth = mainCamera.pixelWidth;
        GameVO.Instance.PixelHeight = mainCamera.pixelHeight;

        GameVO.Instance.editor = editor;

        //是否开启编辑器
        if (editor)
        {
            EditorMain.SetActive(true);
            return;
        }
        EditorMain.SetActive(false);

        //读取配置
        ConfigDecode.Decode();

        gameObject.AddComponent<hexjig.Start>();
    }

    void Update () {
    }
}