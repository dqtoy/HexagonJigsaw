using lib;
using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace hexjig
{
    public class Game : EventDispatcher
    {
        public Array<Piece> pieces = new Array<Piece>();

        public HaxgonCoord<Coord> coordSys = new HaxgonCoord<Coord>();

        private int maxx = 23;
        private int miny = -9;
        private int[] movesy = { 0,0,0, 0, 0, 0, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 2, 1, 2, 2, 2, 2,2,2 };

        public float offx;
        public float offy;
        public float offx1;
        public float offy1;

        private bool finish = false;

        private DateTime startTime;

        public GameObject root;

        public GameObject stageRoot;

        public GameObject rootStage;

        public GameObject changeOutRoot;

        private List<GameObject> backgroundgrids = new List<GameObject>();

        public static Game Instance;

        private GameObject outBackground;

        private List<Tweener> outTweens;

        public List<Piece> history = new List<Piece>();
        public List<Point2D> history2 = new List<Point2D>();

        public Game(LevelConfig config)
        {
            Instance = this;
            //创建跟接点
            root = new GameObject();
            root.name = "GameRoot";
            root.layer = 8;

            rootStage = new GameObject();
            rootStage.transform.parent = root.transform;
            rootStage.name = "rootStage";

            MainData.Instance.dispatcher.AddListener(EventType.BACK_STEP, BACK_STEP);
            MainData.Instance.dispatcher.AddListener(EventType.RESTART, OnRestart);
            MainData.Instance.dispatcher.AddListener(EventType.SHOW_TIP, OnShowTip);
            MainData.Instance.dispatcher.AddListener(EventType.HIDE_GAME, OnHideGame);
            MainData.Instance.dispatcher.AddListener(EventType.SHOW_START_EFFECT, ShowStartEffect);
            MainData.Instance.dispatcher.AddListener(EventType.SHOW_CUT, ShowCut); 
            MainData.Instance.dispatcher.AddListener(hexjig.EventType.SHOW_GAME_CHANGE_OUT_EFFECT0, OnShowGameChangeOut0);
            MainData.Instance.dispatcher.AddListener(hexjig.EventType.SHOW_GAME_CHANGE_OUT_EFFECT, OnShowGameChangeOut);
            MainData.Instance.dispatcher.AddListener(hexjig.EventType.SHOW_GAME_CHANGE_IN_EFFECT, OnShowGameChangeIn);

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
                piece.outCoord = new Point2D(config.coords[i].x,config.coords[i].y);
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

            //计时
            MainData.Instance.time.value = 0;
            startTime = System.DateTime.Now;
        }

        private void OnShowGameChangeOut0(lib.Event e)
        {
            for (int i = 0, len = outBackground.transform.childCount; i < len; i++)
            {
                GameBufferPool.ReleaseGridBg(outBackground.transform.GetChild(0).gameObject);
            }
            for(int i = 0; i < pieces.length; i++)
            {
                if(pieces[i].isInStage == false)
                {
                    pieces[i].Hide();
                }
            }
        }

        private void OnShowGameChangeOut(lib.Event e)
        {
            for (int i = 0; i < backgroundgrids.Count; i++)
            {
                GameBufferPool.ReleaseGridBg(backgroundgrids[i]);
            }
            changeOutRoot = new GameObject();
            changeOutRoot.name = "GameChangeOut";
            changeOutRoot.transform.parent = root.transform.parent;
            for (int i = 0; i < this.pieces.length; i++)
            {
                pieces[i].ShowChangeOut();
            }
            root.SetActive(false);
            outTweens = new List<Tweener>();
            float maxTime = 0;
            Tweener maxTween = null;
            foreach (Transform child in changeOutRoot.transform)
            {
                foreach(Transform child2 in child)
                {
                    float time1 = UnityEngine.Random.Range(0.1f, 0.6f);
                    float time2 = UnityEngine.Random.Range(0.0f, 0.3f);
                    child2.GetComponent<SpriteRenderer>().DOColor(new Color(1,1,1,0), time1).SetDelay(time2);
                    Tweener tween = child2.DOMove(new Vector3(UnityEngine.Random.Range(-7.2f, -8.0f), UnityEngine.Random.Range(child2.position.y - 3f, child2.position.y + 3f)), time1).SetDelay(time2).SetEase(Ease.InSine);
                    outTweens.Add(tween);
                    tween.onComplete = OnShowGameChangeOutComplete;
                    if(time1 + time2 > maxTime)
                    {
                        maxTime = time1 + time2;
                        maxTween = tween;
                    }
                }
            }
        }

        private void OnShowGameChangeOutComplete()
        {
            for(int i = 0; i < outTweens.Count; i++)
            {
                if(outTweens[i].IsPlaying() == true)
                {
                    return;
                }
            }
            for (int i = 0, len = changeOutRoot.transform.childCount; i < len; i++)
            {
                for (int j = 0, len2 = changeOutRoot.transform.GetChild(i).childCount; j < len2; j++)
                {
                    GameBufferPool.ReleaseGrid(changeOutRoot.transform.GetChild(i).GetChild(0).gameObject);
                }
            }
            MainData.Instance.dispatcher.DispatchWith(EventType.SHOW_GAME_CHANGE_OUT_EFFECT_COMPLETE2, this);
        }

        private void OnShowGameChangeIn(lib.Event e)
        {
            foreach (Transform child in stageRoot.transform)
            {
                float x = child.localPosition.x;
                float y = child.localPosition.y;
                float z = child.localPosition.z;
                child.localPosition = new Vector3(x + UnityEngine.Random.Range(7f, 10f), y + UnityEngine.Random.Range(-3, 3f), z);
                float time1 = UnityEngine.Random.Range(0.1f, 0.5f);
                float time2 = UnityEngine.Random.Range(0.0f, 0.3f);
                child.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
                child.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 1), time1).SetDelay(time2 + 0.07f).SetEase(Ease.InSine);
                child.DOLocalMove(new Vector3(x,y,z), time1).SetDelay(time2).SetEase(Ease.InSine);
            }
        }

        /// <summary>
        /// 显示截图
        /// </summary>
        /// <param name="e"></param>
        private void ShowCut(lib.Event e)
        {
            //0.2 1.07 -17
            root.transform.localPosition = new Vector3(root.transform.localPosition.x, root.transform.localPosition.y, 0);
            stageRoot.transform.parent = root.transform.parent;
            for (int i = 0; i < this.pieces.length; i++)
            {
                pieces[i].ShowCut();
            }
            float size = GameVO.Instance.Width;
            (new ScreenCut((int)((GameVO.Instance.Width * 0.5f - size * 0.5f) * 100), (int)((GameVO.Instance.Height * 0.5f + GameVO.Instance.Height * 0.2f - size * 0.5f) * 100), (int)((size) * 100), (int)((size) * 100),stageRoot.transform, -offx, -offy + GameVO.Instance.Height * 0.2f,1)).AddListener(lib.Event.COMPLETE,OnShowCut2);
            //添加一个缩放层，达到以中心为缩放点缩放的效果
            MainData.Instance.showCutRoot = new GameObject();
            stageRoot.transform.localPosition = new Vector3(stageRoot.transform.localPosition.x, stageRoot.transform.localPosition.y - GameVO.Instance.Height * 0.2f, stageRoot.transform.localPosition.z);
            stageRoot.transform.parent = MainData.Instance.showCutRoot.transform;
            MainData.Instance.showCutRoot.transform.localPosition = new Vector3(0, GameVO.Instance.Height * 0.2f);
        }

        private void OnShowCut2(lib.Event e)
        {
            float size = GameVO.Instance.Width + 0.4f;
            GameObject image = ResourceManager.CreateImage("image/uiitem/rect");
            image.transform.parent = stageRoot.transform;
            image.transform.localPosition = new Vector3(-offx, -offy + GameVO.Instance.Height * (float)0.2f);
            image.GetComponent<SpriteRenderer>().drawMode = SpriteDrawMode.Sliced;
            image.GetComponent<SpriteRenderer>().size = new Vector2(size, size);
            MainData.Instance.dispatcher.DispatchWith(EventType.SHOW_CUT_COMPLETE);
        }

        private void ShowStartEffect(lib.Event e)
        {
            root.SetActive(true);
        }

        private void OnHideGame(lib.Event e)
        {
            root.SetActive(false);
        }

        private void OnShowTip(lib.Event e)
        {
            for (int i = 0; i < pieces.length; i++)
            {
                if(pieces[i].isAnswer && pieces[i].hasShowTip == false)
                {
                    pieces[i].ShowTip();
                    return;
                }
            }
        }

        /// <summary>
        /// 退回一步
        /// </summary>
        /// <param name="e"></param>
        private void BACK_STEP(lib.Event e)
        {
            if(history.Count > 0)
            {
                history[history.Count - 1].Check(history2[history2.Count - 1].x, history2[history2.Count - 1].y, true,false);
                history.RemoveAt(history.Count - 1);
                history2.RemoveAt(history2.Count - 1);
            }
        }

        /// <summary>
        /// 重新开始关卡
        /// </summary>
        /// <param name="e"></param>
        private void OnRestart(lib.Event e)
        {
            //重置所有的片
            for(int i = 0; i < pieces.length; i++)
            {
                pieces[i].Reset();
            }

            //重置点阵信息
            foreach (var item in coordSys.coords)
            {
                Coord coord = item.Value;
                coord.piece = null;
                coord.type = 0;
            }
        }

        private void CreateDisplay()
        {
            //生成背景
            float minX = 1000;
            float maxX = -1000;
            float minY = 1000;
            float maxY = -1000;
            GameObject p = new GameObject();
            p.transform.parent = rootStage.transform;
            foreach (var item in coordSys.coords)
            {
                Coord coord = item.Value;
                GameObject image = GameBufferPool.CreateGridBg();
                backgroundgrids.Add(image);
                Point2D position = HaxgonCoord<Coord>.CoordToPosition(Point2D.Create(coord.x, coord.y), 0.4f);
                image.transform.position = new Vector3(position.x, position.y, 5);
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
            MainData.Instance.levelWidth = maxX - minX + 1.5f;
            MainData.Instance.levelHeight = maxY - minY + 1.5f;
            offx = -((maxX - minX) * 0.5f + minX);
            offy = -((maxY - minY) * 0.5f + minY) + GameVO.Instance.Height * 0.2f;
            p.transform.position = new Vector3(offx, offy - GameVO.Instance.Height * 0.2f);
            stageRoot = p;

            rootStage.transform.localPosition = new Vector3(0, GameVO.Instance.Height * 0.2f);

            outBackground = new GameObject();
            outBackground.transform.parent = root.transform;
            
            //生成背景
            minX = 1000;
            maxX = -1000;
            minY = 1000;
            maxY = -1000;
            GameObject p1 = new GameObject();
            p1.transform.parent = root.transform;
            for (int x = 0; x < this.maxx; x++)
            {
                for (int py = 0; py > this.miny; py--)
                {
                    int y = py - 3 + movesy[x];
                    Point2D position = HaxgonCoord<Coord>.CoordToPosition(Point2D.Create(x, y), 0.2f);

                    /*
                    GameObject image = ResourceManager.CreateImage("image/grid/gridBg");
                    image.transform.localScale = new Vector3(0.5f, 0.5f);
                    image.transform.position = new Vector3(position.x, position.y,100);
                    image.transform.parent = p1.transform;
                    //*/

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
            offy1 = -((maxY - minY) * 0.5f + minY) -2.4f;
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
            for(int i = 0; i < pieces.length; i++)
            {
                pieces[i].Update();
            }

            if(!finish)
            {
                double time = System.DateTime.Now.Subtract(startTime).TotalMilliseconds;
                MainData.Instance.time.value = (int)time;

                if (Input.GetAxis("Fire1") > 0 && lastClick == 0)
                {
                    Vector3 pos = Input.mousePosition;
                    pos.x = (pos.x / GameVO.Instance.PixelWidth - 0.5f) * GameVO.Instance.Width;
                    pos.y = (pos.y / GameVO.Instance.PixelHeight - 0.5f) * GameVO.Instance.Height;
                    //Point2D p = HaxgonCoord<Coord>.PositionToCoord(Point2D.Create(pos.x - offx1, pos.y - offy1), 0.2f);
                    //Point2D p1 = HaxgonCoord<Coord>.PositionToCoord(Point2D.Create(pos.x - offx, pos.y - offy), 0.4f);
                    for (int i = 0; i < pieces.length; i++)
                    {
                        if (pieces[i].IsTouchIn(pos.x, pos.y))
                        {
                            pieces[i].StartDrag(pos.x, pos.y);
                            dragPiece = pieces[i];
                            break;
                        }
                    }
                    isDragMove = true;
                }
                else if (lastClick > 0 && Input.GetAxis("Fire1") == 0)
                {
                    if (dragPiece != null)
                    {
                        Vector3 pos = Input.mousePosition;
                        pos.x = (pos.x / GameVO.Instance.PixelWidth - 0.5f) * GameVO.Instance.Width;
                        pos.y = (pos.y / GameVO.Instance.PixelHeight - 0.5f) * GameVO.Instance.Height;
                        dragPiece.StopDrag(pos.x, pos.y);
                    }
                    dragPiece = null;
                    isDragMove = false;
                }
                else if (isDragMove)
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
            this.finish = finish;
            if (finish)
            {
                MainData.Instance.time.value = ((int)(MainData.Instance.time.value / 1000)) * 1000;
                root.transform.localPosition = new Vector3(root.transform.localPosition.x, root.transform.localPosition.y,100);
                MainData.Instance.dispatcher.DispatchWith(EventType.FINISH_LEVEL, MainData.Instance.time.value);
            }
        }

        public void Dispose()
        {
            MainData.Instance.dispatcher.RemoveListener(EventType.RESTART, BACK_STEP);
            MainData.Instance.dispatcher.RemoveListener(EventType.RESTART, OnRestart);
            MainData.Instance.dispatcher.RemoveListener(EventType.SHOW_TIP, OnShowTip);
            MainData.Instance.dispatcher.RemoveListener(EventType.HIDE_GAME, OnHideGame);
            MainData.Instance.dispatcher.RemoveListener(EventType.SHOW_START_EFFECT, ShowStartEffect);
            MainData.Instance.dispatcher.RemoveListener(EventType.SHOW_CUT, ShowCut);
            MainData.Instance.dispatcher.RemoveListener(hexjig.EventType.SHOW_GAME_CHANGE_OUT_EFFECT0, OnShowGameChangeOut0);
            MainData.Instance.dispatcher.RemoveListener(EventType.SHOW_GAME_CHANGE_OUT_EFFECT, OnShowGameChangeOut);
            MainData.Instance.dispatcher.RemoveListener(hexjig.EventType.SHOW_GAME_CHANGE_IN_EFFECT, OnShowGameChangeIn);
        }
    }
}