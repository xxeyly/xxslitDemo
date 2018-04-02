using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ctrl_BuyTootip : MonoBehaviour
{
    public void BuyItem(int gold, int diamod, int itemID, int itemBuyAmount)
    {
        if (Ctrl_HeroProperty.Instance.GetGold() >= gold && Ctrl_HeroProperty.Instance.GetDiamods() >= diamod)
        {
            Ctrl_HeroProperty.Instance.RemoveGold(gold);
            Ctrl_HeroProperty.Instance.RemoveDiamods(diamod);
            for (int i = 0; i < itemBuyAmount; i++)
            {
                Ctrl_InventoryManager.Instance.AddItem(itemID);
            }
            Ctrl_TootipManager.Instance.HideShopTootip();
        }
        else
        {
            Ctrl_TootipManager.Instance.ShowNotification("大侠,你钱不够啊!");
        }
    }

    /// <summary>
    /// 增加购买的个数
    /// </summary>
    /// <param name="item">购买的物品</param>
    /// <param name="goldText">所需金币的Text</param>
    /// <param name="diamodText">所需钻石的Text</param>
    /// <param name="buyAmount">购买的个数</param>
    public void AddBuyAmount(Model_Item item, Text goldText, Text diamodText, InputField buyAmount)
    {
        //从新计算价格
        //限定一次购买物品的数量
        if (Int32.Parse(buyAmount.text) <= 99)
        {
            //如果购买的数量小于已存在相同物品剩余格子的所和
            if (Int32.Parse(buyAmount.text) < Ctrl_SlotManager.Instance.GetIdenticaAmount(item))
            {
                //当前购买数量
                if (buyAmount.text != "")
                {
                    int currentBuyAmount = Int32.Parse(buyAmount.text);
                    buyAmount.text = (currentBuyAmount + 1).ToString();
                    //当前购买所需金币
                    goldText.text = (Int32.Parse(buyAmount.text) * item.buyPriceByGold).ToString();
                    //当前购买所需钻石
                    diamodText.text = (Int32.Parse(buyAmount.text) * item.buyPriceByDiamond).ToString();
                }
            }
            else
            {
                if (Ctrl_InventoryManager.Instance.emptySlot != null)
                {
                    int inventorySurplusAmount = Ctrl_InventoryManager.Instance.slotList.Length;
                    //剩余空的格子数量
                    if (Ctrl_InventoryManager.Instance.ActiveSlot() != null)
                    {
                        inventorySurplusAmount = Ctrl_InventoryManager.Instance.slotList.Length -
                                                 Ctrl_InventoryManager.Instance.ActiveSlot().Count;
                    }

                    if (Int32.Parse(buyAmount.text) < inventorySurplusAmount * item.maxStack)
                    {
                        //当前购买数量
                        int currentBuyAmount = Int32.Parse(buyAmount.text);
                        buyAmount.text = (currentBuyAmount + 1).ToString();
                        //当前购买所需金币
                        goldText.text = (Int32.Parse(buyAmount.text) * item.buyPriceByGold).ToString();
                        //当前购买所需钻石
                        diamodText.text = (Int32.Parse(buyAmount.text) * item.buyPriceByDiamond).ToString();
                    }
                }
            }
        }
    }

    public int GetBuyItemAmountSun(Model_Item item)
    {
        int purchaseQuantity = 0;
        //已存在相同物品剩余格子的所和
        purchaseQuantity += Ctrl_SlotManager.Instance.GetIdenticaAmount(item);
        //背包物品是否满了
        if (Ctrl_InventoryManager.Instance.emptySlot == null)
        {
            return purchaseQuantity;
        }
        else
        {
            if (Ctrl_InventoryManager.Instance.ActiveSlot() != null)
            {
                //剩余空的格子数量
                int inventorySurplusAmount = Ctrl_InventoryManager.Instance.slotList.Length -
                                             Ctrl_InventoryManager.Instance.ActiveSlot().Count;
                inventorySurplusAmount = inventorySurplusAmount * (item.maxStack);
                return inventorySurplusAmount;
            }
            else
            {
                return item.maxStack * Ctrl_InventoryManager.Instance.slotList.Length;
            }
        }
    }

    /// <summary>
    /// 减少购买的个数
    /// </summary>
    /// <param name="item">购买的物品</param>
    /// <param name="goldText">所需金币的Text</param>
    /// <param name="diamodText">所需钻石的Text</param>
    /// <param name="buyAmount">购买的个数</param>
    public void RemoveAmount(Model_Item item, Text goldText, Text diamodText, InputField buyAmount)
    {
        //购买的个数小于1,不执行操作
        if (Int32.Parse(buyAmount.text) > 1 && buyAmount.text != "")
        {
            buyAmount.text = (Int32.Parse(buyAmount.text) - 1).ToString();
            goldText.text = (Int32.Parse(buyAmount.text) * item.buyPriceByGold).ToString();
            diamodText.text = (Int32.Parse(buyAmount.text) * item.buyPriceByDiamond).ToString();
        }
    }
}