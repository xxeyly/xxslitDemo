using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class View_QuestMissionRequired : MonoBehaviour
{
    [SerializeField] private Text currentCount;
    [SerializeField] private Text questCount;
    [SerializeField] private Text questName;
    [SerializeField] private Image mark;

    public Text CurrentCount
    {
        get { return currentCount; }

        set { currentCount = value; }
    }

    public Text QuestCount
    {
        get { return questCount; }

        set { questCount = value; }
    }

    public Text QuestName
    {
        get { return questName; }

        set { questName = value; }
    }

    public void HideMark()
    {
        mark.gameObject.SetActive(false);
    }

    public void ShowMark()
    {
        mark.gameObject.SetActive(true);
    }
}