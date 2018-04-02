using System;
using System.Collections;
using UnityEngine;

public class Ctrl_Enemy_DropProperty : MonoBehaviour
{
    [SerializeField] private int rewardGold; //奖励金币
    [SerializeField] private int rewardExp; //奖励经验
    [SerializeField] private EnemyDropItem[] rewardItems; //奖励物品
    [SerializeField] private bool m_Random;

    void Start()
    {
        foreach (EnemyDropItem rewardItem in rewardItems)
        {
            if (rewardItem.Random)
            {
                Debug.Log(Ctrl_InventoryManager.Instance.GetItemById(3).maxStack);
                rewardItem.item = UnityEngine.Random.Range(1, Ctrl_InventoryManager.Instance.ItemList.Count - 1);
                rewardItem.number = UnityEngine.Random.Range(1, Ctrl_InventoryManager.Instance.GetItemById(rewardItem.item).maxStack);
            }
        }
    }


    public int RewardGold
    {
        get { return rewardGold; }
    }

    public int RewardExp
    {
        get { return rewardExp; }
    }

    public EnemyDropItem[] RewardItems
    {
        get
        {
            return rewardItems;
        }
    }

    [Serializable]
    public class EnemyDropItem
    {
        public bool Random;
        public int number;
        public int item;
    }
}