using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using lib;

public class HexProgress : MonoBehaviour {

    private Transform line0;
    private Transform line1;
    private Transform line2;
    private Transform line3;
    private Transform line4;
    private Transform line5;
    private float size = 0.82f;


    void Start()
    {
        line0 = CreateLine(0);
        /*line1 = CreateLine(1);
        line2 = CreateLine(2);
        line3 = CreateLine(3);
        line4 = CreateLine(4);
        line5 = CreateLine(5);*/
    }

    

    private Transform CreateLine(int index)
    {
        GameObject line = ResourceManager.CreateUIImage("image/uiitem/line1");
        line.transform.parent = gameObject.transform;
        line.transform.localScale = new Vector3(size, 1, 1);
        if (index == 0)
        {
            line.transform.localPosition = new Vector3(-size + 0.5f * size * 0.5f, 0.5f * size * 0.8660254037844386f);
            line.transform.localEulerAngles = new Vector3(0,0,60);
        }
        else if(index == 1)
        {
            line.transform.localPosition = new Vector3(-size/2 + 0.5f * size, size * 0.8660254037844386f);
            line.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        else if (index == 2)
        {
            line.transform.localPosition = new Vector3(size / 2 + 0.5f * size * 0.5f, size * 0.8660254037844386f - 0.5f * size * 0.8660254037844386f);
            line.transform.localEulerAngles = new Vector3(0, 0, -60);
        }
        else if (index == 3)
        {
            line.transform.localPosition = new Vector3(size / 2 - 0.5f * size * 0.5f, -size * 0.8660254037844386f - 0.5f * size * 0.8660254037844386f);
            line.transform.localEulerAngles = new Vector3(0, 0, -120);
        }
        else if(index == 4)
        {
            line.transform.localPosition = new Vector3(size / 2 - 0.5f * size * 0.5f, -size * 0.8660254037844386f);
            line.transform.localEulerAngles = new Vector3(0, 0, -180);
        }
        else if (index == 5)
        {
            line.transform.localPosition = new Vector3(-size / 2 - 0.5f * size * 0.5f, -size * 0.8660254037844386f + 0.5f * size * 0.8660254037844386f);
            line.transform.localEulerAngles = new Vector3(0, 0, -240);
        }
        return line.transform;
    }

    private void SetLineProgress(int index,Transform trans,float progress)
    {
        trans.localScale = new Vector3(size * progress, 1);
        if (index == 0)
        {
            trans.localPosition = new Vector3(-size + progress * 0.5f * size * 0.5f, progress * 0.5f * size * 0.8660254037844386f);
        }
        else if (index == 1)
        {
            trans.localPosition = new Vector3(-size / 2 + progress * 0.5f * size, size * 0.8660254037844386f);
        }
        else if (index == 2)
        {
            trans.localPosition = new Vector3(size / 2 + progress * 0.5f * size * 0.5f, size * 0.8660254037844386f - progress * 0.5f * size * 0.8660254037844386f);
        }
        else if (index == 3)
        {
            trans.localPosition = new Vector3(size / 2 - progress * 0.5f * size * 0.5f, -size * 0.8660254037844386f - progress * 0.5f * size * 0.8660254037844386f);
        }
        else if (index == 4)
        {
            trans.localPosition = new Vector3(size / 2 - progress * 0.5f * size * 0.5f, -size * 0.8660254037844386f);
        }
        else if (index == 5)
        {
            trans.localPosition = new Vector3(-size / 2 - progress * 0.5f * size * 0.5f, -size * 0.8660254037844386f + progress * 0.5f * size * 0.8660254037844386f);
        }
    }

    public static GameObject Create(float size)
    {
        GameObject obj = new GameObject();
        obj.AddComponent<HexProgress>().size = size;
        return obj;
    }
}
