using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_ShopManager : Singleton<Ctrl_ShopManager>
{
    [SerializeField] public Transform shopItemSlots;
    
    [SerializeField] private int[] shelfGoods; //上架的商品
    [SerializeField] private GameObject shopItemPre;
    

    private void Start()
    {
        for (int i = 0; i < shelfGoods.Length; i++)
        {
            GameObject shopItem = ObjectPoolTool.Instance.Pop(shopItemPre);
            shopItem.transform.parent = shopItemSlots;
            shopItem.transform.localScale = Vector3.one;
            shopItem.gameObject.GetComponent<View_ShopGroupItem>().Item =
                Ctrl_InventoryManager.Instance.GetItemById(shelfGoods[i]);
        }
    }
}