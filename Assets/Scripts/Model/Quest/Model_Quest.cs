using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///
public class Model_Quest : MonoBehaviour
{
    public int id; //任务ID
    public string questReleaseNpc; //发布任务的Npc
    public int questOverNpc;//提交任务的NPC
    public string questBriefIntroduction; //任务简介
    public string questSescribe; //任务描述
    public string[] questDialogueNpc; //任务对话
    public string[] questDialoguePlayer; //任务对话
    public string questUnfinished; //任务未完成是提示
    public int[] questBranch; //任务分支
    public int questLevelLimit; //任务等级限制
    public QuestNPC[] questTalkNPC; //任务完成需要对话的NPC
    public bool questAccept; //任务接受状态
    public bool questState; //任务完成状态
    public bool questSubmit; //任务提交
    public QuestEnemy[] questKillEnemy; //完成任务所需要击杀的Enemy
    public QuestItem[] questItem; //完成任务所需的任务物品
    public QuestReward questReward; //任务完成的奖励

    public class QuestNPC
    {
        public int npcId; //谈话NPCID
        public bool isTalk; //是否已经谈过话
        public string[] questDialogueNpc; //任务对话
    }

    public class QuestEnemy
    {
        public int enemyId; //击杀的怪物类型
        public int enemyNumber; //击杀的怪物数量
        public int currentEnemyNumber; //当前击杀数量
    }

    public class QuestItem
    {
        public int itemId; //所需物品Id;
        public int itemNumber; //任务所需物品的个数
    }

    public class QuestReward
    {
        public int gold; // 任务奖励金币
        public int diamod; // 任务奖励钻石
        public int exp; //任务奖励经验
        public QuestItem[] questRewardItem; //任务奖励物品
    }

    public override string ToString()
    {
        return string.Format(
            "{0}, Id: {1}, QuestBriefIntroduction: {2}, QuestSescribe: {3}, QuestDialogueNpc: {4}, QuestDialoguePlayer: {5}, QuestBranch: {6}, QuestLevelLimit: {7}, QuestTalkNpc: {8}, QuestState: {9}, QuestKillEnemy: {10}, QuestItem: {11}, QuestReward: {12}",
            base.ToString(), id, questBriefIntroduction, questSescribe, questDialogueNpc, questDialoguePlayer,
            questBranch, questLevelLimit, questTalkNPC, questState, questKillEnemy, questItem, questReward);
    }
}