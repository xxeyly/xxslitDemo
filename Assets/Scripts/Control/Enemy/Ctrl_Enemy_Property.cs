using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 冥府守门狗属性脚本
/// </summary>
public class Ctrl_Enemy_Property : MonoBehaviour
{
    [SerializeField] private int enemyId;
    [SerializeField] private int intMaxHealth = 1000;
    [SerializeField] private int intCurrentHealth;
    [SerializeField] public int intDefender = 20;
    [SerializeField] public int intAttack = 20;
    public float AttackCD = 2;
    public float AttackTimer = 0;

    [SerializeField]
    private GlobalParametr.SimplyEnemyState _CurrentState = GlobalParametr.SimplyEnemyState.Idle; //当前动画状态

    public GlobalParametr.del_EnemyBloodGroove eveEnemyBloodGroove;

    /// <summary>
    /// 当前的状态
    /// </summary>
    public GlobalParametr.SimplyEnemyState CurrentState
    {
        get { return _CurrentState; }

        set { _CurrentState = value; }
    }
    /// <summary>
    /// 当前健康值
    /// </summary>
    public int IntCurrentHealth
    {
        get { return intCurrentHealth; }

        set
        {
            intCurrentHealth = value;
            eveEnemyBloodGroove((float) intCurrentHealth / intMaxHealth);
        }
    }

    void Start()
    {
        IntCurrentHealth = intMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
    }
    /// <summary>
    /// 怪物收到伤害
    /// </summary>
    /// <param name="hurtNumber"></param>
    public void OnHurt(int hurtNumber)
    {
        _CurrentState = GlobalParametr.SimplyEnemyState.Hurt;
        if (hurtNumber <= intDefender)
        {
            IntCurrentHealth -= 1;
        }
        else
        {
            IntCurrentHealth -= (hurtNumber - 20);
            if (IntCurrentHealth <= 0)
            {
                //死亡取消碰撞器
                GetComponent<CharacterController>().enabled = false;
                _CurrentState = GlobalParametr.SimplyEnemyState.Death;
                //角色增加经验值
                Ctrl_HeroProperty.Instance.UpgradeConition(GetComponent<Ctrl_Enemy_DropProperty>().RewardExp);
                //角色增加金币
                Ctrl_HeroProperty.Instance.AddGold(GetComponent<Ctrl_Enemy_DropProperty>().RewardGold);
                //角色增加物品
                foreach (Ctrl_Enemy_DropProperty.EnemyDropItem enemyDropItem in GetComponent<Ctrl_Enemy_DropProperty>()
                    .RewardItems)
                {
                    for (int i = 0; i < enemyDropItem.number; i++)
                    {
                        Ctrl_InventoryManager.Instance.AddItem(enemyDropItem.item);
                    }
                }

                //增加杀敌数量
                Ctrl_HeroProperty.Instance.AddKillnumber();
                //当前接受的任务中,击杀怪物中包含本身,并且当前任务不是提交状态,增加击杀个数
                foreach (Model_Quest quest in Ctrl_PlayerQuest.Instance.PlayQuestList)
                {
                    if (!quest.questSubmit)
                    {
                        foreach (Model_Quest.QuestEnemy enemy in quest.questKillEnemy)
                        {
                            if (enemy.enemyId == enemyId)
                            {
                                if (enemy.currentEnemyNumber < enemy.enemyNumber)
                                {
                                    enemy.currentEnemyNumber++;
                                    if (Ctrl_QuestItemManager.Instance.GetSelectOverlay().Quest.id == quest.id)
                                    {
                                        Ctrl_QuestItemManager.Instance.ShowQuestInfo(quest);
                                    }
                                }
                            }
                        }
                    }
                }

                StartCoroutine(WaitTimeDestroy());
            }
        }
    }

    IEnumerator WaitTimeDestroy()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}