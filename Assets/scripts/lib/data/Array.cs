namespace lib
{
    public class Array : ValueBase
    {
        public Int length = new Int();

        public int Length
        {
            get { return (int)length.Value; }
        }
    }
}
