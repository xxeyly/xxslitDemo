using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ctrl_Enemy_AI : MonoBehaviour
{
    private GameObject _GoHero; //主角
    public float FloMoveSpeed = 5f;
    private Ctrl_Enemy_Property enemyProperty; //属性脚本
    [SerializeField] private float FloMoveingDistance = 5; //移动距离
    [SerializeField] private float FloAttackDistance = 2; //攻击距离

    private GameObject playerUnit; //获取玩家单位
    private Vector3 initialPosition; //初始位置

    public float wanderRadius; //游走半径，移动状态下，如果超出游走半径会返回出生位置
    public float defendRadius; //自卫半径，玩家进入后怪物会追击玩家，当距离<攻击距离则会发动攻击（或者触发战斗）
    public float chaseRadius; //追击半径，当怪物超出追击半径后会放弃追击，返回追击起始位置

    public float attackRange; //攻击距离
    public float walkSpeed; //移动速度
    public float runSpeed; //跑动速度
    public float turnSpeed; //转身速度，建议0.1

    public int[] actionWeight = {1, 2}; //设置待机时各种动作的权重，顺序依次为呼吸、观察、移动


    private float diatanceToPlayer; //怪物与玩家的距离
    private float diatanceToInitial; //怪物与初始位置的距离
    private Quaternion targetRotation; //怪物的目标朝向
    private CharacterController _cc; //角色控制器

    // Use this for initialization
    void Start()
    {
        //怪物属性脚本
        enemyProperty = GetComponent<Ctrl_Enemy_Property>();
        //怪物控制器
        _cc = GetComponent<CharacterController>();
        playerUnit = GameObject.FindGameObjectWithTag("Player");
        //保存初始位置信息
        initialPosition = gameObject.GetComponent<Transform>().position;

        //检查并修正怪物设置
        //1. 攻击距离不大于自卫半径，否则就无法触发追击状态，直接开始战斗了
        attackRange = Mathf.Min(defendRadius, attackRange);
        //2. 游走半径不大于追击半径，否则怪物可能刚刚开始追击就返回出生点
        wanderRadius = Mathf.Min(chaseRadius, wanderRadius);

        //随机一个待机动作
        RandomAction();
        StartCoroutine(EnemyAi());
    }

    /// <summary>
    /// 根据权重随机待机指令
    /// </summary>
    void RandomAction()
    {
        //更新行动时间
        enemyProperty.LastActTime = 0;
        //根据权重随机
        int number = Random.Range(0, actionWeight[0] + actionWeight[1]);
        if (number == actionWeight[0])
        {
            enemyProperty.CurrentState = GlobalParametr.SimplyEnemyState.Idle;
        }
        else
        {
            enemyProperty.CurrentState = GlobalParametr.SimplyEnemyState.Walking;
            //随机一个朝向
            targetRotation = Quaternion.Euler(0, Random.Range(1, 5) * 90, 0);
            transform.rotation = targetRotation;
        }
    }

    IEnumerator EnemyAi()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            switch (enemyProperty.CurrentState)
            {
                //待机状态，等待actRestTme后重新随机指令
                case GlobalParametr.SimplyEnemyState.Idle:
                    if (enemyProperty.isPlayAnim())
                    {
                        RandomAction(); //随机切换指令
                    }

                    //该状态下的检测指令
                    WanderRadiusCheck();
                    break;

                //游走，根据状态随机时生成的目标位置修改朝向，并向前移动
                case GlobalParametr.SimplyEnemyState.Walking:
//                    transform.Translate(Vector3.forward * Time.deltaTime * walkSpeed);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed);

                    if (enemyProperty.isPlayAnim())
                    {
                        RandomAction(); //随机切换指令
                    }

                    _cc.SimpleMove(transform.forward * walkSpeed);
                    //该状态下的检测指令
                    WanderRadiusCheck();
                    break;
                //追击状态，朝着玩家跑去
                case GlobalParametr.SimplyEnemyState.Run:

                    //朝向玩家位置
//                    transform.LookAt(playerUnit.transform);
                    targetRotation =
                        Quaternion.LookRotation(playerUnit.transform.position - transform.position, Vector3.up);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1);
                    transform.Translate(Vector3.forward * Time.deltaTime * runSpeed);
//                    _cc.SimpleMove(transform.forward * runSpeed);
//                    _cc.Move(transform.position * runSpeed);

                    //该状态下的检测指令
                    ChaseRadiusCheck();
                    break;

                //返回状态，超出追击范围后返回出生位置
                case GlobalParametr.SimplyEnemyState.Return:

                    //朝向初始位置移动
                    targetRotation = Quaternion.LookRotation(initialPosition - transform.position, Vector3.up);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed);
                    transform.Translate(Vector3.forward * Time.deltaTime * runSpeed);
                    //该状态下的检测指令
                    ReturnCheck();
                    break;
                case GlobalParametr.SimplyEnemyState.Attack:
