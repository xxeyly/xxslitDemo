using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test_CompleteQuest : MonoBehaviour
{
    [SerializeField] private InputField questField;
    [SerializeField]private Ctrl_NPCQuest npcQuest;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CompleteQuest()
    {
        npcQuest.QuestList[Int32.Parse(questField.text)].questSubmit = true;
    }
}