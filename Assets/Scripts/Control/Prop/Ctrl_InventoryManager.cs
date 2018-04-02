using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

public class Ctrl_InventoryManager : Singleton<Ctrl_InventoryManager>
{
    [SerializeField] private TextAsset itemJson;
    [SerializeField] private Canvas canvas;
    public GameObject Tootip;
    public GameObject PickUpItem;
    public bool isToolTipShow = false;

    //Tootip偏移
    private Vector2 toolTipPosionOffset = new Vector2(10, -10);

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

    public bool IsPickedItem { get; set; }

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
                        return true;
                    }
                }
            }
        }

        if (emptySlot != null)
        {
            emptySlot.Item = new Model_Item
            {
                id = item.id,
                itemName = item.itemName,
                itemType = item.itemType,
                equipmentType = item.equipmentType,
                maxStack = item.maxStack,
                currentNumber = item.currentNumber,
                buyPriceByGold = item.buyPriceByGold,
                buyPriceByDiamond = item.buyPriceByDiamond,
                sellPriceByGold = item.sellPriceByGold,
                sellPriceByDiamond = item.sellPriceByDiamond,
                minLevel = item.minLevel,
                sellable = item.sellable,
                tradable = item.tradable,
                destroyable = item.destroyable,
                description = item.description,
                sprite = item.sprite,
                useDestroy = item.useDestroy,
                useHealth = item.useHealth,
                useMagic = item.useMagic,
                useExperience = item.useExperience,
                equipHealthBonus = item.equipHealthBonus,
                equipManaBonus = item.equipManaBonus,
                equipDamageBonus = item.equipDamageBonus,
                equipDefenseBonus = item.equipDefenseBonus,
                equipSpeedcBonus = item.equipSpeedcBonus,
                modelPrefab = item.modelPrefab
            };

            return true;
        }
        else
        {
            return false;
        }
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

    private void Update()
    {
        if (IsPickedItem)
        {
            //如果我们捡起了物品，我们就要让物品跟随鼠标
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,
                Input.mousePosition, canvas.worldCamera, out position);
            PickUpItem.GetComponent<Ctrl_PickUp>().SetLocalPosition(position);
        }

        if (isToolTipShow)
        {
            //控制提示面板跟随鼠标
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,
                Input.mousePosition, canvas.worldCamera, out position);
            Tootip.GetComponent<View_ToolTip>().SetLocalPotion(position + toolTipPosionOffset);
        }
        else
        {
            Tootip.GetComponent<View_ToolTip>().Hide();
        }
    }
}