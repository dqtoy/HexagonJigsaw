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
        if (editor)
        {
            EditorMain.SetActive(true);
            return;
        }
        EditorMain.SetActive(false);

        //读取配置
        ConfigDecode.Decode();

        LevelConfig level = LevelConfig.GetConfig(1);

        /*for(int i = 0; i < level.coords.Count; i++)
        {
            GameObject obj = ResourceManager.CreateImage("BBB/D");
            obj.transform.Rotate(new Vector3(0, 0, 90f));
            Point2D position = HaxgonCoord.CoordToPosition(Point2D.Create(level.coords[i].x - 4, level.coords[i].y + 4), 0.42f);
            obj.transform.position = new Vector3(position.x, position.y);
        }


        for(int n = 0; n < level.pieces.Count; n++)
        {
            for (int i = 0; i < level.pieces[n].coords.Count; i++)
            {
                GameObject obj = ResourceManager.CreateImage("BBB/D");
                obj.transform.Rotate(new Vector3(0, 0, 90f));
                Point2D position = HaxgonCoord.CoordToPosition(Point2D.Create(level.pieces[n].coords[i].x - 4 + n * 4, level.pieces[n].coords[i].y - 4), 0.42f);
                obj.transform.position = new Vector3(position.x, position.y);
            }
        }*/
        
    }

    private float lastClick = 0;

    // Update is called once per frame
    void Update () {

        /*
        if (Input.GetAxis("Fire1") > 0 && lastClick == 0)
        {
            Vector3 pos = Input.mousePosition;
            pos.x = (pos.x / GameVO.Instance.PixelWidth - 0.5f) * GameVO.Instance.Width;
            pos.y = (pos.y / GameVO.Instance.PixelHeight - 0.5f) * GameVO.Instance.Height;
            Point2D p = HaxgonCoord.PositionToCoord(Point2D.Create(pos.x, pos.y), 0.46f);
            Debug.Log(p.x + "," + p.y);
        }
        lastClick = Input.GetAxis("Fire1");*/
    }
}