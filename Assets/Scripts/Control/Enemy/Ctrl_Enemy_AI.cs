using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_Enemy_AI : MonoBehaviour
{
    private GameObject _GoHero; //主角
    public float FloMoveSpeed = 5f;
    private Ctrl_Enemy_Property enemyProperty; //属性脚本
    [SerializeField] private float FloMoveingDistance = 5;
    [SerializeField] private float FloAttackDistance = 2;


    private CharacterController _cc; //角色控制器

    // Use this for initialization
    void Start()
    {
        _GoHero = GameObject.FindGameObjectWithTag("Player");
        enemyProperty = gameObject.GetComponent<Ctrl_Enemy_Property>();
        _cc = gameObject.GetComponent<CharacterController>();
        StartCoroutine(ThinkProcess());
        StartCoroutine(MoveingProcess());
    }

    // Update is called once per frame
    void Update()
    {
        enemyProperty.AttackTimer += Time.deltaTime;
    }

    /// <summary>
    /// 思考协成
    /// </summary>
    /// <returns></returns>
    IEnumerator ThinkProcess()
    {
        yield return new WaitForSeconds(1f);
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (enemyProperty.CurrentState != GlobalParametr.SimplyEnemyState.Death)
            {
                //得到当前主角方位
                Vector3 VecHero = _GoHero.transform.position;
                //得到与主角的距离
                float FloDistance = Vector3.Distance(VecHero, transform.position);

                //判断距离
                if (FloDistance < FloAttackDistance)
                {
                    //攻击
                    if (enemyProperty.AttackTimer >= enemyProperty.AttackCD)
                    {
                        enemyProperty.CurrentState = GlobalParametr.SimplyEnemyState.Attack;
                    }
                    else
                    {
                        enemyProperty.CurrentState = GlobalParametr.SimplyEnemyState.Idle;
                    }
                }
                else if (FloDistance < FloMoveingDistance)
                {
                    //追击主角
                    enemyProperty.CurrentState = GlobalParametr.SimplyEnemyState.Walking;
                }
                else
                {
                    //休闲
                    enemyProperty.CurrentState = GlobalParametr.SimplyEnemyState.Idle;
                }
            }


            //小于攻击距离
            //小于警戒距离
            //小于警戒距离
        }
    }

    /// <summary>
    /// 移动协成
    /// </summary>
    /// <returns></returns>
    IEnumerator MoveingProcess()
    {
        yield return new WaitForSeconds(1f);
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (enemyProperty.CurrentState != GlobalParametr.SimplyEnemyState.Death)
            {
                //移动
                switch (enemyProperty.CurrentState)
                {
                    case GlobalParametr.SimplyEnemyState.Idle:
                        break;
                    case GlobalParametr.SimplyEnemyState.Walking:
                        //面向主角
                        transform.rotation = Quaternion.Lerp(transform.rotation,
                            Quaternion.LookRotation(new Vector3(_GoHero.transform.position.x, 0,
                                                        _GoHero.transform.position.z) -
                                                    new Vector3(transform.position.x, 0, transform.position.z)), 0.3f);
                        Vector3 v = Vector3.ClampMagnitude(_GoHero.transform.position - transform.position,
                            FloMoveSpeed * Time.deltaTime);
                        _cc.Move(v);
                        break;
                    case GlobalParametr.SimplyEnemyState.Attack:
                        break;
                    case GlobalParametr.SimplyEnemyState.Hurt:
                        break;
                    case GlobalParametr.SimplyEnemyState.Death:
                        break;
                }
            }
        }
    }
}