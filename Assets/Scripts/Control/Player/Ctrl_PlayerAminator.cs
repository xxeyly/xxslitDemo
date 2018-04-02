using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_PlayerAminator : Singleton<Ctrl_PlayerAminator> {

    private Animator anim;

    private void Update()
    {
//        Debug.Log(anim.GetCurrentAnimatorClipInfo(0)[0].clip.name);
    }

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void NormalAttack()
    {
        anim.SetTrigger("NormalAttack");
    }

    public void Skill1()
    {
        anim.SetTrigger("Skill1");
    }
    public void Skill2()
    {
        anim.SetTrigger("Skill2");
    }
    public void Skill3()
    {
        anim.SetTrigger("Skill3");
    }

    public void Block(bool value)
    {
        anim.SetBool("IsBlock",value);
    }

    public void BlockBreak(bool value)
    {
        anim.SetBool("IsBlockBreak", value);

    }

    public bool GetCurrentBlockState()
    {
        return anim.GetBool("IsBlock");

    }
    public bool GetCurrentBlockBreakState()
    {
        return anim.GetBool("IsBlockBreak");
    }

    public void KickL()
    {
        anim.SetTrigger("KickL");
    }

    public void KickR()
    {
        anim.SetTrigger("KickR");
    }

    public void SetHVValue(float h,float v)
    {
        anim.SetFloat("Vertical",v);
        anim.SetFloat("Horizontal", h);
    }

    public void LeftAndRight(bool value)
    {
        anim.SetBool("LeftAndRight",value);
    }
}
