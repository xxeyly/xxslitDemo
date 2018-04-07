using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ctrl_PickUp : MonoBehaviour
{
    public GlobalParametr.del_PickUp DelPickUp;
    private Model_Item _item;
    private string collderObjectName;

    public Model_Item Item
    {
        get { return _item; }
        set
        {
            _item = value;
            DelPickUp();
        }
    }

    private View_PickUpItem viewItem;

    void Start()
    {
        viewItem = GetComponent<View_PickUpItem>();
    }

    public void ChangeItem(Model_Item item)
    {
        viewItem.ChangeItem(item);
    }

    public void SetLocalPosition(Vector3 position)
    {
        transform.localPosition = position;
    }

    //丢弃物品
    public void DropItem()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Item = null;
        }
    }
    /// <summary>
    /// 更新数量显示
    /// </summary>
    public void UpdateAmount()
    {
        viewItem.UpdateAmount();
    }

    private void Update()
    {
        //当前手上有物品
        if (Ctrl_TootipManager.Instance.IsPickedItem)
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

    void OnCollisionEnter2D(Collision2D coll)
    {
        collderObjectName = coll.gameObject.name;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collderObjectName = "";
    }
}