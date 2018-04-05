using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class View_QuestInfo : MonoBehaviour
{
    private Model_Quest quest;
    [SerializeField] private Text questBriefIntroduction; //任务标题
    [SerializeField] private Text questSescribe; //任务描述
    [SerializeField] private Transform questDemandTransform; //任务需求
    [SerializeField] private Transform questRewardTransform; //任务奖励
    [SerializeField] private GameObject canelQuest; //放弃任务
    [SerializeField] private GameObject overQuest; //完成任务

    public Model_Quest Quest
    {
        get { return quest; }

        set
        {
            quest = value;
            InitQuest();
        }
    }

    /// <summary>
    /// 初始化 
    /// </summary>
    public void InitQuest()
    {
        questBriefIntroduction.text = quest.questBriefIntroduction;
        questSescribe.text = quest.questSescribe;
        ClearQuestDemandAndReward();
        //击杀需求
        foreach (Model_Quest.QuestEnemy enemy in quest.questKillEnemy)
        {
            //没有击杀任务,跳过
            if (enemy.enemyNumber != 0)
            {
                GameObject MissionRequired = Resources.Load<GameObject>("Prefabs/UI/Quest/MissionRequired");
                View_QuestMissionRequired missionRequired =
                    MissionRequired.GetComponent<View_QuestMissionRequired>();
                missionRequired.CurrentCount.text = enemy.currentEnemyNumber.ToString();
                missionRequired.QuestCount.text = enemy.enemyNumber.ToString();
                missionRequired.QuestName.text = GlobalParametr.GetEnemyName(enemy.enemyId);
                missionRequired.HideMark();
                missionRequired.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                Instantiate(MissionRequired).transform.parent = questDemandTransform;
            }
        }

        //对话内容
        foreach (Model_Quest.QuestNPC questNpc in quest.questTalkNPC)
        {
            if (questNpc.npcId != -1)
            {
                GameObject MissionRequired = Resources.Load<GameObject>("Prefabs/UI/Quest/MissionRequired");
                View_QuestMissionRequired missionRequired =
                    MissionRequired.GetComponent<View_QuestMissionRequired>();
                missionRequired.CurrentCount.text = questNpc.isTalk ? "1" : "0";
                missionRequired.QuestCount.text = "1";
                missionRequired.QuestName.text = GlobalParametr.GetNpcName(questNpc.npcId);
                missionRequired.ShowMark();
                missionRequired.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                Instantiate(MissionRequired).transform.parent = questDemandTransform;
            }
        }

        //物品需求
        foreach (Model_Quest.QuestItem questItem in quest.questItem)
        {
            if (questItem.itemId != -1)
            {
                GameObject MissionRequired = Resources.Load<GameObject>("Prefabs/UI/Quest/MissionRequired");
                View_QuestMissionRequired missionRequired =
                    MissionRequired.GetComponent<View_QuestMissionRequired>();
                missionRequired.CurrentCount.text = Ctrl_InventoryManager.Instance
                    .GetItemCount(questItem.itemId).ToString();
                missionRequired.QuestCount.text = questItem.itemNumber.ToString();
                missionRequired.QuestName.text = Ctrl_InventoryManager.Instance.GetItemById(questItem.itemId).itemName;
                missionRequired.HideMark();
                missionRequired.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                Instantiate(MissionRequired).transform.parent = questDemandTransform;
            }
        }

        #region 任务奖励

        if (quest.questReward.gold > 0)
        {
            GameObject QuestRewardSlot = Resources.Load<GameObject>("Prefabs/UI/Quest/QuestRewardSlot");
            QuestRewardSlot.GetComponent<Ctrl_QuestRewardSlot>().ShowGoldView(quest.questReward.gold);
            QuestRewardSlot.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            Instantiate(QuestRewardSlot).transform.parent = questRewardTransform;
        }

        if (quest.questReward.diamod > 0)
        {
            GameObject QuestRewardSlot = Resources.Load<GameObject>("Prefabs/UI/Quest/QuestRewardSlot");
            QuestRewardSlot.GetComponent<Ctrl_QuestRewardSlot>().ShowDiamodView(quest.questReward.diamod);
            QuestRewardSlot.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            Instantiate(QuestRewardSlot).transform.parent = questRewardTransform;
        }

        if (quest.questReward.exp > 0)
        {
            GameObject QuestRewardSlot = Resources.Load<GameObject>("Prefabs/UI/Quest/QuestRewardSlot");
            QuestRewardSlot.GetComponent<Ctrl_QuestRewardSlot>().ShowExpView(quest.questReward.exp);
            QuestRewardSlot.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            Instantiate(QuestRewardSlot).transform.parent = questRewardTransform;
        }

        if (quest.questReward.questRewardItem.Length >= 1)
        {
            foreach (Model_Quest.QuestItem item in quest.questReward.questRewardItem)
            {
                GameObject QuestRewardSlot = Resources.Load<GameObject>("Prefabs/UI/Quest/QuestRewardSlot");
                QuestRewardSlot = Instantiate(QuestRewardSlot);
                QuestRewardSlot.GetComponent<Ctrl_QuestRewardSlot>().Item =
                    Ctrl_InventoryManager.Instance.GetItemById(item.itemId);
                QuestRewardSlot.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                QuestRewardSlot.transform.parent = questRewardTransform;
            }
        }

        if (Quest.questState)
        {
            overQuest.SetActive(true);
        }
        else
        {
            overQuest.SetActive(false);
        }

        #endregion
    }

    /// <summary>
    /// 清空任务需求和任务奖励,防止刷新出的BUG
    /// </summary>
    private void ClearQuestDemandAndReward()
    {
        foreach (Transform demand in questDemandTransform)
        {
            Destroy(demand.gameObject);
        }

        foreach (Transform reward in questRewardTransform)
        {
            Destroy(reward.gameObject);
        }
    }

    /// <summary>
    /// 放弃任务 任务击杀和任务对话清空
    /// </summary>
    public void CanelQuest()
    {
        Quest.questAccept = false;
        Quest.questState = false;
        Quest.questSubmit = false;
        foreach (Model_Quest.QuestEnemy enemy in Quest.questKillEnemy)
        {
            enemy.currentEnemyNumber = 0;
        }

        foreach (Model_Quest.QuestNPC npc in Quest.questTalkNPC)
        {
            npc.isTalk = false;
        }

        //移除当前任务
        Ctrl_PlayerQuest.Instance.PlayQuestList.Remove(quest);
        Ctrl_QuestItemManager.Instance.ClearQuestItem(quest);
        //更新任务列表
        Ctrl_QuestItemManager.Instance.UpdateQuestItem();
    }

    /// <summary>
    /// 完成任务 获得奖励
    /// </summary>
    public void OverQuest()
    {
        // 增加金币
        Ctrl_HeroProperty.Instance.AddGold(Quest.questReward.gold);
        //增加钻石
        Ctrl_HeroProperty.Instance.AddDiamods(Quest.questReward.diamod);
        //增加经验
        Ctrl_HeroProperty.Instance.AddExp(Quest.questReward.exp);
        //增加任务物品
        foreach (Model_Quest.QuestItem item in quest.questReward.questRewardItem)
        {
            for (int i = 0; i < item.itemNumber; i++)
            {
                Ctrl_InventoryManager.Instance.AddItem(item.itemId);
            }
        }

        //移除当前任务
        Ctrl_PlayerQuest.Instance.PlayQuestList.Remove(quest);
        Ctrl_QuestItemManager.Instance.ClearQuestItem(quest);
        //更新任务列表
        Ctrl_QuestItemManager.Instance.UpdateQuestItem();
        Quest.questSubmit = true;
    }
    /// <summary>
    /// 显示任务完成按钮
    /// </summary>
    public void ShowOver()
    {
        overQuest.SetActive(true);
    }
}