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


    IEnumerator PlayEnemyAnimation()
    {
        yield return new WaitForEndOfFrame();
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            switch (enemyProperty.CurrentState)
            {
                case GlobalParametr.SimplyEnemyState.Idle:
                    anim.SetBool("Idle", true);
                    anim.SetBool("Walk", false);
                    anim.SetBool("Run", false);
                    break;
                case GlobalParametr.SimplyEnemyState.Walking:
                    anim.SetBool("Idle", false);
                    anim.SetBool("Walk", true);
                    anim.SetBool("Run", false);

                    break;
                case GlobalParametr.SimplyEnemyState.Run:
                    anim.SetBool("Run", true);
                    anim.SetBool("Idle", false);
                    anim.SetBool("Walk", false);
                    break;
                case GlobalParametr.SimplyEnemyState.Attack:
                    anim.SetBool("Run", false);
                    if (enemyProperty.isPlayAnim())
                    {
                        anim.SetTrigger("Attack");
                        enemyProperty.CurrentState = GlobalParametr.SimplyEnemyState.Idle;
                    }

                    anim.SetBool("Idle", false);
                    anim.SetBool("Walk", false);

                    break;
                case GlobalParametr.SimplyEnemyState.Hurt:
                    anim.SetTrigger("GetHit");
                    anim.SetBool("Idle", false);
                    anim.SetBool("Walk", false);
                    anim.SetBool("Run", false);
                    enemyProperty.CurrentState = GlobalParametr.SimplyEnemyState.Idle;
                    break;
                case GlobalParametr.SimplyEnemyState.Death:
                    anim.SetTrigger("Death");
                    StopAllCoroutines();
                    break;
            }
        }
    }

    public void AttackHerobyAnimationEvent()
    {
        _HeroProperty.DecreaseHealthValues(enemyProperty.intAttack);
    }
}