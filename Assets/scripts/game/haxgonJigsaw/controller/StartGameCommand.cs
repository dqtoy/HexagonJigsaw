using lib;

namespace hexjig
{
    /// <summary>
    /// 开始关卡
    /// </summary>
    public class StartGameCommand
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="levelId">关卡 id</param>
        public StartGameCommand(int levelId)
        {
            MainData.Instance.dispatcher.DispatchWith(EventType.DISPOSE_GAME);

            LevelConfig level = LevelConfig.GetConfig(levelId);
            if(level != null)
            {
                MainData.Instance.levelId.value = levelId;
                MainData.Instance.time.value = 0;
                MainData.Instance.config = level;
                new Game(level);

                MainData.Instance.dispatcher.DispatchWith(EventType.START_LEVEL);
            }
        }
    }
}