using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_SlotManager : Singleton<Ctrl_SlotManager>
{
    private Ctrl_Slot[] slotList;

    public Ctrl_Slot[] SlotList
    {
        get { return slotList; }

        set { slotList = value; }
    }

    private List<Ctrl_Slot> activeSlot;

    public List<Ctrl_Slot> GetAllActiveSlot()
    {
        activeSlot = new List<Ctrl_Slot>();
        for (int i = 0; i < SlotList.Length; i++)
        {
            if (slotList[i].Item != null)
            {
                activeSlot.Add(slotList[i]);
            }
        }

        return activeSlot;
    }

    /// <summary>
    /// 获得已经存在但未达到上限的格子
    /// </summary>
    /// <returns></returns>
    public List<Model_Item> GetUnfilledMaxSlot()
    {
        List<Model_Item> unfilldList = new List<Model_Item>();
        if (activeSlot != null)
        {
            foreach (Ctrl_Slot ctrlSlot in activeSlot)
            {
                if (ctrlSlot.Item != null)
                {
                    if (ctrlSlot.Item.currentNumber < ctrlSlot.Item.maxStack)
                    {
                        unfilldList.Add(ctrlSlot.Item);
                    }
                }
            }
        }

        return unfilldList;
    }

    /// <summary>
    /// 所有存在并且物品未达到上限的格子中筛选指定物品未满的格子
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public List<Model_Item> GetIdenticalSlot(Model_Item item)
    {
        List<Model_Item> itemList = new List<Model_Item>();
        foreach (Model_Item Item in GetUnfilledMaxSlot())
        {
            if (item.id == Item.id)
            {
                itemList.Add(Item);
            }
        }

        return itemList;
    }

    /// <summary>
    /// 所有存在并且物品未达到上限的格子中筛选指定物品未满的格子可以存储的个数
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public int GetIdenticaAmount(Model_Item item)
    {
        int amount = 0;
        foreach (Model_Item modelItem in GetIdenticalSlot(item))
        {
            amount += (modelItem.maxStack - modelItem.currentNumber);
        }

        return amount;
    }

    void Start()
    {
        slotList = gameObject.GetComponentsInChildren<Ctrl_Slot>();
    }

    /// <summary>
    /// 获得空的格子
    /// </summary>
    /// <returns></returns>
    public Ctrl_Slot GetEmptySlot()
    {
        for (int i = 0; i < slotList.Length; i++)
        {
            if (slotList[i].Item == null)
            {
                return slotList[i];
            }
        }

        return null;
    }

    /// <summary>
    /// 获得背包中存在多少个指定ID的物品
    /// </summary>
    /// <param name="itemId"></param>
    /// <returns></returns>
    public int GetItemCount(int itemId)
    {
        int amount = 0;
        if (activeSlot != null)
        {
            foreach (Ctrl_Slot slot in activeSlot)
            {
                if (slot.Item.id==itemId)
                {
                    amount += slot.Item.currentNumber;
                }
            }
        }

        return amount;
    }

    void Update()
    {
    }

    protected void Awake()
    {
        base.Awake();
    }
}