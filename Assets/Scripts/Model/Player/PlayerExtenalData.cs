public class PlayerExtenalData
{
    //定义事件:扩展数据
    public static event GlobalParametr.del_PlayerKernalModel evePlayerExtenalData; //玩家扩展数据

    private int _IntExperience; //经验
    private int _IntLevelExperience;//当前等级经验
    private int _IntKillNumber; //杀怪数量
    private int _IntLevel; //当前等级
    private int _IntGold; //金币
    private int _IntDiamods; //钻石

    public PlayerExtenalData(int IntExperience, int IntLevelExperience, int IntKillNumber, int IntLevel, int IntGold, int IntDiamods)
    {
        _IntExperience = IntExperience;
        _IntLevelExperience = IntLevelExperience;
        _IntKillNumber = IntKillNumber;
        _IntLevel = IntLevel;
        _IntGold = IntGold;
        _IntDiamods = IntDiamods;
    }

    public int Experience
    {
        get { return _IntExperience; }

        set
        {
            _IntExperience = value;
            if (evePlayerExtenalData != null)
            {
                GlobalParametr.KeyValuesUpdate kv = new GlobalParametr.KeyValuesUpdate("Experience", value);
                evePlayerExtenalData(kv);
              
            }
        }
    }

    public int LevelExperience
    {
        get { return _IntLevelExperience; }
        set
        {
            _IntLevelExperience = value;
            GlobalParametr.KeyValuesUpdate kv = new GlobalParametr.KeyValuesUpdate("LevelExperience", value);
            evePlayerExtenalData(kv);
        }
    }

    public int KillNumber
    {
        get { return _IntKillNumber; }

        set
        {
            _IntKillNumber = value;
            if (evePlayerExtenalData != null)
            {
                GlobalParametr.KeyValuesUpdate kv = new GlobalParametr.KeyValuesUpdate("KillNumber", value);
                evePlayerExtenalData(kv);
            }
        }
    }

    public int Level
    {
        get { return _IntLevel; }

        set
        {
            _IntLevel = value;
            if (evePlayerExtenalData != null)
            {
                GlobalParametr.KeyValuesUpdate kv = new GlobalParametr.KeyValuesUpdate("Level", value);
                evePlayerExtenalData(kv);
            }
        }
    }

    public int Gold
    {
        get { return _IntGold; }

        set
        {
            _IntGold = value;
            if (evePlayerExtenalData != null)
            {
                GlobalParametr.KeyValuesUpdate kv = new GlobalParametr.KeyValuesUpdate("Gold", value);
                evePlayerExtenalData(kv);
            }
        }
    }

    public int Diamods
    {
        get { return _IntDiamods; }

        set
        {
            _IntDiamods = value;
            if (evePlayerExtenalData != null)
            {
                GlobalParametr.KeyValuesUpdate kv = new GlobalParametr.KeyValuesUpdate("Diamonds", value);
                evePlayerExtenalData(kv);
            }
        }
    }

    public void Test(GlobalParametr.KeyValuesUpdate ke)
    {
        evePlayerExtenalData(ke);
    }

   
}