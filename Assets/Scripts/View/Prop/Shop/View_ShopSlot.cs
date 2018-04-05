using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class View_ShopSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        Ctrl_InventoryManager.Instance.isToolTipShow = true;
        Ctrl_InventoryManager.Instance.Tootip.GetComponent<View_ToolTip>()
            .Show(GetComponent<Ctrl_ShopSlot>().Item.ItemInfo());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Ctrl_InventoryManager.Instance.isToolTipShow = false;
    }
}