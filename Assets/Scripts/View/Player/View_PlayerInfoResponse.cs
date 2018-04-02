using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View_PlayerInfoResponse : MonoBehaviour
{
    public GameObject goPlayerDetailInfoPanel; //玩家详细界面面板

    /// <summary>
    /// 显示隐藏玩家详细面板
    /// </summary>
    public void DisplayOrHidePlayerDetailInfoPanel()
    {
        goPlayerDetailInfoPanel.SetActive(!goPlayerDetailInfoPanel.activeSelf);
    }

    #region 玩家点击事件

    /*public void ResponseNormalAtk()
    {
        Ctrl_HeroAttackInputByKey.Instance.NormalAttack();
    }

    public void ResponseAtkByMagicA()
    {
        Control_HeroAttackInputByKey.Instance.MagicTrickA();
    }

    public void ResponseAtkByMagicB()
    {
        Control_HeroAttackInputByKey.Instance.MagicTrickB();
    }

    public void ResponseAtkByMagicC()
    {
        Control_HeroAttackInputByKey.Instance.MagicTrickC();
    }*/

    #endregion
}