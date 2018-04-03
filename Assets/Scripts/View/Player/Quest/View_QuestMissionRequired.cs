using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class View_QuestMissionRequired : MonoBehaviour
{
    [SerializeField]private Text currentCount;
    [SerializeField]private Text questCount;
    [SerializeField]private Text questName;

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

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}