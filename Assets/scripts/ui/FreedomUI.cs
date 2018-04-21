using System;
using System.Collections;
using System.Collections.Generic;
using lib;
using UnityEngine;
using UnityEngine.UI;

public class FreedomUI : MonoBehaviour
{

    private void Awake()
    {
        ButtonClick.dispatcher.AddListener("quitFreedom", OnQuit);
        ButtonClick.dispatcher.AddListener("easy", OnEasy);
        ButtonClick.dispatcher.AddListener("normal", OnNormal);
        ButtonClick.dispatcher.AddListener("hard", OnHard);
    }

    private void OnHard(lib.Event e)
    {
        GameVO.Instance.model = GameModel.Freedom;
        GameVO.Instance.difficulty = DifficultyMode.Hard;
        GameVO.Instance.ShowModule(ModuleName.Game);
    }

    private void OnNormal(lib.Event e)
    {
        GameVO.Instance.model = GameModel.Freedom;
        GameVO.Instance.difficulty = DifficultyMode.Normal;
        GameVO.Instance.ShowModule(ModuleName.Game);
    }

    private void OnEasy(lib.Event e)
    {
        GameVO.Instance.model = GameModel.Freedom;
        GameVO.Instance.difficulty = DifficultyMode.Easy;
        GameVO.Instance.ShowModule(ModuleName.Game);
    }

    private void OnQuit(lib.Event e)
    {
        GameVO.Instance.ShowModule(ModuleName.Main);
    }

    private void OnEnable()
    {
    }
}
