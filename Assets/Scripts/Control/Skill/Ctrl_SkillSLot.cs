using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ctrl_SkillSLot : MonoBehaviour, IPointerDownHandler
{
    private Model_Skill skill;

    public Model_Skill Skill
    {
        get { return skill; }

        set
        {
            skill = value;
            GetComponent<View_SkillSlot>().Init(value);
        }
    }
    /// <summary>
    /// 当前技能是否是锁定状态
    /// </summary>
    public bool IsLock
    {
        get { return isLock; }
    }

    private bool isLock = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        //如果按下右键,捡起当前技能
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            //锁定的技能不能拿起
            if (IsLock)
            {
                Ctrl_TootipManager.Instance.ShowPickUp(Skill);
            }
        }
    }

    /// <summary>
    /// 等级达到了,解锁格子
    /// </summary>
    public void UnLock()
    {
        GetComponent<View_SkillSlot>().Unlock();
        isLock = true;
    }

    /// <summary>
    /// 技能等级提升
    /// </summary>
    public void UpdateSkillLv()
    {
        GetComponent<View_SkillSlot>().UpdateSkillLv(skill);
    }
}