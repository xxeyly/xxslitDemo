using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 主角攻击脚本
/// </summary>
public class Ctrl_HeroAttack : MonoBehaviour
{
    [SerializeField] private List<GameObject> _LisEnmeys; //敌人集合
    private Transform _TraNearestEnemy; //最近敌人方位
    [SerializeField] private float _FloMaxDistance = 5; //最大距离
    private float _FloMinDistance = 3; //最小距离
    private float FloRealAttackArea = 2; //攻击距离
    public View_ATKBtnCDEffect[] cdgame;
    private PlayerSkillData playerSkillData;

    void Start()
    {
        StartCoroutine(RecordNearbyEnemyToArray());
        playerSkillData = new PlayerSkillData();
    }

    private void Awake()
    {
        Ctrl_PlayerSkillByKey.evePlayerControl += ResponseAttack;
    }

    //攻击动画
    public void ResponseAttack(string ControlType)
    {
        switch (ControlType)
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
        }
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
        _LisEnmeys = new List<GameObject>();
        GameObject[] GoEnemys = GameObject.FindGameObjectsWithTag(GlobalParametr.TAG_ENEMY);
        foreach (GameObject enemy in GoEnemys)
        {
            //判断敌人是否存活
            if (enemy.GetComponent<Ctrl_Enemy_Property>().CurrentState != GlobalParametr.SimplyEnemyState.Death &&
                Vector3.Distance(this.transform.position, enemy.transform.position) <=
                _FloMaxDistance)
            {
                _LisEnmeys.Add(enemy);
            }
        }
    }

    /* //判断敌人集合,找出最近的敌人
     public void GetNearestEnemy()
     {
         if (_LisEnmeys != null && _LisEnmeys.Count >= 1)
         {
             foreach (GameObject enmey in _LisEnmeys)
             {
                 float floDistance = Vector3.Distance(this.transform.position, enmey.transform.position);
                 if (floDistance <= _FloMaxDistance)
                 {
                     _FloMaxDistance = floDistance;
                     _TraNearestEnemy = enmey.transform; //最近敌人方位
                 }
             }
         }
     }*/

    private void AttackEnemy(int hurt)
    {
        if (_LisEnmeys != null && _LisEnmeys.Count >= 1)
        {
            foreach (GameObject enemy in _LisEnmeys)
            {
                if (enemy != null)
                {
                    //敌我距离
                    float floDistance = Vector3.Distance(this.gameObject.transform.position, enemy.transform.position);
                    //敌我方向(主角是否面对)
                    Vector3 dir = (enemy.transform.position - transform.position).normalized;
                    //敌我的夹角
                    float floDirection = Vector3.Dot(dir, transform.forward);
                    if (floDirection > 0 && floDistance <= FloRealAttackArea)
                    {
                        enemy.SendMessage("OnHurt", hurt);
                    }
                }
            }
        }
    }
}