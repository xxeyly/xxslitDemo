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
    public string modelPrefab; //装备模型

    public Drawing[] drawing;//制作所需物品
    public int drawingItemId;//设计图对应的ID
    public int makeTime;//制作需要的时间

    public class Drawing
    {
        public int itemId;
        public int itemCount;
    }

    public Model_Item()
    {
    }

    public Model_Item(int id, string itemName, string itemType, string materialType, int consumption,
        string equipmentType, int maxStack, int currentNumber, int buyPriceByGold, int buyPriceByDiamond,
        int sellPriceByGold, int sellPriceByDiamond, int minLevel, bool sellable, bool tradable, bool destroyable,
        string description, string sprite, bool useDestroy, int useHealth, int useMagic, int useExperience,
        int equipHealthBonus, int equipManaBonus, int equipDamageBonus, int equipDefenseBonus, int equipSpeedcBonus,
        string modelPrefab, Drawing[] drawing,int drawingItemId,int makeTime)
    {
        this.id = id;
        this.itemName = itemName;
        this.itemType = itemType;
        this.materialType = materialType;
        this.consumption = consumption;
        this.equipmentType = equipmentType;
        this.maxStack = maxStack;
        this.currentNumber = currentNumber;
        this.buyPriceByGold = buyPriceByGold;
        this.buyPriceByDiamond = buyPriceByDiamond;
        this.sellPriceByGold = sellPriceByGold;
        this.sellPriceByDiamond = sellPriceByDiamond;
        this.minLevel = minLevel;
        this.sellable = sellable;
        this.tradable = tradable;
        this.destroyable = destroyable;
        this.description = description;
        this.sprite = sprite;
        this.useDestroy = useDestroy;
        this.useHealth = useHealth;
        this.useMagic = useMagic;
        this.useExperience = useExperience;
        this.equipHealthBonus = equipHealthBonus;
        this.equipManaBonus = equipManaBonus;
        this.equipDamageBonus = equipDamageBonus;
        this.equipDefenseBonus = equipDefenseBonus;
        this.equipSpeedcBonus = equipSpeedcBonus;
        this.modelPrefab = modelPrefab;
        this.drawing = drawing;
        this.drawingItemId = drawingItemId;
        this.makeTime = makeTime;
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

    public string EquipTootip()
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

    public string ConsumableTootip()
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

    public string MateriaTootip()
    {
        string tip = "";
        if (materialType == "Fuel")
        {
            tip += "燃烧时间:" + consumption.ToString() + "\n";
        }

        return tip;
    }

    public string DrawingTootip()
    {
        string tip = "";
        foreach (Drawing draw in drawing)
        {
            tip += Ctrl_InventoryManager.Instance.GetItemById(draw.itemId).itemName + ":" +
                   Ctrl_BlacksmithStation.Instance.GetItemCountById(draw.itemId) + "/" + draw.itemCount + "\n";
        }

        return tip;
    }

    
}