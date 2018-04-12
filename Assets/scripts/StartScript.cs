using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using lib;

public class StartScript : MonoBehaviour {

    public Camera mainCamera;

    // Use this for initialization
    void Start () {

        //存储屏幕信息
        Vector3 size = new Vector3();
        size = mainCamera.ViewportToWorldPoint(size);
        GameVO.Instance.Width = Mathf.Abs(size.x * 2);
        GameVO.Instance.Height = Mathf.Abs(size.y * 2);
        GameVO.Instance.PixelWidth = mainCamera.pixelWidth;
        GameVO.Instance.PixelHeight = mainCamera.pixelHeight;

        for(int y = -4; y < 5; y++)
        {
            for (int x = -4; x < 5; x++)
            {
                GameObject obj = ResourceManager.CreateImage("BBB/D");
                obj.transform.Rotate(new Vector3(0,0, 90f));
                Point2D position = HaxgonCoord.CoordToPosition(Point2D.Create(x, y), 0.46f);
                obj.transform.position = new Vector3(position.x,position.y);
            }
        }

    }

    private float lastClick = 0;

    // Update is called once per frame
    void Update () {
        if (Input.GetAxis("Fire1") > 0 && lastClick == 0)
        {
            Vector3 pos = Input.mousePosition;
            pos.x = (pos.x / GameVO.Instance.PixelWidth - 0.5f) * GameVO.Instance.Width;
            pos.y = (pos.y / GameVO.Instance.PixelHeight - 0.5f) * GameVO.Instance.Height;
            Point2D p = HaxgonCoord.PositionToCoord(Point2D.Create(pos.x, pos.y), 0.46f);
            Debug.Log(p.x + "," + p.y);
        }
        lastClick = Input.GetAxis("Fire1");
    }
}

public class Point<T>
{
    public T x;
    public T y;

    public Point(T x,T y)
    {
        this.x = x;
        this.y = y;
        Debug.Log("[new Point] " + x + "," + y);
    }

    public void Print()
    {
        Debug.Log(x + "," + y);
    }

    private static List<Point<T>> pools = new List<Point<T>>();

    public static void Release(Point<T> p)
    {
        pools.Add(p);
        Debug.Log("[release Point] " + pools.Count);
    }

    private static int id = 0;
    public static int Id
    {
        get
        {
            id++;
            return id;
        }
    }
    
    public static Point<T> Create(T x, T y)
    {
        if(pools.Count > 0)
        {
            Point<T> p = pools[pools.Count - 1];
            pools.RemoveAt(pools.Count - 1);
            p.x = x;
            p.y = y;
            return p;
        }
        return new Point<T>(x,y);
    }
}
