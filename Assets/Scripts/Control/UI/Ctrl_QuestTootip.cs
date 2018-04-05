using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ctrl_QuestTootip : MonoBehaviour
{
    private Model_Quest quest;
    [SerializeField] private Text questContent; //对话内容
    [SerializeField] private Button Accept;
    [SerializeField] private Button Abandon;
    [SerializeField] private Button Next;
    [SerializeField] private Text QuestReleaseNpcText;
    [SerializeField] private GameObject QuestReleaseNpcGo;
    [SerializeField] private GameObject Me;
    [SerializeField] private int playerTalkIndex; //英雄对话进度
    [SerializeField] private int npcTalkIndex; //NPC对话进度
    [SerializeField] private bool isNpcTalk; //Npc是否在说话
    [SerializeField] private Button Complete; //完成按钮
    private bool isInterval = false; //任务区间,正常接受任务为false,任务之中与NPC谈话为true

    public Model_Quest Quest
    {
        get { return quest; }

        set
        {
            quest = value;
            QuestInit();
        }
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void QuestInit()
    {
        //初始化任务
        if (!Quest.questAccept)
        {
            playerTalkIndex = 0; //角色对话进度
            npcTalkIndex = 0; //npc对话进度
            Accept.gameObject.SetActive(false); //初始化按钮
            Abandon.gameObject.SetActive(false); //初始化按钮
            Complete.gameObject.SetActive(false); //初始化按钮
            questContent.text = quest.questDialogueNpc[0]; //对话
            QuestReleaseNpcText.text = quest.questReleaseNpc; //对话标识(对话框右上角显示的npc名称)
            //npc对话只有一句,直接跳过对话,接受任务
            if (quest.questDialogueNpc.Length == 1)
            {
                Accept.gameObject.SetActive(true);
                Abandon.gameObject.SetActive(true);
            }
            else
            {
                npcTalkIndex++;
                Next.gameObject.SetActive(true);
                isNpcTalk = false;
            }
        }
        //已经接受过这个任务隐藏不需要的按钮
        else if (Quest.questState)
        {
            questContent.text = Quest.questUnfinished;
            Accept.gameObject.SetActive(false);
            Abandon.gameObject.SetActive(false);
            Next.gameObject.SetActive(false);
            Complete.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 完成按钮
    /// </summary>
    public void OnComplete()
    {
        Ctrl_TootipManager.Instance.HideQuest();
    }

    /// <summary>
    /// 接受任务
    /// </summary>
    public void OnAccept()
    {
        Quest.questAccept = true;
        Quest.questState = false;
        Quest.questSubmit = false;

        Ctrl_TootipManager.Instance.HideQuest(); //隐藏对话框
        Ctrl_TootipManager.Instance.ShowNotification(quest.questBriefIntroduction, 1f, "接受任务"); //弹出接受任务对话框
        Ctrl_PlayerQuest.Instance.PlayQuestList.Add(quest); //添加到任务列表
        Ctrl_QuestItemManager.Instance.UpdateQuestItem(); //更新任务列表
    }

    /// <summary>
    /// 取消任务
    /// </summary>
    public void OnAbandon()
    {
        gameObject.SetActive(false);
        QuestInit();
        Accept.gameObject.SetActive(false);
        Abandon.gameObject.SetActive(false);
    }

    /// <summary>
    /// 继续对话  
    /// </summary>
    public void OnNext()
    {
        if (isNpcTalk)
        {
            if (npcTalkIndex < quest.questDialogueNpc.Length)
            {
                Me.gameObject.SetActive(false); //隐藏角色图标
                QuestReleaseNpcGo.SetActive(true); //显示NPC图标
                questContent.text = quest.questDialogueNpc[npcTalkIndex]; //设置当前对话
                npcTalkIndex++; //npc对话进度+1
                isNpcTalk = false;
                if (npcTalkIndex == quest.questDialogueNpc.Length)
                {
                    Accept.gameObject.SetActive(true);
                    Abandon.gameObject.SetActive(true);
                    Next.gameObject.SetActive(false);
                }
            }
            else
            {
                Accept.gameObject.SetActive(true);
                Abandon.gameObject.SetActive(true);
                Next.gameObject.SetActive(false);
            }
        }
        else
        {
            //角色跟npc有对话
            if (quest.questDialoguePlayer.Length > 0)
            {
                Me.gameObject.SetActive(true); //显示角色对话
                QuestReleaseNpcGo.SetActive(false); //隐藏npc对话
                questContent.text = quest.questDialoguePlayer[playerTalkIndex]; //设置对话内容
                playerTalkIndex++; //角色对话进度+1
                isNpcTalk = true; //轮到npc讲话了
            }
        }
    }
    /// <summary>
    /// 任务需求中与NPC谈话内容,只有一句
    /// </summary>
    /// <param name="quest"></param>
    public void TalkInMission(Model_Quest.QuestNPC quest)
    {
        Complete.gameObject.SetActive(true); //显示完成按钮
        Accept.gameObject.SetActive(false); //隐藏接受按钮
        Abandon.gameObject.SetActive(false); //隐藏取消按钮
        questContent.text = quest.questDialogueNpc[0]; //NPC说话内容
        QuestReleaseNpcText.text = GlobalParametr.GetNpcName(quest.npcId); //对话显示角色
    }
}