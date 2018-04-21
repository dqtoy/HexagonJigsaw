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

        private void Awake()
        {
            MainData.Instance.dispatcher.AddListener(EventType.DISPOSE_GAME, OnDisposeGame);
            MainData.Instance.dispatcher.AddListener(EventType.QUIT_LEVEL, OnDisposeGame);

            //创建背景
            background = new Background();
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
