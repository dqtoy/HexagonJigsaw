using System;
using lib;
using UnityEngine;

/// <summary>
/// 六边形拼图游戏
/// </summary>
namespace hexjig
{
    public class Start : MonoBehaviour
    {
        private Background background;

        public static Background backgroundInstance;

        private void Awake()
        {
            MainData.Instance.dispatcher.AddListener(EventType.DISPOSE_GAME, OnDisposeGame);
            MainData.Instance.dispatcher.AddListener(EventType.QUIT_LEVEL, OnDisposeGame); 
            MainData.Instance.dispatcher.AddListener(EventType.SHOW_GAME_CHANGE_OUT_EFFECT_COMPLETE2, OnDisposeGameChangeOut);

            //创建背景
            background = Background.Create();
            background.bsize = GameVO.Instance.Height * 1.42f;
            backgroundInstance = background;
        }

        private void OnDisposeGameChangeOut(lib.Event e)
        {
            Destroy((e.Data as Game).changeOutRoot);
        }

        private void OnDisposeGame(lib.Event e)
        {
            if (Game.Instance != null)
            {
                Game.Instance.Dispose();
                Destroy(Game.Instance.root);
                Game.Instance = null;
            }
        }

        private void Update()
        {
            background.Update();
            if (Game.Instance != null)
            {
                Game.Instance.Update();
            }
        }
    }
}
