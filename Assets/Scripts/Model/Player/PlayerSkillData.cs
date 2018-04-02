using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillData
{
    public static PlayerSkillData Instance;

    public PlayerSkillData()
    {
        Instance = this;
    }

    public int SkillConsume(int skill)
    {
        switch (skill)
        {
            case 0:
                return 0;
            case 1:
                return 10;
            case 2:
                return 20;

            case 3:
                return 30;

            case 4:
                return 40;

            case 5:
                return 50;

            case 6:
                return 60;
        }

        return 0;
    }

    public int SkillAttack(int skill)
    {
        switch (skill)
        {
            case 0:
                return Ctrl_HeroProperty.Instance.GetCurrentATK();
            case 1:
                return Ctrl_HeroProperty.Instance.GetCurrentATK() * (2) + 100;
            case 2:
                return Ctrl_HeroProperty.Instance.GetCurrentATK() * (3) + 100;
            case 3:
                return Ctrl_HeroProperty.Instance.GetCurrentATK() * (5) + 100;
            case 4:
                return Ctrl_HeroProperty.Instance.GetCurrentATK() * (10) + 100;
            case 5:
                return Ctrl_HeroProperty.Instance.GetCurrentATK() * (12) + 100;
        }

        return 0;
    }
}