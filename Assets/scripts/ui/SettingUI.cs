using System;
using System.Collections;
using System.Collections.Generic;
using lib;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SettingUI : MonoBehaviour {

    public Image zh_cn;
    public Image en_us;
    public Image zh_tw;
    public Image ja_jp;
    public Image ko_kr;
    public Image de_de;
    public Image es_la;

    public GameObject musicVolumn;
    public Image musicBg;
    public Text musicVolumnTxt;
    public Image musicIcon;
    public Image musicIcon2;

    public GameObject soundVolumn;
    public Image soundBg;
    public Text soundVolumnTxt;
    public Image soundIcon;
    public Image soundIcon2;

    public Image soundProgress;
    public Image soundProgressLess;
    public Image musicProgress;
    public Image musicProgressLess;

    public RectTransform line;
    public RectTransform hex;
    public RectTransform buttons;
    public RectTransform quit;

    public Transform hitEffect;

    private void Awake()
    {
        line.sizeDelta = new Vector2(line.sizeDelta.x, GameVO.Instance.PixelHeight * GameVO.Instance.scale);
        UIFix.SetDistanceToBottom(hitEffect);

        ButtonClick.dispatcher.AddListener("quitSetting", OnQuit);
        ButtonClick.dispatcher.AddListener("zh_cn", OnZhCn);
        ButtonClick.dispatcher.AddListener("en_us", OnEnUs);
        ButtonClick.dispatcher.AddListener("zh_tw", OnZhTw);
        ButtonClick.dispatcher.AddListener("ja_jp", OnJaJp);
        ButtonClick.dispatcher.AddListener("ko_kr", OnKoKr);
        ButtonClick.dispatcher.AddListener("de_de", OnDeDe);
        ButtonClick.dispatcher.AddListener("es_la", OnEsLa);
        GameVO.Instance.language.AddListener(lib.Event.CHANGE, OnChange);

        ButtonClick.dispatcher.AddListener("music", OnClickMusic);
        ButtonClick.dispatcher.AddListener("musicPlus", OnClickMusicPlus);
        ButtonClick.dispatcher.AddListener("musicReduce", OnClickMusicReduce);
        GameVO.Instance.musicEditor.AddListener(lib.Event.CHANGE, OnMusicEditorChange);
        GameVO.Instance.musicVolumn.AddListener(lib.Event.CHANGE, OnMusicVolumnChange);

        ButtonClick.dispatcher.AddListener("sound", OnClickSound);
        ButtonClick.dispatcher.AddListener("soundPlus", OnClickSoundPlus);
        ButtonClick.dispatcher.AddListener("soundReduce", OnClickSoundReduce);
        GameVO.Instance.soundEditor.AddListener(lib.Event.CHANGE, OnSoundEditorChange);
        GameVO.Instance.soundVolumn.AddListener(lib.Event.CHANGE, OnSoundVolumnChange);
    }

    private void OnClickMusicPlus(lib.Event e)
    {
        GameVO.Instance.musicVolumn.value += 10;
        if(GameVO.Instance.musicVolumn.value > 100)
        {
            GameVO.Instance.musicVolumn.value = 100;
        }
    }

    private void OnClickMusicReduce(lib.Event e)
    {
        GameVO.Instance.musicVolumn.value -= 10;
        if (GameVO.Instance.musicVolumn.value < 0)
        {
            GameVO.Instance.musicVolumn.value = 0;
        }
    }

    private void OnMusicEditorChange(lib.Event e = null)
    {
        if ((bool)GameVO.Instance.musicEditor.value == true)
        {
            GameVO.Instance.soundEditor.value = false;
            musicVolumn.SetActive(true);
            musicBg.gameObject.GetComponent<RectTransform>().DORotate(new Vector3(0, 0, 30), 0.2f);
            GameObjectUtils.DisableComponentAllChildren<Shadow>(musicIcon.gameObject.GetComponent<RectTransform>().parent.gameObject);
        }
        else
        {
            musicVolumn.SetActive(false);
            musicBg.gameObject.GetComponent<RectTransform>().DORotate(new Vector3(0, 0, 0), 0.2f);
            GameObjectUtils.EnableComponentAllChildren<Shadow>(musicIcon.gameObject.GetComponent<RectTransform>().parent.gameObject);
        }
    }

    private void OnMusicVolumnChange(lib.Event e = null)
    {
        musicVolumnTxt.text = "" + GameVO.Instance.musicVolumn.value;
        musicProgress.fillAmount = GameVO.Instance.musicVolumn.value / 100.0f;
        musicProgressLess.fillAmount = 1 - GameVO.Instance.musicVolumn.value / 100.0f;
        if (GameVO.Instance.musicVolumn.value == 0)
        {
            musicIcon.gameObject.SetActive(false);
            musicIcon2.gameObject.SetActive(true);
        }
        else
        {
            musicIcon.gameObject.SetActive(true);
            musicIcon2.gameObject.SetActive(false);
        }
    }

    private void OnClickMusic(lib.Event e)
    {
        GameVO.Instance.musicEditor.value = !(bool)GameVO.Instance.musicEditor.value;
    }

    private void OnClickSoundPlus(lib.Event e)
    {
        GameVO.Instance.soundVolumn.value += 10;
        if (GameVO.Instance.soundVolumn.value > 100)
        {
            GameVO.Instance.soundVolumn.value = 100;
        }
    }

    private void OnClickSoundReduce(lib.Event e)
    {
        GameVO.Instance.soundVolumn.value -= 10;
        if (GameVO.Instance.soundVolumn.value < 0)
        {
            GameVO.Instance.soundVolumn.value = 0;
        }
    }

    private void OnSoundEditorChange(lib.Event e = null)
    {
        if ((bool)GameVO.Instance.soundEditor.value == true)
        {
            GameVO.Instance.musicEditor.value = false;
            soundVolumn.SetActive(true);
            soundBg.gameObject.GetComponent<RectTransform>().DORotate(new Vector3(0, 0, 30), 0.2f);
            GameObjectUtils.DisableComponentAllChildren<Shadow>(soundIcon.gameObject.GetComponent<RectTransform>().parent.gameObject);
        }
        else
        {
            soundVolumn.SetActive(false);
            soundBg.gameObject.GetComponent<RectTransform>().DORotate(new Vector3(0, 0, 0), 0.2f);
            GameObjectUtils.EnableComponentAllChildren<Shadow>(soundIcon.gameObject.GetComponent<RectTransform>().parent.gameObject);
        }
    }

    private void OnSoundVolumnChange(lib.Event e = null)
    {
        soundVolumnTxt.text = "" + GameVO.Instance.soundVolumn.value;
        soundProgress.fillAmount = GameVO.Instance.soundVolumn.value / 100.0f;
        soundProgressLess.fillAmount = 1 - GameVO.Instance.soundVolumn.value / 100.0f;
        if (GameVO.Instance.soundVolumn.value == 0)
        {
            soundIcon.gameObject.SetActive(false);
            soundIcon2.gameObject.SetActive(true);
        }
        else
        {
            soundIcon.gameObject.SetActive(true);
            soundIcon2.gameObject.SetActive(false);
        }
    }

    private void OnClickSound(lib.Event e)
    {
        GameVO.Instance.soundEditor.value = !(bool)GameVO.Instance.soundEditor.value;
    }

    private void OnQuit(lib.Event e)
    {
        GameVO.Instance.musicEditor.value = false;
        GameVO.Instance.soundEditor.value = false;
        GameVO.Instance.ShowModule(ModuleName.Main);
    }

    private void OnEsLa(lib.Event e)
    {
        GameVO.Instance.musicEditor.value = false;
        GameVO.Instance.soundEditor.value = false;
        GameVO.Instance.language.value = LanguageTypeConfig.GetConfigWidth("name", "es_la").id;
    }

    private void OnDeDe(lib.Event e)
    {
        GameVO.Instance.musicEditor.value = false;
        GameVO.Instance.soundEditor.value = false;
        GameVO.Instance.language.value = LanguageTypeConfig.GetConfigWidth("name", "de_de").id;
    }

    private void OnKoKr(lib.Event e)
    {
        GameVO.Instance.musicEditor.value = false;
        GameVO.Instance.soundEditor.value = false;
        GameVO.Instance.language.value = LanguageTypeConfig.GetConfigWidth("name", "ko_kr").id;
    }

    private void OnJaJp(lib.Event e)
    {
        GameVO.Instance.musicEditor.value = false;
        GameVO.Instance.soundEditor.value = false;
        GameVO.Instance.language.value = LanguageTypeConfig.GetConfigWidth("name", "ja_jp").id;
    }

    private void OnZhTw(lib.Event e)
    {
        GameVO.Instance.musicEditor.value = false;
        GameVO.Instance.soundEditor.value = false;
        GameVO.Instance.language.value = LanguageTypeConfig.GetConfigWidth("name", "zh_tw").id;
    }

    private void OnEnUs(lib.Event e)
    {
        GameVO.Instance.musicEditor.value = false;
        GameVO.Instance.soundEditor.value = false;
        GameVO.Instance.language.value = LanguageTypeConfig.GetConfigWidth("name", "en_us").id;
    }

    private void OnZhCn(lib.Event e)
    {
        GameVO.Instance.musicEditor.value = false;
        GameVO.Instance.soundEditor.value = false;
        GameVO.Instance.language.value = LanguageTypeConfig.GetConfigWidth("name", "zh_cn").id;
    }

    private void OnEnable()
    {
        GameVO.Instance.musicEditor.value = false;
        GameVO.Instance.soundEditor.value = false;
        OnChange();
        OnMusicEditorChange();
        OnMusicVolumnChange();
        OnSoundEditorChange();
        OnSoundVolumnChange();
    }

    private void OnChange(lib.Event e = null)
    {
        Select(zh_cn, false);
        Select(en_us, false);
        Select(zh_tw, false);
        Select(ja_jp, false);
        Select(ko_kr, false);
        Select(de_de, false);
        Select(es_la, false);
        switch (LanguageTypeConfig.GetConfig(GameVO.Instance.language.value).name)
        {
            case "zh_cn":
                Select(zh_cn,true);
                break;
            case "en_us":
                Select(en_us, true);
                break;
            case "zh_tw":
                Select(zh_tw, true);
                break;
            case "ja_jp":
                Select(ja_jp, true);
                break;
            case "ko_kr":
                Select(ko_kr, true);
                break;
            case "de_de":
                Select(de_de, true);
                break;
            case "es_la":
                Select(es_la, true);
                break;
        }
    }

    private void Select(Image obj,bool flag)
    {
        if(flag)
        {
            GameObjectUtils.DisableComponentAllChildren<Shadow>(obj.gameObject.GetComponent<RectTransform>().parent.gameObject);
            obj.gameObject.GetComponent<RectTransform>().DORotate(new Vector3(0, 0, 30), 0.2f);
            obj.DOColor(new Color((float)(246.0 / 255.0), (float)(120.0 / 255.0), (float)(119.0 / 255.0)), 0.2f);
        }
        else
        {
            GameObjectUtils.EnableComponentAllChildren<Shadow>(obj.gameObject.GetComponent<RectTransform>().parent.gameObject);
            if (obj.gameObject.GetComponent<RectTransform>().localEulerAngles.z != 0)
            {
                obj.gameObject.GetComponent<RectTransform>().DORotate(new Vector3(0, 0, 0), 0.2f);
            }
            obj.DOColor(new Color((float)(254.0 / 255.0), (float)(250.0 / 255.0), (float)(203.0 / 255.0)), 0.2f);
        }
    }
}
