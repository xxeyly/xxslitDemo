using UnityEngine;

public class PlayerExtenalDataProxy : PlayerExtenalData
{
    private static PlayerExtenalDataProxy _Instance;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="IntExperience">当前经验</param>
    /// <param name="IntLevelExperience">等级经验</param>
    /// <param name="IntKillNumber">击杀数量</param>
    /// <param name="IntLevel">等级</param>
    /// <param name="IntGold">金币</param>
    /// <param name="IntDiamods">钻石</param>
    public PlayerExtenalDataProxy(int IntExperience, int IntLevelExperience, int IntKillNumber, int IntLevel, int IntGold, int IntDiamods) : base(IntExperience, IntLevelExperience, IntKillNumber, IntLevel, IntGold, IntDiamods)
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
    public static PlayerExtenalDataProxy GetInstance()
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

    #region 经验

    public void AddExp(int expValues)
    {
        base.Experience += expValues;
        //TUDO 当前等级经验满了
    }

    public void UpgradeConition(int expValues)
    {
        PlayerUpgradeRule.GetInstance().UpgradeConition(expValues);
    }

    public int GetExp()
    {
        return base.Experience;
    }

    public int GetLevelExp()
    {
        return base.LevelExperience;
    }

    #endregion

    #region 杀敌数量

    public void AddKillnumber()
    {
        base.KillNumber++;
    }

    public int GetKillNumber()
    {
        return base.KillNumber;
    }

    #endregion

    #region 等级

    public void AddLevel()
    {
        base.Level++;
        //TUDO s属性提升
        PlayerUpgradeRule.GetInstance().UpgradeOperation();
        base.LevelExperience = PlayerUpgradeRule._DicLevel[base.Level];
        Ctrl_TootipManager.Instance.ShowNotificationLevel(Level.ToString());
    }

    public int GetLevel()
    {
        return base.Level;
    }

    #endregion

    #region 金币

    public void AddGold(int goldValues)
    {
        base.Gold += goldValues;
    }

    public int GetGold()
    {
        return base.Gold;
    }

    public void RemoveGold(int removeGoldValues)
    {
        base.Gold -= removeGoldValues;
    }

    #endregion

    #region 钻石

    public void AddDiamods(int diamodsValues)
    {
        base.Gold += diamodsValues;
    }

    public int GetDiamods()
    {
        return base.Diamods;
    }

    public void RemoveDiamods(int RemoveValues)
    {
        base.Diamods -= RemoveValues;
    }

    #endregion

    public void DisplayAllOriginalValues()
    {
        base.Experience = base.Experience;
        base.LevelExperience = base.LevelExperience;
        base.Level = base.Level;
        base.Gold = base.Gold;
        base.Diamods = base.Diamods;
        base.KillNumber = base.KillNumber;
    }

}