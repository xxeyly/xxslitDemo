using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 技能冷却时间
/// 更改
/// </summary>
public class View_ActionBarSlot : MonoBehaviour
{
    [SerializeField] private Image skillIcon; //技能图标

    [SerializeField] private Image SkillCDIcon; //技能冷却图标

    [SerializeField] private Text TextCD; //技能冷却时间
    private Ctrl_ActionBarSlot actionBarSlot;

    private void Start()
    {
        actionBarSlot = GetComponent<Ctrl_ActionBarSlot>();
    }

    /// <summary>
    /// 根据传进来的值 显示技能信息
    /// </summary>
    /// <param name="skill"></param>
    public void Init(Model_Skill skill)
    {
        if (skill != null)
        {
            skillIcon.sprite = Resources.Load<Sprite>(skill.skillSprite);
            skillIcon.gameObject.SetActive(true);
            //技能还在冷却
            if (skill.skillCurrentCD < skill.skillCD)
            {
                SkillCD(true);
            }
            else
            {
                SkillCD(false);
            }
        }
        else
        {
            SkillCD(false);
            skillIcon.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 技能冷却
    /// </summary>
    public void SkillCD(bool isCD)
    {
        SkillCDIcon.gameObject.SetActive(isCD);
        TextCD.gameObject.SetActive(isCD);
    }

    /// <summary>
    /// 更新技能冷却UI
    /// </summary>
    /// <param name="currentCD"></param>
    /// <param name="skillCD"></param>
    public void UpdateSkillCooding(float currentCD, int skillCD)
    {
        SkillCD(true);
        SkillCDIcon.fillAmount = 1 - (currentCD / skillCD);
//        TextCD.text = (Int32.Parse((int) skillCD - currentCD));
        if (skillCD - (int) currentCD <= 0.1)
        {
            TextCD.text = "";
        }
        else
        {
            TextCD.text = (skillCD - (int) currentCD).ToString();
        }
    }
    /*// Update is called once per frame
    void Update()
    {
        if (IsStartTimer)
        {
            _FloTimerDelta += Time.deltaTime;
            SkillCDIcon.fillAmount = 1 - (_FloTimerDelta / FloCDTime);
            TextCD.text = (FloCDTime - _FloTimerDelta).ToString();
            if (_FloTimerDelta >= FloCDTime)
            {
                isSkillRead = true;
                SkillCDIcon.fillAmount = 0;
                TextCD.gameObject.SetActive(false);
                IsStartTimer = true;
            }
        }
    }*/

    /*  public void ResponseBtnClick()
      {
          if (_FloTimerDelta < FloCDTime)
          {
  //            Debug.Log("冷却时间未到...");
          }
          else
          {
              isSkillRead = false;
              _FloTimerDelta = 0;
              IsStartTimer = true;
              SkillCDIcon.fillAmount = 1;
              TextCD.gameObject.SetActive(true);
              TextCD.text = FloCDTime.ToString();
          }
      }*/
}