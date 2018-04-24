using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 主角攻击输入键盘控制
/// </summary>
public class Control_HeroAttackInputByKey : Singleton<Control_HeroAttackInputByKey>
{
    //事件:主角控制事件
    public static event GlobalParametr.del_PlayerControlWithStr evePlayerControl;

    // Update is called once per frame
    public View_ActionBarSlot[] cdgame;

    public void NormalAttack()
    {
    /*    if (evePlayerControl)
        {
            evePlayerControl("NormalAttack");
        }*/
    }

    public void MagicTrickA()
    {
        /*if (evePlayerControl != null && cdgame[1].isSkillRead)
        {
            evePlayerControl("MagicTrickA");
        }*/
    }

    public void MagicTrickB()
    {
        /*if (evePlayerControl != null && cdgame[2].isSkillRead)
        {
            evePlayerControl("MagicTrickB");
        }*/
    }

    public void MagicTrickC()
    {
        /*if (evePlayerControl != null && cdgame[3].isSkillRead)
        {
            evePlayerControl("MagicTrickC");
        }*/
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            NormalAttack();

//            cdgame[0].ResponseBtnClick();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            MagicTrickA();

//            cdgame[1].ResponseBtnClick();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            MagicTrickB();

//            cdgame[2].ResponseBtnClick();
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            MagicTrickC();

//            cdgame[3].ResponseBtnClick();
        }
    }
}