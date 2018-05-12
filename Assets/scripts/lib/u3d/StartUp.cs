using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace lib
{
    public class StartUp : MonoBehaviour
    {
        //主相机
        public Camera mainCamera;

        private void Awake()
        {
            Instance = this;

            BufferPool.bufferRoot = new GameObject();
            BufferPool.bufferRoot.name = "BufferPool";
            BufferPool.bufferRoot.SetActive(false);
        }

        public void Print(string val)
        {
            Debug.Log(val);
        }


        // Update is called once per frame
        void Update()
        {
        }
        
        public GameObject Create(UnityEngine.Object obj)
        {
            return Instantiate(obj) as GameObject;
        }

        public void GetScreenCut(ScreenCut cut,int x,int y,int width,int height)
        {
            StartCoroutine(getScreenTexture(cut, x, y, width, height));
        }

        IEnumerator getScreenTexture(ScreenCut cut,int capx,int capy,int capwidth,int capheight)
        {
            yield return new WaitForEndOfFrame();
            Texture2D t = new Texture2D(capwidth, capheight, TextureFormat.RGB24, true);//需要正确设置好图片保存格式  
            t.ReadPixels(new UnityEngine.Rect(capx, capy, capwidth, capheight), 0, 0, false);//按照设定区域读取像素；注意是以左下角为原点读取  
            t.Apply();
            cut.DispatchWith(lib.Event.COMPLETE, t);
        }

        public static StartUp Instance;
    }

}