using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ctrl_ActionBarSlot : MonoBehaviour, IPointerDownHandler
{
    private Model_Skill skill;

    public Model_Skill Skill
    {
        get { return skill; }

        set
        {
            skill = value;
            GetComponent<View_ActionBarSlot>().Init(value);
        }
    }

    public bool IsCooding
    {
        get { return isCooding; }

        set
        {
            isCooding = value;
            skill.skillCurrentCD = 0;
        }
    }
    //是否冷却中
    private bool isCooding;

    private void Update()
    {
        if (skill != null)
        {
            if (IsCooding)
            {
                GetComponent<View_ActionBarSlot>().UpdateSkillCooding(skill.skillCurrentCD, skill.skillCD);
            }
        }
    }

    /// <summary>
    /// 使用技能
    /// </summary>
    public void UseSkill()
    {
        if (skill != null)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Ctrl_HeroAttack>().ReleaseSkills(this);
        }
    }

    public void UpdateSkillCooding()
    {
        GetComponent<View_ActionBarSlot>().UpdateSkillCooding(skill.skillCurrentCD, skill.skillCD);
    }

    /// <summary>
    /// 按钮按下
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        //如果右键点击,替换或者放置技能
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //获得当前手上的信息
            Model_Skill pickupSkill = Ctrl_TootipManager.Instance.PickUp.GetComponent<Ctrl_PickUp>().Skill;
            //如果有技能
            if (pickupSkill != null)
            {
                //当前技能槽也有技能
                if (Skill != null)
                {
                    //不是相同技能,执行替换操作
                    if (Skill.skillId != pickupSkill
                            .skillId)
                    {
                        Model_Skill tempSkill;
                        tempSkill = Skill;
                        Skill = pickupSkill;
//                        StartSkillCooling();
                        Ctrl_TootipManager.Instance.ShowPickUp(tempSkill);
                    }
                }
                else
                {
                    Skill = pickupSkill;
                    UpdateSkillCooding();
                    Ctrl_TootipManager.Instance.PickUp.GetComponent<Ctrl_PickUp>().Skill = null;
                }
            }
            else
            {
                //TUDO
                //处理技能的释放
                UseSkill();
            }
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (Ctrl_TootipManager.Instance.PickUp.GetComponent<Ctrl_PickUp>().Skill != null)
            {
            }
            else
            {
                Ctrl_TootipManager.Instance.ShowPickUp(Skill);
                Skill = null;
            }
        }
    }
}