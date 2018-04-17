using lib;
using UnityEngine;

namespace hexjig
{
    public class Piece
    {
        public Game game;

        /// <summary>
        /// 包含的点
        /// </summary>
        public Array<Coord> coords = new Array<Coord>();

        /// <summary>
        /// 当前是否在舞台中
        /// </summary>
        public bool isInStage = false;

        /// <summary>
        /// 在舞台中的位置
        /// </summary>
        public Point2D stageCoord = new Point2D();

        /// <summary>
        /// 在外面的位置
        /// </summary>
        public Point2D outCoord = new Point2D();

        public bool isAnswer;

        public int offx;

        public int offy;

        //
        private float tipOffx;
        private float tipOffy;

        //显示相关
        public GameObject background;
        private GameObject show;
        private GameObject shader;
        //阴影透明度
        private float shaderAlpha = 0.5f;
        private GameObject showOut;
        private GameObject outBackground;
        private GameObject stageBackground;

        public Piece()
        {
        }

        public void Init()
        {
            int minx = coords[0].x;
            int maxy = coords[0].y;
            float pminX = 1000;
            float pmaxX = -1000;
            float pminY = 1000;
            float pmaxY = -1000;
            for (int i = 1; i < coords.length; i++)
            {
                if (coords[i].x < minx)
                {
                    minx = coords[i].x;
                }
                if (coords[i].y > maxy)
                {
                    maxy = coords[i].y;
                }

                //计算所有的点位置范围
                Point2D position = HaxgonCoord<Coord>.CoordToPosition(Point2D.Create(coords[i].x, coords[i].y), 0.4f);
                if(position.x < pminX)
                {
                    pminX = position.x;
                }
                if(position.x > pmaxX)
                {
                    pmaxX = position.x;
                }
                if(position.y < pminY)
                {
                    pminY = position.y;
                }
                if(position.y > pmaxY)
                {
                    pmaxY = position.y;
                }
            }
            offx = minx % 2 == 0 ? -minx : -minx + 1;
            offy = -maxy;

            tipOffx = -pminX - (pmaxX - pminX) * 0.5f;
            tipOffy = -pminY + 0.6f;

            show = new GameObject();
            show.transform.parent = game.root.transform;
            shader = new GameObject();
            shader.transform.parent = game.root.transform;
            showOut = new GameObject();
            showOut.transform.parent = game.root.transform;
        }

