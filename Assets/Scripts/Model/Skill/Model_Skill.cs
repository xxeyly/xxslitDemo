using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model_Skill
{
    public int skillId; //技能ID
    public string skillName; //技能名称
    public int skillLockLv; //技能解锁等级
    public int skillCurrentLv; //当前技能等级
    public int skillMaxLv; //当前技能等级
    public int skillConsumption; //技能消耗
    public float skillCurrentCD; //当前技能CD
    public int skillCD; //技能冷却时间
    public SkillType skillType; //技能类型
    public SkillBuff skillBuff; //BUFF类型;
    public CasterType casterType; //魔法类型
    public string casterBullet; //魔法子弹
    public string skillSprite; //技能图片
    public int skillRange; //技能范围
    public int skillIncludedAngle; //技能攻击的角度
    public int[][] skillDmage; //技能伤害
    public int skillCurrentProficiency; //当前熟练度
    public int[] skillProficiency; //技能熟练度
    public string skillDescription; //技能描述
    public int skillStrikeDistance; //击飞距离

    public enum CasterType
    {
        None, //无属性
        Jin, //金
        Mu, //木
        water, //水
        fire, //火
        soil, //土
    }

    public enum SkillType
    {
        Melee, //近战
        Caster, //施法
        State //状态--BUFF
    }

    public enum SkillBuff
    {
        None,
        Destruction, //破坏
        Gain //增益
    }

    public override string ToString()
    {
        string tip = "";
        tip += string.Format("技能:{0}\n解锁等级:{1}\n等级:{2}/{3}\n技能消耗:{4}\n冷却时间:{5}\n熟练度{6}/{7}\n", skillName,
            skillLockLv, skillCurrentLv, skillMaxLv, skillConsumption, skillCD, skillCurrentProficiency,
            skillProficiency[skillCurrentLv - 1]);
        switch (skillType)
        {
            case SkillType.Melee:
                tip += "技能类型:" + "近战\n";
                break;
            case SkillType.Caster:
                tip += "技能类型:" + "魔法\n";
                break;
            case SkillType.State:
                tip += "技能类型:" + "增益/辅助\n";
                break;
        }

        switch (casterType)
        {
            case CasterType.None:
                break;
            case CasterType.Jin:
                tip += "魔法类型:" + "金\n";
                break;
            case CasterType.Mu:
                tip += "魔法类型:" + "木\n";
                break;
            case CasterType.water:
                tip += "魔法类型:" + "水\n";
                break;
            case CasterType.fire:
                tip += "魔法类型:" + "火\n";
                break;
            case CasterType.soil:
                tip += "魔法类型:" + "土\n";
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        switch (skillBuff)
        {
            case SkillBuff.None:
                break;
            case SkillBuff.Destruction:
                tip += "魔法类型:" + "减益\n";
                break;
            case SkillBuff.Gain:
                tip += "魔法类型:" + "增益\n";
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        for (int i = 0; i < skillDmage[skillCurrentLv - 1].Length; i++)
        {
            tip += "当前等级伤害" + (i + 1) + "段" + skillDmage[skillCurrentLv - 1][i] + "\n";
        }

        tip += skillDescription;
        return tip;
    }
}