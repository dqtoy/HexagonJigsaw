using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using lib;
using DG.Tweening;

public class DailyUI : MonoBehaviour {

    private List<Text> txts = new List<Text>();
    private List<Text> times = new List<Text>();
    private List<Image> icons = new List<Image>();
    private List<GameObject> locks = new List<GameObject>();
    private Color[] colors = {
        new Color(255/255.0f,103/255.0f,102/255.0f),
        new Color(250/255.0f,228/255.0f,108/255.0f),
        new Color(186/255.0f,250/255.0f,103/255.0f),
        new Color(127/255.0f,230/255.0f,108/255.0f),
        new Color(103/255.0f,235/255.0f,250/255.0f),
        new Color(103/255.0f,190/255.0f,250/255.0f),
        new Color(103/255.0f,141/255.0f,250/255.0f),
        new Color(165/255.0f,108/255.0f,244/255.0f),
        new Color(212/255.0f,111/255.0f,241/255.0f),
        new Color(240/255.0f,113/255.0f,217/255.0f)
    };

    public Text dailyProgressTxt;

    public Transform buttonsTransform;
    public Image progress;
    public Image progressLess;

    public RectTransform line;
    public RectTransform hex;

    public Transform hit1;

    private void Awake()
    {
        UIFix.SetDistanceToTop(hex);
        UIFix.SetDistanceToBottom(hit1);

        line.sizeDelta = new Vector2(line.sizeDelta.x, GameVO.Instance.PixelHeight);

        ButtonClick.dispatcher.AddListener("quitDaily", OnQuit);
        foreach (Transform child in buttonsTransform)
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
                    if (child2.gameObject.name == "icon")
                    {
                        icons.Add(child2.gameObject.GetComponent<Image>());
                    }
                    if(child2.gameObject.name == "lock")
                    {
                        locks.Add(child2.gameObject);
                    }
                }
            }
        }
        txts.Reverse();
        times.Reverse();
        icons.Reverse();
        locks.Reverse();

        GameVO.Instance.daily.progress.AddListener(lib.Event.CHANGE, OnDailyProgressChange);
    }

    private void OnDailyProgressChange(lib.Event e = null)
    {
        dailyProgressTxt.text = GameVO.Instance.daily.progress.value + "/" + GameVO.Instance.daily.all.value;
        progress.fillAmount = 1.0f * GameVO.Instance.daily.progress.value / GameVO.Instance.daily.all.value;
        progressLess.fillAmount = 1 - 1.0f * GameVO.Instance.daily.progress.value / GameVO.Instance.daily.all.value;
    }

    /// <summary>
    /// 点击关卡
    /// </summary>
    /// <param name="e"></param>
    private void OnClickLevel(lib.Event e)
    {
        GameObject obj = e.Data as GameObject;
        int index = (int)StringUtils.ToNumber(StringUtils.Slice(obj.name,"level".Length,obj.name.Length)) - 1;
        if(index != 0)
        {
            if(GameVO.Instance.daily.levels[index - 1].pass == false)
            {
                Color c = txts[GameVO.Instance.daily.GetCurrentLevel()].color;
                Sequence mySequence = DOTween.Sequence();
                mySequence.Append(txts[GameVO.Instance.daily.GetCurrentLevel()].DOColor(new Color(c.r,c.g,c.b,0),0.2f));
                mySequence.Append(txts[GameVO.Instance.daily.GetCurrentLevel()].DOColor(new Color(c.r, c.g, c.b, 1), 0.2f));
                mySequence.Append(txts[GameVO.Instance.daily.GetCurrentLevel()].DOColor(new Color(c.r, c.g, c.b, 0), 0.2f));
                mySequence.Append(txts[GameVO.Instance.daily.GetCurrentLevel()].DOColor(new Color(c.r, c.g, c.b, 1), 0.2f));
                return;
            }
        }
        DailyUIFade.dailyIndex = index;
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
            if (vo.pass)
            {
                times[i].text = StringUtils.TimeToMS(vo.time);
                txts[i].color = colors[i];
                icons[i].color = colors[i];
                txts[i].gameObject.SetActive(true);
                locks[i].SetActive(false);
            }
            else
            {
                times[i].text = "";
                txts[i].color = new Color((float)(254.0 / 255.0), (float)(250.0 / 255.0), (float)(203.0 / 255.0));
                icons[i].color = new Color((float)(254.0 / 255.0), (float)(250.0 / 255.0), (float)(203.0 / 255.0));
                if (i == 0 || GameVO.Instance.daily.levels[i - 1].pass == true)
                {
                    txts[i].color = colors[i];
                    txts[i].gameObject.SetActive(true);
                    locks[i].SetActive(false);
                }
                else
                {
                    txts[i].gameObject.SetActive(false);
                    locks[i].SetActive(true);
                }
            }
        }
        OnDailyProgressChange();
    }
}
