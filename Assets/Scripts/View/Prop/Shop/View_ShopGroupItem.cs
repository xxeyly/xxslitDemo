using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class View_ShopGroupItem : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    private Model_Item item;
    [SerializeField] private Text itemName;
    [SerializeField] private Text goldText;
    [SerializeField] private Text diamodText;
    [SerializeField] private Image icon;
    [SerializeField] private GameObject hoverOverlay;
    public GlobalParametr.del_ShopSlot updateShopSlot;

    public Model_Item Item
    {
        get { return item; }

        set
        {
            item = value;
            Init(item);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverOverlay.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoverOverlay.gameObject.SetActive(false);
    }

    public void Init(Model_Item item)
    {
        this.item = item;
        itemName.text = item.itemName;
        goldText.text = item.buyPriceByGold.ToString();
        diamodText.text = item.buyPriceByDiamond.ToString();
        icon.gameObject.SetActive(true);
        icon.sprite = Resources.Load<Sprite>(item.sprite);
        GetComponentInChildren<Ctrl_ShopSlot>().Item = item;
    }
}