using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class View_EquipmentSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private Image _ImgHoverOverlay;
    private Image _ImgIcon;
    private Ctrl_EquipmentSlot equipmentSlot;

    public void EquipmentWear()
    {
        if (equipmentSlot.Item == null)
        {
            _ImgIcon.gameObject.SetActive(false);
            Ctrl_TootipManager.Instance.isToolTipShow = false;
        }
        else
        {
            _ImgIcon.sprite = Resources.Load<Sprite>(equipmentSlot.Item.sprite);
            _ImgIcon.gameObject.SetActive(true);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //右键点击脱下装备,如果当前背包内还有剩余空间
        if (equipmentSlot.Item != null && eventData.button == PointerEventData.InputButton.Right)
        {
            if (Ctrl_InventoryManager.Instance.emptySlot != null)
            {
                //将装备放回背包
                Ctrl_InventoryManager.Instance.AddItem(equipmentSlot.Item.id);
                //拿下身上穿戴的装备
                equipmentSlot.Item = null;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _ImgHoverOverlay.gameObject.SetActive(true);
        if (equipmentSlot.Item != null && View_PlayerinfoPespons.Instance.GetPlayerCharacterPanelAlpha() > 0f)
        {
            Ctrl_TootipManager.Instance.isToolTipShow = true;
            Ctrl_TootipManager.Instance.ShowItemInfo(equipmentSlot.Item);
        }
        else
        {
            Ctrl_TootipManager.Instance.isToolTipShow = false;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _ImgHoverOverlay.gameObject.SetActive(false);
        Ctrl_TootipManager.Instance.isToolTipShow = false;
    }

    void Start()
    {
        equipmentSlot = GetComponent<Ctrl_EquipmentSlot>();
        _ImgHoverOverlay = transform.Find("Hover Overlay").GetComponent<Image>();
        _ImgIcon = transform.Find("Icon").GetComponent<Image>();
        equipmentSlot.delEquipment += EquipmentWear;
    }

    // Update is called once per frame
    void Update()
    {
    }
}