using System;
using System.Collections;
using System.Collections.Generic;
using DuloGames.UI;
using UnityEngine;
using UnityEngine.UI;

public class View_DisplayPlayerInfo : MonoBehaviour
{
    //屏幕上的信息显示
    public Text TextCurrentLevelByScreen; //当前等级
    public Text TextCurHpByScreen; //当前生命值
    public Text TextMaxHpByScreen; //最大生命值
    public Text TextCurMpByScreen; //当前魔法值
    public Text TextMaxMpByScreen; //最大魔法值
    public Text TextExpByScreen; //经验数值
    public Text TextGoldByScreen; //金币
    public UIProgressBar SliderCyrrentHp; //
    public UIProgressBar SliderCyrrentMp; //

    public Text TextDiamondsByScreen; //钻石
    //玩家详细信息

    public Text TextCurHp; //当前生命值
    public Text TextMaxHp; //最大生命值
    public Text TextCurMp; //当前魔法值
    public Text TextMaxMp; //最大魔法值
    public Text TextCurATK; //当前攻击力
    public Text TextMaxATK; //最大攻击力
    public Text TextCurDEF; //当前防御
    public Text TextMaxDEF; //最大防御
    public Text TextDex; //速度
    public Text TextMaxDex; //速度
    public Text TextLevel; //当前等级
    public Text TextKillNum; //击杀数量
    public Text TextExp; //当前经验
    public Text TextLevelExp; //等级经验
    public Text TextGold; //当前金币
    public Text TextDiamonds; //当前钻石

    private void Awake()
    {
        //核心数值事件注册
        PlayerKernalData.evePlayerKernalData += DisPlayerInfo;
        PlayerExtenalData.evePlayerExtenalData += DisPlayerInfo;
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.3f);
        /*PlayerKernalDataProxy playerKernalData =
            new PlayerKernalDataProxy(100, 100, 100, 20, 5, 100, 100, 1000, 1000, 20, 0, 0, 0);
        PlayerExtenalDataProxy playerExtenalData = new PlayerExtenalDataProxy(0,200,2,1,200,200);*/
        PlayerKernalDataProxy.GetInstance().DiaplayAllOriginaValues();
        PlayerExtenalDataProxy.GetInstance().DisplayAllOriginalValues();
    }

    public void AddExp()
    {
        PlayerExtenalDataProxy.GetInstance().UpgradeConition(200);
    }


    private void DisPlayerInfo(GlobalParametr.KeyValuesUpdate kv)
    {
        switch (kv.Key)
        {
            case "Health":
                TextCurHp.text = kv.Values.ToString();
                TextCurHpByScreen.text = kv.Values.ToString();
                SliderCyrrentHp.fillAmount = (float) PlayerKernalDataProxy.GetInstance().Health /
                                             PlayerKernalDataProxy.GetInstance().MaxHealth;
                break;
            case "MaxHealth":
                TextMaxHp.text = kv.Values.ToString();
                TextMaxHpByScreen.text = kv.Values.ToString();
                SliderCyrrentHp.fillAmount = (float) PlayerKernalDataProxy.GetInstance().Health /
                                             PlayerKernalDataProxy.GetInstance().MaxHealth;
                break;
            case "Magic":
                TextCurMp.text = kv.Values.ToString();
                TextCurMpByScreen.text = kv.Values.ToString();
                SliderCyrrentMp.fillAmount = (float) PlayerKernalDataProxy.GetInstance().Magic /
                                             PlayerKernalDataProxy.GetInstance().MaxMagic;
                break;
            case "MaxMagic":
                TextMaxMp.text = kv.Values.ToString();
                TextMaxMpByScreen.text = kv.Values.ToString();
                SliderCyrrentMp.fillAmount = (float) PlayerKernalDataProxy.GetInstance().Magic /
                                             PlayerKernalDataProxy.GetInstance().MaxMagic;
                break;
            case "Attack":
                TextCurATK.text = kv.Values.ToString();
                break;
            case "MaxAttack":
                TextMaxATK.text = kv.Values.ToString();
                break;
            case "Defence":
                TextCurDEF.text = kv.Values.ToString();

                break;
            case "MaxDefence":
                TextMaxDEF.text = kv.Values.ToString();

                break;
            case "MaxDexterity":
                TextMaxDex.text = kv.Values.ToString();
                break;
            case "Level":
                TextLevel.text = kv.Values.ToString();
                TextCurrentLevelByScreen.text = kv.Values.ToString();
                break;
            case "LevelExperience":
                TextLevelExp.text = kv.Values.ToString();

                break;
            case "Experience":
                TextExp.text = kv.Values.ToString();
                TextExpByScreen.text = kv.Values.ToString();

                break;
            case "KillNumber":
                TextKillNum.text = kv.Values.ToString();

                break;
            case "Gold":
                TextGold.text = kv.Values.ToString();
                TextGoldByScreen.text = kv.Values.ToString();
                break;
            case "Dexterity":
                TextDex.text = kv.Values.ToString();

                break;
            case "Diamonds":
                TextDiamondsByScreen.text = kv.Values.ToString();
                TextDiamonds.text = kv.Values.ToString();
                break;
        }
    }
}