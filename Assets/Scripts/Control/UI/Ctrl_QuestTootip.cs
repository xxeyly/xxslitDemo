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
        if (!Quest.questAccept)
        {
            playerTalkIndex = 0;
            npcTalkIndex = 0;
            isNpcTalk = true;
            Accept.gameObject.SetActive(false);
            Abandon.gameObject.SetActive(false);
            Complete.gameObject.SetActive(false);
            questContent.text = quest.questDialogueNpc[0];
            QuestReleaseNpcText.text = quest.questReleaseNpc;
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
        Quest.questState = true;
        Quest.questSubmit = false;
        Ctrl_TootipManager.Instance.HideQuest();
        Ctrl_TootipManager.Instance.ShowNotification(quest.questBriefIntroduction, 1f, "接受任务");
        Ctrl_PlayerQuest.Instance.PlayQuestList.Add(quest);
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
                Me.gameObject.SetActive(false);
                QuestReleaseNpcGo.SetActive(true);
                questContent.text = quest.questDialogueNpc[npcTalkIndex];
                npcTalkIndex++;
                isNpcTalk = true;
                if (npcTalkIndex <= quest.questDialogueNpc.Length)
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
            if (quest.questDialoguePlayer.Length > 0)
            {
                Me.gameObject.SetActive(true);
                QuestReleaseNpcGo.SetActive(false);
                questContent.text = quest.questDialoguePlayer[playerTalkIndex];
                playerTalkIndex++;
                isNpcTalk = true;
            }
        }
    }
}