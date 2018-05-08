using lib;
using System.Collections.Generic;

namespace data
{
   
    public class TestItem
    {
        /// <summary>
        /// int 类型
        /// </summary>
        public Int _int = new Int(2);

        public Float _float = new Float(1.2f);

        public Bool _bool = new Bool(true);

        public String _string = new String("cxx");

        public Array<int> _ints = new Array<int>(new int[] {5, 7, 9});
    }

    public class IntType
    {
        public int A = 1;
        public int B = 2;
        public int C = 3;
    }
}