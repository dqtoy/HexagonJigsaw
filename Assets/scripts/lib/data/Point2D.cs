using System.Collections.Generic;

namespace lib
{
    public class Point2D
    {
        private FloatValue xValue = new FloatValue();
        private FloatValue yValue = new FloatValue();

        public Point2D(float x = 0,float y = 0)
        {
            this.xValue.Value = x;
            this.yValue.Value = y;
        }

        public float x
        {
            get { return (float)xValue.Value; }
            set { xValue.Value = value; }
        }
        public float y
        {
            get { return (float)yValue.Value; }
            set { yValue.Value = value; }
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
    }
}