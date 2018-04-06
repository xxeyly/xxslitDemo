using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ctrl_CookingProductsSlot : MonoBehaviour, IPointerDownHandler
{
    private Model_Item item;

    public Model_Item Item
    {
        get { return item; }

        set
        {
            item = value;
            GetComponent<View_CookingProductsSlot>().InitView(value);
        }
    }
    /// <summary>
    /// 更新个数显示
    /// </summary>
    public void UpdateAmount()
    {
        GetComponent<View_CookingProductsSlot>().UpdateAmount(Item);
    }

    /// <summary>
    /// 只能拿取 不能拖放
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        //手上的物品
        Ctrl_PickUp pickUp = Ctrl_InventoryManager.Instance.PickUpItem.GetComponent<Ctrl_PickUp>();
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            //手上无物品
            if (pickUp.Item == null)
            {
                pickUp.Item = Item;
                Item = null;
            }
        }
    }
    /// <summary>
    /// 添加物品 食材成熟后如果当前格子为空 则新开辟一个物品地址,不为空就直接添加个数即可
    /// </summary>
    /// <param name="itemID"></param>
    public void AddItem(int itemID)
    {
        //根据ID找到与之对应的物品 如烧烤的是一块生肉 那么成品应是一块熟肉
        Model_Item item = Ctrl_InventoryManager.Instance.GetItemById(GlobalParametr.GetCookingProduct(itemID));

        if (Item == null)
        {
            Item = new Model_Item
            {
                id = item.id,
                itemName = item.itemName,
                itemType = item.itemType,
                equipmentType = item.equipmentType,
                materialType = item.materialType,
                consumption = item.consumption,
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
        }
        else
        {
            if (this.item.currentNumber < item.maxStack)
            {
                this.item.currentNumber += 1;
                UpdateAmount();
            }
        }
    }
}