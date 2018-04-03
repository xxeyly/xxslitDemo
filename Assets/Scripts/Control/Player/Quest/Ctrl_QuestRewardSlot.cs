using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_QuestRewardSlot : MonoBehaviour
{
    private Model_Item item;

    public Model_Item Item
    {
        get { return item; }

        set
        {
            item = value;
            if (value != null)
            {
                GetComponent<View_QuestRewardSlot>().InitSlot();
            }
        }
    }

    void Start()
    {
    }

    public void ShowGoldView(int goldCount)
    {
        GetComponent<View_QuestRewardSlot>().ShowGoldView(goldCount);
    }

    public void ShowDiamodView(int diamodCount)
    {
        GetComponent<View_QuestRewardSlot>().ShowDiamodView(diamodCount);
    }

    public void ShowExpView(int expCount)
    {
        GetComponent<View_QuestRewardSlot>().ShowExpView(expCount);
    }

    void Update()
    {
    }
}