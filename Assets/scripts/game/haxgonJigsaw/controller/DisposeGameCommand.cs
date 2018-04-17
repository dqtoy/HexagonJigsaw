using lib;

namespace hexjig
{
    /// <summary>
    /// 删除游戏
    /// </summary>
    public class DisposeGameCommand
    {
        public DisposeGameCommand()
        {
            MainData.Instance.dispatcher.DispatchWith(EventType.DISPOSE_GAME);
        }
    }
}