using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ctrl_Ingredients : MonoBehaviour, IPointerDownHandler
{
    private Model_Item item;
    private bool startTheTimer;//进度显示 烹饪的时候有个烹饪进度 当食材烹饪开始的时候会设为True,进度条就会开始走

    public Model_Item Item
    {
        get { return item; }

        set
        {
            item = value;
            if (item == null)
            {
                StartTheTimer = false;
                StopAllCoroutines();
            }
            else
            {
                //当前开着火
                if (Ctrl_CampfireManager.Instance.IsCombustion)
                {
//                    StopAllCoroutines();//先停止
                    Ingredients(true);
                }
            }

            GetComponent<View_Ingredients>().InitView(value);
        }
    }
    /// <summary>
    /// 是否开始烹饪进度
    /// </summary>
    public bool StartTheTimer
    {
        get { return startTheTimer; }

        set { startTheTimer = value; }
    }
    /// <summary>
    /// 更新食材个数显示
    /// </summary>
    public void UpdateAmount()
    {
        GetComponent<View_Ingredients>().UpdateAmount(Item);
    }
    /// <summary>
    /// 同背包物品交换 这里做了限制 只有食材才能替换或者添加
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        //手上的物品
        Ctrl_PickUp pickUp = Ctrl_InventoryManager.Instance.PickUpItem.GetComponent<Ctrl_PickUp>();
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //如果手上有物品
            if (pickUp.Item != null)
            {
                //如果当前格子没有物品
                if (Item == null)
                {
                    //只有食材可以替换
                    if (pickUp.Item.materialType == "Ingredients")
                    {
                        //按住CTRL键,一下添加一个
                        if (Input.GetKey(KeyCode.LeftControl))
                        {
                            Item = new Model_Item
                            {
                                id = pickUp.Item.id,
                                itemName = pickUp.Item.itemName,
                                itemType = pickUp.Item.itemType,
                                materialType = pickUp.Item.materialType,
                                equipmentType = pickUp.Item.equipmentType,
                                maxStack = pickUp.Item.maxStack,
                                currentNumber = 1,
                                buyPriceByGold = pickUp.Item.buyPriceByGold,
                                buyPriceByDiamond = pickUp.Item.buyPriceByDiamond,
                                sellPriceByGold = pickUp.Item.sellPriceByGold,
                                sellPriceByDiamond = pickUp.Item.sellPriceByDiamond,
                                minLevel = pickUp.Item.minLevel,
                                sellable = pickUp.Item.sellable,
                                tradable = pickUp.Item.tradable,
                                destroyable = pickUp.Item.destroyable,
                                description = pickUp.Item.description,
                                sprite = pickUp.Item.sprite,
                                useDestroy = pickUp.Item.useDestroy,
                                useHealth = pickUp.Item.useHealth,
                                useMagic = pickUp.Item.useMagic,
                                useExperience = pickUp.Item.useExperience,
                                equipHealthBonus = pickUp.Item.equipHealthBonus,
                                equipManaBonus = pickUp.Item.equipManaBonus,
                                equipDamageBonus = pickUp.Item.equipDamageBonus,
                                equipDefenseBonus = pickUp.Item.equipDefenseBonus,
                                equipSpeedcBonus = pickUp.Item.equipSpeedcBonus,
                                modelPrefab = pickUp.Item.modelPrefab
                            };
                            //如果手上的物品数量只剩一个
                            if (pickUp.Item.currentNumber - 1 == 0)
                            {
                                pickUp.Item = null;
                            }
                            else
                            {
                                pickUp.Item.currentNumber -= 1;
                                pickUp.UpdateAmount();
                            }

                            return;
                        }

                        Item = pickUp.Item;
                        pickUp.Item = null;
                    }
                }
                else
                {
                    //如果手上的物品跟燃烧的物品一样
                    if (pickUp.Item.id == Item.id)
                    {
                        //按住CTRL键,一下添加一个
                        if (Input.GetKey(KeyCode.LeftControl))
                        {
                            //当前燃料格子未满
                            if (Item.currentNumber < Item.maxStack)
                            {
                                item.currentNumber++;
                                UpdateAmount(); //更新个数UI显示
                                //如果手上的物品数量只剩一个
                                if (pickUp.Item.currentNumber - 1 == 0)
                                {
                                    pickUp.Item = null;
                                }
                                else
                                {
                                    pickUp.Item.currentNumber -= 1;
                                    pickUp.UpdateAmount();
                                }
                            }

                            return;
                        }

                        //如果燃烧的材料达到上限的个数大于手上物品的个数
                        if (Item.maxStack - Item.currentNumber > pickUp.Item.currentNumber)
                        {
                            Item.currentNumber += pickUp.Item.currentNumber;
                            UpdateAmount(); //更新个数UI显示
                            pickUp.Item = null;
                        }
                        else
                        {
                            pickUp.Item.currentNumber -= (Item.maxStack - Item.currentNumber);
                            Item.currentNumber = Item.maxStack;
                            UpdateAmount(); //更新个数UI显示
                            pickUp.UpdateAmount(); //更新UI显示
                        }
                    }
                    else
                    {
                        //如果都有物品,交换
                        Model_Item tempItem;
                        tempItem = Item;
                        Item = pickUp.Item;
                        pickUp.Item = tempItem;
                    }
                }
            }
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            //手上无物品
            if (pickUp.Item == null)
            {
                pickUp.Item = Item;
                Item = null;
            }
        }
    }
    /// <summary>
    /// 开启关闭食材协程
    /// </summary>
    /// <param name="value"></param>
    public void Ingredients(bool value)
    {
        if (value)
        {
            StartCoroutine(Ingredients());
        }
        else
        {
            StopAllCoroutines();
        }
    }
    /// <summary>
    /// 食材协程
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerator Ingredients()
    {
        if (Item != null)
        {
            StartTheTimer = true;
            yield return new WaitForSeconds(GlobalParametr.STEAMINGTIME);//全局--烹饪时间,生肉多久才会熟
            if (Item.currentNumber >= 1)
            {
                Item.currentNumber -= 1;
                //判断成品格子中是否还有空间
                foreach (Ctrl_CookingProductsSlot productsSlot in Ctrl_CampfireManager.Instance.CookingProductsList)
                {
                    if (productsSlot.Item == null)
                    {
                        productsSlot.AddItem(Item.id);
                        StartCoroutine(Ingredients());
                        break;
                    }
                    else
                    {
                        if (productsSlot.Item.currentNumber < productsSlot.Item.maxStack)
                        {
                            productsSlot.AddItem(Item.id);
                            StartCoroutine(Ingredients());
                            break;
                        }
                    }
                }

                if (Item.currentNumber == 0)
                {
                    Item = null;//如果食材为0,会自动停止协程
                }
                else
                {
                    UpdateAmount();//更新UI显示个数
                }
            }
            else
            {
                Item = null;
            }
        }
    }
}