using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_Enemy : MonoBehaviour
{
    [SerializeField]
    private int intMaxHealth = 1000;
    [SerializeField]
    private int intCurrentHealth;
    [SerializeField]
    private int intDefender = 20;

    public bool IsAlive
    {
        get
        {
            if (intCurrentHealth <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    void Start()
    {
        intCurrentHealth = intMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnHurt(int hurtNumber)
    {
        if (hurtNumber <= intDefender)
        {
            intCurrentHealth -= 1;
        }
        else
        {
            intCurrentHealth -= (hurtNumber - 20);
            if (intCurrentHealth<=0)
            {
                //角色增加经验值
                Ctrl_HeroProperty.Instance.UpgradeConition(100);
                //增加杀敌数量
                Ctrl_HeroProperty.Instance.AddKillnumber();

                StartCoroutine(WaitTimeDestroy());
            }
        }

    }

    IEnumerator WaitTimeDestroy()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}