using System.Collections.Generic;

public class Ctrl_HeroProperty : Singleton<Ctrl_HeroProperty>
{
    //核心数据
    public int Health; //健康
    public int Magic; //魔法
    public int Attack; //攻击
    public int Defence; //防御
    public int Dexterity; //敏捷

    public int MaxHealth; //健康最大值
    public int MaxMagic; //魔法最大值
    public int MaxAttack; //攻击最大值
    public int MaxDefence; //防御最大值
    public int MaxDexterity; //敏捷最大值

    public int AttackByProp; //装备攻击
    public int DefenceByProp; //装备防御

    public int DexterityByProp; //装备敏捷

    //扩展数据
    public int Experience; //经验
    public int LevelExperience; //当前等级经验
    public int KillNumber; //杀怪数量
    public int Level; //当前等级
    public int Gold; //金币
    public int Diamods; //钻石

    /// <summary>
    /// 初始化模型层
    /// </summary>
    private void Start()
    {
        PlayerKernalDataProxy playerKernalData = new PlayerKernalDataProxy(Health, Magic, Attack, Defence, Dexterity,
            MaxHealth, MaxMagic, MaxAttack, MaxDefence, MaxDexterity, AttackByProp, DefenceByProp, DexterityByProp);
        PlayerExtenalDataProxy playerExtenalData =
            new PlayerExtenalDataProxy(Experience, LevelExperience, KillNumber, Level, Gold, Diamods);
    }

    #region 生命力数值操作 

    /// <summary>
    /// 减少生命值 
    /// 算法:Health = Health-(敌人攻击力-主角自身防御力-装备防御力)
    /// </summary>
    /// <param name="enemyAttack">敌人攻击力</param>
    public void DecreaseHealthValues(int enemyAttack)
    {
        PlayerKernalDataProxy.GetInstance().DecreaseHealthValues(enemyAttack);
    }

    /// <summary>
    /// 增加生命值
    /// </summary>
    public void IncreaseHealthValues(int healthValue)
    {
        PlayerKernalDataProxy.GetInstance().IncreaseHealthValues(healthValue);
    }

    /// <summary>
    /// 得到当前的生命数值
    /// </summary>
    /// <returns></returns>
    public int GetCurrentHealth()
    {
        return PlayerKernalDataProxy.GetInstance().GetCurrentHealth();
    }

    /// <summary>
    /// 增加最大生命值
    /// </summary>
    /// <param name="healthValue"></param>
    public void IncreaseMaxHealth(int healthValue)
    {
        PlayerKernalDataProxy.GetInstance().IncreaseMaxHealth(healthValue);
    }

    /// <summary>
    /// 减少最大生命上限
    /// </summary>
    /// <param name="values"></param>
    public void DecreaseMaxHealth(int values)
    {
        PlayerKernalDataProxy.GetInstance().DecreaseMaxHealth(values);
    }

    /// <summary>
    /// 获得最大生命数值
    /// </summary>
    /// <returns></returns>
    public int GetMaxHealth()
    {
        return PlayerKernalDataProxy.GetInstance().GetMaxHealth();
    }

    #endregion

    #region 魔法数值操作 

    /// <summary>
    /// 减少魔法值 
    /// 算法:Magic = Magic-释放魔法消耗
    /// </summary>
    /// <param name="magicValue">魔法损耗</param>
    public void DecreaseMagicValues(int magicValue)
    {
        PlayerKernalDataProxy.GetInstance().DecreaseMagicValues(magicValue);
    }

    /// <summary>
    /// 增加魔法值
    /// </summary>
    public void IncreaseMagicValues(int magicValue)
    {
        PlayerKernalDataProxy.GetInstance().IncreaseMagicValues(magicValue);
    }

    /// <summary>
    /// 得到当前的魔法值
    /// </summary>
    /// <returns></returns>
    public int GetCurrentMagic()
    {
        return PlayerKernalDataProxy.GetInstance().GetCurrentMagic();
    }

    /// <summary>
    /// 增加最大魔法值
    /// </summary>
    /// <param name="magicValue">增加的魔法值</param>
    public void IncreaseMaxMagic(int magicValue)
    {
        PlayerKernalDataProxy.GetInstance().IncreaseMaxMagic(magicValue);
    }

