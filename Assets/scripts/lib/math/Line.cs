namespace lib
{
    public class Line
    {
        /// <summary>
        /// 判断点是否高于直线
        /// </summary>
        /// <param name="p"></param>
        /// <param name="k">直线斜率</param>
        /// <param name="b">直线与 y 的交点位置</param>
        /// <returns></returns>
        public static bool IsPointHigher(Point2D p,float k,float b)
        {
            return p.y > k * p.x + b;
        }

        /// <summary>
        /// 判断点是否低于直线
        /// </summary>
        /// <param name="p"></param>
        /// <param name="k">直线斜率</param>
        /// <param name="b">直线与 y 的交点位置</param>
        /// <returns></returns>
        public static bool IsPointLower(Point2D p, float k, float b)
        {
            return p.y < k * p.x + b;
        }
    }
}