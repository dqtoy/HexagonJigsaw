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
        private void Awake()
        {
            MainData.Instance.dispatcher.AddListener(EventType.DISPOSE_GAME, OnDisposeGame);
        }

        private void OnDisposeGame(lib.Event e)
        {
            if (Game.Instance != null)
            {
                Destroy(Game.Instance.root);
                Game.Instance = null;
            }
        }

        private void Update()
        {
            if(Game.Instance != null)
            {
                Game.Instance.Update();
            }
        }
    }
}
