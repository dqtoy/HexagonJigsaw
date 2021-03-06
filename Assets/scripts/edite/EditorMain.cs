﻿using UnityEngine;
using System.Collections;
using lib;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.IO;

public class EditorMain : MonoBehaviour
{
    /// <summary>
    /// 颜色值
    /// </summary>
    public Dropdown colorDropDown;

    public Image grid;

    public Text levelTxt;

    public Text maxLevelTxt;

    private float offx = -0.4f * 1.5f * 8;
    private float offy = +0.4f * 10;

    private float offx1 = -0.4f * 1.5f * 8;
    private float offy1 = -0.4f * 3;

    private float offx2 = +0.4f * 1.0f * 1;
    private float offy2 = -0.4f * 10;

    // Use this for initialization
    void Start()
    {
        new EditorMainThread();
        new CheckThread();

        ConfigDecode.Decode();

        //删除重复的 piece 信息
        for(int i = 0; i < PieceConfig.Configs.Count; i ++)
        {
            for(int j = i + 1; j < PieceConfig.Configs.Count; j++)
            {
                if(PieceConfig.Configs[i].id == PieceConfig.Configs[j].id)
                {
                    PieceConfig.Configs.RemoveAt(j);
                    EditorTip.Show("重复的片信息 : " + PieceConfig.Configs[i].id);
                    j--;
                }
            }
        }

        EditorVO.Instance.dispatcher.AddListener("UIsaveHandle", OnSave);
        EditorVO.Instance.dispatcher.AddListener("UIloadHandle", OnLoad);
        EditorVO.Instance.dispatcher.AddListener("UIclearHandle", OnClear);

        //colorDropDown.options = EditorVO.Instance.colors;
        //colorDropDown.value = EditorVO.Instance.color.value;

        //生成格子
        List<GridVO> grids = EditorVO.Instance.grids;
        for(int i = 0; i < grids.Count; i++)
        {
            GameObject obj = ResourceManager.CreateImage("image/grid/gridBg");
            Point2D position = HaxgonCoord<Point2D>.CoordToPosition(Point2D.Create(grids[i].x.value, grids[i].y.value), 0.4f);
            obj.transform.position = new Vector3((float)(position.x + offx), position.y + offy);
            (obj.AddComponent<GameGrid>()).vo = grids[i];
        }

        grids = EditorVO.Instance.otherGrids1;
        for (int i = 0; i < grids.Count; i++)
        {
            GameObject obj = ResourceManager.CreateImage("image/grid/gridBg");
            Point2D position = HaxgonCoord<Point2D>.CoordToPosition(Point2D.Create(grids[i].x.value, grids[i].y.value), 0.4f);
            obj.transform.position = new Vector3((float)(position.x + offx1), position.y + offy1);
            (obj.AddComponent<GameGrid>()).vo = grids[i];
        }

        grids = EditorVO.Instance.piecesGrids;
        for (int i = 0; i < grids.Count; i++)
        {
            GameObject obj = ResourceManager.CreateImage("image/grid/gridBg");
            obj.transform.localScale = new Vector3(0.5f, 0.5f);
            Point2D position = HaxgonCoord<Point2D>.CoordToPosition(Point2D.Create(grids[i].x.value, grids[i].y.value), 0.2f);
            obj.transform.position = new Vector3((float)(position.x + 2), position.y);
            (obj.AddComponent<GameGrid>()).vo = grids[i];
        }

        for (int i = 0; i < LevelConfig.Configs.Count; i++)
        {
            for (int j = 0; j < LevelConfig.Configs[i].pieces.Count; j++)
            {
                if (LevelConfig.Configs[i].pieces[j] == null)
                {
                    EditorTip.Show("关卡 " + LevelConfig.Configs[i].id + " 缺少片信息");
                }
            }
            for (int j = 0; j < LevelConfig.Configs[i].pieces2.Count; j++)
            {
                if (LevelConfig.Configs[i].pieces2[j] == null)
                {
                    EditorTip.Show("关卡 " + LevelConfig.Configs[i].id + " 缺少片信息");
                }
            }
        }
    }

