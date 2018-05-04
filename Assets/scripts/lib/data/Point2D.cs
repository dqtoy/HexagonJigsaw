using System.Collections.Generic;

namespace lib
{
    public class Point2D : ObjectValue
    {
        private Float xValue = new Float();
        private Float yValue = new Float();

        public Point2D(float x = 0,float y = 0)
        {
            this.xValue.value = x;
            this.yValue.value = y;
        }

        public float x
        {
            get { return (float)xValue.value; }
            set { xValue.value = value; }
        }

        public float y
        {
            get { return (float)yValue.value; }
            set { yValue.value = value; }
        }

        public Point2D Clone()
        {
            return Point2D.Create(x, y);
        }

        private static List<Point2D> pools = new List<Point2D>();

        public static Point2D Create(float x = 0, float y = 0)
        {
            if(pools.Count > 0)
            {
                Point2D p = pools[pools.Count - 1];
                pools.RemoveAt(pools.Count - 1);
                p.x = x;
                p.y = y;
                return p;
            }
            return new Point2D(x, y);
        }

        public static void Release(Point2D p)
        {
            pools.Add(p);
        }


        /// <summary>
        /// 范序列化
        /// </summary>
        /// <param name="val"></param>
        override protected void Decode(string val)
        {

        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <returns></returns>
        override protected string Encode()
        {
            return "{'x': " + x + ",'y': " + y + "}";
        }
    }
}