using System;
using System.Collections.Generic;

namespace lib
{
    /// <summary>
    /// 六边形坐标系
    /// </summary>
    public class HaxgonCoord<T>
    {
        private HaxgonCoordDirection direction;

        public Dictionary<string, T> coords = new Dictionary<string, T>();

        public HaxgonCoord(HaxgonCoordDirection direction = HaxgonCoordDirection.HORIZONTAL)
        {
            this.direction = direction;
        }

        public void SetCoord(Point2D coord, T value)
        {
            if (coords.ContainsKey(coord.x + "," + coord.y))
            {
                coords[coord.x + "," + coord.y] = value;
            }
            else
            {
                coords.Add(coord.x + "," + coord.y, value);
            }
        }

        public T GetCoord(Point2D coord)
        {
            if(coords.ContainsKey(coord.x + "," + coord.y))
            {
                return coords[coord.x + "," + coord.y];
            }
            return default(T);
        }

        /// <summary>
        /// 把坐标转换成像素位置
        /// </summary>
        /// <param name="coordx">x 坐标</param>
        /// <param name="cooordy">y 坐标</param>
        /// <param name="radius">六边形半径</param>
        /// <param name="direction">六边形的方向</param>
        /// <returns>位置信息</returns>
        public static Point2D CoordToPosition(Point2D coord, float radius, HaxgonCoordDirection direction = HaxgonCoordDirection.HORIZONTAL)
        {
            Point2D p = Point2D.Create();
            if(direction == HaxgonCoordDirection.HORIZONTAL)
            {
                p.x = coord.x * 1.5f * radius;
                p.y = (2 * coord.y + (coord.x % 2 == 0 ? 0 : 1)) * halfSqrt3 * radius;
            }
            else
            {

            }
            return p;
        }

        public static Point2D PositionToCoord(Point2D position, float radius, HaxgonCoordDirection direction = HaxgonCoordDirection.HORIZONTAL)
        {
            Point2D p = Point2D.Create();
            if (direction == HaxgonCoordDirection.HORIZONTAL)
            {
                int x = (int)Math.Floor(position.x * 2 / radius);
                int y = (int)Math.Floor(position.y / (radius * halfSqrt3));
                p.x = (float)Math.Floor((x + 1) / 3.0);
                p.y = (float)Math.Floor((y + (p.x % 2 == 0 ? 1 : 0)) / 2.0);
                if (x % 3 == 1 || x % 3 == -2)
                {
                    Point2D p2 = CoordToPosition(p, radius, direction);
                    p2.x = position.x - p2.x;
                    p2.y = position.y - p2.y;
                    if (Line.IsPointHigher(p2, -2 * halfSqrt3, 2 * halfSqrt3 * radius))
                    {
                        if(p.x % 2 == 0)
                        {
                            p.x++;
                        }
                        else
                        {
                            p.x++;
                            p.y++;
                        }
                    }
                    else if (Line.IsPointLower(p2, 2 * halfSqrt3, -2 * halfSqrt3 * radius))
                    {

                        if (p.x % 2 == 0)
                        {
                            p.x++;
                            p.y--;
                        }
                        else
                        {
                            p.x++;
                        }
                    }
                    Point2D.Release(p2);
                }
            }
            else
            {

            }
            return p;
        }

        /// <summary>
        /// 获取与某点相邻的所有坐标点
        /// </summary>
        /// <param name="coord"></param>
        /// <returns></returns>
        public static List<Point2D> GetCoordsNextTo(Point2D coord, HaxgonCoordDirection direction = HaxgonCoordDirection.HORIZONTAL)
        {
            List<Point2D> coords = new List<Point2D>();
            if (direction == HaxgonCoordDirection.HORIZONTAL)
            {
                if(coord.x % 2 == 0)
                {
                    coords.Add(Point2D.Create(1, 0));
                    coords.Add(Point2D.Create(1, -1));
                    coords.Add(Point2D.Create(0, -1));
                    coords.Add(Point2D.Create(-1, -1));
                    coords.Add(Point2D.Create(-1, 0));
                    coords.Add(Point2D.Create(0, 1));
                }
                else
                {
                    coords.Add(Point2D.Create(1, 0));
                    coords.Add(Point2D.Create(0, -1));
                    coords.Add(Point2D.Create(-1, 0));
                    coords.Add(Point2D.Create(-1, 1));
                    coords.Add(Point2D.Create(0, 1));
                    coords.Add(Point2D.Create(1, 1));
                }
            }
            for(int i = 0; i < coords.Count; i++)
            {
                coords[i].x += coord.x;
                coords[i].y += coord.y;
            }
            return coords;
        }

        private static float halfSqrt3 = 0.8660254037844386f;
    }

    public enum HaxgonCoordDirection
    {
        HORIZONTAL,
        VERTICAL
    }
}