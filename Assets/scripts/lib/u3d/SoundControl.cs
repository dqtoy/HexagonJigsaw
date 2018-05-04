using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace lib
{
    public class SoundControl : MonoBehaviour, IEventDispatcher
    {

        private AudioSource audio;

        private EventDispatcher dispatcher;

        public SoundControl()
        {
            dispatcher = new EventDispatcher(this);
        }

        public void AddListener(string type, listenerBack listener)
        {
            dispatcher.AddListener(type, listener);
        }

        public void RemoveListener(string type, listenerBack listener)
        {
            dispatcher.RemoveListener(type, listener);
        }

        public void Dispatch(lib.Event e)
        {
            dispatcher.Dispatch(e);
        }

        public void DispatchWith(string type, object data = null)
        {
            dispatcher.DispatchWith(type, data);
        }


        // Use this for initialization
        void Start()
        {
            audio = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            if (audio.isPlaying == false)
            {
                Destroy(gameObject);
            }
        }
    }
}