    private void OnColorChange(lib.Event e)
    {
        grid.sprite = e.Data as Sprite;
        EditorVO.Instance.SelectColor(e.Data as Sprite);
    }

    private void OnClear(lib.Event e)
    {
        //清除之前的颜色
        List<GridVO> grids = EditorVO.Instance.piecesGrids;
        for (int i = 0; i < grids.Count; i++)
        {
            grids[i].color.value = 0;
        }
        grids = EditorVO.Instance.grids;
        for (int i = 0; i < grids.Count; i++)
        {
            grids[i].color.value = 0;
        }
        grids = EditorVO.Instance.otherGrids1;
        for (int i = 0; i < grids.Count; i++)
        {
            grids[i].color.value = 0;
        }
        EditorTip.Show("已清除");
    }

    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="e"></param>
    private void OnSave(lib.Event e)
    {
        if(levelTxt.text == "")
        {
            EditorTip.Show("未输入关卡");
            return;
        }
        EditorTip.Show("保存中...");
        new SaveLevelCommand(Convert.ToInt32(levelTxt.text));
        EditorTip.Show("保存成功!");
        DrawPieces();
    }

    private void OnLoad(lib.Event e)
    {
        if (levelTxt.text == "")
        {
            EditorTip.Show("未输入关卡");
            return;
        }
        LevelConfig level = LevelConfig.GetConfig(Convert.ToInt32(levelTxt.text));
        if (level == null)
        {
            EditorTip.Show("没有对应的关卡!");
            return;
        }
        //清除之前的颜色
        List<GridVO> grids = EditorVO.Instance.piecesGrids;
        for (int i = 0; i < grids.Count; i++)
        {
            grids[i].color.value = 0;
        }
        grids = EditorVO.Instance.grids;
        for (int i = 0; i < grids.Count; i++)
        {
            grids[i].color.value = 0;
        }
        grids = EditorVO.Instance.otherGrids1;
        for (int i = 0; i < grids.Count; i++)
        {
            grids[i].color.value = 0;
        }

        for(int i = 0; i < level.pieces.Count; i++)
        {
            for(int n = 0; n < level.pieces[i].coords.Count; n++)
            {
                if(EditorVO.Instance.GetGrid(level.pieces[i].coords[n].x, level.pieces[i].coords[n].y).color.value != 0)
                {
                    EditorTip.Show("关卡数据错误，有重叠!");
                }
                EditorVO.Instance.GetGrid(level.pieces[i].coords[n].x, level.pieces[i].coords[n].y).color.value = i + 1;
            }
        }
        for (int i = 0; i < level.pieces2.Count; i++)
        {
            for (int n = 0; n < level.pieces2[i].coords.Count; n++)
            {
                if (EditorVO.Instance.GetGrid1(level.pieces2[i].coords[n].x, level.pieces2[i].coords[n].y).color.value != 0)
                {
                    EditorTip.Show("关卡数据错误，多余片有重叠!");
                }
                EditorVO.Instance.GetGrid1(level.pieces2[i].coords[n].x, level.pieces2[i].coords[n].y).color.value = level.pieces.Count + i + 1;
            }
        }
        EditorTip.Show("加载成功!");
        new CreateLevelCommand();
        DrawPieces();
    }

