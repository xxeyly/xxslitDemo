using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_PlayerEquipmentProperty : Singleton<Ctrl_PlayerEquipmentProperty>
{
    [SerializeField] private Model_Item headEquipment;
    [SerializeField] private Model_Item neckEquipment;
    [SerializeField] private Model_Item shouldersEquipment;
    [SerializeField] private Model_Item chestEquipment;
    [SerializeField] private Model_Item backEquipment;
    [SerializeField] private Model_Item bracerEquipment;
    [SerializeField] private Model_Item glovesEquipment;
    [SerializeField] private Model_Item beltEquipment;
    [SerializeField] private Model_Item pantsEquipment;
    [SerializeField] private Model_Item bootsEquipment;
    [SerializeField] private Model_Item fingerEquipment;
    [SerializeField] private Model_Item trinketEquipment;
    [SerializeField] private Model_Item weaponEquipment;
    [SerializeField] private Model_Item shieldEquipment;
    private List<Model_Archiving.Equip> equips;


    public List<Model_Archiving.Equip> GetEquipData()
    {
        equips = new List<Model_Archiving.Equip>();
        if (headEquipment != null)
        {
            equips.Add(new Model_Archiving.Equip()
            {
                equip = GlobalParametr.EquipmentType.Head,
                itemId = headEquipment.id
            });
        }

        if (neckEquipment != null)
        {
            equips.Add(new Model_Archiving.Equip
            {
                equip = GlobalParametr.EquipmentType.Neck,
                itemId = neckEquipment.id
            });
        }

        if (shouldersEquipment != null)
        {
            equips.Add(new Model_Archiving.Equip
            {
                equip = GlobalParametr.EquipmentType.Shoulders,
                itemId = shouldersEquipment.id
            });
        }

        if (chestEquipment != null)
        {
            equips.Add(new Model_Archiving.Equip
            {
                equip = GlobalParametr.EquipmentType.Chest,
                itemId = chestEquipment.id
            });
        }

        if (backEquipment != null)
        {
            equips.Add(new Model_Archiving.Equip
            {
                equip = GlobalParametr.EquipmentType.Back,
                itemId = backEquipment.id
            });
        }

        if (bracerEquipment != null)
        {
            equips.Add(new Model_Archiving.Equip
            {
                equip = GlobalParametr.EquipmentType.Bracer,
                itemId = bracerEquipment.id
            });
        }

        if (glovesEquipment != null)
        {
            equips.Add(new Model_Archiving.Equip
            {
                equip = GlobalParametr.EquipmentType.Gloves,
                itemId = glovesEquipment.id
            });
        }

        if (beltEquipment != null)
        {
            equips.Add(new Model_Archiving.Equip
            {
                equip = GlobalParametr.EquipmentType.Belt,
                itemId = beltEquipment.id
            });
        }

        if (pantsEquipment != null)
        {
            equips.Add(new Model_Archiving.Equip
            {
                equip = GlobalParametr.EquipmentType.Pants,
                itemId = pantsEquipment.id
            });
        }

        if (bootsEquipment != null)
        {
            equips.Add(new Model_Archiving.Equip
            {
                equip = GlobalParametr.EquipmentType.Boots,
                itemId = bootsEquipment.id
            });
        }

        if (fingerEquipment != null)
        {
            equips.Add(new Model_Archiving.Equip
            {
                equip = GlobalParametr.EquipmentType.Finger,
                itemId = fingerEquipment.id
            });
        }

        if (trinketEquipment != null)
        {
            equips.Add(new Model_Archiving.Equip
            {
                equip = GlobalParametr.EquipmentType.Trinket,
                itemId = trinketEquipment.id
            });
        }

        if (weaponEquipment != null)
        {
            equips.Add(new Model_Archiving.Equip
            {
                equip = GlobalParametr.EquipmentType.Weapon,
                itemId = weaponEquipment.id
            });
        }

        if (shieldEquipment != null)
        {
            equips.Add(new Model_Archiving.Equip
            {
                equip = GlobalParametr.EquipmentType.Shield,
                itemId = shieldEquipment.id
            });
        }

        return equips;
    }

    public Model_Item HeadEquipment
    {
        get { return headEquipment; }

        set
        {
            WearableEquipment(headEquipment, value);
            headEquipment = value;
        }
    }

    public Model_Item NeckEquipment
    {
        get { return neckEquipment; }

        set
        {
            WearableEquipment(neckEquipment, value);
            neckEquipment = value;
        }
    }

    public Model_Item ShouldersEquipment
    {
        get { return shouldersEquipment; }

        set
        {
            WearableEquipment(shouldersEquipment, value);
            shouldersEquipment = value;
        }
    }

    public Model_Item ChestEquipment
    {
        get { return chestEquipment; }

        set
        {
            WearableEquipment(chestEquipment, value);
            chestEquipment = value;
        }
    }

    public Model_Item BackEquipment
    {
        get { return backEquipment; }

        set
        {
            WearableEquipment(backEquipment, value);
            backEquipment = value;
        }
    }

    public Model_Item BracerEquipment
    {
        get { return bracerEquipment; }

        set
        {
            WearableEquipment(bracerEquipment, value);
            bracerEquipment = value;
        }
    }

    public Model_Item GlovesEquipment
    {
        get { return glovesEquipment; }

        set
        {
            WearableEquipment(glovesEquipment, value);
            glovesEquipment = value;
        }
    }

    public Model_Item BeltEquipment
    {
        get { return beltEquipment; }

        set
        {
            WearableEquipment(beltEquipment, value);
            beltEquipment = value;
        }
    }

    public Model_Item PantsEquipment
    {
        get { return pantsEquipment; }

        set
        {
            WearableEquipment(pantsEquipment, value);
            pantsEquipment = value;
        }
    }

    public Model_Item BootsEquipment
    {
        get { return bootsEquipment; }

        set
        {
            WearableEquipment(bootsEquipment, value);
            bootsEquipment = value;
        }
    }

    public Model_Item FingerEquipment
    {
        get { return fingerEquipment; }

        set
        {
            WearableEquipment(fingerEquipment, value);
            fingerEquipment = value;
        }
    }

    public Model_Item TrinketEquipment
    {
        get { return trinketEquipment; }

        set
        {
            WearableEquipment(trinketEquipment, value);
            trinketEquipment = value;
        }
    }

    public Model_Item WeaponEquipment
    {
        get { return weaponEquipment; }

        set
        {
            WearableEquipment(weaponEquipment, value);
            weaponEquipment = value;
        }
    }

    public Model_Item ShieldEquipment
    {
        get { return shieldEquipment; }

        set
        {
            WearableEquipment(shieldEquipment, value);
            shieldEquipment = value;
        }
    }


    /// <summary>
    /// 如果newItem==null,代表脱下装备,如果不为空,判断当前装备栏是否有装备
    /// 如果无,直接增加装备属性,如果有,判断是否是同一间装备,如果是跳过,如果
    /// 不是,先减去之前装备的属性,在加上新装备的属性(先脱装备后穿上)
    /// </summary>
    /// <param name="currentItem">当前穿戴的装备</param>
    /// <param name="newItem">新装备</param>
    private void WearableEquipment(Model_Item currentItem, Model_Item newItem)
    {
        if (newItem == null)
        {
            //脱掉装备
            Ctrl_HeroProperty.Instance.DecreaseATK(currentItem.equipDamageBonus);
            Ctrl_HeroProperty.Instance.DecreaseDEF(currentItem.equipDefenseBonus);
            Ctrl_HeroProperty.Instance.DecreaseDEX(currentItem.equipSpeedcBonus);
            Ctrl_HeroProperty.Instance.DecreaseMaxHealth(currentItem.equipHealthBonus);
            Ctrl_HeroProperty.Instance.DecreaseMaxMagic(currentItem.equipManaBonus);
        }
        else
        {
            if (currentItem == null)
            {
                Ctrl_HeroProperty.Instance.AddATK(newItem.equipDamageBonus);
                Ctrl_HeroProperty.Instance.AddDEF(newItem.equipDefenseBonus);
                Ctrl_HeroProperty.Instance.AddDEX(newItem.equipSpeedcBonus);
                Ctrl_HeroProperty.Instance.IncreaseMaxHealth(newItem.equipHealthBonus);
                Ctrl_HeroProperty.Instance.IncreaseMaxMagic(newItem.equipManaBonus);
            }
            else
            {
                if (currentItem == newItem)
                {
                    return;
                }

                //先脱掉装备
                Ctrl_HeroProperty.Instance.DecreaseATK(currentItem.equipDamageBonus);
                Ctrl_HeroProperty.Instance.DecreaseDEF(currentItem.equipDefenseBonus);
                Ctrl_HeroProperty.Instance.DecreaseDEX(currentItem.equipSpeedcBonus);
                Ctrl_HeroProperty.Instance.DecreaseMaxHealth(currentItem.equipHealthBonus);
                Ctrl_HeroProperty.Instance.DecreaseMaxMagic(currentItem.equipManaBonus);
                //穿上新装备
                Ctrl_HeroProperty.Instance.AddATK(newItem.equipDamageBonus);
                Ctrl_HeroProperty.Instance.AddDEF(newItem.equipDefenseBonus);
                Ctrl_HeroProperty.Instance.AddDEX(newItem.equipSpeedcBonus);
                Ctrl_HeroProperty.Instance.IncreaseMaxHealth(newItem.equipHealthBonus);
                Ctrl_HeroProperty.Instance.IncreaseMaxMagic(newItem.equipManaBonus);
            }
        }
    }
}