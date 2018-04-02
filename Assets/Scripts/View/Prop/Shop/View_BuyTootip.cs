using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class View_BuyTootip : MonoBehaviour
{
    private Ctrl_BuyTootip tootip;
    private Model_Item buyItem;
    [SerializeField] private Image icon; //商品图片
    [SerializeField] private Text gold; //购买金币数量
    [SerializeField] private Text diamod; //购买钻石数量
    [SerializeField] private InputField buyAmount; //购买个数
    [SerializeField] private Button buy; //购买
    [SerializeField] private Button close; //关闭
    [SerializeField] private Button addAmount; //增加购买个数
    [SerializeField] private Button removeAmount; //减少购买个数

    public Model_Item BuyItem
    {
        get { return buyItem; }

        set
        {
            buyItem = value;
            icon.gameObject.SetActive(true);
            buyAmount.text = 1.ToString();
            icon.sprite = Resources.Load<Sprite>(BuyItem.sprite);
            gold.text = buyItem.buyPriceByGold.ToString();
            diamod.text = buyItem.buyPriceByDiamond.ToString();
        }
    }

    void Start()
    {
        tootip = GetComponent<Ctrl_BuyTootip>();
    }

    public void AddBuyAmount()
    {
        tootip.AddBuyAmount(BuyItem, gold, diamod, buyAmount);
    }

    public void RemoveAmount()
    {
        tootip.RemoveAmount(BuyItem, gold, diamod, buyAmount);
    }

    public void Buy()
    {
//        Debug.Log(tootip.GetBuyItemAmountSun(buyItem));
        if (Int32.Parse(buyAmount.text) <= tootip.GetBuyItemAmountSun(buyItem))
        {
            tootip.BuyItem(Int32.Parse(gold.text), Int32.Parse(diamod.text), buyItem.id, Int32.Parse(buyAmount.text));
        }
        else
        {
            if (tootip.GetBuyItemAmountSun(buyItem) > 0)
            {
                buyAmount.text = tootip.GetBuyItemAmountSun(buyItem).ToString();
                gold.text = (BuyItem.buyPriceByGold * (Int32.Parse(buyAmount.text))).ToString();
                diamod.text = (BuyItem.buyPriceByDiamond * (Int32.Parse(buyAmount.text))).ToString();
            }
            else
            {
                buyAmount.text = 1.ToString();
                gold.text = BuyItem.buyPriceByGold.ToString();
                diamod.text = buyItem.buyPriceByDiamond.ToString();
            }

            Ctrl_TootipManager.Instance.ShowNotification("背包已经存不下更多的东西,已经自动给你设置到可以购买的最大个数.",
                GlobalParametr.SHOPSHOWTIME);
        }
    }
}