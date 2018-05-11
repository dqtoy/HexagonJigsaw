using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using lib;
using UnityEngine;

public class ShopUI : MonoBehaviour {

    private void Awake()
    {
        ButtonClick.dispatcher.AddListener("quitShop", OnQuitShop);
    }

    private void OnQuitShop(lib.Event e)
    {
        GameVO.Instance.ShowModule(ModuleName.Main);
    }
}
