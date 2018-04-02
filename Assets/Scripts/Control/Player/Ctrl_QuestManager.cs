using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

public class Ctrl_QuestManager : Singleton<Ctrl_QuestManager>
{
    [SerializeField] private TextAsset questjson;
    private List<Model_Quest> questList;

    protected override void Awake()
    {
        base.Awake();
        ParseQuestJson();
    }


    void Start()
    {
    }

    public Model_Quest GetQuest(int questId)
    {
        foreach (Model_Quest quest in questList)
        {
            if (quest.id == questId)
            {
                return quest;
            }
        }

        return null;
    }

    void Update()
    {
    }

    /// <summary>
    /// 解析Json物品数据
    /// </summary>
    private void ParseQuestJson()
    {
        questList = JsonMapper.ToObject<List<Model_Quest>>(questjson.text);
    }
}