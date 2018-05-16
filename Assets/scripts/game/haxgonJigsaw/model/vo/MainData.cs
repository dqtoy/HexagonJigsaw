using lib;
using UnityEngine;

namespace hexjig
{
    public class MainData
    {
        /// <summary>
        /// 相关事件类型参考 hexjig.EventType
        /// </summary>
        public EventDispatcher dispatcher = new EventDispatcher();

        /// <summary>
        /// 当前关卡耗时，毫秒
        /// </summary>
        public Int time = new Int();

        /// <summary>
        /// 关卡 id
        /// </summary>
        public Int levelId = new Int();

        //关卡尺寸大小
        public float levelWidth;
        public float levelHeight;
        public bool isLoading = true;

        public GameObject showCutRoot; 

        /// <summary>
        /// 当前关卡配置
        /// </summary>
        public LevelConfig config;

        private static MainData instance;

        public static MainData Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new MainData();
                }
                return instance;
            }
        }
    }

    public class EventType
    {
        /// <summary>
        /// 开始关卡
        /// </summary>
        public static string START_LEVEL = "start_level";

        /// <summary>
        /// 重新开启关卡
        /// </summary>
        public static string RESTART = "restart";

        /// <summary>
        /// 退回
        /// </summary>
        public static string BACK_STEP = "back_step";

        /// <summary>
        /// 完成关卡
        /// </summary>
        public static string FINISH_LEVEL = "finish_level";

        /// <summary>
        /// 显示提示
        /// </summary>
        public static string SHOW_TIP = "show_tip";

        /// <summary>
        /// 退出关卡
        /// </summary>
        public static string QUIT_LEVEL = "quit_level";

        /// <summary>
        /// 显示截图
        /// </summary>
        public static string SHOW_CUT = "show_cut";

        /// <summary>
        /// 显示截图
        /// </summary>
        public static string SHOW_CUT_COMPLETE = "show_cut_complete";

        /// <summary>
        /// 放置了一片在舞台上
        /// </summary>
        public static string SET_PIECE = "set_piece";

        public static string SHOW_GAME_CHANGE_OUT_EFFECT0 = "show_game_change_out_effect0";
        public static string SHOW_GAME_CHANGE_OUT_EFFECT = "show_game_change_out_effect";
        internal static string SHOW_GAME_CHANGE_OUT_EFFECT_COMPLETE2 = "show_game_change_out_effect_complete2";
        public static string SHOW_GAME_CHANGE_OUT_EFFECT_COMPLETE = "show_game_change_out_effect_complete";
        public static string SHOW_GAME_CHANGE_IN_EFFECT = "show_game_change_in_effect";

        /// <summary>
        /// 消除游戏
        /// </summary>
        internal static string DISPOSE_GAME = "dispose_game";

        /// <summary>
        /// 隐藏游戏
        /// </summary>
        internal static string HIDE_GAME = "hide_game";



        /// <summary>
        /// 
        /// </summary>
        internal static string SHOW_START_EFFECT = "show_start_effect";


    }
}