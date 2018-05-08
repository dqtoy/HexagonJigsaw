using System;
using System.Collections;
using System.Collections.Generic;
using lib;
using UnityEngine;
using UnityEngine.UI;

public class FreedomUI : MonoBehaviour
{
    public RectTransform hex;
    public RectTransform line1;
    public RectTransform line2;
    public RectTransform quit;

    private void Awake()
    {
        line2.sizeDelta = new Vector2(line2.sizeDelta.x, GameVO.Instance.PixelHeight);
        UIFix.SetDistanceToTop(hex);
        UIFix.SetDistanceToTop(line1);
        UIFix.SetDistanceToTop(line2);
        line2.localPosition = new Vector3(line2.localPosition.x, line2.localPosition.y - GameVO.Instance.PixelHeight * 0.5f + 1280 * 0.5f);
        UIFix.SetDistanceToTop(quit);

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
