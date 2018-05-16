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
        ButtonClick.dispatcher.AddListener("shopBuy", OnBuy);
    }

    private void OnBuy(lib.Event e)
    {
        GameVO.Instance.googlePlatform.Buy("item_0");
    }

    private void OnQuitShop(lib.Event e)
    {
        GameVO.Instance.ShowModule(ModuleName.Main);
    }
}
