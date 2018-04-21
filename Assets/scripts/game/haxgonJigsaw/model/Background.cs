using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using lib;

namespace hexjig
{
    public class Background
    {
        private GameObject bg1;
        private GameObject bg2;
        private float pos = 0.5f;
        private float posSpeed = 0.00015f;
        private float rotation = 0;
        private float rotationSpeed = 0.020f;
        private GameObject container;

        public Background()
        {
            container = new GameObject();

            bg1 = ResourceManager.CreateImage("image/bg1");
            bg1.transform.parent = container.transform;

            bg2 = ResourceManager.CreateImage("image/bg2");
            bg2.transform.parent = container.transform;

            posSpeed = (Random.Range(1,2) == 1? 1 : - 1) *(posSpeed + (Random.Range(0, posSpeed/2) - posSpeed / 4));
            rotationSpeed = (Random.Range(1, 2) == 1 ? 1 : -1) * (rotationSpeed + (Random.Range(0, rotationSpeed / 2) - rotationSpeed / 4));
            container.transform.Rotate(new Vector3(0, 0, 360 * Random.Range(0, 1.0f)));
            Update();
        }


        public void Update()
        {
            pos += posSpeed;

            bool flag = false;
            float sin = Mathf.Sin((container.transform.localEulerAngles.z + 90) * Mathf.PI / 180);
            float cos = Mathf.Cos((container.transform.localEulerAngles.z + 90) * Mathf.PI / 180);
            float cx = GameVO.Instance.Height * 1.42f * (pos - 0.5f) * Mathf.Cos(container.transform.localEulerAngles.z * Mathf.PI / 180);
            float cy = GameVO.Instance.Height * 1.42f * (pos - 0.5f) * Mathf.Sin(container.transform.localEulerAngles.z * Mathf.PI / 180);
            //line (y - cy) * cos = (x - cx) * sin  
            if (sin != 0)
            {
                //与上边框的交点 y = GameVO.Instance.Height * 0.5f;
                float x = (GameVO.Instance.Height * 0.5f - cy) * cos / sin + cx;
                if(x < GameVO.Instance.Width * 0.5f && x > GameVO.Instance.Width * -0.5f)
                {
                    flag = true;
                }
                //与下边框的交点 y = -GameVO.Instance.Height * 0.5f;
                x = (-GameVO.Instance.Height * 0.5f - cy) * cos / sin + cx;
                if (x < GameVO.Instance.Width * 0.5f && x > GameVO.Instance.Width * -0.5f)
                {
                    flag = true;
                }
            }
            if(cos != 0)
            {
                //与右边框的交点 x = GameVO.Instance.Width * 0.5f;
                float y = (GameVO.Instance.Width * 0.5f - cx) * sin / cos + cy;
                if(y < GameVO.Instance.Height * 0.5f && y > - GameVO.Instance.Height * 0.5f)
                {
                    flag = true;
                }
                //与左边框的交点 x = GameVO.Instance.Width * 0.5f;
                y = (-GameVO.Instance.Width * 0.5f - cx) * sin / cos + cy;
                if (y < GameVO.Instance.Height * 0.5f && y > -GameVO.Instance.Height * 0.5f)
                {
                    flag = true;
                }
            }
            //与四个边框都没交点
            if (flag == false)
            {
                posSpeed = -posSpeed;
            }

            bg1.transform.localScale = new Vector3(GameVO.Instance.Height * 1.42f * 100 * pos, GameVO.Instance.Height * 1.42f * 100);
            bg1.transform.localPosition = new Vector3(GameVO.Instance.Height * 1.42f * (pos * 0.5f - 0.5f), 0, 110);

            bg2.transform.localScale = new Vector3(GameVO.Instance.Height * 1.42f * 100 * (1.0f - pos), GameVO.Instance.Height * 1.42f * 100);
            bg2.transform.localPosition = new Vector3(GameVO.Instance.Height * 1.42f * (0.5f - (1.0f - pos) * 0.5f), 0, 110);

            container.transform.Rotate(new Vector3(0, 0, rotationSpeed));
        }
    }
}