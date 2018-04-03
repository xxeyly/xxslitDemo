using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ctrl_QuestItem : MonoBehaviour, IPointerDownHandler
{
    private Model_Quest quest;

    public Model_Quest Quest
    {
        get { return quest; }

        set
        {
            quest = value;
            Ctrl_QuestItemManager.Instance.ShowQuestInfo(quest);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            GetComponent<View_QuestItem>().ShowSelectOverlay();
        }
    }
}