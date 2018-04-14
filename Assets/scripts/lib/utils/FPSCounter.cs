using System;

namespace lib
{
    public class FPSCounter
    {
        private DateTime lastDate;
        private int time;
        private int frame;
        public Int Fps;

        public FPSCounter()
        {
            time = 0;
            frame = 0;
            lastDate = DateTime.Now;
            Fps = new Int();
        }

        public void Update()
        {
            DateTime now = DateTime.Now;
            TimeSpan timeGap = now.Subtract(lastDate).Duration();
            lastDate = now;
            time += timeGap.Milliseconds;
            frame++;
            if(time > 1000)
            {
                Fps.value = frame * 1000 / time;
                time = 0;
                frame = 0;
            }
        }
    }
}