    private float lastClick = 0;
    // Update is called once per frame
    void Update()
    {
        EditorMainThread.Instance.Update();
        CheckThread.Instance.Update();

        if (Input.GetAxis("Fire1") > 0 && lastClick == 0)
        {
            Vector3 pos = Input.mousePosition;
            pos.x = (pos.x / GameVO.Instance.PixelWidth - 0.5f) * GameVO.Instance.Width;
            pos.y = (pos.y / GameVO.Instance.PixelHeight - 0.5f) * GameVO.Instance.Height;

            Point2D p = HaxgonCoord<Point2D>.PositionToCoord(Point2D.Create(pos.x - offx, pos.y - offy), 0.4f);
            GridVO grid = EditorVO.Instance.GetGrid((int)p.x, (int)p.y);
            if(grid != null)
            {
                grid.color.value = EditorVO.Instance.color.value;
            }


            p = HaxgonCoord<Point2D>.PositionToCoord(Point2D.Create(pos.x - offx1, pos.y - offy1), 0.4f);
            grid = EditorVO.Instance.GetGrid1((int)p.x, (int)p.y);
            if (grid != null)
            {
                grid.color.value = EditorVO.Instance.color.value;
            }
            new CreateLevelCommand();
            DrawPieces();
        }
        lastClick = Input.GetAxis("Fire1");

        int max = 0;
        for (int i = 0; i < LevelConfig.Configs.Count; i++)
        {
            if(LevelConfig.Configs[i].id > max)
            {
                max = LevelConfig.Configs[i].id;
            }
        }
        maxLevelTxt.text = "最大关卡:" + max;
    }

    private void DrawPieces()
    {
        //清除之前的颜色
        List<GridVO> grids = EditorVO.Instance.piecesGrids;
        for(int i = 0; i < grids.Count; i++)
        {
            grids[i].color.value = 0;
        }

        //放置新的关卡片信息
        for(int i = 0; i < EditorVO.Instance.level.pieces.Count; i++)
        {
            SetPiece(EditorVO.Instance.level.pieces[i]);
        }
        for (int i = 0; i < EditorVO.Instance.level.otherPieces.Count; i++)
        {
            SetPiece(EditorVO.Instance.level.otherPieces[i]);
        }
    }

    private void SetPiece(EditorLevelPiece piece)
    {
        List<GridVO> grids = EditorVO.Instance.piecesGrids;
        int minx = piece.grids[0].x;
        int maxy = piece.grids[0].y;
        for (int i = 1; i < piece.grids.Count; i++)
        {
            if (piece.grids[i].x < minx)
            {
                minx = piece.grids[i].x;
            }
            if (piece.grids[i].y > maxy)
            {
                maxy = piece.grids[i].y;
            }
        }
        int offx = minx % 2 == 0 ? -minx : -minx + 1;
        int offy = -maxy;
        for (int y = 0; y > -10; y--)
        {
            for (int x = 0; x < 22; x++,x++)
            {
                bool find = true;
                for (int i = 0; i < piece.grids.Count; i++)
                {
                    GridVO grid = EditorVO.Instance.GetGridPiece((int)piece.grids[i].x + offx + x, (int)piece.grids[i].y + offy + y);
                    if (grid == null || grid.color.value != 0)
                    {
                        find = false;
                        break;
                    }
                    else
                    {
                        //获取周围的格子
                        List<Point2D> nextCoords = HaxgonCoord<Point2D>.GetCoordsNextTo(Point2D.Create((int)piece.grids[i].x + offx + x, (int)piece.grids[i].y + offy + y));
                        for (int n = 0; n < nextCoords.Count; n++)
                        {
                            GridVO nextGrid = EditorVO.Instance.GetGridPiece((int)nextCoords[n].x, (int)nextCoords[n].y);
                            if (nextGrid != null && nextGrid.color.value != 0 && nextGrid.color.value != grid.color.value)
                            {
                                find = false;
                                break;
                            }
                        }
                    }
                }
                if (find)
                {
                    for (int i = 0; i < piece.grids.Count; i++)
                    {
                        EditorVO.Instance.GetGridPiece((int)piece.grids[i].x + offx + x, (int)piece.grids[i].y + offy + y).color.value = piece.grids[i].color;
                    }
                    return;
                }
            }
        }
    }

    void OnDestroy()
    {
        Thread.ExitAll();
    }
}
