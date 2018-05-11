using System;
using System.Collections;
using System.Collections.Generic;
using lib;
using UnityEngine;

public class StorageVO {

    public StorageVO()
    {
        if (!PlayerPrefs.HasKey("bgm"))
        {
            PlayerPrefs.SetInt("bgm", GameVO.Instance.bgmId.value);
        }
        GameVO.Instance.bgmId.value = PlayerPrefs.GetInt("bgm");
        GameVO.Instance.bgmId.AddListener(lib.Event.CHANGE, OnBgmChange);
    }

    private void OnBgmChange(lib.Event e)
    {
        if(GameVO.Instance.bgmId.value < 1000)
        {
            PlayerPrefs.SetInt("bgm", GameVO.Instance.bgmId.value);
        }
    }
}