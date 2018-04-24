using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 主角攻击脚本
/// </summary>
public class Ctrl_HeroAttack : MonoBehaviour
{
    [SerializeField] private List<GameObject> _ListEnmeys; //敌人集合
    [SerializeField] private float _FloMaxDistance = 5; //最大距离
    private Transform _TraNearestEnemy; //最近敌人方位
    private float _FloMinDistance = 3; //最小距离
    private float FloRealAttackArea = 2; //攻击距离
    public View_ActionBarSlot[] cdgame;
    private PlayerSkillData playerSkillData;

    void Start()
    {
        StartCoroutine(RecordNearbyEnemyToArray());
        playerSkillData = new PlayerSkillData();
    }

    private void Awake()
    {
    }

    /// <summary>
    /// 释放技能
    /// </summary>
    /// <param name="skill"></param>
    public void ReleaseSkills(Ctrl_ActionBarSlot slot)
    {
        //技能冷却好了
        if (slot.Skill.skillCurrentCD >= slot.Skill.skillCD)
        {
            //技能消耗
            if (Ctrl_HeroProperty.Instance.GetCurrentMagic() >= slot.Skill.skillConsumption)
            {
                //减去技能消耗
                Ctrl_HeroProperty.Instance.DecreaseMagicValues(slot.Skill.skillConsumption);
                //释放技能
                GetComponent<Ctrl_PlayerAminator>().ReleaseSkill(slot.Skill.skillId);
                //技能熟练度+1
                if (slot.Skill.skillCurrentLv < slot.Skill.skillMaxLv)
                {
                    slot.Skill.skillCurrentProficiency += 1;
                }
                else
                {
                    if (slot.Skill.skillCurrentProficiency < slot.Skill.skillProficiency[slot.Skill.skillCurrentLv - 1])
                    {
                        slot.Skill.skillCurrentProficiency += 1;
                    }
                }

                //冷却重置
                slot.IsCooding = true;
            }
        }
    }

    //攻击动画
    public void ResponseAttack(string ControlType)
    {
        /*switch (ControlType)
        {
            // 响应普通攻击
            case "NormalAttack":
                //TUDO
                if (cdgame[0].isSkillRead)
                {
                    //技能消耗
                    Ctrl_HeroProperty.Instance.DecreaseMagicValues(PlayerSkillData.Instance.SkillConsume(0));
                    //播放动画
                    Ctrl_PlayerAminator.Instance.NormalAttack();
                    //特定敌人伤害处理
                    AttackEnemy(PlayerSkillData.Instance.SkillAttack(0));
                    cdgame[0].ResponseBtnClick();
                }

                break;
            // 响应技能1
            case "MagicTrickA":
                if (cdgame[1].isSkillRead)
                {
                    if (Ctrl_HeroProperty.Instance.GetCurrentMagic() >=
                        PlayerSkillData.Instance.SkillConsume(1))
                    {
                        //技能消耗
                        Ctrl_HeroProperty.Instance.DecreaseMagicValues(PlayerSkillData.Instance.SkillConsume(1));

                        //播放动画
                        Ctrl_PlayerAminator.Instance.Skill1();
                        //特定敌人伤害处理
                        AttackEnemy(PlayerSkillData.Instance.SkillAttack(1));
                        cdgame[1].ResponseBtnClick();
                    }
                    else
                    {
                        Ctrl_TootipManager.Instance.ShowNotification("魔法不足!", GlobalParametr.SKILLSHOWTIME);
                    }
                }
                else
                {
                    Ctrl_TootipManager.Instance.ShowNotification("技能还没准备好!", GlobalParametr.SKILLSHOWTIME);
                }

                break;

            //响应技能2
            case "MagicTrickB":
                if (cdgame[2].isSkillRead)
                {
                    if (Ctrl_HeroProperty.Instance.GetCurrentMagic() >=
                        PlayerSkillData.Instance.SkillConsume(2))
                    {
                        //技能消耗
                        Ctrl_HeroProperty.Instance.DecreaseMagicValues(PlayerSkillData.Instance.SkillConsume(2));

                        //播放动画
                        Ctrl_PlayerAminator.Instance.Skill2();
                        //特定敌人伤害处理
                        AttackEnemy(PlayerSkillData.Instance.SkillAttack(2));
                        cdgame[2].ResponseBtnClick();
                    }
                    else
                    {
                        Ctrl_TootipManager.Instance.ShowNotification("魔法不足!", GlobalParametr.SKILLSHOWTIME);
                    }
                }
                else
                {
                    Ctrl_TootipManager.Instance.ShowNotification("技能还没准备好!", GlobalParametr.SKILLSHOWTIME);
                }

                break;
            //响应技能3
            case "MagicTrickC":
                if (cdgame[3].isSkillRead)
                {
                    if (Ctrl_HeroProperty.Instance.GetCurrentMagic() >=
                        PlayerSkillData.Instance.SkillConsume(3))
                    {
                        //技能消耗
                        Ctrl_HeroProperty.Instance.DecreaseMagicValues(PlayerSkillData.Instance.SkillConsume(3));

                        //播放动画
                        Ctrl_PlayerAminator.Instance.Skill3();
                        //特定敌人伤害处理
                        AttackEnemy(PlayerSkillData.Instance.SkillAttack(3));
                        cdgame[3].ResponseBtnClick();
                    }
                    else
                    {
                        Ctrl_TootipManager.Instance.ShowNotification("魔法不足!", GlobalParametr.SKILLSHOWTIME);
                    }
                }
                else
                {
                    Ctrl_TootipManager.Instance.ShowNotification("技能还没准备好!", GlobalParametr.SKILLSHOWTIME);
                }

                break;
        }*/
    }

