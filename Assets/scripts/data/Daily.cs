using lib;
using System.Collections.Generic;

namespace data
{
    public class Daily : ObjectValue
    {
        public Int passLevel = new Int();

        public Array<int> levels = new Array<int>();

        public Float progress = new Float();

        public Daily()
        {
        }

        override protected void Decode(Dictionary<string,object> val)
        {
            passLevel.value = (int)val["passLevel"];
            //for(int i = 0; i < )
        }


        override protected string Encode()
        {
            string content = "";
            content += "{";
            content += "passLevel:" + passLevel + ",";
            content += "levels:[";
            for(int i = 0; i < levels.length; i++)
            {
                content += levels[i] + (i < levels.length - 1 ? "," : "");
            }
            content += "],";
            content += "}";
            return content;
        }
    }
}