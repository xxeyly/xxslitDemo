using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class View_Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private Image _ImgHoverOverlay;
    private Image _ImgIcon;
    private Ctrl_Slot slot;
    private Text _Amount;
    private Ctrl_PickUp pickupItemSlot;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _ImgHoverOverlay.gameObject.SetActive(true);
        if (slot.Item != null && View_PlayerinfoPespons.Instance.GetPlayerPackagePanelAlpha() > 0f)
        {
            Ctrl_TootipManager.Instance.isToolTipShow = true;
            Ctrl_TootipManager.Instance.ShowItemInfo(slot.Item);
        }
    }

    private void Awake()
    {
        slot = gameObject.GetComponent<Ctrl_Slot>();
        slot.slotItemInit += SlotInit;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _ImgHoverOverlay.gameObject.SetActive(false);
        Ctrl_TootipManager.Instance.isToolTipShow = false;
    }

    public void SlotInit()
    {
        if (slot.Item != null)
        {
            _ImgIcon.gameObject.SetActive(true);
            Sprite sprite = Resources.Load<Sprite>(slot.Item.sprite);
            _ImgIcon.sprite = sprite;
            _Amount.text = slot.Item.currentNumber.ToString();
        }
        else
        {
            IconSetNull();
        }
    }

    void Start()
    {
        _ImgHoverOverlay = transform.Find("Hover Overlay").GetComponent<Image>();
        _ImgIcon = transform.Find("Icon").GetComponent<Image>();
        _Amount = transform.Find("Amount").GetComponent<Text>();
        pickupItemSlot = Ctrl_TootipManager.Instance.PickUpItem.GetComponent<Ctrl_PickUp>();
    }


    private void UseItem()
    {
        slot.UseItem();
    }

    public void IconSetNull()
    {
        _ImgIcon.sprite = null;
        _ImgIcon.gameObject.SetActive(false);
        _Amount.text = "";
    }

    public void UpdateAmount()
    {
        _Amount.text = slot.Item.currentNumber.ToString();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (View_PlayerinfoPespons.Instance.GetPlayerPackagePanelAlpha() > 0)
        {
            //鼠标右键点击
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                //如果现在手上没有物品,并且当前slot有物品
                if (Ctrl_TootipManager.Instance.IsPickedItem == false && slot.Item != null)
                {
                    Ctrl_TootipManager.Instance.IsPickedItem = true;
                    pickupItemSlot.Item = slot.Item;
                    slot.Item = null;
                }
            }

            //鼠标左键点击
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                //当前手上存在物品
                if (Ctrl_TootipManager.Instance.IsPickedItem)
                {
                    //当前格子内没有物品
                    if (slot.Item == null)
                    {
                        //按住Ctrl放下一个物品
                        if (Input.GetKey(KeyCode.LeftControl))
                        {
                            slot.Item = Ctrl_InventoryManager.Instance.NewItem(pickupItemSlot.Item.id);
                            pickupItemSlot.Item.currentNumber -= 1;
                            if (pickupItemSlot.Item.currentNumber == 0)
                            {
                                pickupItemSlot.Item = null;
                                Ctrl_TootipManager.Instance.IsPickedItem = false;
                            }
                        }
                        else
                        {
                            slot.Item = pickupItemSlot.Item;
                            pickupItemSlot.Item = null;
                            Ctrl_TootipManager.Instance.IsPickedItem = false;
                        }
                    }
                    //格子内存在物品
                    else
                    {
                        //如果是相同的物品,叠加 不是相同物品替换
                        if (pickupItemSlot.Item.id == slot.Item.id)
                        {
                            if (Input.GetKey(KeyCode.LeftControl))
                            {
                                if (slot.Item.currentNumber != slot.Item.maxStack)
                                {
                                    slot.Item.currentNumber += 1;
                                    slot.UpdateAmount();
                                    pickupItemSlot.Item.currentNumber -= 1;
                                    if (pickupItemSlot.Item.currentNumber == 0)
                                    {
                                        pickupItemSlot.Item = null;
                                        Ctrl_TootipManager.Instance.IsPickedItem = false;
                                    }
                                }
                            }
                            else
                            {
                                //相同物品下 当前手上的物品个数小于格子物品剩余到达上限的个数,手上物品的个数添加到格子中
                                //大于的情况下 添加到格子上限 手上保存剩下的物品
                                if (pickupItemSlot.Item.currentNumber <= (slot.Item.maxStack - slot.Item.currentNumber))
                                {
                                    slot.Item.currentNumber += pickupItemSlot.Item.currentNumber;
                                    slot.UpdateAmount();
                                    pickupItemSlot.Item = null;
                                    Ctrl_TootipManager.Instance.IsPickedItem = false;
                                }
                                else
                                {
                                    //更新手上物品数量
                                    pickupItemSlot.Item.currentNumber -= (slot.Item.maxStack - slot.Item.currentNumber);
                                    //更新格子物品数量及UI
                                    slot.Item.currentNumber = slot.Item.maxStack;
                                    slot.UpdateAmount();
                                }
                            }
                        }
                        else
                        {
                            //交换Item
                            Model_Item tempItem;
                            tempItem = slot.Item;
                            slot.Item = pickupItemSlot.Item;
                            pickupItemSlot.gameObject.GetComponent<Ctrl_PickUp>().Item = tempItem;
                        }
                    }
                }
                else
                {
                    UseItem();
                }
            }
        }
    }
}