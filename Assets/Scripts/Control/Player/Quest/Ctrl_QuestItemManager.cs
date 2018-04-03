using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_QuestItemManager : Singleton<Ctrl_QuestItemManager>
{
    [SerializeField] private Sprite downSprite;
    [SerializeField] private Sprite upSprite;
    private View_QuestItem[] questItems;
    [SerializeField] private GameObject questInfoGo;
    [SerializeField] private Transform questItemsTransform;
    private View_QuestInfo questInfo;

    void Start()
    {
        questItems = transform.GetComponentsInChildren<View_QuestItem>();
        questInfo = this.questInfoGo.GetComponent<View_QuestInfo>();
    }

    public View_QuestItem GetSelectOverlay()
    {
        if (questItems != null)
        {
            foreach (View_QuestItem questItem in questItems)
            {
                if (questItem.SelectOverlay.activeSelf)
                {
                    return questItem;
                }
            }
        }


        return null;
    }

    public void HideAllSelectOverlay()
    {
        if (questItems != null)
        {
            foreach (View_QuestItem questItem in questItems)
            {
                questItem.SelectOverlay.SetActive(false);
            }
        }
    }

    public void ShowQuestInfo(Model_Quest quest)
    {
        questInfo.Quest = quest;
    }

    public void UpdateQuestItem()
    {
        foreach (Model_Quest quest in Ctrl_PlayerQuest.Instance.PlayQuestList)
        {
            GameObject questItem = Resources.Load<GameObject>("Prefabs/UI/Quest/Quest");
            questItem.GetComponent<Ctrl_QuestItem>().Quest = quest;
            questItem = Instantiate(questItem);
            questItem.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
            questItem.transform.parent = questItemsTransform;
        }
    }
}