        public bool IsTouchIn(float x,float y)
        {
            if (isInStage)
            {
                Point2D p = HaxgonCoord<Coord>.PositionToCoord(Point2D.Create(x - show.transform.position.x, y - show.transform.position.y), 0.4f);
                for (int i = 0; i < coords.length; i++)
                {
                    if (p.x == coords[i].x && p.y == coords[i].y)
                    {
                        return true;
                    }
                }
            }
            else
            {
                Point2D p = HaxgonCoord<Coord>.PositionToCoord(Point2D.Create(x - game.offx1, y - game.offy1), 0.2f);
                for (int i = 0; i < coords.length; i++)
                {
                    if (p.x == outCoord.x + coords[i].x + offx && p.y == outCoord.y + coords[i].y + offy)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void CreateDisplay()
        {
            //创建拖动显示
            for(int i = 0; i < coords.length; i++)
            {
                GameObject image = ResourceManager.CreateImage("image/grid/" + coords[i].type);
                Point2D position = HaxgonCoord<Coord>.CoordToPosition(Point2D.Create(coords[i].x, coords[i].y),0.4f);
                image.transform.position = new Vector3(position.x,position.y,1);
                image.transform.parent = show.transform;
            }
            show.SetActive(false);

            //创建阴影
            for (int i = 0; i < coords.length; i++)
            {
                GameObject image = ResourceManager.CreateImage("image/grid/" + coords[i].type);
                Point2D position = HaxgonCoord<Coord>.CoordToPosition(Point2D.Create(coords[i].x, coords[i].y), 0.4f);
                image.transform.position = new Vector3(position.x, position.y,2);
                image.transform.parent = shader.transform;
                image.GetComponent<SpriteRenderer>().color = new Color(image.GetComponent<SpriteRenderer>().color.r, image.GetComponent<SpriteRenderer>().color.g, image.GetComponent<SpriteRenderer>().color.b, shaderAlpha);
            }
            shader.SetActive(false);

            //创建库中的显示
            for (int i = 0; i < coords.length; i++)
            {
                GameObject image = ResourceManager.CreateImage("image/grid/" + coords[i].type);
                image.transform.localScale = new Vector3(0.5f, 0.5f);
                Point2D position = HaxgonCoord<Coord>.CoordToPosition(Point2D.Create(outCoord.x + coords[i].x + offx, outCoord.y + coords[i].y + offy), 0.2f);
                image.transform.position = new Vector3(position.x, position.y,2);
                image.transform.parent = showOut.transform;

                image = ResourceManager.CreateImage("image/grid/gridBg");
                image.transform.localScale = new Vector3(0.5f, 0.5f);
                image.transform.position = new Vector3(position.x, position.y, 3);
                image.transform.parent = background.transform;
            }
            showOut.transform.position = new Vector3(game.offx1, game.offy1);
        }

        public bool IsInPiece(int x,int y)
        {
            for(int i = 0; i < coords.length; i++)
            {
                if(coords[i].x == x && coords[i].y == y)
                {
                    return true;
                }
            }
            return false;
        }

        private float startDragX;
        private float startDragY;
        private float startDragTouchX;
        private float startDragTouchY;
        public void StartDrag(float x,float y)
        {
            show.transform.position = new Vector3(x + tipOffx, y + tipOffy);
            startDragTouchX = x;
            startDragTouchY = y;
            startDragX = show.transform.position.x;
            startDragY = show.transform.position.y;
            show.SetActive(true);
            showOut.SetActive(false);
        }

        public void DragMove(float x,float y)
        {
            Check(x, y);
        }

        public void StopDrag(float x,float y)
        {
            Check(x, y, true);
        }

        private void Check(float x,float y,bool showResult = false)
        {
            HaxgonCoord<Coord> sys = game.coordSys;
            foreach (var item in sys.coords)
            {
                Coord coord = item.Value;
                if (coord.piece == this)
                {
                    coord.piece = null;
                    coord.type = 0;
                }
            }
            bool find = true;
            show.transform.position = new Vector3(startDragX - startDragTouchX + x, startDragY - startDragTouchY + y, showResult ? 0 : -1);
            for (int i = 0; i < this.coords.length; i++)
            {
                Point2D position = HaxgonCoord<Coord>.CoordToPosition(Point2D.Create(coords[i].x, coords[i].y), 0.4f);
                position.x += show.transform.position.x - game.offx;
                position.y += show.transform.position.y - game.offy;
                Point2D pos = HaxgonCoord<Coord>.PositionToCoord(position, 0.4f);
                Coord coord = sys.GetCoord(pos);
                if (coord == null || coord.piece != null)
                {
                    find = false;
                    break;
                }
            }
            if (find)
            {

                Point2D position0 = new Point2D();
                Point2D copy = new Point2D();
                for (int i = 0; i < this.coords.length; i++)
                {
                    Point2D position = HaxgonCoord<Coord>.CoordToPosition(Point2D.Create(coords[i].x, coords[i].y), 0.4f);
                    if(i == 0)
                    {
                        copy.x = position.x;
                        copy.y = position.y;
                    }
                    position.x += show.transform.position.x - game.offx;
                    position.y += show.transform.position.y - game.offy;
                    Point2D pos = HaxgonCoord<Coord>.PositionToCoord(position, 0.4f);
                    if (i == 0)
                    {
                        position0 = HaxgonCoord<Coord>.CoordToPosition(pos, 0.4f);
                    }
                    Coord coord = sys.GetCoord(pos);
                    coord.piece = this;
                    coord.type = coords[i].type;
                }
                if (showResult)
                {
                    show.transform.position = new Vector3(position0.x + game.offx - copy.x, position0.y + game.offy - copy.y);
                    show.SetActive(true);
                    shader.SetActive(false);
                    showOut.SetActive(false);
                    isInStage = true;
                    game.CheckFinish();
                }
                else
                {
                    shader.transform.position = new Vector3(position0.x + game.offx - copy.x, position0.y + game.offy - copy.y);
                    shader.SetActive(true);
                }
            }
            else
            {
                if (showResult)
                {
                    show.SetActive(false);
                    shader.SetActive(false);
                    showOut.SetActive(true);
                    isInStage = false;
                }
                else
                {
                    shader.SetActive(false);
                }
            }
        }
    }
}