    /// <summary>
    /// 减少最大魔法上限
    /// </summary>
    /// <param name="values"></param>
    public void DecreaseMaxMagic(int values)
    {
        PlayerKernalDataProxy.GetInstance().DecreaseMaxMagic(values);
    }

    /// <summary>
    /// 获得最大魔法值
    /// </summary>
    /// <returns></returns>
    public int GetMaxMagic()
    {
        return PlayerKernalDataProxy.GetInstance().GetMaxMagic();
    }

    #endregion

    #region 防御数值操作 

    /// <summary>
    /// 增加防御力/
    /// </summary>
    /// <param name="values"></param>
    public void AddDEF(int values)
    {
        PlayerKernalDataProxy.GetInstance().AddDEF(values);
    }

    /// <summary>
    /// 减少防御力
    /// </summary>
    /// <param name="values"></param>
    public void DecreaseDEF(int values)
    {
        PlayerKernalDataProxy.GetInstance().DecreaseDEF(values);
    }

    /// <summary>
    /// 获得当前防御力
    /// </summary>
    /// <returns></returns>
    public int GetCurrentDEF()
    {
        return PlayerKernalDataProxy.GetInstance().GetCurrentDEF();
    }

    /// <summary>
    /// 增加最大防御力
    /// </summary>
    /// <param name="increaseAtk"></param>
    public void IncreaseMaxDEF(int increaseDEF)
    {
        PlayerKernalDataProxy.GetInstance().IncreaseMaxDEF(increaseDEF);
    }

    /// <summary>
    /// 得到最大的防御力
    /// </summary>
    /// <returns></returns>
    public int GetMaxDEF()
    {
        return PlayerKernalDataProxy.GetInstance().GetMaxDEF();
    }

    #endregion

    #region 攻击数值操作 

    /// <summary>
    /// 增加攻击力/装备脱下或更换
    /// </summary>
    /// <param name="values"></param>
    public void AddATK(int values)
    {
        PlayerKernalDataProxy.GetInstance().AddATK(values);
    }

    /// <summary>
    /// 减少攻击力/装备脱下或更换
    /// </summary>
    /// <param name="values"></param>
    public void DecreaseATK(int values)
    {
        PlayerKernalDataProxy.GetInstance().DecreaseATK(values);
    }

    /// <summary>
    /// 获得当前攻击力
    /// </summary>
    /// <returns></returns>
    public int GetCurrentATK()
    {
        return PlayerKernalDataProxy.GetInstance().GetCurrentATK();
    }

    /// <summary>
    /// 增加最大攻击力
    /// </summary>
    /// <param name="increaseAtk"></param>
    public void IncreaseMaxATK(int increaseAtk)
    {
        PlayerKernalDataProxy.GetInstance().IncreaseMaxATK(increaseAtk);
    }

    /// <summary>
    /// 得到最大的攻击力
    /// </summary>
    /// <returns></returns>
    public int GetMaxAtk()
    {
        return PlayerKernalDataProxy.GetInstance().GetMaxAtk();
    }

    #endregion

    #region 敏捷数值操作 

    /// <summary>
    /// 增加速度
    /// </summary>
    /// <param name="values"></param>
    public void AddDEX(int values)
    {
        PlayerKernalDataProxy.GetInstance().AddDEX(values);
    }

    /// <summary>
    /// 减少速度
    /// </summary>
    /// <param name="values"></param>
    public void DecreaseDEX(int values)
    {
        PlayerKernalDataProxy.GetInstance().DecreaseDEX(values);
    }

    /// <summary>
    /// 获得当前速度
    /// </summary>
    /// <returns></returns>
    public int GetCurrentDEX()
    {
        return PlayerKernalDataProxy.GetInstance().GetCurrentDEX();
    }

    /// <summary>
    /// 增加最大速度
    /// </summary>
    /// <param name="increaseDEX"></param>
    public void IncreaseMaxDEX(int increaseDEX)
    {
        PlayerKernalDataProxy.GetInstance().IncreaseMaxDEX(increaseDEX);
    }

