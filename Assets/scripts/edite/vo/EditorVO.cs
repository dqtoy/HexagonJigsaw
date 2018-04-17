using lib;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorVO
{
    public EventDispatcher dispatcher = new EventDispatcher();

    /// <summary>
    /// 当前填充的颜色类型
    /// </summary>
    public Int color = new Int();

    public List<Dropdown.OptionData> colors = new List<Dropdown.OptionData>();

    public List<GridVO> grids = new List<GridVO>();

    public List<GridVO> otherGrids1 = new List<GridVO>();

    public List<GridVO> piecesGrids = new List<GridVO>();

    //关卡配置信息
    public EditorLevelConfig level;

    public EditorVO()
    {
        Dropdown.OptionData op = new Dropdown.OptionData();
        op.text = "无";
        op.image = ResourceManager.CreateSprite("image/grid/gridBg");
        colors.Add(op);

        for (int i = 0; i < 12; i++)
        {
            op = new Dropdown.OptionData();
            op.text = "颜色" + (i + 1);
            op.image = ResourceManager.CreateSprite("image/grid/" + (i + 1));
            colors.Add(op);
        }

        color.value = 1;

        //生成格子
        for (int x = 0; x < 11; x++)
        {
            for (int y = 0; y > -7; y--)
            {
                GridVO grid = new GridVO();
                grid.x = new Int(x);
                grid.y = new Int(y);
                grid.color = new Int();
                grids.Add(grid);
            }
        }

        //生成干扰格子
        for (int x = 0; x < 11; x++)
        {
            for (int y = 0; y > -5; y--)
            {
                GridVO grid = new GridVO();
                grid.x = new Int(x);
                grid.y = new Int(y);
                grid.color = new Int();
                otherGrids1.Add(grid);
            }
        }

        //生成片的格子
        for(int x = 0; x < 22; x++)
        {
            for(int y = 0; y > -10; y--)
            {
                GridVO grid = new GridVO();
                grid.x = new Int(x);
                grid.y = new Int(y);
                grid.color = new Int();
                piecesGrids.Add(grid);
            }
        }
    }

    public GridVO GetGrid(int x, int y)
    {
        for(int i = 0; i < grids.Count; i++)
        {
            if(grids[i].x.value == x && grids[i].y.value == y)
            {
                return grids[i];
            }
        }
        return null;
    }

    public GridVO GetGrid1(int x, int y)
    {
        for (int i = 0; i < otherGrids1.Count; i++)
        {
            if (otherGrids1[i].x.value == x && otherGrids1[i].y.value == y)
            {
                return otherGrids1[i];
            }
        }
        return null;
    }

    public GridVO GetGridPiece(int x, int y)
    {
        for (int i = 0; i < piecesGrids.Count; i++)
        {
            if (piecesGrids[i].x.value == x && piecesGrids[i].y.value == y)
            {
                return piecesGrids[i];
            }
        }
        return null;
    }

    public void SelectColor(Sprite image)
    {
        for(int i = 0; i < colors.Count; i++)
        {
            if(colors[i].image == image)
            {
                color.value = i;
            }
        }
    }


    private static EditorVO instance;
    public static EditorVO Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new EditorVO();
            }
            return instance;
        }
    }
}