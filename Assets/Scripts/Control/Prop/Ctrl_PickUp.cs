using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ctrl_PickUp : MonoBehaviour
{
    private Model_Item item; //物品
    private Model_Skill skill; //技能
    private string collderObjectName;
    private View_PickUp viewPickUp;

    public Model_Item Item
    {
        get { return item; }
        set
        {
            item = value;
            GetComponent<View_PickUp>().InitUI();
        }
    }

    public Model_Skill Skill
    {
        get { return skill; }

        set
        {
            skill = value;
            GetComponent<View_PickUp>().InitUI();
        }
    }


    void Start()
    {
        viewPickUp = GetComponent<View_PickUp>();
    }

    /// <summary>
    /// 更改物品
    /// </summary>
    /// <param name="item"></param>
    public void ChangeItem(Model_Item item)
    {
        viewPickUp.ChangeItem(item);
    }

    /// <summary>
    /// 更改技能
    /// </summary>
    /// <param name="skill"></param>
    public void ChangeSkill(Model_Skill skill)
    {
        viewPickUp.ChangeSkill(skill);
    }

    /// <summary>
    /// 设置显示位置
    /// </summary>
    /// <param name="position"></param>
    public void SetLocalPosition(Vector3 position)
    {
        transform.localPosition = position;
    }

    /// <summary>
    /// 丢弃物品/技能
    /// </summary>
    public void DropItem()
    {
        //鼠标下面不是UI
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Item = null;
            Skill = null;

        }
    }

    /// <summary>
    /// 更新数量显示
    /// </summary>
    public void UpdateAmount()
    {
        viewPickUp.UpdateAmount();
    }

    private void Update()
    {
        //当前手上有物品
        if (Ctrl_TootipManager.Instance.IsPicked)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //如果鼠标指针下不是UI界面,丢弃物品
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    //丢弃物品
                    DropItem();
                }
                else
                {
                    //捡起的东西为物品才执行贩卖操作
                    if (Item != null)
                    {
                        if (collderObjectName == "PlayerShop")
                        {
                            for (int i = 0; i < Item.currentNumber; i++)
                            {
                                Ctrl_HeroProperty.Instance.AddGold(Item.sellPriceByGold);
                                Ctrl_HeroProperty.Instance.AddDiamods(Item.sellPriceByDiamond);
                            }

                            Item = null;
                        }
                    }
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        collderObjectName = coll.gameObject.name;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collderObjectName = "";
    }
    /// <summary>
    /// 当前手上有没有物品
    /// </summary>
    /// <returns></returns>
    public bool IsEmpty()
    {
        if (Skill == null && item == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}