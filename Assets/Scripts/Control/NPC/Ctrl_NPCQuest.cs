using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_NPCQuest : MonoBehaviour
{
    private List<Model_Quest> questList;
    [SerializeField] private int[] quests;

    public int[] Quests
    {
        get
        {
            return quests;
        }

        set
        {
            quests = value;
        }
    }

    public List<Model_Quest> QuestList
    {
        get
        {
            return questList;
        }

        set
        {
            questList = value;
        }
    }

    private void Start()
    {
        QuestList = new List<Model_Quest>();
        for (int i = 0; i < Quests.Length; i++)
        {
            Model_Quest quest = Ctrl_QuestManager.Instance.GetQuest(Quests[i]);
            QuestList.Add(quest);
        }
    }


    private void OnMouseDown()
    {
        foreach (Model_Quest quest in QuestList)
        {
            if (!quest.questSubmit)
            {
                Ctrl_TootipManager.Instance.ShowQuest(quest);
                break;
            }
        }
    }

}