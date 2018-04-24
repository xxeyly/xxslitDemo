using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 冥府守门狗属性脚本
/// </summary>
public class Ctrl_Enemy_Property : MonoBehaviour
{
    [SerializeField] private int enemyId;
    [SerializeField] private int intMaxHealth = 1000; //最大生命上限
    [SerializeField] private int intCurrentHealth; //当前生命值
    [SerializeField] public int intDefender = 20; //防御
    [SerializeField] public int intAttack = 20; //攻击
    [SerializeField] private int physicalResistance; //物理抗性
    [SerializeField] private int jinResistance; //金抗性
    [SerializeField] private int muResistance; //木抗性
    [SerializeField] private int waterResistance; //水抗性
    [SerializeField] private int fireResistance; //火抗性
    [SerializeField] private int soilResistance; //土抗性
    [SerializeField] private int MagicalBenefitsResistance; //魔法减益抗性
    [SerializeField] private Vector3 offset;

    [SerializeField]
    private GlobalParametr.SimplyEnemyState _CurrentState = GlobalParametr.SimplyEnemyState.Idle; //当前动画状态

    public GlobalParametr.del_EnemyBloodGroove eveEnemyBloodGroove;
    [SerializeField] private float actRestTme; //更换待机指令的间隔时间
    private float lastActTime; //最近一次指令时间

    public bool isPlayAnim()
    {
        return LastActTime > ActRestTme;
    }

    /// <summary>
    /// 当前的状态
    /// </summary>
    public GlobalParametr.SimplyEnemyState CurrentState
    {
        get { return _CurrentState; }

        set { _CurrentState = value; }
    }

    private void Update()
    {
        LastActTime += Time.deltaTime;
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

    public float LastActTime
    {
        get { return lastActTime; }

        set { lastActTime = value; }
    }

    public float ActRestTme
    {
        get { return actRestTme; }

        set { actRestTme = value; }
    }

    void Start()
    {
        IntCurrentHealth = intMaxHealth;
    }

    /// <summary>
    /// 怪物收到伤害
    /// </summary>
    /// <param name="hurtNumber"></param>
    public void OnHurt(int hurtNumber, float moveDirection)
    {
        _CurrentState = GlobalParametr.SimplyEnemyState.Hurt;
        if (hurtNumber <= intDefender)
        {
            IntCurrentHealth -= 1;
            //位移更新,增加技能击退效果,会根据技能的属性,来位置距离
            iTween.MoveBy(gameObject,
                transform.InverseTransformDirection(GameObject.FindGameObjectWithTag("Player").transform.forward +
                                                    GameObject.FindGameObjectWithTag("Player").transform.forward *
                                                    moveDirection), 1f);
            Hud(1);
        }
        else
        {
            IntCurrentHealth -= (hurtNumber - intDefender);
            iTween.MoveBy(gameObject,
                transform.InverseTransformDirection(GameObject.FindGameObjectWithTag("Player").transform.forward +
                                                    GameObject.FindGameObjectWithTag("Player").transform.forward *
                                                    moveDirection), 1f);
            Hud(hurtNumber - intDefender);

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

                #region 任务处理

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

                #endregion

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
    /// <summary>
    /// 伤害HUD处理 就是飘血
    /// </summary>
    /// <param name="hurtNumber"></param>
    public void Hud(int hurtNumber)
    {
        GameObject hub = ObjectPoolTool.Instance.Pop(ObjectPoolTool.Instance.GetObject(objPool.HubText));
        hub.transform.parent = Ctrl_TootipManager.Instance.UICanvas;
        hub.transform.position = Camera.main.WorldToScreenPoint(this.transform.position) + offset;
        hub.GetComponent<Text>().text = "-" + hurtNumber;
        hub.GetComponent<View_HubText>().ShowHud();
    }
}