using System;
using UnityEngine;

namespace lib
{
    public class ScreenCut : EventDispatcher {

        Transform parent;
        float localX;
        float localY;
        float localZ;
        float scale;

        public ScreenCut(int x,int y,int width,int height,Transform parent = null,float localX = 0,float localY = 0,float localZ = 0,float scale = 1)
        {
            this.parent = parent;
            this.localX = localX;
            this.localY = localY;
            this.localZ = localZ;
            this.scale = scale;
            StartUp.Instance.GetScreenCut(this, x, y, width, height);
            if(parent != null)
            {
                AddListener(lib.Event.COMPLETE, OnComplete);
            }
        }

        private void OnComplete(Event e)
        {
            Texture2D txt2d = e.Data as Texture2D;
            GameObject obj = new GameObject();
            obj.name = "screenCut";
            SpriteRenderer spr = obj.AddComponent<SpriteRenderer>();
            UnityEngine.Rect rec = new UnityEngine.Rect(0, 0, txt2d.width, txt2d.height);
            spr.sprite = Sprite.Create(txt2d, rec, new Vector2(0.5f, 0.5f));
            obj.transform.parent = parent;
            obj.transform.localPosition = new Vector3(localX, localY,localZ);
            obj.transform.localScale = new Vector3(scale, scale, 1);
        }
    }
}

