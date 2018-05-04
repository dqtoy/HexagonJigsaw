using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        public static Sprite CreateSprite(string url)
        {
            Texture2D txt2d = ResourceManager.GetResource<Texture2D>(url);
            UnityEngine.Rect rec = new UnityEngine.Rect(0, 0, txt2d.width, txt2d.height);
            Sprite sp = Sprite.Create(txt2d, rec, new Vector2(0.5f, 0.5f));
            return sp;
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

        public static GameObject CreateUIImage(string url)
        {
            Texture2D txt2d = ResourceManager.GetResource<Texture2D>(url);
            GameObject obj = new GameObject();
            Image spr = obj.AddComponent<Image>();
            UnityEngine.Rect rec = new UnityEngine.Rect(0, 0, txt2d.width, txt2d.height);
            Sprite sp = Sprite.Create(txt2d, rec, new Vector2(0.5f, 0.5f));
            spr.sprite = sp;
            obj.GetComponent<RectTransform>().localScale = new Vector3(1, 1);
            obj.GetComponent<RectTransform>().sizeDelta = new Vector2(txt2d.width, txt2d.height);
            return obj;
        }

        public static AudioSource PlaySound(string url,bool loop = false,float volume = 1.0f)
        {
            GameObject obj = new GameObject();
            AudioSource audio = obj.AddComponent<AudioSource>();
            obj.AddComponent<SoundControl>();
            audio.clip = ResourceManager.GetResource<AudioClip>(url);
            audio.loop = loop;
            audio.volume = volume;
            audio.Play();
            return audio;
        }
    }

}