    /// <summary>
    /// 得到最大的速度
    /// </summary>
    /// <returns></returns>
    public int GetMaxDEX()
    {
        return PlayerKernalDataProxy.GetInstance().GetMaxDEX();
    }

    #endregion

    #region 更新攻击力/防御/速度

    #endregion

    #region 显示所有的初始数值

    public void DiaplayAllOriginaValues()
    {
        PlayerKernalDataProxy.GetInstance().DiaplayAllOriginaValues();
    }

    #endregion

    #region 经验

    public void AddExp(int expValues)
    {
        PlayerExtenalDataProxy.GetInstance().AddExp(expValues);
    }

    public void UpgradeConition(int expValues)
    {
        PlayerExtenalDataProxy.GetInstance().UpgradeConition(expValues);
    }

    public int GetExp()
    {
        return PlayerExtenalDataProxy.GetInstance().GetExp();
    }

    public int GetLevelExp()
    {
        return PlayerExtenalDataProxy.GetInstance().GetLevelExp();
    }

    public void SetCurrentExp(int exp)
    {
        PlayerExtenalDataProxy.GetInstance().SetCurrentExp(exp);
    }

    #endregion

    #region 杀敌数量

    public void AddKillnumber()
    {
        PlayerExtenalDataProxy.GetInstance().AddKillnumber();
    }

    public int GetKillNumber()
    {
        return PlayerExtenalDataProxy.GetInstance().GetKillNumber();
    }

    #endregion

    #region 等级

    public void AddLevel()
    {
        PlayerExtenalDataProxy.GetInstance().AddLevel();
    }

    public int GetLevel()
    {
        return PlayerExtenalDataProxy.GetInstance().GetLevel();
    }

    public void SetLevel(int level)
    {
        PlayerExtenalDataProxy.GetInstance().SetLevel(level);
    }

    #endregion

    #region 金币

    public void AddGold(int goldValues)
    {
        PlayerExtenalDataProxy.GetInstance().AddGold(goldValues);
    }

    public int GetGold()
    {
        return PlayerExtenalDataProxy.GetInstance().GetGold();
    }

    public void RemoveGold(int removeGoldValues)
    {
        PlayerExtenalDataProxy.GetInstance().RemoveGold(removeGoldValues);
    }

    #endregion

    #region 钻石

    public void AddDiamods(int diamodsValues)
    {
        PlayerExtenalDataProxy.GetInstance().AddDiamods(diamodsValues);
    }

    public int GetDiamods()
    {
        return PlayerExtenalDataProxy.GetInstance().GetDiamods();
    }

    public void RemoveDiamods(int RemoveValues)
    {
        PlayerExtenalDataProxy.GetInstance().RemoveDiamods(RemoveValues);
    }

    #endregion

    #region 更新装备数据

    /// <summary>
    /// 穿戴装备
    /// </summary>
    /// <param name="item"></param>
    public void UpEquipmentData(Model_Item item)
    {
        if (item != null)
        {
            switch (item.equipmentType)
            {
                case "Head":
                    Ctrl_PlayerEquipmentProperty.Instance.HeadEquipment = item;
                    break;
                case "Neck":
                    Ctrl_PlayerEquipmentProperty.Instance.NeckEquipment = item;

                    break;
                case "Shoulders":
                    Ctrl_PlayerEquipmentProperty.Instance.ShouldersEquipment = item;

                    break;
                case "Chest":
                    Ctrl_PlayerEquipmentProperty.Instance.ChestEquipment = item;

                    break;
                case "Back":
                    Ctrl_PlayerEquipmentProperty.Instance.BackEquipment = item;

                    break;
                case "Bracer":
                    Ctrl_PlayerEquipmentProperty.Instance.BracerEquipment = item;

                    break;
                case "Gloves":
                    Ctrl_PlayerEquipmentProperty.Instance.GlovesEquipment = item;

                    break;
                case "Belt":
                    Ctrl_PlayerEquipmentProperty.Instance.BeltEquipment = item;

                    break;
                case "Pants":
                    Ctrl_PlayerEquipmentProperty.Instance.PantsEquipment = item;

                    break;
                case "Boots":
                    Ctrl_PlayerEquipmentProperty.Instance.BootsEquipment = item;

                    break;
                case "Finger":
                    Ctrl_PlayerEquipmentProperty.Instance.FingerEquipment = item;

                    break;
                case "Trinket":
                    Ctrl_PlayerEquipmentProperty.Instance.TrinketEquipment = item;

                    break;
                case "Weapon":
                    Ctrl_PlayerEquipmentProperty.Instance.WeaponEquipment = item;

                    break;
                case "Shield":
                    Ctrl_PlayerEquipmentProperty.Instance.ShieldEquipment = item;

                    break;
            }
        }
    }