//                    transform.Translate(Vector3.forward * Time.deltaTime * runSpeed);
                    //朝向玩家位置
                    targetRotation =
                        Quaternion.LookRotation(playerUnit.transform.position - transform.position, Vector3.up);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.1f);
//                    transform.LookAt(playerUnit.transform);
                    AttackCheck();
                    break;
            }
        }
    }

    /// <summary>
    /// 游走状态检测，检测敌人距离及游走是否越界
    /// </summary>
    void WanderRadiusCheck()
    {
        //玩家与怪物的距离
        diatanceToPlayer = Vector3.Distance(playerUnit.transform.position, transform.position);
        //怪物与出生点的距离
        diatanceToInitial = Vector3.Distance(transform.position, initialPosition);
        //小于攻击距离,攻击玩家
        if (diatanceToPlayer < attackRange)
        {
            //进入战斗场景
            enemyProperty.CurrentState = GlobalParametr.SimplyEnemyState.Attack;
        }
        //小于自卫距离,追击玩家
        else if (diatanceToPlayer < defendRadius)
        {
            enemyProperty.CurrentState = GlobalParametr.SimplyEnemyState.Run;
        }

        //大于出生点位置
        if (diatanceToInitial > wanderRadius)
        {
            //朝向调整为初始方向
            targetRotation = Quaternion.LookRotation(initialPosition - transform.position, Vector3.up);
            enemyProperty.CurrentState = GlobalParametr.SimplyEnemyState.Walking;
        }
    }

    /// <summary>
    /// 追击状态检测，检测敌人是否进入攻击范围以及是否离开警戒范围
    /// </summary>
    void ChaseRadiusCheck()
    {
        //玩家与怪物的距离
        diatanceToPlayer = Vector3.Distance(playerUnit.transform.position, transform.position);
        diatanceToInitial = Vector3.Distance(transform.position, initialPosition);
        //小于攻击距离攻击玩家
        if (diatanceToPlayer < attackRange)
        {
            //进入战斗场景
            enemyProperty.CurrentState = GlobalParametr.SimplyEnemyState.Attack;
        }

        //估计大于游走半径,返回出生点
        if (diatanceToInitial > wanderRadius)
        {
            enemyProperty.CurrentState = GlobalParametr.SimplyEnemyState.Return;
        }
    }

    /// <summary>
    /// 超出追击半径，返回状态的检测，不再检测敌人距离
    /// </summary>
    void ReturnCheck()
    {
        //与出生点的距离
        diatanceToInitial = Vector3.Distance(transform.position, initialPosition);
        diatanceToPlayer = Vector3.Distance(playerUnit.transform.position, transform.position);
        /*    //如果小于自卫半径
            if (diatanceToInitial < defendRadius)
            {
                if (diatanceToPlayer < defendRadius)
                {
                    enemyProperty.CurrentState = GlobalParametr.SimplyEnemyState.Run;
                }
            }*/

        /*if (diatanceToPlayer < defendRadius)
        {
            enemyProperty.CurrentState = GlobalParametr.SimplyEnemyState.Run;
        }*/

        if (diatanceToInitial < defendRadius)
        {
            if (diatanceToPlayer < defendRadius)
            {
                enemyProperty.CurrentState = GlobalParametr.SimplyEnemyState.Run;
            }
            RandomAction();
        }

        /* //如果已经接近初始位置，则随机一个待机状态.定义在巡逻半径的一半
         if (diatanceToInitial < 0.5f)
         {
 //            is_Running = false;
             RandomAction();
         }*/
    }

    /// <summary>
    /// 攻击状态
    /// </summary>
    void AttackCheck()
    {
        //玩家与怪物的距离
        diatanceToPlayer = Vector3.Distance(playerUnit.transform.position, transform.position);
        diatanceToInitial = Vector3.Distance(transform.position, initialPosition);

        /*//离得还不够近了
        if (diatanceToPlayer >= 0.5f)
        {
            _cc.SimpleMove(transform.forward * runSpeed);
        }*/

        //大于切换时间
        if (enemyProperty.isPlayAnim())
        {
            //攻击
            enemyProperty.CurrentState = GlobalParametr.SimplyEnemyState.Attack;
            //更新行动时间
            enemyProperty.LastActTime = 0;
        }

        //距离大于游走半径,返回出生点
        if (diatanceToInitial > wanderRadius)
        {
            enemyProperty.CurrentState = GlobalParametr.SimplyEnemyState.Return;
        }
    }
}