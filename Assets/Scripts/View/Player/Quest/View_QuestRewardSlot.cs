using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class View_QuestRewardSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image icon;
    [SerializeField] private Text itemCount;
    [SerializeField] private GameObject hoverOverlay;

    public void InitSlot()
    {
        icon.sprite = Resources.Load<Sprite>(GetComponent<Ctrl_QuestRewardSlot>().Item.sprite);
        itemCount.text = GetComponent<Ctrl_QuestRewardSlot>().Item.currentNumber.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverOverlay.SetActive(true);
        if (GetComponent<Ctrl_QuestRewardSlot>().Item == null)
        {
            return;
        }

        Ctrl_TootipManager.Instance.isToolTipShow = true;
        Ctrl_TootipManager.Instance.ShowItemInfo(GetComponent<Ctrl_QuestRewardSlot>().Item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoverOverlay.SetActive(false);
        Ctrl_TootipManager.Instance.isToolTipShow = false;
    }

    public void ShowGoldView(int amount)
    {
        icon.sprite = Resources.Load<Sprite>("Sprites/Items/Currency_Gold");
        itemCount.text = amount.ToString();
    }

    public void ShowDiamodView(int amount)
    {
        icon.sprite = Resources.Load<Sprite>("Sprites/Items/gem");
        itemCount.text = amount.ToString();
    }

    public void ShowExpView(int amount)
    {
        icon.sprite = Resources.Load<Sprite>("Sprites/Items/exp");
        itemCount.text = amount.ToString();
    }
}