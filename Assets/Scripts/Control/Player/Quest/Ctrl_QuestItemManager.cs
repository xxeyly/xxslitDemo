using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_QuestItemManager : Singleton<Ctrl_QuestItemManager>
{
    private Ctrl_QuestItem[] questItems;
    [SerializeField] private GameObject questInfoGo;
    [SerializeField] private Transform questItemsTransform;
    private View_QuestInfo questInfo;

    public Ctrl_QuestItem[] QuestItems
    {
        get { return questItems; }

        set { questItems = value; }
    }

    void Start()
    {
//        questItems = transform.GetComponentsInChildren<View_QuestItem>();
        questInfo = this.questInfoGo.GetComponent<View_QuestInfo>();
    }

    /// <summary>
    /// 获得选中状态的任务
    /// </summary>
    /// <returns></returns>
    public Ctrl_QuestItem GetSelectOverlay()
    {
        if (QuestItems != null)
        {
            foreach (Ctrl_QuestItem questItem in QuestItems)
            {
                if (questItem.GetSelectOverlay())
                {
                    return questItem;
                }
            }
        }

        return null;
    }

    /// <summary>
    /// 隐藏所有选中状态
    /// </summary>
    public void HideAllSelectOverlay()
    {
        if (QuestItems != null)
        {
            foreach (Ctrl_QuestItem questItem in GetQuestItem())
            {
                questItem.SetSelectOverlay(false);
            }
        }
    }

    /// <summary>
    /// 任务详情赋值
    /// </summary>
    /// <param name="quest"></param>
    public void ShowQuestInfo(Model_Quest quest)
    {
        questInfoGo.SetActive(true);
        questInfo.Quest = quest;
    }
    /// <summary>
    /// 显示任务完成
    /// </summary>
    public void ShowOver()
    {
        questInfo.ShowOver();
    }
    /// <summary>
    /// 更新QuestItem列表,接受任务时更新
    /// </summary>
    public void UpdateQuestItem()
    {
        foreach (Model_Quest quest in Ctrl_PlayerQuest.Instance.PlayQuestList)
        {
            //如果添加的任务Item没有存在,则可以继续添加
            if (!IsSameExistenceQuest(quest))
            {
                GameObject questItem = Resources.Load<GameObject>("Prefabs/UI/Quest/Quest");
                questItem = Instantiate(questItem);
                questItem.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                questItem.transform.parent = questItemsTransform;
                questItem.GetComponent<Ctrl_QuestItem>().Quest = quest;
            }
            //任务初始化 如果只接了一个任务 默认显示第一个任务
            if (GetQuestItem().Length == 1)
            {
                ShowQuestInfo(quest);
                GetQuestItem()[0].SetSelectOverlay(true);
            }
        }
    }

    /// <summary>
    /// 清空当前的QuestItem
    /// </summary>
    public void ClearQuestItem(Model_Quest quest)
    {
        foreach (Ctrl_QuestItem questItem in QuestItems)
        {
            if (questItem.Quest.id == quest.id)
            {
                Destroy(questItem.gameObject);
            }
        }
        questInfoGo.SetActive(false);
    }

    /// <summary>
    /// 获得所有已接受任务的Item
    /// </summary>
    /// <returns></returns>
    private Ctrl_QuestItem[] GetQuestItem()
    {
        return QuestItems = transform.GetComponentsInChildren<Ctrl_QuestItem>();
    }

    /// <summary>
    /// 是否存在相同的任务 
    /// </summary>
    /// <param name="quest"></param>
    /// <returns></returns>
    private bool IsSameExistenceQuest(Model_Quest quest)
    {
        bool isExist = false;
        foreach (var QuestItem in GetQuestItem())
        {
            if (QuestItem.Quest.id == quest.id)
            {
                return isExist = true;
            }
        }

        return isExist;
    }
}