    /// <summary>
    /// 脱下装备
    /// </summary>
    /// <param name="item"></param>
    public void UpEquipmentData(GlobalParametr.EquipmentType equipmentType)
    {
        switch (equipmentType)
        {
            case GlobalParametr.EquipmentType.Head:
                Ctrl_PlayerEquipmentProperty.Instance.HeadEquipment = null;
                break;
            case GlobalParametr.EquipmentType.Neck:
                Ctrl_PlayerEquipmentProperty.Instance.NeckEquipment = null;

                break;
            case GlobalParametr.EquipmentType.Shoulders:
                Ctrl_PlayerEquipmentProperty.Instance.ShouldersEquipment = null;

                break;
            case GlobalParametr.EquipmentType.Chest:
                Ctrl_PlayerEquipmentProperty.Instance.ChestEquipment = null;

                break;
            case GlobalParametr.EquipmentType.Back:
                Ctrl_PlayerEquipmentProperty.Instance.BackEquipment = null;

                break;
            case GlobalParametr.EquipmentType.Bracer:
                Ctrl_PlayerEquipmentProperty.Instance.BracerEquipment = null;

                break;
            case GlobalParametr.EquipmentType.Gloves:
                Ctrl_PlayerEquipmentProperty.Instance.GlovesEquipment = null;

                break;
            case GlobalParametr.EquipmentType.Belt:
                Ctrl_PlayerEquipmentProperty.Instance.BeltEquipment = null;

                break;
            case GlobalParametr.EquipmentType.Pants:
                Ctrl_PlayerEquipmentProperty.Instance.PantsEquipment = null;

                break;
            case GlobalParametr.EquipmentType.Boots:
                Ctrl_PlayerEquipmentProperty.Instance.BootsEquipment = null;

                break;
            case GlobalParametr.EquipmentType.Finger:
                Ctrl_PlayerEquipmentProperty.Instance.FingerEquipment = null;

                break;
            case GlobalParametr.EquipmentType.Trinket:
                Ctrl_PlayerEquipmentProperty.Instance.TrinketEquipment = null;

                break;
            case GlobalParametr.EquipmentType.Weapon:
                Ctrl_PlayerEquipmentProperty.Instance.WeaponEquipment = null;

                break;
            case GlobalParametr.EquipmentType.Shield:
                Ctrl_PlayerEquipmentProperty.Instance.ShieldEquipment = null;

                break;
        }
    }

    #endregion
    /// <summary>
    /// 获得当前游戏数据,方便存储
    /// </summary>
    /// <returns></returns>
    public Model_Archiving GetHeroCurrentData()
    {
        Model_Archiving archiving = new Model_Archiving();
        archiving.x = (int) transform.position.x;
        archiving.y = (int) transform.position.y;
        archiving.z = (int) transform.position.z;
        archiving.hp = GetCurrentHealth();
        archiving.mp = GetMaxMagic();
        archiving.exp = GetExp();
        archiving.gold = GetGold();
        archiving.diamod = GetDiamods();
        archiving.level = GetLevel();

        foreach (Ctrl_Slot ctrlSlot in Ctrl_InventoryManager.Instance.ActiveSlot())
        {
            archiving.items.Add(new Model_Archiving.Item()
            {
                itemId = ctrlSlot.Item.id,
                currentCount = ctrlSlot.Item.currentNumber
            });
        }

        archiving.equips = GetComponent<Ctrl_PlayerEquipmentProperty>().GetEquipData();

        return archiving;
    }
}