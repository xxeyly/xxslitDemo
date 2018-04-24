using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_PlayerAminator : Singleton<Ctrl_PlayerAminator> {

    private Animator anim;
    private GlobalParametr.SkillEnum skillEnum;
    private void Update()
    {

    }

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    /// <summary>
    /// 释放技能
    /// </summary>
    /// <param name="skillId"></param>
    public void ReleaseSkill(int skillId)
    {
        skillEnum = (GlobalParametr.SkillEnum) skillId;
        anim.SetTrigger(skillEnum.ToString());

    }
    /// <summary>
    /// 设置行走的值
    /// </summary>
    /// <param name="h"></param>
    /// <param name="v"></param>
    public void SetHVValue(float h,float v)
    {
        anim.SetFloat("Vertical",v);
        anim.SetFloat("Horizontal", h);
    }
}
