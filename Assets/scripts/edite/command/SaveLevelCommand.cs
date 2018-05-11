using hexjig;
using lib;
using System;
using System.Collections.Generic;
using System.IO;

public class SaveLevelCommand
{
    public SaveLevelCommand(int level)
    {
        //读取配置
        //ConfigDecode.Decode();

        //删除之前的 level 相关信息
        if (LevelConfig.GetConfig(level) != null)
        {
            LevelConfig old = LevelConfig.GetConfig(level);
            LevelConfig.Configs.Remove(old);
            for(int i = 0; i < old.pieces.Count; i++)
            {
                PieceConfig.Configs.Remove(PieceConfig.GetConfig(old.pieces[i].id));
            }
            for (int i = 0; i < old.pieces2.Count; i++)
            {
                PieceConfig.Configs.Remove(PieceConfig.GetConfig(old.pieces2[i].id));
            }
        }
        
        if(EditorVO.Instance.level.pieces.Count > 0)
        {
            LevelConfig levelConfig = new LevelConfig();
            LevelConfig.Configs.Add(levelConfig);
            levelConfig.id = level;

            for (int i = 0; i < EditorVO.Instance.level.pieces.Count; i++)
            {
                EditorLevelPiece piece = EditorVO.Instance.level.pieces[i];
                PieceConfig pieceConfig = new PieceConfig();
                levelConfig.pieces.Add(pieceConfig);
                pieceConfig.id = level * 100 + i;
                for (int c = 0; c < piece.grids.Count; c++)
                {
                    if (CoordConfig.GetConfig(piece.grids[c].x * 1000 + -piece.grids[c].y) == null)
                    {
                        CoordConfig coord = new CoordConfig();
                        coord.x = piece.grids[c].x;
                        coord.y = piece.grids[c].y;
                        coord.id = piece.grids[c].x * 1000 + -piece.grids[c].y;
                        CoordConfig.Configs.Add(coord);
                    }
                    pieceConfig.coords.Add(CoordConfig.GetConfig(piece.grids[c].x * 1000 + -piece.grids[c].y));
                }
                PieceConfig.Configs.Add(pieceConfig);
            }

            for (int i = 0; i < EditorVO.Instance.level.otherPieces.Count; i++)
            {
                EditorLevelPiece piece = EditorVO.Instance.level.otherPieces[i];
                PieceConfig pieceConfig = new PieceConfig();
                levelConfig.pieces2.Add(pieceConfig);
                pieceConfig.id = level * 100 + EditorVO.Instance.level.pieces.Count + i;
                for (int c = 0; c < piece.grids.Count; c++)
                {
                    if (CoordConfig.GetConfig(piece.grids[c].x * 1000 + -piece.grids[c].y) == null)
                    {
                        CoordConfig coord = new CoordConfig();
                        coord.x = piece.grids[c].x;
                        coord.y = piece.grids[c].y;
                        coord.id = piece.grids[c].x * 1000 + -piece.grids[c].y;
                        CoordConfig.Configs.Add(coord);
                    }
                    pieceConfig.coords.Add(CoordConfig.GetConfig(piece.grids[c].x * 1000 + -piece.grids[c].y));
                }
                PieceConfig.Configs.Add(pieceConfig);
            }

            CheckLevelPiecePosition(LevelConfig.GetConfig(level));
        }

        //CheckAllLevelPiecePosition();

        Save();
        //CheckOut();
    }

    private void Save()
    {
        //保存 CoordConfig
        string file = "int,int,int\n";
        file += "id,x,y\n";
        for (int i = 0; i < CoordConfig.Configs.Count; i++)
        {
            file += CoordConfig.Configs[i].id + "," + CoordConfig.Configs[i].x + "," + CoordConfig.Configs[i].y + "\n";
        }
        SaveFile("./Assets/Resources/config/Coord.csv", file);

        //保存 PieceConfig
        file = "int,(Coord)Array(int)\n";
        file += "id,coords\n";
        for (int i = 0; i < PieceConfig.Configs.Count; i++)
        {
            file += PieceConfig.Configs[i].id + "," + "\"";
            for (int c = 0; c < PieceConfig.Configs[i].coords.Count; c++)
            {
                file += PieceConfig.Configs[i].coords[c].id + (c < PieceConfig.Configs[i].coords.Count - 1 ? "," : "");
            }
            file += "\"\n";
        }
        SaveFile("./Assets/Resources/config/Piece.csv", file);

        //保存 LevelConfig
        file = "int,(Piece)Array(int),(Piece)Array(int),(Coord)Array(int)\n";
        file += "id,pieces,pieces2,coords\n";
        for (int i = 0; i < LevelConfig.Configs.Count; i++)
        {
            file += LevelConfig.Configs[i].id + "," + "\"";
            for (int c = 0; c < LevelConfig.Configs[i].pieces.Count; c++)
            {
                file += LevelConfig.Configs[i].pieces[c].id + (c < LevelConfig.Configs[i].pieces.Count - 1 ? "," : "");
            }
            file += "\",\"";
            for (int c = 0; c < LevelConfig.Configs[i].pieces2.Count; c++)
            {
                file += LevelConfig.Configs[i].pieces2[c].id + (c < LevelConfig.Configs[i].pieces2.Count - 1 ? "," : "");
            }
            file += "\",\"";
            for (int c = 0; c < LevelConfig.Configs[i].coords.Count; c++)
            {
                file += LevelConfig.Configs[i].coords[c].id + (c < LevelConfig.Configs[i].coords.Count - 1 ? "," : "");
            }
            file += "\"\n";
        }
        SaveFile("./Assets/Resources/config/Level.csv", file);
    }

