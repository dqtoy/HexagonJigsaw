using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using lib;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class DailyUIFade : UIFade
{
    public RectTransform line1;
    public RectTransform line2;
    public RectTransform hex;
    public RectTransform daily;
    public RectTransform daily2;
    public RectTransform quit;
    public RectTransform buttons;

    public RectTransform level1;
    public RectTransform level2;
    public RectTransform level3;
    public RectTransform level4;
    public RectTransform level5;
    public RectTransform level6;
    public RectTransform level7;
    public RectTransform level8;
    public RectTransform level9;
    public RectTransform level10;

    public GameObject level6Txt;
    public GameObject level9Txt;
    public GameObject level10Txt;
    public GameObject effect6;
    public GameObject effect9;
    public GameObject effect10;
    public GameObject effectAllPass;

    [HideInInspector]
    public static int dailyIndex;

    private ModuleName moduleName;

    override public void FadeOut(ModuleName name)
    {
        moduleName = name;
        if (name == ModuleName.Main)
        {
            line1.DOScaleX(0, outTime).onComplete = TweenComplete;
            line2.DOScaleX(0, outTime);
            daily2.DOScaleX(0, 0.1f);
            quit.DOScaleX(0, 0.1f);
            //buttons.DOScale(new Vector3(0, 0),outTime);
        }
        else if(name == ModuleName.Game)
        {
            daily2.DOScaleX(0, 0.1f);
            quit.DOScaleX(0, 0.1f).onComplete = FadeOut2;
        }
    }

    private void FadeOut2()
    {
        if(moduleName == ModuleName.Game)
        {
            line1.DOScaleX(0, 0.1f);
            line2.DOScaleX(0, 0.1f).onComplete = FadeOut3;
        }
    }

    private void FadeOut3()
    {
        if (moduleName == ModuleName.Game)
        {
            hex.DOLocalMove(new Vector3(-670, 950), 0.1f);
            hex.DOLocalRotate(new Vector3(0, 0, 280), 0.1f).onComplete = FadeOut4;
        }
    }

    private void FadeOut4()
    {
        if(dailyIndex != 0)
        {
            level1.DOScaleX(0,0.1f).onComplete = FadeOut5;
        }
        if (dailyIndex != 1)
        {
            if(dailyIndex == 0)
            {
                level2.DOScaleX(0, 0.1f).onComplete = FadeOut5;
            }
            else
            {
                level2.DOScaleX(0, 0.1f);
            }
        }
        if (dailyIndex != 2)
        {
            level3.DOScaleX(0, 0.1f);
        }
        if (dailyIndex != 3)
        {
            level4.DOScaleX(0, 0.1f);
        }
        if (dailyIndex != 4)
        {
            level5.DOScaleX(0, 0.1f);
        }
        if (dailyIndex != 5)
        {
            level6.DOScaleX(0, 0.1f);
        }
        if (dailyIndex != 6)
        {
            level7.DOScaleX(0, 0.1f);
        }
        if (dailyIndex != 7)
        {
            level8.DOScaleX(0, 0.1f);
        }
        if (dailyIndex != 8)
        {
            level9.DOScaleX(0, 0.1f);
        }
        if (dailyIndex != 9)
        {
            level10.DOScaleX(0, 0.1f);
        }
    }

    private void FadeOut5()
    {
        if (dailyIndex == 0)
        {
            level1.DOLocalMoveX(-33 - 500, 0.1f).onComplete = TweenComplete;
        }
        if (dailyIndex == 1)
        {
            level2.DOLocalMoveX(-116 - 500, 0.1f).onComplete = TweenComplete;
        }
        if (dailyIndex == 2)
        {
            level3.DOLocalMoveX(-33 - 500, 0.1f).onComplete = TweenComplete;
        }
        if (dailyIndex == 3)
        {
            level4.DOLocalMoveX(-116 - 500, 0.1f).onComplete = TweenComplete;
        }
        if (dailyIndex == 4)
        {
            level5.DOLocalMoveX(50 - 500, 0.1f).onComplete = TweenComplete;
        }
        if (dailyIndex == 5)
        {
            level6.DOLocalMoveX(-33 - 500, 0.1f).onComplete = TweenComplete;
        }
        if (dailyIndex == 6)
        {
            level7.DOLocalMoveX(-33 - 500, 0.1f).onComplete = TweenComplete;
        }
        if (dailyIndex == 7)
        {
            level8.DOLocalMoveX(-116 - 500, 0.1f).onComplete = TweenComplete;
        }
        if (dailyIndex == 8)
        {
            level9.DOLocalMoveX(-116 - 500, 0.1f).onComplete = TweenComplete;
        }
        if (dailyIndex == 9)
        {
            level10.DOLocalMoveX(200 + 400, 0.1f).onComplete = TweenComplete;
        }
    }

    private void TweenComplete()
    {
        dispatcher.DispatchWith(lib.Event.COMPLETE);
    }

    override public void FadeIn(ModuleName name)
    {
        if(GameVO.Instance.daily.levels[5].pass == false)
        {
            effect6.SetActive(false);
        }
        else if (GameVO.Instance.daily.levels[5].pass && GameVO.Instance.daily.levels[5].hasCheck == true)
        {
            level6Txt.SetActive(false);
            effect6.SetActive(true);
        }
        if (GameVO.Instance.daily.levels[8].pass == false)
        {
            effect9.SetActive(false);
        }
        else if (GameVO.Instance.daily.levels[8].pass && GameVO.Instance.daily.levels[8].hasCheck == true)
        {
            level9Txt.SetActive(false);
            effect9.SetActive(true);
        }
        if (GameVO.Instance.daily.levels[9].pass == false)
        {
            effect10.SetActive(false);
        }
        else if (GameVO.Instance.daily.levels[9].pass && GameVO.Instance.daily.levels[9].hasCheck == true)
        {
            level10Txt.SetActive(false);
            effect10.SetActive(true);
        }
        if(GameVO.Instance.daily.HasAllPass() == false)
        {
            effectAllPass.SetActive(false);
        }
        else if(GameVO.Instance.daily.HasAllPass() == true && GameVO.Instance.daily.checkAll == true)
        {
            effectAllPass.SetActive(true);
        }

        moduleName = name;
        if(name == ModuleName.Main)
        {
            line1.localScale = new Vector3(0, 1);
            line2.localScale = new Vector3(0, 1);
            daily2.localScale = new Vector3(0, 1);
            quit.localScale = new Vector3(0, 1);
            //buttons.localScale = new Vector3(0, 0);

            line1.DOScaleX(1, inTime);
            line2.DOScaleX(1, inTime).onComplete = CheckPassEffect;
            daily2.DOScaleX(1, 0.1f);
            quit.DOScaleX(1, 0.1f);

            //buttons.DOScale(new Vector3(1, 1),inTime);
        }
        else if(name == ModuleName.Game || name == ModuleName.Result)
        {
            if (dailyIndex == 0)
            {
                level1.DOLocalMoveX(-33, 0.2f).onComplete = FadeIn2;
            }
            if (dailyIndex == 1)
            {
                level2.DOLocalMoveX(-116, 0.2f).onComplete = FadeIn2;
            }
            if (dailyIndex == 2)
            {
                level3.DOLocalMoveX(-33, 0.2f).onComplete = FadeIn2;
            }
            if (dailyIndex == 3)
            {
                level4.DOLocalMoveX(-116, 0.2f).onComplete = FadeIn2;
            }
            if (dailyIndex == 4)
            {
                level5.DOLocalMoveX(50, 0.2f).onComplete = FadeIn2;
            }
            if (dailyIndex == 5)
            {
                level6.DOLocalMoveX(-33, 0.2f).onComplete = FadeIn2;
            }
            if (dailyIndex == 6)
            {
                level7.DOLocalMoveX(-33, 0.2f).onComplete = FadeIn2;
            }
            if (dailyIndex == 7)
            {
                level8.DOLocalMoveX(-116, 0.2f).onComplete = FadeIn2;
            }
            if (dailyIndex == 8)
            {
                level9.DOLocalMoveX(-116, 0.2f).onComplete = FadeIn2;
            }
            if (dailyIndex == 9)
            {
                level10.DOLocalMoveX(200, 0.2f).onComplete = FadeIn2;
            }
        }
    }

    private void FadeIn2()
    {
        if (moduleName == ModuleName.Game || moduleName == ModuleName.Result)
        {
            if (dailyIndex != 0)
            {
                level1.DOScaleX(1, 0.2f).onComplete = FadeIn3;
            }
            if (dailyIndex != 1)
            {
                if (dailyIndex == 0)
                {
                    level2.DOScaleX(1, 0.2f).onComplete = FadeIn3;
                }
                else
                {
                    level2.DOScaleX(1, 0.2f);
                }
            }
            if (dailyIndex != 2)
            {
                level3.DOScaleX(1, 0.2f);
            }
            if (dailyIndex != 3)
            {
                level4.DOScaleX(1, 0.2f);
            }
            if (dailyIndex != 4)
            {
                level5.DOScaleX(1, 0.2f);
            }
            if (dailyIndex != 5)
            {
                level6.DOScaleX(1, 0.2f);
            }
            if (dailyIndex != 6)
            {
                level7.DOScaleX(1, 0.2f);
            }
            if (dailyIndex != 7)
            {
                level8.DOScaleX(1, 0.2f);
            }
            if (dailyIndex != 8)
            {
                level9.DOScaleX(1, 0.2f);
            }
            if (dailyIndex != 9)
            {
                level10.DOScaleX(1, 0.2f);
            }
        }
    }

    private void FadeIn3()
    {
        if (moduleName == ModuleName.Game || moduleName == ModuleName.Result)
        {
            hex.DOLocalMove(new Vector3(-225, 651), 0.1f);
            hex.DOLocalRotate(new Vector3(0, 0, 0), 0.1f).onComplete = FadeIn4;
        }
    }

    private void FadeIn4()
    {
        if (moduleName == ModuleName.Game || moduleName == ModuleName.Result)
        {
            line1.DOScaleX(1, 0.1f);
            line2.DOScaleX(1, 0.1f).onComplete = FadeIn5;
        }
    }

    private void FadeIn5()
    {
        if (moduleName == ModuleName.Game || moduleName == ModuleName.Result)
        {
            daily2.DOScaleX(1, 0.1f);
            quit.DOScaleX(1, 0.1f).onComplete = CheckPassEffect;
        }
    }

    private void CheckPassEffect()
    {
        if(GameVO.Instance.daily.levels[5].pass && !GameVO.Instance.daily.levels[5].hasCheck)
        {
            GameVO.Instance.daily.levels[5].hasCheck = true;
            level6Txt.SetActive(false);
            effect6.SetActive(true);
            effect6.GetComponent<AnimatorControl>().AddListener(lib.Event.COMPLETE, CheckNextEffect);
            return;
        }
        if (GameVO.Instance.daily.levels[8].pass && !GameVO.Instance.daily.levels[8].hasCheck)
        {
            GameVO.Instance.daily.levels[8].hasCheck = true;
            level9Txt.SetActive(false);
            effect9.SetActive(true);
            effect9.GetComponent<AnimatorControl>().AddListener(lib.Event.COMPLETE, CheckNextEffect);
            return;
        }
        if (GameVO.Instance.daily.levels[9].pass && !GameVO.Instance.daily.levels[9].hasCheck)
        {
            GameVO.Instance.daily.levels[9].hasCheck = true;
            level10Txt.SetActive(false);
            effect10.SetActive(true);
            effect10.GetComponent<AnimatorControl>().AddListener(lib.Event.COMPLETE, CheckNextEffect);
            return;
        }
        if(GameVO.Instance.daily.HasAllPass() == true && GameVO.Instance.daily.checkAll == false)
        {
            effectAllPass.SetActive(true);
        }
    }

    private void CheckNextEffect(lib.Event e)
    {
        CheckPassEffect();
    }
}