using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_PlayerSkillByKey : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ReleaseSkill(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ReleaseSkill(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ReleaseSkill(3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ReleaseSkill(4);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ReleaseSkill(5);
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            ReleaseSkill(6);
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            ReleaseSkill(7);
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            ReleaseSkill(8);
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            ReleaseSkill(9);
        }
    }

    public void ReleaseSkill(int key)
    {
        Ctrl_ActionBarSlot actionBarSlot = Ctrl_SkillManager.Instance.GetCurrentActionBarSlot(key-1);
        if (actionBarSlot != null)
        {
            actionBarSlot.UseSkill();
        }
    }
}