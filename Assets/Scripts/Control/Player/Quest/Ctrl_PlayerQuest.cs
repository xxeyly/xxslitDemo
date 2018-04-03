using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_PlayerQuest : Singleton<Ctrl_PlayerQuest>
{
    private List<Model_Quest> playQuestList;

    public List<Model_Quest> PlayQuestList
    {
        get { return playQuestList; }

        set { playQuestList = value; }
    }

    protected override void Awake()
    {
        base.Awake();
        playQuestList = new List<Model_Quest>();
    }

    void Start()
    {

    }

    void Update()
    {
        if (PlayQuestList.Count >= 1)
        {
            Debug.Log(PlayQuestList[0].questKillEnemy[0].enemyNumber);
            Debug.Log(PlayQuestList[0].questKillEnemy[0].currentEnemyNumber);
        }
    }
}