using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalParametr : MonoBehaviour
{
    private void Awake()
    {
        enemyDictionary = new Dictionary<int, string>();
        enemyDictionary.Add(0, "地狱三头犬");
        enemyDictionary.Add(1, "木妖");
        enemyDictionary.Add(2, "百年蟾蜍");
        enemyDictionary.Add(3, "疾风兔");

        npcDictionary = new Dictionary<int, string>();
        npcDictionary.Add(0, "村长");
        npcDictionary.Add(1, "王铁匠");
        npcDictionary.Add(2, "草木精灵");
        npcDictionary.Add(3, "王婶");

        cookingProductDictionary = new Dictionary<int, int>();
        cookingProductDictionary.Add(15, 16);
    }

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
    /// <summary>
    /// 物品/存档等敏感操作,询问是否保留时的委托
    /// </summary>
    public delegate void del_AgreeCancel();
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
    public const float STEAMINGTIME = 3f;
    public const string SAVEPATH = "/Resources/" + "/Data";
    private static Dictionary<int, string> enemyDictionary;
    private static Dictionary<int, string> npcDictionary;
    private static Dictionary<int, int> cookingProductDictionary;

    /// <summary>
    /// 根据ID获得怪物名称
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static string GetEnemyName(int id)
    {
        string enemyName;
        enemyDictionary.TryGetValue(id, out enemyName);
        return enemyName;
    }

    /// <summary>
    /// 根据ID获得NPC名称
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static string GetNpcName(int id)
    {
        string npcName;
        npcDictionary.TryGetValue(id, out npcName);
        return npcName;
    }
    /// <summary>
    /// 根据ID获得熟品名称
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static int GetCookingProduct(int id)
    {
        int cookingProduct;
        cookingProductDictionary.TryGetValue(id, out cookingProduct);
        return cookingProduct;
    }

    #endregion
}