using System;
using System.Collections.Generic;

namespace lib
{
    /// <summary>
    /// 六边形坐标系
    /// </summary>
    public class HaxgonCoord
    {
        private HaxgonCoordDirection direction;
        private Dictionary<string, object> coords = new Dictionary<string, object>();

        public HaxgonCoord(HaxgonCoordDirection direction = HaxgonCoordDirection.HORIZONTAL)
        {
            this.direction = direction;
        }

        public void SetCoord(Point2D coord, object value)
        {
            coords.Add(coord.x + "," + coord.y, value);
        }

        public object GetCoord(Point2D coord)
        {
            return coords[coord.x + "," + coord.y];
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

        private static float halfSqrt3 = 0.8660254037844386f;
    }

    public enum HaxgonCoordDirection
    {
        HORIZONTAL,
        VERTICAL
    }
}