    private void SaveFile(string url,string content)
    {
        FileStream fs = new FileStream(url, FileMode.Create);
        //获得字节数组
        byte[] data = System.Text.Encoding.Default.GetBytes(content);
        //开始写入
        fs.Write(data, 0, data.Length);
        //清空缓冲区、关闭流
        fs.Flush();
        fs.Close();
    }

    /// <summary>
    /// 检测是否有放不下的
    /// </summary>
    private void CheckOut()
    {
        List<LevelConfig> levels = LevelConfig.Configs;
        for (int i = 0; i < levels.Count; i++)
        {
            ThreadEvent te = ThreadEvent.Create("check",levels[i]);
            te.URL = "gameLevel/level1/SceneBackground";
            ThreadEventList.GetList(EditorMainThread.ThreadId, CheckThread.ThreadId).AddEvent(te);
        }
    }

    private void CheckAllLevelPiecePosition()
    {
        List<LevelConfig> levels = LevelConfig.Configs;
        for (int i = 0; i < levels.Count; i++)
        {
            CheckLevelPiecePosition(levels[i]);
        }
    }

    private void CheckLevelPiecePosition(LevelConfig config)
    {
        //外面有 23 x 9 的大小
        HaxgonCoord<Coord> sys = new HaxgonCoord<Coord>();
        for (int py = 0; py > miny; py--)
        {
            for (int x = 0; x < maxx; x++)
            {
                int y = py - 3 + movesy[x];
                sys.SetCoord(Point2D.Create(x, y), new Coord { type = 0 });
            }
        }

        Array<Piece> pieces = new Array<Piece>();

        //颜色信息
        int type = 1;
        //生成片信息
        for (int i = 0; i < config.pieces.Count; i++)
        {
            Piece piece = new Piece();
            piece.isAnswer = true;
            pieces[i] = piece;
            for (int p = 0; p < config.pieces[i].coords.Count; p++)
            {
                Coord coord = new Coord
                {
                    x = config.pieces[i].coords[p].x,
                    y = config.pieces[i].coords[p].y,
                    piece = piece,
                    type = type
                };
                piece.coords.Add(coord);
            }
            type++;
            piece.Init();
        }
        for (int i = 0; i < config.pieces2.Count; i++)
        {
            Piece piece = new Piece();
            piece.isAnswer = false;
            pieces.Add(piece);
            for (int p = 0; p < config.pieces2[i].coords.Count; p++)
            {
                Coord coord = new Coord
                {
                    x = config.pieces2[i].coords[p].x,
                    y = config.pieces2[i].coords[p].y,
                    piece = piece,
                    type = type
                };
                piece.coords.Add(coord);
            }
            type++;
            piece.Init();
        }

        config.coords.Clear();
        //创建主坐标系
        for (int i = 0; i < pieces.length; i++)
        {
            Piece piece = pieces[i];
            Point2D p = AutoSetPiece(pieces[i], sys);
            int key = ((int)(p.x)) * 1000 + -(int)(p.y);
            if (CoordConfig.GetConfig(key) == null)
            {
                CoordConfig coord = new CoordConfig();
                coord.x = (int)p.x;
                coord.y = (int)p.y;
                coord.id = key;
                CoordConfig.Configs.Add(coord);
            }
            config.coords.Add(CoordConfig.GetConfig(key));
        }
    }


    public HaxgonCoord<Coord> coordSys = new HaxgonCoord<Coord>();

    private int maxx = 23;
    private int miny = -9;
    private int[] movesy = { 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 2, 1, 2, 2, 2, 2, 2, 2 };

