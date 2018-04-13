using UnityEngine;
using System.Collections;
using lib;
using UnityEngine.UI;
using System;

public class EditorMain : MonoBehaviour
{
    /// <summary>
    /// 颜色值
    /// </summary>
    public Dropdown colorDropDown;

    public Image grid;

    /// <summary>
    /// 要载入的关卡
    /// </summary>
    public Text loadLevelTxt;


    // Use this for initialization
    void Start()
    {
        EditorVO.Instance.dispatcher.AddListener("UIcolorDropDownHandle", OnColorChange);

        //生成背景
        for (int x = 0; x < 9; x++)
        {
            for (int y = 0; y > -6; y--)
            {
                GameObject obj = ResourceManager.CreateImage("image/grid/gridBg");
                obj.transform.Rotate(new Vector3(0, 0, 90f));
                Point2D position = HaxgonCoord.CoordToPosition(Point2D.Create(x - 4, y + 2), 0.42f);
                obj.transform.position = new Vector3(position.x, position.y);
            }
        }
    }

    private void OnColorChange(lib.Event e)
    {
        grid.sprite = e.Data as Sprite;
    }

    void CreateLevel()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
