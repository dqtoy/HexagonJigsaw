using System.Collections;
using UnityEngine;

namespace lib
{
    public class HttpRequest : EventDispatcher
    {
        public IEnumerator Get(string _url)
        {
            WWW getData = new WWW(_url);
            yield return getData;
            if (getData.error != null)
            {
                DispatchWith(Event.ERROR, getData.error);
            }
            else
            {
                DispatchWith(Event.COMPLETE, getData.text);
            }
        }

        public IEnumerator Post(string _url, WWWForm _wForm)
        {
            WWW postData = new WWW(_url, _wForm);
            yield return postData;
            if (postData.error != null)
            {
                DispatchWith(Event.ERROR, postData.error);
            }
            else
            {
                DispatchWith(Event.COMPLETE, postData.text);
            }
        }
    }
}