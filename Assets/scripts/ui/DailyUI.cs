using System;
using System.Collections;
using System.Collections.Generic;
using lib;
using UnityEngine;
using UnityEngine.UI;

public class DailyUI : MonoBehaviour {

    private List<Text> txts = new List<Text>();
    private List<Text> times = new List<Text>();
    private Color[] colors = {
        new Color(245,108,113),
        new Color(250,228,103),
        new Color(186,250,103),
        new Color(118,240,112),
        new Color(103,235,250),
        new Color(103,190,250),
        new Color(103,141,250),
        new Color(165,108,244),
        new Color(212,111,241),
        new Color(240,113,217)
    };

    private void Awake()
    {
        ButtonClick.dispatcher.AddListener("quitDaily", OnQuit);
        foreach (Transform child in transform)
        {
            if(StringUtils.Slice(child.gameObject.name,0,"level".Length) == "level")
            {
                ButtonClick.dispatcher.AddListener(child.gameObject.name, OnClickLevel);
                foreach (Transform child2 in child.transform)
                {
                    if (child2.gameObject.name == "txt")
                    {
                        txts.Add(child2.gameObject.GetComponent<Text>());
                    }
                    if (child2.gameObject.name == "time")
                    {
                        times.Add(child2.gameObject.GetComponent<Text>());
                    }
                }
            }
        }
    }

    /// <summary>
    /// 点击关卡
    /// </summary>
    /// <param name="e"></param>
    private void OnClickLevel(lib.Event e)
    {
        GameObject obj = e.Data as GameObject;
        int index = (int)StringUtils.ToNumber(StringUtils.Slice(obj.name,"level".Length,obj.name.Length)) - 1;
        GameVO.Instance.model = GameModel.Daily;
        GameVO.Instance.ShowModule(ModuleName.Game, GameVO.Instance.daily.levels[index].config.id);
    }

    private void OnQuit(lib.Event e)
    {
        GameVO.Instance.ShowModule(ModuleName.Main);
    }

    private void OnEnable()
    {
        for(int i = 0; i < GameVO.Instance.daily.levels.length; i++)
        {
            DailyLevelVO vo = GameVO.Instance.daily.levels[i];
            if(vo.pass)
            {
                times[i].text = StringUtils.TimeToMS(vo.time);
                times[i].color = colors[i];
            }
            else
            {
                times[i].text = "";
            }
        }
    }
}
