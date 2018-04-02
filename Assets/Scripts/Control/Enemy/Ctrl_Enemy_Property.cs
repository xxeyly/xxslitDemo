using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 冥府守门狗属性脚本
/// </summary>
public class Ctrl_Enemy_Property : MonoBehaviour
{
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
                _CurrentState = GlobalParametr.SimplyEnemyState.Death;
                //角色增加经验值
                Ctrl_HeroProperty.Instance.UpgradeConition(GetComponent<Ctrl_Enemy_DropProperty>().RewardExp);
                //角色增加金币
                Ctrl_HeroProperty.Instance.AddGold(GetComponent<Ctrl_Enemy_DropProperty>().RewardGold);
                //角色增加物品
                foreach (Ctrl_Enemy_DropProperty.EnemyDropItem enemyDropItem in GetComponent<Ctrl_Enemy_DropProperty>().RewardItems)
                {
                    for (int i = 0; i < enemyDropItem.number; i++)
                    {
                        Ctrl_InventoryManager.Instance.AddItem(enemyDropItem.item);
                    }
                }
                //增加杀敌数量
                Ctrl_HeroProperty.Instance.AddKillnumber();

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