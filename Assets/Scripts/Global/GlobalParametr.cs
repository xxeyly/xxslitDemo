using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalParametr : MonoBehaviour
{
    #region 动画

    public const string UnarmedWalk = "IsUnarmed-Walk";
    public const string UnarmedNormalAttack = "UnarmedNormalAttack";
    public const string UnarmedSkill1 = "UnarmedSkill1";
    public const string UnarmedSkill2 = "UnarmedSkill2";
    public const string UnarmedSkill3 = "UnarmedSkill3";

    #endregion

    #region Tag
    public const string TAG_ENEMY = "Enemy";

    #region 枚举

    public enum PlayerAttribute
    {
        Health,
        MaxHealth,
        Magic,
        MaxMagic,
        Attack,
        MaxAttack,
        Defence,
        MaxDefence,
        Dexterity,
        MaxDexterity,
        Experience,
        killNumber,
        Gold,
        Diamonds
    }
    public enum PlayerAnim
    {
        WAIT00,
        WALK00_F,
        WALK00_L,
        WALK00_R,
        WALK00_B,
        RUN00_F,
        RUN00_L,
        RUN00_R,
        RUN00_B,

    }
    public enum SimplyEnemyState
    {
        Idle,
        Walking,
        Attack,
        Hurt,
        Death,

    }
    public enum ItemType
    {
        Drugs,
        Equipment,
        Material,
    }
    public enum EquipmentType
    {
        None,
        Head,
        Neck,
        Shoulders,
        Chest,
        Back,
        Bracer,
        Gloves,
        Belt,
        Pants,
        Boots,
        Finger,
        Trinket,
        Weapon,
        Shield

    }
    #endregion
    #endregion

    #region 委托

    /// <summary>
    /// 委托:主角控制
    /// </summary>
    /// <param name="controlType"></param>
    public delegate void del_PlayerControlWithStr(string controlType);
    /// <summary>
    /// 委托:玩家核心模型数值
    /// </summary>
    /// <param name="kv"></param>
    public delegate void del_PlayerKernalModel(KeyValuesUpdate kv);
    /// <summary>
    /// 委托:使用物品
    /// </summary>
    public delegate void del_SlotUseItem();
    /// <summary>
    /// 穿戴装备
    /// </summary>
    public delegate void del_Equipment();
    /// <summary>
    /// 
    /// </summary>
    public delegate void del_PickUp();
    /// <summary>
    /// 商店的委托slot
    /// </summary>
    public delegate void del_ShopSlot(Model_Item item);
    public delegate void del_EnemyBloodGroove(float value);
    public class KeyValuesUpdate
    {
        private string _Key;
        private object _Values;

        public string Key
        {
            get { return _Key; }
        }

        public object Values
        {
            get { return _Values; }
        }

        public KeyValuesUpdate(string Key, object Values)
        {
            _Key = Key;
            _Values = Values;
        }
    }

    #endregion

    #region 常量
    public const float DEFAULTSHOWTIME = 0.3f;
    public const float SKILLSHOWTIME = 0.3f;
    public const float SHOPSHOWTIME = 0.3f;

    #endregion
}