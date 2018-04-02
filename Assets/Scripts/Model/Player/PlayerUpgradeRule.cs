using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgradeRule
{
    private static PlayerUpgradeRule _Instance;
    public static Dictionary<int, int> _DicLevel = new Dictionary<int, int>();
    private int basisLevelExp = 100;

    public PlayerUpgradeRule()
    {
        InitLevelRule();
    }

    public static PlayerUpgradeRule GetInstance()
    {
        if (_Instance == null)
        {
            _Instance = new PlayerUpgradeRule();
        }

        return _Instance;
    }

    /// <summary>
    /// 升级规则
    /// </summary>
    /// <param name="exp">传入的经验</param>
    public void UpgradeConition(int exp)
    {
        int currentLevel = 0; //当前等级
        int currentExp = 0; //当前经验
        int levelNextUpExp = 0; //下一级提升等级所需的经验
        int levelExp; //当前等级满级经验
        int levelNextExp; //下一等级满级经验
        int[] levelNextsUpExp = new int[_DicLevel.Count - currentLevel]; //存储剩下每级提升所需的经验
        currentExp = PlayerExtenalDataProxy.GetInstance().GetExp() + exp; //当前经验
        currentLevel = PlayerExtenalDataProxy.GetInstance().GetLevel(); //当前等级
        _DicLevel.TryGetValue(currentLevel, out levelExp);
        _DicLevel.TryGetValue(currentLevel + 1, out levelNextExp);


        levelNextUpExp = levelNextExp - levelExp; //下一等级提升所需经验
        if (currentExp >= levelExp) //当前获得经验大于该等级提升所需的经验
        {
            int cumulativeExperience = 0; //经验累加
            if (currentExp - Mathf.Abs(levelNextExp) >= levelNextUpExp) //如果多余的经验大于下一级升级所需的经验
            {
                for (int i = 0; i < _DicLevel.Count - currentLevel; i++) //存储剩下每级提升所需的经验
                {
                    int currentLevelExp = 0; //当前等级所需的经验
                    int nextLevelExp = 0; //下一等级所需的经验
                    _DicLevel.TryGetValue(currentLevel + i, out currentLevelExp);
                    _DicLevel.TryGetValue(currentLevel + i + 1, out nextLevelExp);
                    levelNextsUpExp[i] = nextLevelExp - currentLevelExp;
                }

                for (int i = 0; i < levelNextsUpExp.Length; i++)
                {
                    if (i == 0)
                    {
                        cumulativeExperience = levelNextsUpExp[i];
                    }
                    else
                    {
                        cumulativeExperience += levelNextsUpExp[i] - levelNextsUpExp[i - 1]; //该等级加上上一个等级所需的经验
                    }

                    if (currentExp - cumulativeExperience >= levelNextsUpExp[i]) //剩余的经验大于下一集升级所需的经验
                    {
                        currentExp -= cumulativeExperience;
                        PlayerExtenalDataProxy.GetInstance().AddLevel();
                    }
                }

                PlayerExtenalDataProxy.GetInstance().AddExp(exp);
            }
            else
            {
                PlayerExtenalDataProxy.GetInstance().AddLevel();
                PlayerExtenalDataProxy.GetInstance().AddExp(exp);
            }
        }
        else
        {
            PlayerExtenalDataProxy.GetInstance().AddExp(exp);
        }
    }

    /// <summary>
    /// 等级规则
    /// </summary>
    private void InitLevelRule()
    {
        for (int i = 0; i <= 60; i++)
        {
            if (_DicLevel.Count == 0)
            {
                _DicLevel.Add(0, basisLevelExp);
            }

            if (i >= 1 && i <= 10)
            {
                _DicLevel.Add(i, _DicLevel[i - 1] + basisLevelExp * i);
            }
            else if (i > 10 && i <= 20)
            {
                _DicLevel.Add(i, _DicLevel[i - 1] + +basisLevelExp * i * 2);
            }
            else if (i > 20 && i <= 30)
            {
                _DicLevel.Add(i, _DicLevel[i - 1] + basisLevelExp * i * 3);
            }
            else if (i > 30 && i <= 40)
            {
                _DicLevel.Add(i, _DicLevel[i - 1] + basisLevelExp * i * 4);
            }
            else if (i > 40 && i <= 50)
            {
                _DicLevel.Add(i, _DicLevel[i - 1] + basisLevelExp * i * 5);
            }
        }
    }

    /// <summary>
    /// 升级奖励:属性提升10%,状态全满
    /// </summary>
    public void UpgradeOperation()
    {

         PlayerKernalDataProxy.GetInstance().MaxDefence += PlayerKernalDataProxy.GetInstance().MaxDefence /10;
         PlayerKernalDataProxy.GetInstance().MaxHealth += PlayerKernalDataProxy.GetInstance().MaxHealth / 10;
         PlayerKernalDataProxy.GetInstance().MaxMagic += PlayerKernalDataProxy.GetInstance().MaxMagic / 10;
         PlayerKernalDataProxy.GetInstance().MaxAttack += PlayerKernalDataProxy.GetInstance().MaxAttack /10;
         PlayerKernalDataProxy.GetInstance().MaxDexterity += PlayerKernalDataProxy.GetInstance().MaxDexterity / 10;
         PlayerKernalDataProxy.GetInstance().Health = PlayerKernalDataProxy.GetInstance().MaxHealth;
         PlayerKernalDataProxy.GetInstance().Magic = PlayerKernalDataProxy.GetInstance().MaxMagic;
    }
}