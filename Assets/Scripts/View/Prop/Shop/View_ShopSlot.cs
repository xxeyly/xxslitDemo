using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class View_ShopSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        Ctrl_TootipManager.Instance.isToolTipShow = true;
        Ctrl_TootipManager.Instance.ShowItemInfo(GetComponent<Ctrl_ShopSlot>().Item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Ctrl_TootipManager.Instance.isToolTipShow = false;
    }
}