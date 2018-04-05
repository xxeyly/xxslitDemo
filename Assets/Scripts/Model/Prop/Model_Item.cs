using UnityEngine;

public class Model_Item
{
    [Header("基本信息")] public int id; //物品ID
    public string itemName; //物品名称
    public string itemType; //物品类型
    public string materialType; //材料类型
    public int consumption; //消耗时间
    public string equipmentType; //装备类型
    public int maxStack; //上限个数
    public int currentNumber; //当前数量
    public int buyPriceByGold; //购买金币价格
    public int buyPriceByDiamond; //购买砖石价格
    public int sellPriceByGold; //出售金币价格
    public int sellPriceByDiamond; //出售砖石价格
    public int minLevel; // 使用该物品所需的等级
    public bool sellable; //可供出售
    public bool tradable; //可供交易
    public bool destroyable; //可以销毁
    [TextArea(1, 30)] public string description; //文本区
    public string sprite;

    [Header("使用提升")] //属性回复
    public bool useDestroy; //使用摧毁

    public int useHealth; //提升健康
    public int useMagic; //提升魔法
    public int useExperience; //提升经验

    [Header("装备属性提升")] //装备提升
    public int equipHealthBonus; //健康

    public int equipManaBonus; //魔法
    public int equipDamageBonus; //攻击
    public int equipDefenseBonus; //防御
    public int equipSpeedcBonus; //速度
    public GameObject modelPrefab; //装备模型

    public override string ToString()
    {
        return string.Format(
            "Id: {0}, ItemName: {1}, ItemType: {2}, MaterialType: {3}, Consumption: {4}, EquipmentType: {5}, MaxStack: {6}, CurrentNumber: {7}, BuyPriceByGold: {8}, BuyPriceByDiamond: {9}, SellPriceByGold: {10}, SellPriceByDiamond: {11}, MinLevel: {12}, Sellable: {13}, Tradable: {14}, Destroyable: {15}, Description: {16}, Sprite: {17}, UseDestroy: {18}, UseHealth: {19}, UseMagic: {20}, UseExperience: {21}, EquipHealthBonus: {22}, EquipManaBonus: {23}, EquipDamageBonus: {24}, EquipDefenseBonus: {25}, EquipSpeedcBonus: {26}, ModelPrefab: {27}",
            id, itemName, itemType, materialType, consumption, equipmentType, maxStack, currentNumber, buyPriceByGold,
            buyPriceByDiamond, sellPriceByGold, sellPriceByDiamond, minLevel, sellable, tradable, destroyable,
            description, sprite, useDestroy, useHealth, useMagic, useExperience, equipHealthBonus, equipManaBonus,
            equipDamageBonus, equipDefenseBonus, equipSpeedcBonus, modelPrefab);
    }

    public string ItemInfo()
    {
        string tip = "";

        tip += "物品名称:" + itemName + "\n";
        tip += ItemTypeAndItemName();
        tip += "物品上限个数:" + maxStack + "\n";
        tip += "当前个数:" + currentNumber + "\n";
        if (sellPriceByGold != 0 && sellPriceByDiamond == 0)
        {
            tip += "出售价格:" + sellPriceByGold + "金币" + "\n";
        }

        if (sellPriceByGold == 0 && sellPriceByDiamond != 0)
        {
            tip += "出售价格:" + sellPriceByDiamond + "钻石" + "\n";
        }

        if (sellPriceByGold != 0 && sellPriceByDiamond != 0)
        {
            tip += "出售价格:" + sellPriceByGold + "金币," + sellPriceByDiamond + "钻石" + "\n";
        }

        tip += "使用等级:" + minLevel + "\n";
        if (EquipTootip() != "")
        {
            tip += EquipTootip();
        }

        if (ConsumableTootip() != "")
        {
            tip += ConsumableTootip();
        }

        if (useDestroy)
        {
            tip += "使用摧毁" + "\n";
        }

        if (tradable)
        {
            tip += "可以交易\n";
        }
        else
        {
            tip += "禁止交易\n";
        }

        if (sellable)
        {
            tip += "可以出售\n";
        }
        else
        {
            tip += "禁止出售\n";
        }

        if (destroyable)
        {
            tip += "可以丢弃\n";
        }
        else
        {
            tip += "不可丢弃";
        }

//        tip += "描述:"+ description;
        return tip;
    }

    private string ItemTypeAndItemName()
    {
        string consumableTootip = "";
        switch (itemType)
        {
            case "Drugs":
                consumableTootip += "物品类型:消耗品\n";
                break;
            case "Equipment":
                consumableTootip += "物品类型:装备\n";
                break;
            case"Pet":
                consumableTootip += "物品类型:宠物\n";
                break;
            case "Material":
                consumableTootip += "物品类型:材料\n";
                break;
            case "Props":
                consumableTootip += "物品类型:道具\n";
                break;
            default:
                break;
        }

        return consumableTootip;
    }

    private string EquipTootip()
    {
        string tip = "";
        if (equipHealthBonus != 0)
        {
            tip += "生命上限+" + equipHealthBonus.ToString() + "\n";
        }

        if (equipManaBonus != 0)
        {
            tip += "魔法上限+" + equipManaBonus.ToString() + "\n";
        }

        if (equipDamageBonus != 0)
        {
            tip += "攻击:" + equipDamageBonus.ToString() + "\n";
        }

        if (equipDefenseBonus != 0)
        {
            tip += "防御:" + equipDefenseBonus.ToString() + "\n";
        }

        if (equipSpeedcBonus != 0)
        {
            tip += "速度:" + equipSpeedcBonus.ToString() + "\n";
        }

        return tip;
    }

    private string ConsumableTootip()
    {
        string tip = "";
        if (useHealth != 0)
        {
            tip += "生命恢复:" + useHealth.ToString() + "\n";
        }

        if (useMagic != 0)
        {
            tip += "魔法恢复:" + useMagic.ToString() + "\n";
        }

        if (useExperience != 0)
        {
            tip += "经验+" + useExperience.ToString() + "\n";
        }

        return tip;
    }
}