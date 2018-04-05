using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_PlayerQuest : Singleton<Ctrl_PlayerQuest>
{
    private List<Model_Quest> playQuestList;//所有接受的任务,在任务对话时只有接受任务才会存储

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

    private void Start()
    {
    }

    private void Update()
    {
        StartCoroutine(LoopCheckQuest());
    }

    /// <summary>
    /// 每隔1s检测任务当前是否完成
    /// </summary>
    /// <returns></returns>
    IEnumerator LoopCheckQuest()
    {
        while (PlayQuestList.Count >= 1)
        {
            yield return new WaitForSeconds(1f);
            foreach (Model_Quest quest in PlayQuestList)
            {
                //已经是完成状态的任务不参与
                if (!quest.questSubmit)
                {
                    bool skillOver = false;//任务击杀目标是否完成
                    foreach (Model_Quest.QuestEnemy enemy in quest.questKillEnemy)
                    {
                        if (enemy.enemyId != -1)
                        {
                            if (enemy.currentEnemyNumber >= enemy.enemyNumber)
                            {
                                skillOver = true;
                            }
                            else
                            {
                                skillOver = false;
                            }
                        }
                        else
                        {
                            skillOver = true;
                        }
                    }

                    bool talkOver = false;//任务谈话是否完成
                    foreach (Model_Quest.QuestNPC npc in quest.questTalkNPC)
                    {
                        if (npc.npcId != -1)
                        {
                            if (npc.isTalk)
                            {
                                talkOver = true;
                            }
                            else
                            {
                                talkOver = false;
                            }
                        }
                        else
                        {
                            talkOver = true;
                        }
                    }

                    bool itemOver = false;//任务寻找物品是否完成
                    foreach (Model_Quest.QuestItem item in quest.questItem)
                    {
                        if (item.itemId != -1)
                        {
                            if (Ctrl_InventoryManager.Instance.GetItemCount(item.itemId) >= item.itemNumber)
                            {
                                itemOver = true;
                            }
                            else
                            {
                                itemOver = false;
                            }
                        }
                        else
                        {
                            itemOver = true;
                        }
                    }

//                    Debug.Log("击杀:" + skillOver + "谈话:" + talkOver + "物品" + itemOver);
                    if (skillOver && talkOver && itemOver)
                    {
                        quest.questState = true;
                        //如果当前选择显示的任务跟已完成的任务相同 显示任务完成按钮
                        if (Ctrl_QuestItemManager.Instance.GetSelectOverlay().Quest.id == quest.id)
                        {
                            Ctrl_QuestItemManager.Instance.ShowOver();
                        }

                        //循环遍历所有的任务 如果跟完成的任务id相同 判断已经完成任务
                        foreach (Ctrl_QuestItem questItem in Ctrl_QuestItemManager.Instance.QuestItems)
                        {
                            //如果ID相同 设置当前任务为完成状态
                            if (quest.id == questItem.Quest.id)
                            {
                                questItem.SHOverMaker(true);
                            }
                        }
                    }
                    else
                    {
                        quest.questState = false;//如果当前任务未完成,隐藏任务完成的标识
                        foreach (Ctrl_QuestItem questItem in Ctrl_QuestItemManager.Instance.QuestItems)
                        {
                            //如果ID相同 设置当前任务为完成状态
                            if (quest.id == questItem.Quest.id)
                            {
                                questItem.SHOverMaker(false);
                            }
                        }
                    }
                }
            }
        }
    }
}