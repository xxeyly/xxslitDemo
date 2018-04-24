using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

public class Ctrl_InventoryManager : Singleton<Ctrl_InventoryManager>
{
    [SerializeField] private TextAsset itemJson;

    /// <summary>
    ///  物品信息的列表（集合）
    /// </summary>
    private List<Model_Item> itemList;

    /// <summary>
    /// 背包种存在物品的格子
    /// </summary>
    /// <returns></returns>
    public List<Ctrl_Slot> ActiveSlot()
    {
        return Ctrl_SlotManager.Instance.GetAllActiveSlot();
    }

    /// <summary>
    /// 所有格子集合
    /// </summary>
    public Ctrl_Slot[] slotList
    {
        get { return Ctrl_SlotManager.Instance.SlotList; }
    }

    /// <summary>
    /// 获得已经存在的物品格子,并且未满
    /// </summary>
    /// <returns></returns>
    public List<Model_Item> GetUnfillList()
    {
        return Ctrl_SlotManager.Instance.GetUnfilledMaxSlot();
    }

    /// <summary>
    /// 获得相同物品未满的格子
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public List<Model_Item> GetIdenticalSlot(Model_Item item)
    {
        return Ctrl_SlotManager.Instance.GetIdenticalSlot(item);
    }

    /// <summary>
    /// 返回已经存个相同物品的格子的剩余数量之和
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public int GetIdenticaAmount(Model_Item item)
    {
        return Ctrl_SlotManager.Instance.GetIdenticaAmount(item);
    }

    /// <summary>
    /// 获得空的格子
    /// </summary>
    public Ctrl_Slot emptySlot
    {
        get { return Ctrl_SlotManager.Instance.GetEmptySlot(); }
    }


    public List<Model_Item> ItemList
    {
        get { return itemList; }
        set { itemList = value; }
    }

    //添加物品
    public bool AddItem(int itemId)
    {
        Model_Item item = GetItemById(itemId);
        if (ActiveSlot() != null)
        {
            foreach (Ctrl_Slot slot in ActiveSlot())
            {
                //添加的物品已经存在在背包中,并且物品没有达到格子上限
                if (slot.Item != null && slot.Item.id == item.id)
                {
                    if (slot.Item.currentNumber < slot.Item.maxStack)
                    {
                        slot.Item.currentNumber += 1;
                        slot.UpdateAmount();
                        //检测执行的任务中是否是添加的物品
                        foreach (Model_Quest quest in Ctrl_PlayerQuest.Instance.PlayQuestList)
                        {
                            foreach (Model_Quest.QuestItem questItem in quest.questItem)
                            {
                                if (questItem.itemId == itemId)
                                {
                                    if (Ctrl_QuestItemManager.Instance.GetSelectOverlay().Quest.id == quest.id)
                                    {
                                        Ctrl_QuestItemManager.Instance.ShowQuestInfo(quest);
                                    }
                                }
                            }
                        }

                        return true;
                    }
                }
            }
        }

        if (emptySlot != null)
        {
            emptySlot.Item = Ctrl_InventoryManager.Instance.NewItem(item.id);
            //检测执行的任务中是否是添加的物品
            foreach (Model_Quest quest in Ctrl_PlayerQuest.Instance.PlayQuestList)
            {
                foreach (Model_Quest.QuestItem questItem in quest.questItem)
                {
                    if (questItem.itemId == itemId)
                    {
                        if (Ctrl_QuestItemManager.Instance.GetSelectOverlay().Quest.id == quest.id)
                        {
                            Ctrl_QuestItemManager.Instance.ShowQuestInfo(quest);
                        }
                    }
                }
            }

            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 获得背包中存在多少个指定ID的物品
    /// </summary>
    /// <param name="itemId"></param>
    /// <returns></returns>
    public int GetItemCount(int itemId)
    {
        return Ctrl_SlotManager.Instance.GetItemCount(itemId);
    }

    protected override void Awake()
    {
        base.Awake();
        ParseItemJson();
    }

    /// <summary>
    /// 解析Json物品数据
    /// </summary>
    private void ParseItemJson()
    {
        itemList = JsonMapper.ToObject<List<Model_Item>>(itemJson.text);
    }


    /// <summary>
    /// 根据Id获得指定的物品
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Model_Item GetItemById(int id)
    {
        foreach (Model_Item item in itemList)
        {
            if (item.id == id)
            {
                return item;
            }
        }

        return null;
    }

    /// <summary>
    /// 获得一个全新的Item地址
    /// </summary>
    /// <param name="itemID"></param>
    /// <returns></returns>
    public Model_Item NewItem(int itemID)
    {
        Model_Item item = Ctrl_InventoryManager.Instance.GetItemById(itemID);
        return new Model_Item(item.id, item.itemName, item.itemType, item.materialType, item.consumption,
            item.equipmentType, item.maxStack, item.currentNumber, item.buyPriceByGold, item.buyPriceByDiamond,
            item.sellPriceByGold, item.sellPriceByDiamond, item.minLevel, item.sellable, item.tradable,
            item.destroyable, item.description, item.sprite, item.useDestroy, item.useHealth, item.useMagic,
            item.useExperience, item.equipHealthBonus, item.equipManaBonus, item.equipDamageBonus,
            item.equipDefenseBonus, item.equipSpeedcBonus, item.modelPrefab, item.drawing, item.drawingItemId,
            item.makeTime);
    }
}