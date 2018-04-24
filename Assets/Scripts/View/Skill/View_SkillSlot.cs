using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class View_SkillSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image skillIcon;
    [SerializeField] private Text skillLv;
    [SerializeField] private GameObject skillLock;

    public void Init(Model_Skill skill)
    {
        //加载图片
        skillIcon.sprite = Resources.Load<Sprite>(skill.skillSprite);
        //加载技能等级
        skillLv.text = skill.skillCurrentLv + "/" + skill.skillMaxLv;
        //默认技能上锁
        skillLock.SetActive(true);
    }

    /// <summary>
    /// 鼠标进入 显示技能描述
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GetComponent<Ctrl_SkillSLot>().Skill != null && GetComponent<Ctrl_SkillSLot>().IsLock)
        {
            Ctrl_TootipManager.Instance.ShowSkillTootip(GetComponent<Ctrl_SkillSLot>().Skill.ToString());
        }
    }

    /// <summary>
    /// 鼠标移除 隐藏显示
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        Ctrl_TootipManager.Instance.HideSkillTootip();
    }

    /// <summary>
    /// 解锁
    /// </summary>
    public void Unlock()
    {
        skillLock.SetActive(false);
    }

    /// <summary>
    /// 技能等级提升
    /// </summary>
    /// <param name="skill"></param>
    public void UpdateSkillLv(Model_Skill skill)
    {
        skillLv.text = skill.skillCurrentLv + "/" + skill.skillMaxLv;
    }
}