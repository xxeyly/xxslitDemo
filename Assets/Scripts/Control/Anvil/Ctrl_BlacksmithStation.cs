using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_BlacksmithStation : Singleton<Ctrl_BlacksmithStation>
{
    /// <summary>
    /// 获得所有的"铁匠台"格子
    /// </summary>
    public List<Ctrl_Slot> BlacksmithStationList
    {
        get { return new List<Ctrl_Slot>(GetComponentsInChildren<Ctrl_Slot>()); }
    }

    /// <summary>
    /// 获得铁匠台物品中包含指定Id的物品总共有多少个
    /// </summary>
    /// <param name="itemId"></param>
    /// <returns></returns>
    public int GetItemCountById(int itemId)
    {
        int itemCount = 0;
        foreach (Ctrl_Slot ctrlSlot in BlacksmithStationList)
        {
            if (ctrlSlot.Item != null)
            {
                if (ctrlSlot.Item.id == itemId)
                {
                    itemCount += ctrlSlot.Item.currentNumber;
                }
            }
        }

        return itemCount;
    }
    //添加物品
    public bool AddItem(int itemId)
    {
        Model_Item item = Ctrl_InventoryManager.Instance.GetItemById(itemId);
        if (GetAllActiveSlot() != null)
        {
            foreach (Ctrl_Slot slot in GetAllActiveSlot())
            {
                //添加的物品已经存在在"铁匠台"中,并且物品没有达到格子上限
                if (slot.Item != null && slot.Item.id == item.id)
                {
                    if (slot.Item.currentNumber < slot.Item.maxStack)
                    {
                        slot.Item.currentNumber += 1;
                        slot.UpdateAmount();
                        return true;
                    }
                }
            }
        }

        if (GetEmptySlot() != null)
        {
            GetEmptySlot().Item = Ctrl_InventoryManager.Instance.NewItem(item.id);
            //检测执行的任务中是否是添加的物品
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 获得"铁匠台所有存在物品的格子"
    /// </summary>
    /// <returns></returns>
    public List<Ctrl_Slot> GetAllActiveSlot()
    {
        List<Ctrl_Slot> activeSlot = new List<Ctrl_Slot>();
        foreach (Ctrl_Slot ctrlSlot in BlacksmithStationList)
        {
            if (ctrlSlot.Item != null)
            {
                activeSlot.Add(ctrlSlot);
            }
        }

        return activeSlot;
    }

    /// <summary>
    /// 获得空的格子
    /// </summary>
    /// <returns></returns>
    public Ctrl_Slot GetEmptySlot()
    {
        for (int i = 0; i < BlacksmithStationList.Count; i++)
        {
            if (BlacksmithStationList[i].Item == null)
            {
                return BlacksmithStationList[i];
            }
        }

        return null;
    }

    /// <summary>
    /// 移除物品
    /// </summary>
    /// <param name="drawings"></param>
    public void RemoveItem(Model_Item.Drawing[] drawings)
    {
        foreach (Model_Item.Drawing drawing in drawings)
        {
            int count = drawing.itemCount;
            foreach (Ctrl_Slot slot in GetItemById(drawing.itemId))
            {
                if (slot.Item.currentNumber <= count)
                {
                    count -= slot.Item.currentNumber;
                    slot.UpdateAmount();
                    slot.Item = null;
                }
                else
                {
                    slot.Item.currentNumber -= count;
                    slot.UpdateAmount();
                    break;
                }
            }
        }
    }

    /// <summary>
    /// 根据ID获得指定物品
    /// </summary>
    /// <param name="itemID"></param>
    /// <returns></returns>
    public List<Ctrl_Slot> GetItemById(int itemID)
    {
        List<Ctrl_Slot> slotList = new List<Ctrl_Slot>();
        foreach (Ctrl_Slot ctrlSlot in GetAllActiveSlot())
        {
            if (ctrlSlot.Item.id == itemID)
            {
                slotList.Add(ctrlSlot);
            }
        }

        return slotList;
    }


}