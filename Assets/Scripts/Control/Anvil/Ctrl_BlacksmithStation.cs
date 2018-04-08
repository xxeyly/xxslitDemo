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
}