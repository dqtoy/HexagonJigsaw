namespace lib
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    public delegate void listenerBack(Event e);

    public interface IEventDispatcher
    {
        void AddListener(string type, listenerBack listener);

        void RemoveListener(string type, listenerBack listener);

        void Dispatch(Event e);

        void DispatchWith(string type, object data = null);
    }
}