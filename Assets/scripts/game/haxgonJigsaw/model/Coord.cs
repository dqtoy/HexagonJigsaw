using lib;

namespace hexjig
{
    public class Coord : Point2D
    {
        /// <summary>
        /// 格子的类型
        /// </summary>
        public int type;

        public int x;

        public int y;

        public Piece piece;
    }
}