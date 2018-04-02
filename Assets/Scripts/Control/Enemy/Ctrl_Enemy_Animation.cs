using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_Enemy_Animation : MonoBehaviour
{
    private Animator anim;
    private Ctrl_HeroProperty _HeroProperty;
    private Ctrl_Enemy_Property enemyProperty;

    // Use this for initialization
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        _HeroProperty = GameObject.FindGameObjectWithTag("Player").GetComponent<Ctrl_HeroProperty>();
        enemyProperty = GetComponent<Ctrl_Enemy_Property>();
        StartCoroutine(PlayEnemyAnimation());
    }

    // Update is called once per frame
    void Update()
    {
//        Debug.Log(anim.GetCurrentAnimatorClipInfo(0)[0].clip.name);
    }

    IEnumerator PlayEnemyAnimation()
    {
        yield return new WaitForEndOfFrame();
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            switch (enemyProperty.CurrentState)
            {
                case GlobalParametr.SimplyEnemyState.Idle:
                    anim.SetFloat("MoveSpeed", 0);
                    anim.SetBool("Attack", false);
                    break;
                case GlobalParametr.SimplyEnemyState.Walking:
                    anim.SetFloat("MoveSpeed", 1);
                    anim.SetBool("Attack", false);

                    break;
                case GlobalParametr.SimplyEnemyState.Attack:
                    anim.SetFloat("MoveSpeed", 0);
                    anim.SetBool("Attack", true);
                    enemyProperty.AttackTimer = 0;
                    break;
                case GlobalParametr.SimplyEnemyState.Hurt:
                    if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "getHitNormal")
                    {
                        anim.SetTrigger("Hurt");
                    }

                    break;
                case GlobalParametr.SimplyEnemyState.Death:
                    if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "deathAggressive")
                    {
                        anim.SetTrigger("Dead");
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public void AttackHerobyAnimationEvent()
    {
        _HeroProperty.DecreaseHealthValues(enemyProperty.intAttack);
    }
}