    private Point2D AutoSetPiece(Piece piece, HaxgonCoord<Coord> sys)
    {
        List<Point2D> list = new List<Point2D>();
        float minX = 1000;
        float maxX = -1000;
        float minY = 1000;
        float maxY = -1000;
        for (int py = 0; py > this.miny; py--)
        {
            for (int x = 0; x < this.maxx; x++)
            {
                int y = py - 3 + movesy[x];
                list.Add(Point2D.Create(x, y));
                Point2D position = HaxgonCoord<Coord>.CoordToPosition(list[list.Count - 1], 0.2f);
                if (position.x < minX)
                {
                    minX = position.x;
                }
                if (position.x > maxX)
                {
                    maxX = position.x;
                }
                if (position.y < minY)
                {
                    minY = position.y;
                }
                if (position.y > maxY)
                {
                    maxY = position.y;
                }
            }
        }
        Point2D center = HaxgonCoord<Coord>.PositionToCoord(Point2D.Create((minX + maxX) * 0.5f, (minY + maxY) * 0.5f), 0.2f);

        //从可选格子的正中心开始，以六边形的方式向外扩展找可以放的地方
        Dictionary<string, bool> findMap = new Dictionary<string, bool>();
        Dictionary<string, bool> findMap2 = new Dictionary<string, bool>();
        findMap2.Add(center.x + "," + center.y, true);
        List<Point2D> currentList = new List<Point2D>();
        currentList.Add(center);
        while (list.Count > 0)
        {
            for (int i = 0; i < currentList.Count; i++)
            {
                //在 list 中查找该点
                bool findInList = false;
                for (int l = 0; l < list.Count; l++)
                {
                    if (list[l].x == currentList[i].x && list[l].y == currentList[i].y)
                    {
                        list.RemoveAt(l);
                        findInList = true;
                        break;
                    }
                }
                if (findInList)
                {
                    //如果找到则查看该点是否可以放
                    Point2D result = TrySetPiece((int)currentList[i].x, (int)currentList[i].y, piece, sys);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }
            List<Point2D> newList = new List<Point2D>();
            //展开所有的点
            for (int i = 0; i < currentList.Count; i++)
            {
                if (findMap.ContainsKey(currentList[i].x + "," + currentList[i].y))
                {
                    continue;
                }
                //展开
                List<Point2D> nextCoords = HaxgonCoord<Coord>.GetCoordsNextTo(currentList[i]);
                for (int n = 0; n < nextCoords.Count; n++)
                {
                    if (findMap2.ContainsKey(nextCoords[n].x + "," + nextCoords[n].y))
                    {
                        continue;
                    }
                    findMap2.Add(nextCoords[n].x + "," + nextCoords[n].y, true);
                    newList.Add(nextCoords[n]);
                }
            }
            currentList = newList;
        }
        return null;
    }

    private Point2D TrySetPiece(int x, int y, Piece piece, HaxgonCoord<Coord> sys)
    {
        bool find = true;
        for (int i = 0; i < piece.coords.length; i++)
        {
            Point2D p = HaxgonCoord<Coord>.CoordToPosition(Point2D.Create(piece.coords[i].x, piece.coords[i].y), 0.2f);
            p.x += piece.offx * 0.5f;
            p.y += piece.offy * 0.5f;
            Point2D p2 = HaxgonCoord<Coord>.CoordToPosition(Point2D.Create(x, y), 0.2f);
            p2.x += p.x;
            p2.y += p.y;
            p = HaxgonCoord<Coord>.PositionToCoord(p2, 0.2f);
            Coord grid = sys.GetCoord(p);
            if (grid == null || grid.type != 0)
            {
                find = false;
                break;
            }
            else
            {
                //获取周围的格子
                List<Point2D> nextCoords = HaxgonCoord<Coord>.GetCoordsNextTo(p);
                for (int n = 0; n < nextCoords.Count; n++)
                {
                    Coord nextGrid = sys.GetCoord(Point2D.Create(nextCoords[n].x, nextCoords[n].y));
                    if (nextGrid != null && nextGrid.piece != grid.piece)
                    {
                        find = false;
                        break;
                    }
                }
            }
        }
        if (find)
        {
            for (int i = 0; i < piece.coords.length; i++)
            {
                Point2D p = HaxgonCoord<Coord>.CoordToPosition(Point2D.Create(piece.coords[i].x, piece.coords[i].y), 0.2f);
                p.x += piece.offx * 0.5f;
                p.y += piece.offy * 0.5f;
                Point2D p2 = HaxgonCoord<Coord>.CoordToPosition(Point2D.Create(x, y), 0.2f);
                p2.x += p.x;
                p2.y += p.y;
                p = HaxgonCoord<Coord>.PositionToCoord(p2, 0.2f);
                sys.SetCoord(p, piece.coords[i]);
            }
            return Point2D.Create(x, y);
        }
        return null;
    }
}