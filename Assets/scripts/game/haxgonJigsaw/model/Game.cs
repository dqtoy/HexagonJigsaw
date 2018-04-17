using lib;
using System.Collections.Generic;
using UnityEngine;

namespace hexjig
{
    public class Game : EventDispatcher
    {
        public Array<Piece> pieces = new Array<Piece>();

        public HaxgonCoord<Coord> coordSys = new HaxgonCoord<Coord>();

        private int maxx = 19;
        private int miny = -7;

        public float offx;
        public float offy;
        public float offx1;
        public float offy1;

        public GameObject root;

        public static Game Instance;

        public Game(LevelConfig config)
        {
            Instance = this;
            //创建跟接点
            root = new GameObject();
            root.name = "GameRoot";

            //创建背景
            GameObject bg = ResourceManager.CreateImage("image/bg");
            bg.transform.parent = root.transform;
            bg.transform.localScale = new Vector3(GameVO.Instance.PixelWidth, GameVO.Instance.PixelHeight);
            bg.transform.position = new Vector3(0, 0, 101);


            //外面有 19 x 9 的大小
            HaxgonCoord<Coord> sys = new HaxgonCoord<Coord>();
            for (int y = 0; y > miny; y--)
            {
                for (int x = 0; x < maxx; x++)
                {
                    sys.SetCoord(Point2D.Create(x, y), new Coord { type = 0 });
                }
            }
            //颜色信息
            int type = 1;
            //生成片信息
            for (int i = 0; i < config.pieces.Count; i++)
            {
                Piece piece = new Piece();
                piece.game = this;
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
                piece.game = this;
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

            //创建主坐标系
            for(int i = 0; i < pieces.length; i++)
            {
                Piece piece = pieces[i];
                piece.outCoord = AutoSetPiece(pieces[i], sys);
                if (piece.isAnswer)
                {
                    for(int n = 0; n < piece.coords.length; n++)
                    {
                        Coord coord = new Coord
                        {
                            x = piece.coords[n].x,
                            y = piece.coords[n].y,
                            type = 0
                        };
                        coordSys.SetCoord(Point2D.Create(piece.coords[n].x, piece.coords[n].y), coord);
                    }
                }
            }

            //生成显示相关内容
            CreateDisplay();
        }

        private void CreateDisplay()
        {
            //生成背景
            float minX = 1000;
            float maxX = -1000;
            float minY = 1000;
            float maxY = -1000;
            GameObject p = new GameObject();
            p.transform.parent = root.transform;
            foreach (var item in coordSys.coords)
            {
                Coord coord = item.Value;
                GameObject image = ResourceManager.CreateImage("image/grid/gridBg");
                Point2D position = HaxgonCoord<Coord>.CoordToPosition(Point2D.Create(coord.x, coord.y), 0.4f);
                image.transform.position = new Vector3(position.x, position.y, 100);
                image.transform.parent = p.transform;
                if(position.x < minX)
                {
                    minX = position.x;
                }
                if(position.x > maxX)
                {
                    maxX = position.x;
                }
                if(position.y < minY)
                {
                    minY = position.y;
                }
                if(position.y > maxY)
                {
                    maxY = position.y;
                }
            }
            offx = -((maxX - minX) * 0.5f + minX);
            offy = -((maxY - minY) * 0.5f + minY) + 0.6f;
            p.transform.position = new Vector3(offx, offy);

            GameObject outBackground = new GameObject();
            outBackground.transform.parent = root.transform;

            //生成背景
            minX = 1000;
            maxX = -1000;
            minY = 1000;
            maxY = -1000;
            GameObject p1 = new GameObject();
            p1.transform.parent = root.transform;
            for (int y = 0; y > this.miny; y--)
            {
                for (int x = 0; x < this.maxx; x++)
                {
                    Point2D position = HaxgonCoord<Coord>.CoordToPosition(Point2D.Create(x, y), 0.2f);
                    /*GameObject image = ResourceManager.CreateImage("image/grid/gridBg");
                    image.transform.localScale = new Vector3(0.5f, 0.5f);
                    image.transform.position = new Vector3(position.x, position.y,100);
                    image.transform.parent = p1.transform;*/
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
            offx1 = -((maxX - minX) * 0.5f + minX);
            offy1 = -((maxY - minY) * 0.5f + minY) - 3;
            p1.transform.position = new Vector3(offx1, offy1);

            for (int i = 0; i < pieces.length; i++)
            {
                pieces[i].background = outBackground;
                pieces[i].CreateDisplay();
            }

            outBackground.transform.position = new Vector3(offx1, offy1);
        }

        private float lastClick = 0;
        private bool isDragMove = false;
        private Piece dragPiece;

        public void Update()
        {
            if (Input.GetAxis("Fire1") > 0 && lastClick == 0)
            {
                Vector3 pos = Input.mousePosition;
                pos.x = (pos.x / GameVO.Instance.PixelWidth - 0.5f) * GameVO.Instance.Width;
                pos.y = (pos.y / GameVO.Instance.PixelHeight - 0.5f) * GameVO.Instance.Height;

                //Point2D p = HaxgonCoord<Coord>.PositionToCoord(Point2D.Create(pos.x - offx1, pos.y - offy1), 0.2f);
                //Point2D p1 = HaxgonCoord<Coord>.PositionToCoord(Point2D.Create(pos.x - offx, pos.y - offy), 0.4f);
                for (int i = 0; i < pieces.length; i++)
                {
                    if(pieces[i].IsTouchIn(pos.x,pos.y))
                    {
                        pieces[i].StartDrag(pos.x, pos.y);
                        dragPiece = pieces[i];
                        break;
                    }
                }
                isDragMove = true;
            }
            else if(lastClick > 0 && Input.GetAxis("Fire1") == 0)
            {
                if(dragPiece != null)
                {
                    Vector3 pos = Input.mousePosition;
                    pos.x = (pos.x / GameVO.Instance.PixelWidth - 0.5f) * GameVO.Instance.Width;
                    pos.y = (pos.y / GameVO.Instance.PixelHeight - 0.5f) * GameVO.Instance.Height;
                    dragPiece.StopDrag(pos.x, pos.y);
                }
                dragPiece = null;
                isDragMove = false;
            }
            else if(isDragMove)
            {
                if (dragPiece != null)
                {
                    Vector3 pos = Input.mousePosition;
                    pos.x = (pos.x / GameVO.Instance.PixelWidth - 0.5f) * GameVO.Instance.Width;
                    pos.y = (pos.y / GameVO.Instance.PixelHeight - 0.5f) * GameVO.Instance.Height;
                    dragPiece.DragMove(pos.x, pos.y);
                }
            }
            lastClick = Input.GetAxis("Fire1");
        }

        private Point2D AutoSetPiece(Piece piece, HaxgonCoord<Coord> sys)
        {
            for (int y = 0; y > this.miny; y--)
            {
                for (int x = 0; x < this.maxx; x++, x++)
                {
                    bool find = true;
                    for (int i = 0; i < piece.coords.length; i++)
                    {
                        Coord grid = sys.GetCoord(Point2D.Create(piece.coords[i].x + piece.offx + x,piece.coords[i].y + piece.offy + y));
                        if (grid == null || grid.type != 0)
                        {
                            find = false;
                            break;
                        }
                        else
                        {
                            //获取周围的格子
                            List<Point2D> nextCoords = HaxgonCoord<Point2D>.GetCoordsNextTo(Point2D.Create((int)piece.coords[i].x + piece.offx + x, (int)piece.coords[i].y + piece.offy + y));
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
                            sys.SetCoord(Point2D.Create(piece.coords[i].x + piece.offx + x, piece.coords[i].y + piece.offy + y), piece.coords[i]);
                        }
                        return Point2D.Create(x, y);
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 检查游戏是否结束
        /// </summary>
        public void CheckFinish()
        {
            bool finish = true;
            foreach(var item in coordSys.coords)
            {
                Coord coord = item.Value;
                if(coord.type == 0)
                {
                    finish = false;
                    break;
                }
            }
            if(finish)
            {
                MainData.Instance.dispatcher.DispatchWith(EventType.FINISH_LEVEL);
            }
        }
    }
}