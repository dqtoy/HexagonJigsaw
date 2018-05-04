using lib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace hexjig
{
    public class GameBufferPool
    {

        public static GameObject CreateGrid(int type)
       {
            GameObject image;
            if (BufferPool.GetLength("Grid" + type) > 0)
            {
                image = BufferPool.Get<GameObject>("Grid" + type);
                image.transform.localScale = new Vector3(1, 1, 1);
                image.transform.localPosition = new Vector3(0, 0, 0);
                image.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
            else
            {
                image = ResourceManager.CreateImage("image/grid/" + type);
                image.name = type + "";
            }
            return image;
        }

        public static void ReleaseGrid(GameObject grid)
        {
            BufferPool.Add("Grid" + grid.name, grid);
        }

        public static GameObject CreateGridBg()
        {
            GameObject image;
            if (BufferPool.GetLength("gridBg") > 0)
            {
                image = BufferPool.Get<GameObject>("gridBg");
                image.transform.localScale = new Vector3(1, 1, 1);
                image.transform.localPosition = new Vector3(0, 0, 0);
                image.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
            else
            {
                image = ResourceManager.CreateImage("image/grid/gridBg");
                image.name = "gridBg";
            }
            return image;
        }

        public static void ReleaseGridBg(GameObject grid)
        {
            BufferPool.Add("gridBg", grid);
        }
    }
}