    //把附近所有敌人放入数组
    IEnumerator RecordNearbyEnemyToArray()
    {
        while (true)
        {
            yield return new WaitForSeconds(1F);
            //得到所有敌人,放入敌人集合
            GetEnemyToArray();
            //判断敌人集合,找出最近的敌人
//            GetNearestEnemy();
        }
    }

    //得到所有敌人,放入敌人集合
    public void GetEnemyToArray()
    {
        //集合类型初始化
        _ListEnmeys = new List<GameObject>();
        GameObject[] GoEnemys = GameObject.FindGameObjectsWithTag(GlobalParametr.TAG_ENEMY);
        foreach (GameObject enemy in GoEnemys)
        {
            //判断敌人是否存活
            if (enemy.GetComponent<Ctrl_Enemy_Property>().CurrentState != GlobalParametr.SimplyEnemyState.Death &&
                Vector3.Distance(this.transform.position, enemy.transform.position) <=
                _FloMaxDistance)
            {
                _ListEnmeys.Add(enemy);
            }
        }
    }

    /// <summary>
    /// 攻击距离,夹角范围
    /// </summary>
    /// <param name="_AttackDistance">增加的攻击距离</param>
    /// <param name="floDirection">攻击范围</param>
    public void AttackEnemy(string attackInfo)
    {
        string[] attack = attackInfo.Split(',');
        //获得技能信息
        Model_Skill skill = Ctrl_SkillManager.Instance.SkillList[Int32.Parse(attack[0])];
        //如果技能类型是近战,查找附近的敌人,并攻击
        if (skill.skillType == Model_Skill.SkillType.Melee)
        {
            foreach (GameObject enemy in _ListEnmeys)
            {
                //获得附近,
                if (Vector3.Distance(this.transform.position, enemy.transform.position) <=
                    skill.skillRange)
                {
                    //敌我方向(主角是否面对)
                    Vector3 dir = (enemy.transform.position - transform.position).normalized;
                    //敌我的夹角
                    float floDirection = Vector3.Dot(dir, transform.forward);
                    if (floDirection > 1 - ((float) skill.skillIncludedAngle / 10 * 0.2f))
                    {
                        enemy.GetComponent<Ctrl_Enemy_Property>().OnHurt(
                            skill.skillDmage[skill.skillCurrentLv - 1][
                                Int32.Parse(attack[1])] + Ctrl_HeroProperty.Instance.GetCurrentATK(),
                            skill.skillStrikeDistance);

                        /*enemy.GetComponent<EnemyHurt>().PlayGetHit();
                        enemy.GetComponent<Enemyattribute>()
                            .BloodLoss(Model_Skill.SkillType.Melee, skill.InjuryStaircase[Int32.Parse(attack[1])]);*/
                    }
                }
            }
        }
    }
}