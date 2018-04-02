using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_PlayerSkillByKey : MonoBehaviour
{
    //事件:主角控制事件
    public static event GlobalParametr.del_PlayerControlWithStr evePlayerControl;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            evePlayerControl("NormalAttack");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            evePlayerControl("MagicTrickA");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            evePlayerControl("MagicTrickB");

        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            evePlayerControl("MagicTrickC");

        }

        if (Input.GetKey(KeyCode.V))
        {
            if (!Ctrl_PlayerAminator.Instance.GetCurrentBlockBreakState())
            {
                Ctrl_PlayerAminator.Instance.Block(true);
            }
        }
        else
        {
            Ctrl_PlayerAminator.Instance.Block(false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ctrl_PlayerAminator.Instance.BlockBreak(true);
        }
        else
        {
            Ctrl_PlayerAminator.Instance.BlockBreak(false);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Ctrl_PlayerAminator.Instance.KickL();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Ctrl_PlayerAminator.Instance.KickR();
        }
    }
}