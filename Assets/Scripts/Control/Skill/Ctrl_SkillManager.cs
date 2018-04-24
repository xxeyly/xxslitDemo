using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

public class Ctrl_SkillManager : Singleton<Ctrl_SkillManager>
{
    [SerializeField] private TextAsset skillAsset;
    List<Model_Skill> skillList = new List<Model_Skill>();
    List<Ctrl_SkillSLot> skillSlotList = new List<Ctrl_SkillSLot>();
    [SerializeField] private Transform lvZero; //0级
    [SerializeField] private Transform lvFives; //5级
    [SerializeField] private Transform lvTen; //十级
    [SerializeField] private Transform lvFifteen; //十五级
    [SerializeField] private Transform lvTwenty; //二十级
    [SerializeField] private Transform SkillGroup; //底部技能格子

    public List<Model_Skill> SkillList
    {
        get { return skillList; }
    }
    /// <summary>
    /// 技能初始化
    /// 根据Json分析当前技能的所属板块
    /// </summary>
    void Start()
    {
        ParseSkillJson();
        foreach (Model_Skill skill in SkillList)
        {
            skill.skillCurrentCD = skill.skillCD;
            if (skill.skillLockLv >= 0 && skill.skillLockLv < 5)
            {
                GameObject skillItem =
                    ObjectPoolTool.Instance.Pop(ObjectPoolTool.Instance.GetObject(objPool.SkillItem));
                skillItem.transform.parent = lvZero;
                skillItem.GetComponent<Ctrl_SkillSLot>().Skill = skill;
                skillItem.transform.localScale = Vector3.one;
                skillSlotList.Add(skillItem.GetComponent<Ctrl_SkillSLot>());
            }

            if (skill.skillLockLv >= 5 && skill.skillLockLv < 10)
            {
                GameObject skillItem =
                    ObjectPoolTool.Instance.Pop(ObjectPoolTool.Instance.GetObject(objPool.SkillItem));
                skillItem.transform.parent = lvFives;
                skillItem.GetComponent<Ctrl_SkillSLot>().Skill = skill;
                skillItem.transform.localScale = Vector3.one;
                skillSlotList.Add(skillItem.GetComponent<Ctrl_SkillSLot>());
            }

            if (skill.skillLockLv >= 10 && skill.skillLockLv < 15)
            {
                GameObject skillItem =
                    ObjectPoolTool.Instance.Pop(ObjectPoolTool.Instance.GetObject(objPool.SkillItem));
                skillItem.transform.parent = lvTen;
                skillItem.GetComponent<Ctrl_SkillSLot>().Skill = skill;
                skillItem.transform.localScale = Vector3.one;
                skillSlotList.Add(skillItem.GetComponent<Ctrl_SkillSLot>());
            }

            if (skill.skillLockLv >= 15 && skill.skillLockLv < 20)
            {
                GameObject skillItem =
                    ObjectPoolTool.Instance.Pop(ObjectPoolTool.Instance.GetObject(objPool.SkillItem));
                skillItem.transform.parent = lvFifteen;
                skillItem.GetComponent<Ctrl_SkillSLot>().Skill = skill;
                skillItem.transform.localScale = Vector3.one;
                skillSlotList.Add(skillItem.GetComponent<Ctrl_SkillSLot>());
            }

            if (skill.skillLockLv >= 20 && skill.skillLockLv < 25)
            {
                GameObject skillItem =
                    ObjectPoolTool.Instance.Pop(ObjectPoolTool.Instance.GetObject(objPool.SkillItem));
                skillItem.transform.parent = lvTwenty;
                skillItem.GetComponent<Ctrl_SkillSLot>().Skill = skill;
                skillItem.transform.localScale = Vector3.one;
                skillSlotList.Add(skillItem.GetComponent<Ctrl_SkillSLot>());
            }
        }
    }

    /// <summary>
    /// 实时更新检测当前技能的状态
    /// 到达一定级别解锁技能
    /// 技能熟练度更新
    /// 技能升级,伤害变更
    /// </summary>
    private void Update()
    {
        foreach (Ctrl_SkillSLot sLot in skillSlotList)
        {
            //当前技能解锁等级小于人物等级,解锁技能
            if (sLot.Skill.skillLockLv <= Ctrl_HeroProperty.Instance.GetLevel())
            {
                //解锁技能
                sLot.UnLock();
                //解锁状态下,实时更新技能的冷却时间
                if (sLot.Skill.skillCurrentCD < sLot.Skill.skillCD)
                {
                    sLot.Skill.skillCurrentCD += Time.deltaTime;
                }

                //当前技能等级的熟练度满了,自动升下一级,但不会超过技能的等级上限
                if (sLot.Skill.skillCurrentProficiency >= sLot.Skill.skillProficiency[sLot.Skill.skillCurrentLv - 1])
                {
                    if (sLot.Skill.skillCurrentLv < sLot.Skill.skillMaxLv)
                    {
                        sLot.Skill.skillCurrentProficiency = 0;
                        sLot.Skill.skillCurrentLv += 1;
                        //更新技能等级UI
                        sLot.UpdateSkillLv();
                    }
                }
            }
        }
    }


    /// <summary>
    /// 解析技能数据
    /// </summary>
    public void ParseSkillJson()
    {
        skillList = JsonMapper.ToObject<List<Model_Skill>>(skillAsset.ToString());
    }

    /// <summary>
    /// 根据按键获得技能栏对应的技能格子
    /// </summary>
    /// <param name="skill"></param>
    /// <returns></returns>
    public Ctrl_ActionBarSlot GetCurrentActionBarSlot(int skill)
    {
        Dictionary<int, Ctrl_ActionBarSlot> skillDic = new Dictionary<int, Ctrl_ActionBarSlot>();
        Ctrl_ActionBarSlot[] actionBarSlotList = this.SkillGroup.GetComponentsInChildren<Ctrl_ActionBarSlot>();
        for (int i = 0; i < actionBarSlotList.Length; i++)
        {
            skillDic.Add(i, actionBarSlotList[i]);
        }
        Ctrl_ActionBarSlot actionBarSlot;
        skillDic.TryGetValue(skill, out actionBarSlot);
        return actionBarSlot;
    }
}