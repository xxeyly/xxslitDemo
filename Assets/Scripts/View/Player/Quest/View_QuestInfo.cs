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

    public Model_Quest Quest
    {
        get { return quest; }

        set
        {
            quest = value;
            InitQuest();
        }
    }

    void Start()
    {
    }


    void Update()
    {
    }

    public void InitQuest()
    {
        questBriefIntroduction.text = quest.questBriefIntroduction;
        questSescribe.text = quest.questSescribe;
        ClearQuestDemandAndReward();
        if (quest.questKillEnemy.Length >= 1)
        {
            foreach (Model_Quest.QuestEnemy enemy in quest.questKillEnemy)
            {
                GameObject MissionRequired = Resources.Load<GameObject>("Prefabs/UI/Quest/MissionRequired");
                View_QuestMissionRequired missionRequired = MissionRequired.GetComponent<View_QuestMissionRequired>();
                missionRequired.CurrentCount.text = enemy.currentEnemyNumber.ToString();
                missionRequired.QuestCount.text = enemy.enemyNumber.ToString();
                missionRequired.QuestName.text = enemy.enemyId.ToString();
                missionRequired.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                Instantiate(MissionRequired).transform.parent = questDemandTransform;
            }
        }

        if (quest.questReward.gold >= 0)
        {
            GameObject QuestRewardSlot = Resources.Load<GameObject>("Prefabs/UI/Quest/QuestRewardSlot");
            QuestRewardSlot.GetComponent<Ctrl_QuestRewardSlot>().ShowGoldView(quest.questReward.gold);
            QuestRewardSlot.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            Instantiate(QuestRewardSlot).transform.parent = questRewardTransform;
        }

        if (quest.questReward.diamod >= 0)
        {
            GameObject QuestRewardSlot = Resources.Load<GameObject>("Prefabs/UI/Quest/QuestRewardSlot");
            QuestRewardSlot.GetComponent<Ctrl_QuestRewardSlot>().ShowDiamodView(quest.questReward.gold);
            QuestRewardSlot.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            Instantiate(QuestRewardSlot).transform.parent = questRewardTransform;
        }

        if (quest.questReward.exp >= 0)
        {
            GameObject QuestRewardSlot = Resources.Load<GameObject>("Prefabs/UI/Quest/QuestRewardSlot");
            QuestRewardSlot.GetComponent<Ctrl_QuestRewardSlot>().ShowExpView(quest.questReward.gold);
            QuestRewardSlot.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            Instantiate(QuestRewardSlot).transform.parent = questRewardTransform;
        }

        if (quest.questReward.questRewardItem.Length >= 1)
        {
            foreach (Model_Quest.QuestItem item in quest.questReward.questRewardItem)
            {
                GameObject QuestRewardSlot = Resources.Load<GameObject>("Prefabs/UI/Quest/QuestRewardSlot");
                QuestRewardSlot.GetComponent<Ctrl_QuestRewardSlot>().Item =
                    Ctrl_InventoryManager.Instance.GetItemById(item.itemId);
                QuestRewardSlot.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                Instantiate(QuestRewardSlot).transform.parent = questRewardTransform;
            }
        }
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
}