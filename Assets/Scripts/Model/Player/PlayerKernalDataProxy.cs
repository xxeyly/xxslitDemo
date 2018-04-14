using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class PlayerKernalDataProxy : PlayerKernalData
{
    private static PlayerKernalDataProxy _Instance;

    public PlayerKernalDataProxy(int IntHealth, int IntMagic, int IntAttack, int IntDefence, int IntDexterity,
        int IntMaxHealth, int IntMaxMagic, int IntMaxAttack, int IntMaxDefence, int IntMaxDexterity,
        int IntAttackByProp, int IntDefenceByProp, int IntDexterityByProp) : base(IntHealth, IntMagic, IntAttack,
        IntDefence, IntDexterity, IntMaxHealth, IntMaxMagic, IntMaxAttack, IntMaxDefence, IntMaxDexterity,
        IntAttackByProp, IntDefenceByProp, IntDexterityByProp)
    {
        if (_Instance == null)
        {
            _Instance = this;
        }
        else
        {
            return;
        }
    }

    /// <summary>
    /// 得到本类实例
    /// </summary>
    /// <returns></returns>
    public static PlayerKernalDataProxy GetInstance()
    {
        if (_Instance != null)
        {
            return _Instance;
        }
        else
        {
            Debug.LogWarning("先使用构造函数");
            return null;
        }
    }

    #region 生命力数值操作 

    /// <summary>
    /// 减少生命值 
    /// 算法:Health = Health-(敌人攻击力-主角自身防御力-装备防御力)
    /// </summary>
    /// <param name="enemyAttack">敌人攻击力</param>
    public void DecreaseHealthValues(int enemyAttack)
    {
        int enemyReallyATK = 0;
        enemyReallyATK = enemyAttack - base.Defence - base.DefenceByProp;
        //敌人能破防
        if (enemyReallyATK > 0)
        {
            base.Health -= enemyReallyATK;
//            UpdateATK_DEF_DEX();
        }
        //不能破防
        else
        {
            base.Health -= 1;
//            UpdateATK_DEF_DEX();
        }
    }

    /// <summary>
    /// 增加生命值
    /// </summary>
    public void IncreaseHealthValues(int healthValue)
    {
        int floReallyIncreaseHealthValues = 0; //真实增加数值
        floReallyIncreaseHealthValues = base.Health + healthValue;
        //增加的血量没有超过上限,增加血量
        if (floReallyIncreaseHealthValues < base.MaxHealth)
        {
            base.Health += healthValue;
//            UpdateATK_DEF_DEX();
        }
        //增加的血量超过上限,当前生命值替换最大生命值上限
        else
        {
            base.Health = MaxHealth;
//            UpdateATK_DEF_DEX();
        }
    }

    /// <summary>
    /// 得到当前的生命数值
    /// </summary>
    /// <returns></returns>
    public int GetCurrentHealth()
    {
        return base.Health;
    }

    /// <summary>
    /// 增加最大生命值
    /// </summary>
    /// <param name="healthValue"></param>
    public void IncreaseMaxHealth(int healthValue)
    {
        base.MaxHealth += Mathf.Abs(healthValue);
//        UpdateATK_DEF_DEX();
    }
    /// <summary>
    /// 减少最大生命上限
    /// </summary>
    public void DecreaseMaxHealth(int values)
    {
        base.MaxHealth -= values;
    }
    /// <summary>
    /// 设置当前生命值
    /// </summary>
    /// <param name="value"></param>
    public void SetCurrentHealth(int value)
    {
        base.Health = value;
    }
    /// <summary>
    /// 获得最大生命数值
    /// </summary>
    /// <returns></returns>
    public int GetMaxHealth()
    {
        return base.MaxHealth;
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
        if (base.Magic >= magicValue)
        {
            base.Magic -= magicValue;
        }

        //TUDO
        //魔法不足
    }

    /// <summary>
    /// 增加魔法值
    /// </summary>
    public void IncreaseMagicValues(int magicValue)
    {
        float floReallyIncreaseMagicValues = 0f; //真实增加数值
        floReallyIncreaseMagicValues = base.Magic + magicValue;
        //增加的血量没有超过上限,增加血量
        if (floReallyIncreaseMagicValues < base.MaxMagic)
        {
            base.Magic += magicValue;
        }
        //增加的血量超过上限,当前生命值替换最大生命值上限
        else
        {
            base.Magic = MaxMagic;
        }
    }

    /// <summary>
    /// 得到当前的魔法值
    /// </summary>
    /// <returns></returns>
    public int GetCurrentMagic()
    {
        return base.Magic;
    }

    /// <summary>
    /// 增加最大魔法值
    /// </summary>
    /// <param name="magicValue">增加的魔法值</param>
    public void IncreaseMaxMagic(int magicValue)
    {
        base.MaxMagic += Mathf.Abs(magicValue);
    }
    /// <summary>
    /// 减少最大魔法上限
    /// </summary>
    /// <param name="values"></param>
    public void DecreaseMaxMagic(int values)
    {
        base.MaxMagic -= values;
    }

    /// <summary>
    /// 获得最大魔法值
    /// </summary>
    /// <returns></returns>
    public int GetMaxMagic()
    {
        return base.MaxMagic;
    }
    /// <summary>
    /// 设置当前魔法值
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public void SetCurrentMagic(int value)
    {
        base.Magic = value;
    }

    #endregion

    #region 防御数值操作 
    /// <summary>
    /// 增加防御力
    /// </summary>
    /// <param name="defValues"></param>
    public void AddDEF(int defValues)
    {
        base.Defence += defValues;
    }

    public void DecreaseDEF(int defValues)
    {
        base.Defence -= defValues;
    }
    /// <summary>
    /// 更新防御力//作废
    /// 算法:防御 = 防御/2*(Health/MaxHealth)+防具防御力
    /// </summary>
    /// <param name="newDefenceValue">新防具数值</param>
    /*public void UpdateDEFValue(int newDefenceValue = 0)
    {
        int reallyDEFvalue = 0;
        //没有获得新的防具
        if (reallyDEFvalue == 0)
        {
            reallyDEFvalue = base.Defence / 2 * (base.Health / base.MaxHealth) + base.DefenceByProp;
        }
        else if (reallyDEFvalue > 0)
        {
            reallyDEFvalue = base.Defence / 2 * (base.Health / base.MaxHealth) + base.DefenceByProp;
            base.DefenceByProp = reallyDEFvalue;
        }

        //防御有效性验证
        if (reallyDEFvalue >= base.MaxDefence)
        {
            base.Defence = base.MaxDefence;
        }
        else
        {
            base.Defence = reallyDEFvalue;
        }
    }*/

    /// <summary>
    /// 获得当前防御力
    /// </summary>
    /// <returns></returns>
    public int GetCurrentDEF()
    {
        return base.Defence;
    }

    /// <summary>
    /// 增加最大防御力
    /// </summary>
    /// <param name="increaseAtk"></param>
    public void IncreaseMaxDEF(int increaseDEF)
    {
        base.MaxDefence = Mathf.Abs(increaseDEF);
    }

    /// <summary>
    /// 得到最大的防御力
    /// </summary>
    /// <returns></returns>
    public int GetMaxDEF()
    {
        return base.MaxDefence;
    }

    #endregion

    #region 攻击数值操作 

    /// <summary>
    /// 更新攻击力//作废
    /// 算法:Attack = 主角自身攻击/2*(Health/MaxHealth)+武器攻击力
    /// </summary>
    /// <param name="newWeaopnValue">新武器数值</param>
    /*public void UpdateATKValue(int newWeaopnValue = 0)
    {
        int reallyATKvalue = 0;
        //没有获得新的武器道具
        if (newWeaopnValue == 0)
        {
            reallyATKvalue = base.Attack / 2 * (base.Health / base.MaxHealth) + base.AttackByProp;
        }
        else if (newWeaopnValue > 0)
        {
            reallyATKvalue = base.MaxAttack / 2 * (base.Health / base.MaxHealth) + base.AttackByProp;
            base.AttackByProp = reallyATKvalue;
        }

        //攻击有效性验证
        if (reallyATKvalue >= base.MaxAttack)
        {
            base.Attack = base.MaxAttack;
        }
        else
        {
            base.Attack = reallyATKvalue;
        }
    }*/

    /// <summary>
    /// 获得当前攻击力
    /// </summary>
    /// <returns></returns>
    public int GetCurrentATK()
    {
        return base.Attack;
    }

    /// <summary>
    /// 增加最大攻击力
    /// </summary>
    /// <param name="increaseAtk"></param>
    public void IncreaseMaxATK(int increaseAtk)
    {
        base.MaxAttack = Mathf.Abs(increaseAtk);
    }

    /// <summary>
    /// 得到最大的攻击力
    /// </summary>
    /// <returns></returns>
    public int GetMaxAtk()
    {
        return base.MaxAttack;
    }
    /// <summary>
    /// 增加攻击力
    /// </summary>
    /// <param name="values"></param>
    public void AddATK(int values)
    {
        base.Attack += values;
    }
    /// <summary>
    /// 减少攻击力
    /// </summary>
    /// <param name="values"></param>
    public void DecreaseATK(int values)
    {
        base.Attack -= values;
    }
    #endregion

    #region 敏捷数值操作 
    /// <summary>
    /// 增加速度
    /// </summary>
    /// <param name="values"></param>
    public void AddDEX(int values)
    {
        base.Dexterity += values;
    }
    /// <summary>
    /// 减少速度
    /// </summary>
    /// <param name="values"></param>
    public void DecreaseDEX(int values)
    {
        base.Dexterity -= values;
    }
    /// <summary>
    /// 更新防御力//作废
    /// 算法:速度 = 主角速度/2*(生命值/最大生命值)+防具最大值
    /// </summary>
    /// <param name="newWeaopnValue">新防具数值</param>
   /* public void UpdateDEXValue(int newExterityValue = 0)
    {
        int reallyDEXvalue = 0;
        //没有获得新的防具
        if (reallyDEXvalue == 0)
        {
            reallyDEXvalue = base.Dexterity / 2 * (base.Health / base.MaxHealth) + base.DexterityByProp;
        }
        else if (reallyDEXvalue > 0)
        {
            reallyDEXvalue = base.Dexterity / 2 * (base.Health / base.MaxHealth) + base.DexterityByProp;
            base.DexterityByProp = reallyDEXvalue;
        }

        //速度有效性验证
        if (reallyDEXvalue >= base.MaxDexterity)
        {
            base.Dexterity = base.MaxDexterity;
        }
        else
        {
            base.Dexterity = reallyDEXvalue;
        }
    }*/

    /// <summary>
    /// 获得当前速度
    /// </summary>
    /// <returns></returns>
    public int GetCurrentDEX()
    {
        return base.Dexterity;
    }

    /// <summary>
    /// 增加最大速度
    /// </summary>
    /// <param name="increaseDEX"></param>
    public void IncreaseMaxDEX(int increaseDEX)
    {
        base.MaxDexterity = Mathf.Abs(increaseDEX);
    }

    /// <summary>
    /// 得到最大的速度
    /// </summary>
    /// <returns></returns>
    public int GetMaxDEX()
    {
        return base.MaxDexterity;
    }

    #endregion

    #region 更新攻击力/防御/速度//作废

    /*public void UpdateATK_DEF_DEX()
    {
        /*UpdateATKValue();
        UpdateDEFValue();
        UpdateDEXValue();#1#
    }
*/
    #endregion

    #region 显示所有的初始数值

    public void DiaplayAllOriginaValues()
    {
        base.Health = base.Health;
        base.Magic = base.Magic;
        base.Attack = base.Attack;
        base.Defence = base.Defence;
        base.Dexterity = base.Dexterity;

        base.MaxHealth = base.MaxHealth;
        base.MaxMagic = base.MaxMagic;
        base.MaxAttack = base.MaxAttack;
        base.MaxDefence = base.MaxDefence;
        base.MaxDexterity = base.MaxDexterity;

        base.AttackByProp = base.AttackByProp;
        base.DefenceByProp = base.DefenceByProp;
        base.DexterityByProp = base.DexterityByProp;
    }

    #endregion
}