using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class View_PickUpItem : MonoBehaviour
{
    [SerializeField] private Image _ImgIcon;
    [SerializeField] private Text _Amount;
    private Model_Item item;
    [SerializeField] private CanvasGroup canvas;

    private void Awake()
    {
        GetComponent<Ctrl_PickUp>().DelPickUp += InitUI;
    }

    public void InitUI()
    {
        item = GetComponent<Ctrl_PickUp>().Item;
        if (item == null)
        {
            canvas.alpha = 0;
            Ctrl_TootipManager.Instance.IsPickedItem = false;
        }
        else
        {
            canvas.alpha = 1;
            _ImgIcon.sprite = Resources.Load<Sprite>(item.sprite);
            _Amount.text = item.currentNumber.ToString();
            Ctrl_TootipManager.Instance.IsPickedItem = true;
        }
    }

    public void UpdateAmount()
    {
        _Amount.text = item.currentNumber.ToString();
    }

    public void ChangeImg(Sprite sprite)
    {
        _ImgIcon.sprite = sprite;
    }

    public void ChangeAmount(int amount)
    {
        _Amount.text = amount.ToString();
    }

    public void ChangeItem(Model_Item item)
    {
        ChangeImg(Resources.Load<Sprite>(item.sprite));
        ChangeAmount(item.currentNumber);
        this.item = item;
    }

    public void OnSetPickRaycast(bool isRaycast)
    {
        _ImgIcon.raycastTarget = isRaycast;
    }
}