using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace lib
{
    public class ResourceManager
    {
        private static Dictionary<string, UnityEngine.Object> resources = new Dictionary<string, UnityEngine.Object>();

        //获取资源
        public static T GetResource<T>(string url) where T : class
        {
            if (resources.ContainsKey(url) == false)
            {
                resources.Add(url, Resources.Load(url));
                Resources.UnloadUnusedAssets();
            }
            return resources[url] as T;
        }

        public static GameObject CreateImage(string url)
        {
            Texture2D txt2d = ResourceManager.GetResource<Texture2D>(url);
            GameObject obj = new GameObject();
            SpriteRenderer spr = obj.AddComponent<SpriteRenderer>();
            UnityEngine.Rect rec = new UnityEngine.Rect(0, 0, txt2d.width, txt2d.height);
            Sprite sp = Sprite.Create(txt2d, rec, new Vector2(0.5f, 0.5f));
            spr.sprite = sp;
            return obj;
        }
    }

}