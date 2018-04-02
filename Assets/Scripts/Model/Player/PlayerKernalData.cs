/// <summary>
/// 玩家核心类
/// </summary>
public class PlayerKernalData
{
    //定义事件:玩家核心数据
    public static event GlobalParametr.del_PlayerKernalModel evePlayerKernalData;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="IntHealth">生命</param>
    /// <param name="IntMagic">魔法</param>
    /// <param name="IntAttack">攻击</param>
    /// <param name="IntDefence">防御</param>
    /// <param name="IntDexterity">速度</param>
    /// <param name="IntMaxHealth">生命上限</param>
    /// <param name="IntMaxMagic">魔法上限</param>
    /// <param name="IntMaxAttack">攻击上限</param>
    /// <param name="IntMaxDefence">防御上限</param>
    /// <param name="IntMaxDexterity">速度上限</param>
    /// <param name="IntAttackByProp">装备攻击</param>
    /// <param name="IntDefenceByProp">装备防御</param>
    /// <param name="IntDexterityByProp">装备速度</param>
    public PlayerKernalData(int IntHealth, int IntMagic, int IntAttack, int IntDefence, int IntDexterity,
        int IntMaxHealth, int IntMaxMagic, int IntMaxAttack, int IntMaxDefence, int IntMaxDexterity,
        int IntAttackByProp, int IntDefenceByProp, int IntDexterityByProp)
    {
        _IntHealth = IntHealth;
        _IntMagic = IntMagic;
        _IntAttack = IntAttack;
        _IntDefence = IntDefence;
        _IntDexterity = IntDexterity;
        _IntMaxHealth = IntMaxHealth;
        _IntMaxMagic = IntMaxMagic;
        _IntMaxAttack = IntMaxAttack;
        _IntMaxDefence = IntMaxDefence;
        _IntMaxDexterity = IntMaxDexterity;
        _IntAttackByProp = IntAttackByProp;
        _IntDefenceByProp = IntDefenceByProp;
        _IntDexterityByProp = IntDexterityByProp;
    }

    private int _IntHealth; //健康
    private int _IntMagic; //魔法
    private int _IntAttack; //攻击
    private int _IntDefence; //防御
    private int _IntDexterity; //敏捷

    private int _IntMaxHealth; //健康最大值
    private int _IntMaxMagic; //魔法最大值
    private int _IntMaxAttack; //攻击最大值
    private int _IntMaxDefence; //防御最大值
    private int _IntMaxDexterity; //敏捷最大值

    private int _IntAttackByProp; //装备攻击
    private int _IntDefenceByProp; //装备防御
    private int _IntDexterityByProp; //装备敏捷


    public int Health
    {
        get { return _IntHealth; }

        set
        {
            _IntHealth = value;
            if (evePlayerKernalData != null)
            {
                GlobalParametr.KeyValuesUpdate kv = new GlobalParametr.KeyValuesUpdate("Health", value);
                evePlayerKernalData(kv);
            }
        }
    }

    public int Magic
    {
        get { return _IntMagic; }

        set
        {
            _IntMagic = value;
            if (evePlayerKernalData != null)
            {
                GlobalParametr.KeyValuesUpdate kv = new GlobalParametr.KeyValuesUpdate("Magic", value);
                evePlayerKernalData(kv);
            }
        }
    }

    public int Attack
    {
        get { return _IntAttack; }

        set
        {
            _IntAttack = value;
            if (evePlayerKernalData != null)
            {
                GlobalParametr.KeyValuesUpdate kv = new GlobalParametr.KeyValuesUpdate("Attack", value);
                evePlayerKernalData(kv);
            }
        }
    }

    public int Defence
    {
        get { return _IntDefence; }

        set
        {
            _IntDefence = value;
            if (evePlayerKernalData != null)
            {
                GlobalParametr.KeyValuesUpdate kv = new GlobalParametr.KeyValuesUpdate("Defence", value);
                evePlayerKernalData(kv);
            }
        }
    }

    public int Dexterity
    {
        get { return _IntDexterity; }

        set
        {
            _IntDexterity = value;
            if (evePlayerKernalData != null)
            {
                GlobalParametr.KeyValuesUpdate kv = new GlobalParametr.KeyValuesUpdate("Dexterity", value);
                evePlayerKernalData(kv);
            }
        }
    }

    public int MaxHealth
    {
        get { return _IntMaxHealth; }

        set
        {
            _IntMaxHealth = value;
            if (evePlayerKernalData != null)
            {
                GlobalParametr.KeyValuesUpdate kv = new GlobalParametr.KeyValuesUpdate("MaxHealth", value);
                evePlayerKernalData(kv);
            }
        }
    }

    public int MaxMagic
    {
        get { return _IntMaxMagic; }

        set
        {
            _IntMaxMagic = value;
            if (evePlayerKernalData != null)
            {
                GlobalParametr.KeyValuesUpdate kv = new GlobalParametr.KeyValuesUpdate("MaxMagic", value);
                evePlayerKernalData(kv);
            }
        }
    }

    public int MaxAttack
    {
        get { return _IntMaxAttack; }

        set
        {
            _IntMaxAttack = value;
            if (evePlayerKernalData != null)
            {
                GlobalParametr.KeyValuesUpdate kv = new GlobalParametr.KeyValuesUpdate("MaxAttack", value);
                evePlayerKernalData(kv);
            }
        }
    }

    public int MaxDefence
    {
        get { return _IntMaxDefence; }

        set
        {
            _IntMaxDefence = value;
            if (evePlayerKernalData != null)
            {
                GlobalParametr.KeyValuesUpdate kv = new GlobalParametr.KeyValuesUpdate("MaxDefence", value);
                evePlayerKernalData(kv);
            }
        }
    }

    public int MaxDexterity
    {
        get { return _IntMaxDexterity; }

        set
        {
            _IntMaxDexterity = value;
            if (evePlayerKernalData != null)
            {
                GlobalParametr.KeyValuesUpdate kv = new GlobalParametr.KeyValuesUpdate("MaxDexterity", value);
                evePlayerKernalData(kv);
            }
        }
    }

    public int AttackByProp
    {
        get { return _IntAttackByProp; }

        set
        {
            _IntAttackByProp = value;
            if (evePlayerKernalData != null)
            {
                GlobalParametr.KeyValuesUpdate kv = new GlobalParametr.KeyValuesUpdate("AttackByProp", value);
                evePlayerKernalData(kv);
            }
        }
    }

    public int DefenceByProp
    {
        get { return _IntDefenceByProp; }

        set
        {
            _IntDefenceByProp = value;
            if (evePlayerKernalData != null)
            {
                GlobalParametr.KeyValuesUpdate kv = new GlobalParametr.KeyValuesUpdate("DefenceByProp", value);
                evePlayerKernalData(kv);
            }
        }
    }

    public int DexterityByProp
    {
        get { return _IntDexterityByProp; }

        set
        {
            _IntDexterityByProp = value;
            if (evePlayerKernalData != null)
            {
                GlobalParametr.KeyValuesUpdate kv = new GlobalParametr.KeyValuesUpdate("DexterityByProp", value);
                evePlayerKernalData(kv);
            }
        }
    }
}